using ProductCategoryApi.Interfaces.Repositories;
using ProductCategoryApi.Repositories;

namespace ProductCategoryApi.DependencyInjection
{
    public static class RegisterServices
    {
        public static void SetRepositoriesDJ(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }
    }
}
