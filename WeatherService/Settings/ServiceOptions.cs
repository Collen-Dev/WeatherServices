namespace WeatherService.Settings
{
    public class ServiceOptions
    {
        public string BaseUrl { get; set; }
        public string TimeOut { get; set; }
    }

    public class ServiceOptions<T> : ServiceOptions
    {

    }
}
