namespace WeatherService.Interfaces
{
    public interface IWeatherForecastHelper
    {
        Task<HttpResponseMessage> GetWeatherFocastByCity(string city);
    }
}
