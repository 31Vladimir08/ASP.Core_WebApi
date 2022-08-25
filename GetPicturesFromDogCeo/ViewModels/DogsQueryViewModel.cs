using System.Collections.Generic;

namespace GetPicturesFromDogCeo.ViewModels
{
    /// <summary>
    /// Отвечает за получение входных данных с UI.
    /// </summary>
    public class DogsQueryViewModel
    {
        /// <summary>
        /// Команда загрузки изображений
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Кол-во изображений для каждой из пород.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Список пород собак изображения которых буду загружены при выполнении.
        /// </summary>
        public List<string> Breads { get; set; }
    }
}
