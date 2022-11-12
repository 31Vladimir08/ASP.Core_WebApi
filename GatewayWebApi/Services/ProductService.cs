using GatewayWebApi.Interfaces.Services;
using GatewayWebApi.ModelsDto;
using GatewayWebApi.ModelsDto.Filters;

namespace GatewayWebApi.Services
{
    public class ProductService : IProductService
    {
        public async Task CreateProductAsync(ProductDto product)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDto> GetProductAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(ProductFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveProductAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProductAsync(ProductDto product)
        {
            throw new NotImplementedException();
        }
    }
}
