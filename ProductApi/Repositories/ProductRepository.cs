using Microsoft.Extensions.Options;

using MongoDB.Bson;
using MongoDB.Driver;

using ProductApi.Interfaces.Repositories;
using ProductApi.Models;
using ProductApi.Models.Settings;

namespace ProductApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;
        public ProductRepository(
            IOptions<ProductMongoDbSettings> categoryMongoDbSettings,
            IMongoClient mongoClient)
        {
            var db = mongoClient.GetDatabase(categoryMongoDbSettings.Value.DatabaseName);
            _products = db.GetCollection<Product>(categoryMongoDbSettings.Value.CollectionName);
        }

        public async Task CreateProductAsync(Product product)
        {
            await _products.InsertOneAsync(product);
        }

        public async Task<Product> GetProductAsync(string id)
        {
            var products = await _products.Find(x => x.ProductId == id).FirstOrDefaultAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await _products.Find(x => true).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(BsonDocument filter)
        {
            var products = await _products.Find(filter).ToListAsync();
            return products;
        }

        public async Task RemoveProductAsync(string id)
        {
            await _products.DeleteOneAsync(x => x.ProductId == id);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _products.ReplaceOneAsync(x => x.ProductId == product.ProductId, product);
        }
    }
}
