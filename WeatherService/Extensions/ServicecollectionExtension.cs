using WeatherService.Settings;

namespace WeatherService.Extensions
{
    public static class ServicecollectionExtension
    {
        public static IServiceCollection AddRestServices<TInterface, TImplementation>(this IServiceCollection services,
            IConfiguration configuration, string configSetting) 
            where TInterface : class
            where TImplementation : class, TInterface
        {
            var options = configuration.GetSection(configSetting).Get<ServiceOptions>();
            
            if(options == null)
                throw new ArgumentNullException($"No config defined for type {nameof(TInterface)}.");

            if (string.IsNullOrEmpty(options.BaseUrl))
                throw new ArgumentNullException($"No BaseUrl provided for type {typeof(TInterface)}");

            services.Configure<ServiceOptions>(configuration.GetSection(configSetting))
                .AddSingleton<TInterface, TImplementation>()
                .AddHttpClient<TInterface, TImplementation>( client =>
                {
                    client.BaseAddress = new Uri(options.BaseUrl);

                    client.Timeout = (options.TimeOutInMilliSeconds <= 0)
                    ? TimeSpan.FromMilliseconds(ServiceOptions.DEFAULT_TIMEOUT) 
                    : TimeSpan.FromMilliseconds(options.TimeOutInMilliSeconds);
                });

            return services;
        }
    }
}
