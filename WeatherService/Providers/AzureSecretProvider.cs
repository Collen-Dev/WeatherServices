using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using WeatherService.Interfaces;
using WeatherService.Settings;

namespace WeatherService.Providers
{
    public class AzureSecretProvider : IAzureSecretProvider
    {
        private readonly IMemoryCache _cache;
        private readonly IOptions<UriOptions> _uriOptions;
        private readonly ILogger<AzureSecretProvider> _logger;
        public AzureSecretProvider(ILogger<AzureSecretProvider> logger, 
            IMemoryCache cache,
            IOptions<UriOptions> uriOptions) 
        {
            _cache = cache;
            _logger = logger;
            _uriOptions = uriOptions;
        }
        public async Task<string> GetSecretAsync(string secretKey)
        {
            _logger.LogInformation($"Fetching secret with key: {secretKey}");

            var key = "KV_C_AND_T";
            var keyVaultSecret = _cache.GetOrCreateAsync(key, async(entry)=>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(3);
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15);
                return await GetSecretInternalAsync(secretKey);
            });

            _logger.LogInformation($"Finished Fetching a secret.");
            return await keyVaultSecret;
        }

        private async Task<string> GetSecretInternalAsync(string secretKey)
        {
            if (_uriOptions.Value.KeyVault == null)
                throw new ArgumentNullException($"KeyVault Uri was not detected.");

            var keyVaultSecretClient = new SecretClient(new Uri(_uriOptions.Value.KeyVault), new DefaultAzureCredential(),
                new SecretClientOptions()
                {
                    Retry =
                    {
                        Delay = TimeSpan.FromSeconds(2),
                        MaxDelay = TimeSpan.FromSeconds(14),
                        MaxRetries = 3,
                        Mode = Azure.Core.RetryMode.Exponential,
                        NetworkTimeout = TimeSpan.FromSeconds(20),
                    }
                });

            KeyVaultSecret secret = await keyVaultSecretClient.GetSecretAsync(secretKey);
            return secret.Value;
        }
    }
}
