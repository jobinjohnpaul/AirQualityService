using AirQuality.Services.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirQuality.Services
{
    public class EPAAirQualityService : IAirQualityService
    {

        private readonly string _httpUrl;
        private readonly HttpClient _httpClient;

        public EPAAirQualityService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _httpUrl = baseUrl;
        }

        public async Task<Root> GetAirQualityInformation()
        {

            try
            {
                Root defaultRoot = new Root();
                var airQualityResponse = await _httpClient.GetAsync(_httpUrl);
                if (airQualityResponse.IsSuccessStatusCode)
                {
                    defaultRoot = JsonSerializer.Deserialize<Root>(await airQualityResponse.Content.ReadAsStringAsync());
                    return defaultRoot;
                }
                else
                    return defaultRoot;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
