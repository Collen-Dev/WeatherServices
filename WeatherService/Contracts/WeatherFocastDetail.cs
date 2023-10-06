using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WeatherService.Contracts
{
    public class WeatherFocastDetail
    {
        [JsonProperty("cloud")]
        public int Cloud { get; set; }

        [JsonProperty("wind_kph")]
        public int WindSpeed { get; set; }

        [JsonProperty("wind_dir")]
        public string WindDirection { get; set; }

        [JsonProperty("wind_degree")]
        public string WindDegree { get; set; }

        [JsonProperty("humidity")]
        public string Humidity { get; set; }

        [JsonProperty("location")]
        public LocationInfo Location { get; set; }

        [JsonProperty("current")]
        public CurrentInfo Current { get; set; }

        [JsonProperty("condition")]
        public ConditionInfo Condition { get; set; }
    }

    public class LocationInfo
    {
        [JsonProperty("name")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("lon")]
        public string Longitude { get; set; }

        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("localtime")]
        public string LocalTime { get; set; }
    }

    public class CurrentInfo
    {
        [JsonProperty("last_updated")]
        public string LastUpdated { get; set; }

        [JsonProperty("temp_c")]
        public string TemperatureCelsius { get; set; }

        [JsonProperty("temp_f")]
        public string TemperatureFarheit { get; set; }

        [JsonProperty("is_day")]
        public bool IsDay { get; set; }
    }

    public class ConditionInfo
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}
