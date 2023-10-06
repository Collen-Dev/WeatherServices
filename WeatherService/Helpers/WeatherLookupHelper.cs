using WeatherService.Interfaces;

namespace WeatherService.Helpers
{
    public class WeatherLookupHelper : IWeatherLookupHelper
    {
        private readonly IWeatherService _weatherService;
        public WeatherLookupHelper(IWeatherService weatherService) 
        {
            _weatherService = weatherService;
        }

        public async Task<HttpResponseMessage> GetWeatherFocastByCity(string city)
        {
            return await _weatherService.GetWeatherByCity(city);
        }
    }
}
