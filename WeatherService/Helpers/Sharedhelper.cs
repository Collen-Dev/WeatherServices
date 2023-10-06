using Newtonsoft.Json;
using WeatherService.Interfaces;

namespace WeatherService.Helpers
{
    public class Sharedhelper : ISharedhelper
    {
        public T? ValidateAndHandleResponse<T>(string content) where T : class
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
