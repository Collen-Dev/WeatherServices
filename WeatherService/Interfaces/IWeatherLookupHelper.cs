namespace WeatherService.Interfaces
{
    public interface IWeatherLookupHelper
    {
        Task<HttpResponseMessage> GetWeatherFocastByCity(string city);
    }
}
