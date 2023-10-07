using Microsoft.Extensions.Options;
using WeatherService.Interfaces;
using WeatherService.Settings;

namespace WeatherService.Services
{    
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly HttpClient _client;
        private readonly IOptions<ServiceOptions<WeatherForecastService>> _options;
        public WeatherForecastService(HttpClient client,
            IOptions<ServiceOptions<WeatherForecastService>> options) 
        {
            _client = client;
            _options = options;
        }

        public async Task<HttpResponseMessage> GetWeatherByCity(string city)
        {
            // TODO: Code clean-up!!
            var uri = $"v1/current.json?q={city}";
            uri = uri+"&key=97dcfcf6f5414088b62191231230610";
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            return await _client.SendAsync(request);
        }
    }
}
