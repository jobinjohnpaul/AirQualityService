using AirQuality.Services.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AirQuality.Services
{
    public class EPAAirQualityService : IAirQualityService
    {
        private readonly HttpClient _httpClient;

        public EPAAirQualityService(HttpClient httpClient) => _httpClient = httpClient;

        public Task<Root> GetAirQualityInformation(CancellationToken cancellationToken) =>  _httpClient.GetFromJsonAsync<Root>(string.Empty, cancellationToken: cancellationToken);
    }
}
