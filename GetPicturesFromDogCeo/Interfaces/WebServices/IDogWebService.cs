using DogCeoService.EntitiesDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetPicturesFromDogCeo.Interfaces.WebServices
{
    /// <summary>
    /// Сервис для работы с породами
    /// </summary>
    public interface IDogWebService
    {
        /// <summary>
        /// Добавляем коллекцию пород в Redis
        /// </summary>
        /// <param name="dogs">коллекция пород</param>
        Task AddDogsToCachAsync(IEnumerable<DogDto> dogs);

        /// <summary>
        /// Получаем имя картинки из url
        /// </summary>
        /// <param name="url">url картинки</param>
        /// <returns>имя картинки</returns>
        string GetPictureNameFromUrl(string url);
    }
}
