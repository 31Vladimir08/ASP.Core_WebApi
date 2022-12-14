using MessageBus.Interfaces;
using MessageBus.Services;

using ProductCategoryApi.Interfaces.Repositories;
using ProductCategoryApi.Interfaces.Services;
using ProductCategoryApi.Repositories;
using ProductCategoryApi.Services;

namespace ProductCategoryApi.DependencyInjection
{
    public static class RegisterServices
    {
        public static void SetRepositoriesDJ(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }

        public static void SetServicesDJ(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
        }
        public static void SetAzureServiceBusDJ(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IMessageBus>(
                x => new AzureServiceMessageBus(connectionString));
        }

    }
}
