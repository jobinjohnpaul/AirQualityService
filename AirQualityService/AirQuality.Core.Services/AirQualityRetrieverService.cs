using AirQuality.Core.Services.Models;
using AirQuality.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirQuality.Core.Services
{
    public class AirQualityRetrieverService : IAirQualityRetrieverService
    {
        private readonly IAirQualityService _airQualityService;
        private readonly IMapper _mapper;
        public AirQualityRetrieverService(IAirQualityService airQualityService, IMapper mapper)
        {
            _airQualityService = airQualityService;
            _mapper = mapper;
        }
        /// <summary>
        /// Pulled from .NET Framework code in System.Device.GeoCordinate.GetDistanceTo and found as an answer in SO
        /// https://stackoverflow.com/questions/6366408/calculating-distance-between-two-latitude-and-longitude-geocoordinates/51839058#51839058
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="otherLongitude"></param>
        /// <param name="otherLatitude"></param>
        /// <returns></returns>
        public double GetDistance(double longitude, double latitude, double otherLongitude, double otherLatitude)
        {
            var d1 = latitude * (Math.PI / 180.0);
            var num1 = longitude * (Math.PI / 180.0);
            var d2 = otherLatitude * (Math.PI / 180.0);
            var num2 = otherLongitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

        public async Task<LocationAirQuality> GetAirQualityForClosestLocation(double longitude, double latitude)
        {
            if((latitude is >= -90.0 and <= 90.0) || (longitude is >= -180.0 and <= 180.0))
            {
                return null;
            }

            var airQualityInfo = await _airQualityService.GetAirQualityInformation();
            var recs = _mapper.Map<IEnumerable<AirQualityRecordViewModel>>(airQualityInfo.Model.Records);

            foreach(var qualityInfoItem in recs)
            {
                qualityInfoItem.Distance = GetDistance(longitude, latitude, qualityInfoItem.Longitude, qualityInfoItem.Latitude);
            }
            // Iterate and measure distance, sort by smallest distance and take first

            var closestRecord = recs.ToList().OrderBy(item => item.Distance).FirstOrDefault();

            var displayModel = new LocationAirQuality(closestRecord.Longitude, closestRecord.Latitude);
            return displayModel;
        }
    }
}
