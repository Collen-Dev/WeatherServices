namespace WeatherService.Interfaces
{
    public interface IWeatherService
    {
        Task<HttpResponseMessage> GetWeatherByCity(string city);
    }
}
