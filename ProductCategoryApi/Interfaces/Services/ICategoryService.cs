using ProductCategoryApi.EntityDto;

namespace ProductCategoryApi.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto> GetCategoryAsync(string id);
        Task UpdateCategoryAsync(CategoryDto category);
        Task RemoveCategoryAsync(string id);
        Task CreateCategoryAsync(CategoryDto category);
    }
}
