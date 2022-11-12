using GatewayWebApi.ModelsDto;
using GatewayWebApi.ModelsDto.Filters;

namespace GatewayWebApi.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductAsync(string id);
        Task UpdateProductAsync(ProductDto product);
        Task RemoveProductAsync(string id);
        Task CreateProductAsync(ProductDto product);
        Task<IEnumerable<ProductDto>> GetProductsAsync(ProductFilter filter);
    }
}
