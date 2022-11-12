using GatewayWebApi.Interfaces.Services;
using GatewayWebApi.ModelsDto;

using MessageBus.Interfaces;

namespace GatewayWebApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMessageBus _messageBus;

        public CategoryService(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task CreateCategoryAsync(CategoryDto category)
        {
            await _messageBus.PublishMessageAsync(category, "categorymessagetopic");
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            throw new NotImplementedException();
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
