namespace WeatherService.Interfaces
{
    public interface ISharedhelper
    {
        T? ValidateAndHandleResponse<T>(string content) where T : class;
    }
}
