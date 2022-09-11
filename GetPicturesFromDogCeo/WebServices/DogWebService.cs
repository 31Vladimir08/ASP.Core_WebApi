using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using DogCeoService.EntitiesDto;
using DogCeoService.Interfaces;

using GetPicturesFromDogCeo.Interfaces.WebServices;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

using Polly;

using StackExchange.Redis;

namespace GetPicturesFromDogCeo.WebServices
{
    public class DogWebService : IDogWebService
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<DogWebService> _loger;
        private readonly IDogService _dogService;

        public DogWebService(IDistributedCache cache, ILogger<DogWebService> loger, IDogService dogService)
        {
            _cache = cache;
            _loger = loger;
            _dogService = dogService;
        }

        public async Task AddDogsToCachAsync(IEnumerable<DogDto> dogs)
        {
            if (dogs == null || !dogs.Any())
                return;

            foreach (var item in dogs)
            {
                var dogPicturesCount = item.DogPictures.Count;
                var redisBreadHash = new HashEntry[dogPicturesCount];
                var i = 0;
                foreach (var picture in item.DogPictures)
                {
                    var pictureName = GetPictureNameFromUrl(picture.Url);
                    redisBreadHash[i] = new HashEntry(pictureName, picture.Url);
                    i++;
                }

                var d = JsonSerializer.SerializeToUtf8Bytes(redisBreadHash);
                await _cache.SetAsync(item.Breed, d);
            }
        }

        public async Task<IEnumerable<DogDto>> GetDogsAsync(int countPicturesEveryBread, CancellationToken token, List<string> breedsFilter = null)
        {
            _dogService.LogNotify += (x) => _loger.LogInformation(x);
            var dogs = await _dogService.GetDogsAsync(countPicturesEveryBread, token, breedsFilter);
            await AddDogsToCachAsync(dogs);
            return dogs;
        }

        public string GetPictureNameFromUrl(string url)
        {
            var fileName = url.Substring(url.IndexOf("/n") + 2);
            return fileName;
        }
    }
}
