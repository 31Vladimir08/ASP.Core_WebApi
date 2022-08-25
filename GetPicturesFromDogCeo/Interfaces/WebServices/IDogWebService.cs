using DogCeoService.EntitiesDto;

using System;
using System.Collections.Generic;
using System.Threading;
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

        /// <summary>
        /// Получаем породы собак с изображениями
        /// </summary>
        /// <param name="countPicturesEveryBread">кол-во картинок для каждой из пород</param>
        /// <param name="breedsFilter">список пород, по которым нужно получить изображения, если null, получаем по всем породам</param>
        /// <returns>список пород с картинками и ссылками</returns>
        Task<IEnumerable<DogDto>> GetDogsAsync(int countPicturesEveryBread, CancellationToken token, List<string> breedsFilter = null);

        /// <summary>
        /// Событие, информирующее о том, когда сервис начал работу и когда закончил.
        /// </summary>
        public event Action<bool> IsServiceWorksNotify;
    }
}
