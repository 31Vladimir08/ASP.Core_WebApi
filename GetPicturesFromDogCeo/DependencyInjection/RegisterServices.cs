using DogCeoService.Interfaces;
using DogCeoService.Services;

using GetPicturesFromDogCeo.Interfaces.WebServices;
using GetPicturesFromDogCeo.WebServices;
using GetPicturesFromDogCeo.WebServices.HostServices;

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
            services.AddSingleton<DogWebHostService>();
            services.AddScoped<IDogService, DogService>();
            services.AddScoped<IDogWebService, DogWebService>();
        }
    }
}
