using ProductApi.Interfaces.Repositories;
using ProductApi.Repositories;

namespace ProductApi.DependencyInjection
{
    public static class RegisterServices
    {
        public static void SetRepositoriesDJ(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
