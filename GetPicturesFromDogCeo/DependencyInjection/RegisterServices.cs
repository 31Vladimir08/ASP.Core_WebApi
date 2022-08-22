using DogCeoService.Interfaces;
using DogCeoService.Services;

using Microsoft.Extensions.DependencyInjection;

namespace GetPicturesFromDogCeo.DependencyInjection
{
    public static class RegisterServices
    {
        /// <summary>
        /// Регистрация сервисов в IoC
        /// </summary>
        /// <param name="services"></param>
        public static void SetServicesDJ(this IServiceCollection services)
        {
            services.AddScoped<IDogService, DogService>();
        }
    }
}
