using Microsoft.AspNetCore.Mvc;
using WeatherService.Contracts;
using WeatherService.Interfaces;

namespace WeatherService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LookupWeatherController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IWeatherForecastHelper _weatherLookupHelper;
        private readonly ILogger<LookupWeatherController> _logger;
        private readonly ISharedhelper _sharedhelper;

        public LookupWeatherController(ILogger<LookupWeatherController> logger, 
            IWeatherForecastHelper weatherLookupHelper,
            ISharedhelper sharedhelper)
        {
            _logger = logger;
            _weatherLookupHelper = weatherLookupHelper;
            _sharedhelper = sharedhelper;
        }

        [HttpGet("By-city-name")]
        public async Task<IActionResult> Get(string city)
        {
            _logger.LogDebug("Search Weather by City.");

            // integrate with weather service API
            var result = await _weatherLookupHelper.GetWeatherFocastByCity(city);

            try
            {
                if (!result.IsSuccessStatusCode)
                {
                    _logger.LogError($"Service returned error. Code: {result.StatusCode}, Message: {await result.Content.ReadAsStringAsync()}");
                    return new ObjectResult(_sharedhelper.ValidateAndHandleResponse<WeatherSearchError>(await result.Content.ReadAsStringAsync()));
                }
            }
            catch(Exception e)
            {
                _logger.LogCritical($"Critical error occured. {e}");
                throw;
            }

            return Ok(_sharedhelper.ValidateAndHandleResponse<WeatherFocastDetail>(await result.Content.ReadAsStringAsync()));
        }
    }
}