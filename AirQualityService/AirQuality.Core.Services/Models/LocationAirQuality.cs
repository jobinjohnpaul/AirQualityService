using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQuality.Core.Services.Models
{
    public record LocationAirQuality(double Longitude, double Latitude, string Location, string airQuality);
    // implicitly public properties
    // implicitly init only auto properties
}
