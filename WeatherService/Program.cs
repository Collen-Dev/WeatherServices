using Microsoft.Extensions.Caching.Memory;
using WeatherService.Extensions;
using WeatherService.Helpers;
using WeatherService.Interfaces;
using WeatherService.Providers;
using WeatherService.Services;
using WeatherService.Settings;

namespace WeatherServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var uriOptions = new UriOptions();
            var uriSection = builder.Configuration.GetSection("URIs");
            uriSection.Bind(uriOptions);
            builder.Services.Configure<UriOptions>(uriSection);

            builder.Services.AddSingleton<HttpClient>()
                .AddMemoryCache()
                .AddSingleton<ISharedhelper, Sharedhelper>()
                .AddSingleton<IWeatherForecastHelper, WeatherForecastHelper>()
                .AddSingleton<IAzureSecretProvider, AzureSecretProvider>()
                .AddRestServices<IWeatherForecastService, WeatherForecastService>(builder.Configuration, "Services:WeatherService");

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen();

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.AddServerHeader = false;
            });

            builder.Services.AddHealthChecks();

            var app = builder.Build();           

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json","API V1"));

            app.UseHealthChecks("/health");

            if(!app.Environment.IsDevelopment())
            {
                app.UseHsts();

                // TODO: use a middle ware to implement this
                //app.UseSecurityHeader()
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}