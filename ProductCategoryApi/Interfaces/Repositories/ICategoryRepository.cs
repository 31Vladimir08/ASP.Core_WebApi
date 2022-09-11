using ProductCategoryApi.Models;

namespace ProductCategoryApi.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(string id);
        Task UpdateCategoryAsync(Category category);
        Task RemoveCategoryAsync(string id);
        Task CreateCategoryAsync(Category category);
    }
}
