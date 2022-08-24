using DogCeoService.EntitiesDto;

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
        /// <param name="breedsFilter">список пород, по которым нужно получить изображения, если null, получаем по всем породам</param>
        /// <returns>список пород с картинками и ссылками</returns>
        Task<IEnumerable<DogDto>> GetDogsAsync(int countPicturesEveryBread, List<string>? breedsFilter = null);

        /// <summary>
        /// Получаем список всех пород
        /// </summary>
        /// <param name="dogsJson">json всех пород собак</param>
        /// <param name="breedsFilter">фильтр поиска</param>
        /// <returns>список всех пород</returns>
        Task<IEnumerable<string>> GetBreadsFromJsonAsync(string? dogsJson, List<string>? breedsFilter = null);

        /// <summary>
        /// Получаем породу с сылками на картинки и сами картинки
        /// </summary>
        /// <param name="bread">строковое имя породы</param>
        /// <param name="countPicturesEveryBreed">кол-во изображений для породы</param>
        /// <returns>Получаем породу, с содержанием ссылок на картинки и сами картинки</returns>
        Task<DogDto> GetDogAsync(string bread, int countPicturesEveryBreed);

        /// <summary>
        /// получаем список пород согласно фильтра
        /// </summary>
        /// <param name="breeds">коллекция пород</param>
        /// <param name="breedsFilter">колекция для фильтра</param>
        /// <returns></returns>
        IEnumerable<string> GetSelectedDogNames(List<string> breeds, List<string> breedsFilter);
    }
}
