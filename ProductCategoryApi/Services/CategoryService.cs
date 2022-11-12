using AutoMapper;

using ProductCategoryApi.EntityDto;
using ProductCategoryApi.Interfaces.Repositories;
using ProductCategoryApi.Interfaces.Services;
using ProductCategoryApi.Models;

namespace ProductCategoryApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CategoryDto category)
        {
            var categoryEntity = _mapper.Map<Category>(category);
            await _categoryRepository.CreateCategoryAsync(categoryEntity);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            var result = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return result;
        }

        public async Task<CategoryDto> GetCategoryAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveCategoryAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCategoryAsync(CategoryDto category)
        {
            throw new NotImplementedException();
        }
    }
}
