using Microsoft.Extensions.DependencyInjection;
using WeatherService.Extensions;
using WeatherService.Helpers;
using WeatherService.Interfaces;
using WeatherService.Services;
using WeatherService.Settings;

namespace WeatherServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration weatherServiceConfig = builder.Configuration;
            var weatherService = new ServiceOptions();
            weatherServiceConfig.GetSection("Services:WeatherService").Bind(weatherService);
            builder.Services.Configure<ServiceOptions>(weatherServiceConfig);

            builder.Services.AddSingleton<HttpClient>()
            .AddSingleton<ISharedhelper, Sharedhelper>()
            .AddSingleton<IWeatherForecastHelper, WeatherForecastHelper>()
            .AddRestServices<IWeatherForecastService, WeatherForecastService>(builder.Configuration, "Services:WeatherService");

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();           

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json","API V1"));

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}