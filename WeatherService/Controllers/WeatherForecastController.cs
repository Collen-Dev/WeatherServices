using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherService.Contracts;
using WeatherService.Interfaces;

namespace WeatherService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastHelper _weatherForecastHelper;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ISharedhelper _sharedHelper;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
            IWeatherForecastHelper weatherForecastHelper,
            ISharedhelper sharedHelper)
        {
            _logger = logger;
            _weatherForecastHelper = weatherForecastHelper;
            _sharedHelper = sharedHelper;
        }

        [HttpGet("By-city-name")]        
        public async Task<IActionResult> Get(string city)
        {

            _logger.LogDebug("Search Weather by City.");

            // TODO: Remove this later,
            return new JsonResult(new { Temp = "Celsius", Low = "15", High = "25" });

            // integrate with weather service API
            var result = await _weatherForecastHelper.GetWeatherFocastByCity(city);

            try
            {
                if (!result.IsSuccessStatusCode)
                {
                    _logger.LogError($"Service returned error. Code: {result.StatusCode}, Message: {await result.Content.ReadAsStringAsync()}");
                    return new ObjectResult(_sharedHelper.ValidateAndHandleResponse<WeatherSearchError>(await result.Content.ReadAsStringAsync()));
                }
            }
            catch(Exception e)
            {
                _logger.LogCritical($"Critical error. {e}");
                throw;
            }

            return Ok(_sharedHelper.ValidateAndHandleResponse<WeatherFocastDetail>(await result.Content.ReadAsStringAsync()));
        }
    }
}