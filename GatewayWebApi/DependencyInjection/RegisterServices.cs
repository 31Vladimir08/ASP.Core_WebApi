using GatewayWebApi.Interfaces.Services;
using GatewayWebApi.Services;

using MessageBus.Interfaces;
using MessageBus.Services;

namespace GatewayWebApi.DependencyInjection
{
    public static class RegisterServices
    {
        public static void SetServicesDJ(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
        }

        public static void SetAzureServiceBusDJ(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IMessageBus>(
                x => new AzureServiceMessageBus(connectionString));
        }
    }
}
