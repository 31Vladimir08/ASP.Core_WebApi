using System.Collections.Concurrent;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

using DogCeoService.EntitiesDto;
using DogCeoService.Interfaces;
using DogCeoService.UserException;

using Newtonsoft.Json.Linq;

namespace DogCeoService.Services
{
    public class DogService : IDogService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DogService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public event Action<string>? LogNotify;

        public async Task<IEnumerable<string>> GetBreadsFromJsonAsync(string? dogsJson, List<string>? breedsFilter = null)
        {
            var breeds = string.IsNullOrWhiteSpace(dogsJson)
                ? throw new DogCeoException("Отсутствуют входные данные")
                : await Task.Run(() =>
                {
                    var node = JToken.Parse(dogsJson)["message"];

                    var dogBreeds = new List<string>();
                    var name = new List<string>();

                    WalkNode(node, dogBreeds, name);
                    return dogBreeds;
                });

            return breeds == null
                ? throw new DogCeoException("Не удалось получить породы собак")
                : GetSelectedDogNames(breeds, breedsFilter);
        }

        public async Task<DogDto> GetDogAsync(string breed, int countPicturesEveryBreed, CancellationToken token)
        {
            var dog = new DogDto();
            dog.Breed = breed;
            var httpClient = _httpClientFactory.CreateClient("DogCeoService");
            var response = await httpClient.GetAsync($"breed{breed}/images");

            if (!response.IsSuccessStatusCode)
            {
                throw new DogCeoException("Ошибка при обращении на сервер https://dog.ceo");
            }

            var content = await response.Content.ReadAsByteArrayAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var dogUrls = JsonSerializer.Deserialize<DogPictureUrls>(content, options)?.message?.Take(countPicturesEveryBreed);

            if (dogUrls == null)
                throw new DogCeoException("Не удалось получить ссылки на изображения");

            var dataItems = new BlockingCollection<DogPictureDto>();

            foreach (var item in dogUrls)
            {
                LogNotify?.Invoke($"Загружаем изображение {dog.Breed}; URL: {item}");
                dataItems.Add(new DogPictureDto()
                {
                    Dog = dog,
                    Url = item,
                    Picture = await httpClient.GetByteArrayAsync(item)
                });

                LogNotify?.Invoke($"Загружено изображение {dog.Breed}; URL: {item}");
                if (token.IsCancellationRequested)
                    break;
            }

            dog.DogPictures = dataItems.ToList();

            return dog;
        }

        public async Task<IEnumerable<DogDto>> GetDogsAsync(int countPicturesEveryBreed, CancellationToken token, List<string>? breedsFilter = null)
        {
            var dogs = new List<DogDto>();
            var httpClient = _httpClientFactory.CreateClient("DogCeoService");
            var response = await httpClient.GetAsync("breeds/list/all");
            if (!response.IsSuccessStatusCode)
            {
                throw new DogCeoException("Ошибка при обращении на сервер https://dog.ceo");
            }

            var content = await response.Content.ReadAsByteArrayAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var result = JsonSerializer.Deserialize<object>(content, options);
            var str = result?.ToString();

            var breeds = await GetBreadsFromJsonAsync(str, breedsFilter);


            if (breeds == null)
                return dogs;
            foreach (var item in breeds)
            {
                LogNotify?.Invoke($"Загружаем изображения для {item}");
                var dog = await GetDogAsync(item, countPicturesEveryBreed, token);
                dogs.Add(dog);

                LogNotify?.Invoke($"Загружены изображения для {item}");
                if (token.IsCancellationRequested)
                    break;
            }

            return dogs;
        }

        public IEnumerable<string> GetSelectedDogNames(List<string> breeds, List<string>? breedsFilter)
        {
            if (breedsFilter == null || !breedsFilter.Any())
                return breeds;
            var result = new List<string>();
            foreach (var item in breedsFilter)
            {
                var r = breeds.Where(x => x.Trim().ToLower().Contains(item.Trim().ToLower()));

                result.AddRange(r);
            }

            return result.Distinct();
        }

        private void WalkNode(JToken node, List<string> list, List<string> name)
        {
            if (node.Type == JTokenType.Object)
            {
                var children = node.Children<JProperty>();
                foreach (var child in children)
                {
                    name.Add(child.Name);
                    WalkNode(child.Value, list, name);

                    var strName = string.Empty;

                    foreach(var item in name)
                    {
                        strName += $"/{item}"; 
                    }
                    list.Add(strName);
                    name.RemoveAt(name.Count - 1);
                }
            }
            else if (node.Type == JTokenType.Array)                
            {
                var children = node.Children();
                foreach (var child in children)
                {
                    name.Add(child.ToString());
                    WalkNode(child, list, name);

                    var strName = string.Empty;

                    foreach (var item in name)
                    {
                        strName += $"/{item}";
                    }

                    list.Add(strName);

                    name.RemoveAt(name.Count - 1);
                }
            }
        }
    }
}
