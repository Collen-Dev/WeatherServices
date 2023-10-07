using WeatherService.Interfaces;

namespace WeatherService.Helpers
{
    public class WeatherForecastHelper : IWeatherForecastHelper
    {
        private readonly IWeatherForecastService _weatherService;
        public WeatherForecastHelper(IWeatherForecastService weatherService) 
        {
            _weatherService = weatherService;
        }

        public async Task<HttpResponseMessage> GetWeatherFocastByCity(string city)
        {
            return await _weatherService.GetWeatherByCity(city);
        }
    }
}
