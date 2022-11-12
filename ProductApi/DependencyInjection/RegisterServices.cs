using MessageBus.Interfaces;
using MessageBus.Services;

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

        public static void SetAzureServiceBusDJ(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IMessageBus>(
                x => new AzureServiceMessageBus(connectionString));
        }
    }
}
