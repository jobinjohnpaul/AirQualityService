using AirQuality.Core.Services.Models;
using AirQuality.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        /// Pulled from .NET Framework code in System.Device.GeoCordinate.GetDistanceTo and found as an answer in 
        /// https://www.geodatasource.com/developers/c-sharp
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="otherLongitude"></param>
        /// <param name="otherLatitude"></param>
        /// <returns></returns>
        public static double GetDistance(double longitude, double latitude, double otherLongitude, double otherLatitude)
        {
            double theta = longitude - otherLongitude;

            var lat1Rad = latitude * (Math.PI / 180.0);
            var lat2Rad = otherLatitude * (Math.PI / 180.0);
            var thetaRad = theta * (Math.PI / 180);

            double dist = Math.Sin(lat1Rad) * Math.Sin(lat2Rad) + Math.Cos(lat1Rad) * Math.Cos(lat2Rad) * Math.Cos(thetaRad);
            dist = Math.Acos(dist);
            dist *= (Math.PI / 180.0);
            dist = dist * 60 * 1.1515;
            dist *= 1.609344;

            return dist;
        }

        public async Task<LocationAirQuality> GetAirQualityForClosestLocation(double latitude, double longitude, CancellationToken cancellationToken)
        {
            if((latitude is < -90.0 or > 90.0) || (longitude  is < -180.0 or > 180.0))
            {
                return null;
            }

            var airQualityInfo = await _airQualityService.GetAirQualityInformation(cancellationToken);
            var recs = _mapper.Map<IEnumerable<AirQualityRecordViewModel>>(airQualityInfo.Model.Records).ToList();

            recs.ForEach(qualityInfoItem => qualityInfoItem.Distance = GetDistance(longitude, latitude, qualityInfoItem.Longitude, qualityInfoItem.Latitude));
            
            // Iterate and measure distance, sort by smallest distance and take first non faulted device (not sensor)

            var closestRecord = recs.OrderBy(item => item.Distance).FirstOrDefault(x => x.SiteType != "Sensor" && x.HealthAdvice != "NA");

            return new LocationAirQuality(closestRecord.Longitude, closestRecord.Latitude, closestRecord.SiteName, closestRecord.HealthAdvice);
        }
    }
}
