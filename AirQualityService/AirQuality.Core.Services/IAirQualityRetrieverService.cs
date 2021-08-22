using AirQuality.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQuality.Core.Services
{
    public interface IAirQualityRetrieverService
    {
        Task<LocationAirQuality> GetAirQualityForClosestLocation(double longitude, double latitude);
    }
}
