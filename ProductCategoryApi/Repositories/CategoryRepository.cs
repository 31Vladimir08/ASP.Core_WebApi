using Microsoft.Extensions.Options;

using MongoDB.Driver;

using ProductCategoryApi.Interfaces.Repositories;
using ProductCategoryApi.Interfaces.Services;
using ProductCategoryApi.Models;
using ProductCategoryApi.Models.Settings;

namespace ProductCategoryApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryRepository(
            IOptions<CategoryMongoDbSettings> categoryMongoDbSettings,
            IMongoClient mongoClient)
        {
            var db = mongoClient.GetDatabase(categoryMongoDbSettings.Value.DatabaseName);
            _categories = db.GetCollection<Category>(categoryMongoDbSettings.Value.CollectionName);
        }

        public async Task CreateCategoryAsync(Category category)
        {
            await _categories.InsertOneAsync(category);
        }

        public async Task RemoveCategoryAsync(string id)
        {
            await _categories.DeleteOneAsync(x => x.CategoryId == id);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await _categories.Find(x => true).ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategoryAsync(string id)
        {
            var categories = await _categories.Find(x => x.CategoryId == id).FirstOrDefaultAsync();
            return categories;
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _categories.ReplaceOneAsync(x => x.CategoryId == category.CategoryId, category);
        }
    }
}
