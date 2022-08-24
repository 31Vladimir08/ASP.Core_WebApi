using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using DogCeoService.EntitiesDto;

using GetPicturesFromDogCeo.Interfaces.WebServices;

using Microsoft.Extensions.Caching.Distributed;

using StackExchange.Redis;

namespace GetPicturesFromDogCeo.WebServices
{
    public class DogWebService : IDogWebService
    {
        private readonly IDistributedCache _cache;

        public DogWebService(IDistributedCache cache)
        {
            _cache = cache;
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

        public string GetPictureNameFromUrl(string url)
        {
            var fileName = url.Substring(url.IndexOf("/n") + 2);
            return fileName;
        }
    }
}
