namespace WeatherService.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<HttpResponseMessage> GetWeatherByCity(string city);
    }
}
