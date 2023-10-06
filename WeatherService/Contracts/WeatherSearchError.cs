using Newtonsoft.Json;

namespace WeatherService.Contracts
{
    public class WeatherSearchError
    {
        [JsonProperty("error")]
        public SearchError Error { get; set; }
    }

    public class SearchError
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; } 
    }
}
