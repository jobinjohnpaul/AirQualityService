using AirQuality.Core.Services.Models;
using AirQuality.Services.Models;
using AutoMapper;

namespace AirQuality.Core.Services.MappingProfiles
{
    public class AirQualityMappingProfile : Profile
    {
        public AirQualityMappingProfile()
        {
            CreateMap<Record, AirQualityRecordViewModel>()
                .ForMember(c => c.Latitude, opt => opt.MapFrom(d => d.Geometry.Coordinates[0]))
                .ForMember(c => c.Longitude, opt => opt.MapFrom(d => d.Geometry.Coordinates[1]))
                .ForMember(c => c.SiteHealthAdviceSince, opt => opt.MapFrom(d => d.SiteHealthAdvices[0].Since))
                .ForMember(c => c.SiteHealthAdviceUntil, opt => opt.MapFrom(d => d.SiteHealthAdvices[0].Until))
                .ForMember(c => c.HealthParameter, opt => opt.MapFrom(d => d.SiteHealthAdvices[0].HealthParameter))
                .ForMember(c => c.AverageValue, opt => opt.MapFrom(d => d.SiteHealthAdvices[0].AverageValue))
                .ForMember(c => c.HealthAdvice, opt => opt.MapFrom(d => d.SiteHealthAdvices[0].HealthAdvice))
                .ForMember(c => c.HealthAdviceColor, opt => opt.MapFrom(d => d.SiteHealthAdvices[0].HealthAdviceColor))
                .ForMember(c => c.HealthCode, opt => opt.MapFrom(d => d.SiteHealthAdvices[0].HealthCode))
                .ForMember(c => c.Unit, opt => opt.MapFrom(d => d.SiteHealthAdvices[0].Unit))

                .ReverseMap();
        }
    }
}
