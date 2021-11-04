using AirQuality.Core.Services.Models;
using System.Threading;
using System.Threading.Tasks;

namespace AirQuality.Core.Services
{
    public interface IAirQualityRetrieverService
    {
        Task<LocationAirQuality> GetAirQualityForClosestLocation(double latitude, double longitude, CancellationToken cancellationToken = default);
    }
}
