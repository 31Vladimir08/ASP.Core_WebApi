using System.Collections.Concurrent;
using System.Net;
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

        public async Task<IEnumerable<string>?> GetBreadsAsync(string? dogsJson, List<string>? breadsFilter = null)
        {
            var breads = string.IsNullOrWhiteSpace(dogsJson)
                ? null
                : await Task.Run(() =>
                {
                    JToken node = JToken.Parse(dogsJson)["message"];

                    List<string> breads = new List<string>();
                    List<string> name = new List<string>();

                    WalkNode(node, breads, name);
                    return breads;
                });

            return breads == null 
                ? null
                :GetSelectedDogNames(breads, breadsFilter);
        }

        public async Task<DogDto> GetDogAsync(string bread)
        {
            var dog = new DogDto();
            dog.Bread = bread;
            var httpClient = _httpClientFactory.CreateClient("DogCeoService");
            var response = await httpClient.GetAsync($"breed{bread}/images");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };

                var result1 = JsonSerializer.Deserialize<Rootobject>(content, options);

                var dataItems = new BlockingCollection<DogPictureDto>();
                int count = 0;
                var tasks = result1?.message
                    .AsParallel()
                    .Select(async item =>
                    {
                        dataItems.Add(new DogPictureDto()
                        {
                            Dog = dog,
                            Url = item,
                            Picture = await httpClient.GetByteArrayAsync(item)
                        });

                        count++;
                    });
                
                if (tasks != null)
                    await Task.WhenAll(tasks);
                dog.DogPictures = dataItems.ToList();
            }

            return dog;
        }

        public async Task<IEnumerable<DogDto>> GetDogsAsync(int countPicturesEveryBread, List<string>? breadsFilter = null)
        {
            var dogs = new List<DogDto>();
            var httpClient = _httpClientFactory.CreateClient("DogCeoService");
            var response = await httpClient.GetAsync(
            "breeds/list/all");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                var result = JsonSerializer.Deserialize<object>(content, options);
                var str = result?.ToString();

                var breads = await GetBreadsAsync(str, breadsFilter);

                
                if (breads == null)
                    return dogs;
                foreach (var item in breads)
                {
                    var dog = await GetDogAsync(item);
                    dogs.Add(dog);
                }

                return dogs;
            }

            return dogs;
        }

        public IEnumerable<string> GetSelectedDogNames(List<string> breads, List<string>? breadsFilter)
        {
            if (breadsFilter == null || !breads.Any())
                return breads;
            List<string> result = new List<string>();
            foreach (var item in breadsFilter)
            {
                var r = breads.Where(x => x.Trim().ToLower().Contains(item.Trim().ToLower()));
                result.AddRange(r);
            }

            return result.Distinct();
        }

        private void WalkNode(JToken node, List<string> list, List<string> name)
        {
            if (node.Type == JTokenType.Object)
            {
                var children = node.Children<JProperty>();
                foreach (JProperty child in children)
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
                foreach (JToken child in children)
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
