using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AirQuality.Services.Models
{
    public class Geometry
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("coordinates")]
        public List<double> Coordinates { get; set; }
    }

    public class SiteHealthAdvice
    {
        [JsonPropertyName("since")]
        public DateTime Since { get; set; }

        [JsonPropertyName("until")]
        public DateTime Until { get; set; }

        [JsonPropertyName("healthParameter")]
        public string HealthParameter { get; set; }

        [JsonPropertyName("averageValue")]
        public double AverageValue { get; set; }

        [JsonPropertyName("healthAdvice")]
        public string HealthAdvice { get; set; }

        [JsonPropertyName("healthAdviceColor")]
        public string HealthAdviceColor { get; set; }

        [JsonPropertyName("healthCode")]
        public int HealthCode { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class Reading
    {
        [JsonPropertyName("since")]
        public DateTime Since { get; set; }

        [JsonPropertyName("until")]
        public DateTime Until { get; set; }

        [JsonPropertyName("averageValue")]
        public double AverageValue { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("totalSample")]
        public int TotalSample { get; set; }

        [JsonPropertyName("healthAdvice")]
        public string HealthAdvice { get; set; }

        [JsonPropertyName("healthAdviceColor")]
        public string HealthAdviceColor { get; set; }

        [JsonPropertyName("healthCode")]
        public int HealthCode { get; set; }
    }

    public class TimeSeriesReading
    {
        [JsonPropertyName("timeSeriesName")]
        public string TimeSeriesName { get; set; }

        [JsonPropertyName("readings")]
        public List<Reading> Readings { get; set; }
    }

    public class Parameter
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("timeSeriesReadings")]
        public List<TimeSeriesReading> TimeSeriesReadings { get; set; }
    }

    public class Record
    {
        [JsonPropertyName("siteID")]
        public string SiteID { get; set; }

        [JsonPropertyName("siteName")]
        public string SiteName { get; set; }

        [JsonPropertyName("siteType")]
        public string SiteType { get; set; }

        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }

        [JsonPropertyName("distance")]
        public object Distance { get; set; }

        [JsonPropertyName("cameraImageURL")]
        public string CameraImageURL { get; set; }

        [JsonPropertyName("timeLapseURL")]
        public string TimeLapseURL { get; set; }

        [JsonPropertyName("siteHealthAdvices")]
        public List<SiteHealthAdvice> SiteHealthAdvices { get; set; }

        [JsonPropertyName("count")]
        public object Count { get; set; }

        [JsonPropertyName("parameters")]
        public List<Parameter> Parameters { get; set; }
    }

    public class Model
    {
        [JsonPropertyName("totalRecords")]
        public string TotalRecords { get; set; }

        [JsonPropertyName("records")]
        public List<Record> Records { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("Model")]
        public Model Model { get; set; }

        [JsonPropertyName("ErrorResponse")]
        public object ErrorResponse { get; set; }

        [JsonPropertyName("StatusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("APIOffline")]
        public bool APIOffline { get; set; }
    }


}
