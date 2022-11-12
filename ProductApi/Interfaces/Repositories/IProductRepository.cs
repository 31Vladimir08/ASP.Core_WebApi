using MongoDB.Bson;

using ProductApi.Models;

namespace ProductApi.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(string id);
        Task UpdateProductAsync(Product product);
        Task RemoveProductAsync(string id);
        Task CreateProductAsync(Product product);
        Task<IEnumerable<Product>> GetProductsAsync(BsonDocument filter);
    }
}
