namespace WeatherService.Settings
{
    public class ServiceOptions
    {
        public const int DEFAULT_TIMEOUT = 30000;
        public string BaseUrl { get; set; }
        public int TimeOutInMilliSeconds { get; set; }
    }

    public class ServiceOptions<T> : ServiceOptions
    {}
}
