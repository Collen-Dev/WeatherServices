namespace WeatherService.Interfaces
{
    public interface IAzureSecretProvider
    {
        Task<string> GetSecretAsync(string secretKey);
    }
}
