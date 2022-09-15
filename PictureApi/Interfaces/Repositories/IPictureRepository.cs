using MongoDB.Bson;

using PictureApi.Models;

namespace PictureApi.Interfaces.Repositories
{
    public interface IPictureRepository
    {
        Task<IEnumerable<Picture>> GetPicturesAsync();
        Task<Picture?> GetPictureAsync(ObjectId fileId);
        Task UpdatePictureAsync(Picture product);
        Task RemovePictureAsync(ObjectId fileId);
        Task CreatePictureAsync(Picture product);
        Task<IEnumerable<Picture>?> GetCategoryPicturesAsync(string categoryId);
        Task RemoveCategoryPictureAsync(string categoryId);
    }
}
