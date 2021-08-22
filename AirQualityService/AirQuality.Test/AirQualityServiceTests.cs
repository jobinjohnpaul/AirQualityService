using AirQuality.Core.Services;
using AirQuality.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AirQuality.Test
{
    public class AirQualityServiceTests
    {
        private readonly ServiceCollection _serviceCollection;
        private readonly ServiceProvider _serviceProvider;
        public AirQualityServiceTests()
        {
            _serviceCollection= new ServiceCollection();
            _serviceCollection.ConfigureServices("https://www.epa.vic.gov.au/api/envmonitoring/sites?environmentalSegment=air&scientificParameterData=true");
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        [Fact]
        public async Task TestAirQualityGet()
        {
            var airQualityService = _serviceProvider.GetRequiredService<IAirQualityService>();
            var output = await airQualityService.GetAirQualityInformation();

            Assert.NotNull(output);
        }


        [Fact]
        public async Task TestAirQualityRetrieverServiceGet()
        {
            
            var airQualityService = _serviceProvider.GetRequiredService<IAirQualityRetrieverService>();
            var output = await airQualityService.GetAirQualityForClosestLocation(-37.6667124, 145.0642075);

            Assert.NotNull(output);
        }
    }
}
