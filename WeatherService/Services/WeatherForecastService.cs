using Microsoft.Extensions.Options;
using WeatherService.Constants;
using WeatherService.Interfaces;
using WeatherService.Settings;

namespace WeatherService.Services
{    
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly HttpClient _client;
        private readonly IAzureSecretProvider _azureSecretProvider;
        private readonly IOptions<ServiceOptions<WeatherForecastService>> _options;
        public WeatherForecastService(HttpClient client,
            IOptions<ServiceOptions<WeatherForecastService>> options,
            IAzureSecretProvider azureSecretProvider) 
        {
            _client = client;
            _options = options;
            _azureSecretProvider = azureSecretProvider;
        }

        public async Task<HttpResponseMessage> GetWeatherByCity(string city)
        {
            string apiKey = await GetWatherApiKey();
            var uri = $"v1/current.json?q={city}&key={apiKey}";
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            return await _client.SendAsync(request);
        }

        private async Task<string> GetWatherApiKey()
        {
            return await _azureSecretProvider.GetSecretAsync(ConstantValues.KV_C_AND_T);
        }
    }
}
