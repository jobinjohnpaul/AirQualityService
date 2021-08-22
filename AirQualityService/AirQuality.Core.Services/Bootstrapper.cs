using AirQuality.Core.Services;
using AirQuality.Core.Services.MappingProfiles;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace AirQuality.Services
{
    public static class Bootstrapper
    {
        public static void ConfigureServices(this IServiceCollection serviceCollection, string baseUrl)
        {

            var config = new MapperConfiguration(RegisterMappingProfiles);

            serviceCollection.AddSingleton(typeof(IMapper), config.CreateMapper());

            serviceCollection.AddHttpClient<IAirQualityService, EPAAirQualityService>(ctx => ctx.BaseAddress = new Uri(baseUrl))
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());
            // Can configure additional policies here using Polly
            serviceCollection.AddTransient<IAirQualityRetrieverService, AirQualityRetrieverService>();
             
        }


        static void RegisterMappingProfiles(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile(new AirQualityMappingProfile());
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}
