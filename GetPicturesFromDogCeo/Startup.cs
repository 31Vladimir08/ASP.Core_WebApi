using System;

using GetPicturesFromDogCeo.DependencyInjection;
using GetPicturesFromDogCeo.Filters;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Polly;

namespace GetPicturesFromDogCeo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.SetServicesDJ();

            services.AddControllers();

            services.AddMvc().AddMvcOptions(options =>
            {
                options.Filters.Add<NotImplExceptionFilterAttribute>();
                options.Filters.Add<LogingCallsActionFilter>();
            });

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration["RedisConnection"];
                options.InstanceName = "SampleInstance";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GetPicturesFromDogCeo", Version = "v1" });
            });

            services
                .AddHttpClient("DogCeoService", config =>
                {
                    config.BaseAddress = new Uri(Configuration["Services:DogCeo"]);
                })
                .AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(3)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GetPicturesFromDogCeo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
