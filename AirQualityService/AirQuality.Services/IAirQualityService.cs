using AirQuality.Services.Models;
using System.Threading.Tasks;

namespace AirQuality.Services
{
    public interface IAirQualityService
    {
        Task<Root> GetAirQualityInformation();
    }
}