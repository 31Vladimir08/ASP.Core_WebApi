using PictureApi.Interfaces.Repositories;
using PictureApi.Repositories;

namespace PictureApi.DependencyInjection
{
    public static class RegisterServices
    {
        public static void SetRepositoriesDJ(this IServiceCollection services)
        {
            services.AddScoped<IPictureRepository, PictureRepository>();
        }
    }
}
