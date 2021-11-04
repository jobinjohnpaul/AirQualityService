using AirQuality.Core.Services;
using AirQuality.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AirQualityService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string baseUrl = Configuration.GetSection("EPAAirQualityUrl").Value;
            services.ConfigureApplicationServices(baseUrl);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endPoints =>
            {
                var airQualityService  = endPoints.ServiceProvider.GetRequiredService<IAirQualityRetrieverService>();

                endPoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsJsonAsync(airQualityService.GetAirQualityForClosestLocation(-37.6667124, 145.0642075));
                });
            });
        }
    }
}
