using DogCeoService.EntitiesDto;

using Newtonsoft.Json.Linq;

namespace DogCeoService.Interfaces
{
    /// <summary>
    /// Сервис для работы с сайтом https://dog.ceo
    /// </summary>
    public interface IDogService
    {
        /// <summary>
        /// Получаем породы собак с изображениями
        /// </summary>
        /// <param name="countPicturesEveryBread">кол-во картинок для каждой из пород</param>
        /// <param name="breadsFilter">список пород, по которым нужно получить изображения, если null, получаем по всем породам</param>
        /// <returns>список пород с картинками и ссылками</returns>
        Task<IEnumerable<DogDto>> GetDogsAsync(int countPicturesEveryBread, List<string>? breadsFilter = null);

        /// <summary>
        /// Получаем список всех пород
        /// </summary>
        /// <param name="dogsJson">json всех пород собак</param>
        /// <param name="breadsFilter">фильтр поиска</param>
        /// <returns>список всех пород</returns>
        Task<IEnumerable<string>?> GetBreadsAsync(string? dogsJson, List<string>? breadsFilter = null);

        Task<DogDto> GetDogAsync(string bread);

        IEnumerable<string> GetSelectedDogNames(List<string> breads, List<string> breadsFilter);
    }
}
