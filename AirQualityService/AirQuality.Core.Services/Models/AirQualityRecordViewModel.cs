using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQuality.Core.Services.Models
{
    public class AirQualityRecordViewModel
    {
        public string SiteID { get; set; }

        public string SiteName { get; set; }

        public string SiteType { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public object Distance { get; set; }

        public string CameraImageURL { get; set; }

        public string TimeLapseURL { get; set; }

        public DateTime SiteHealthAdviceSince { get; set; }

        public DateTime SiteHealthAdviceUntil { get; set; }

        public string HealthParameter { get; set; }

        public double AverageValue { get; set; }

        public string HealthAdvice { get; set; }

        public string HealthAdviceColor { get; set; }

        public int HealthCode { get; set; }

        public string Unit { get; set; }

        
    }
}
