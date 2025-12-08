using Kosmos.Business;
using Kosmos.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Kosmos.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IWeatherService _service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecastDto>> Get()
        {
            _logger.LogTrace("Start GetWeatherForecast Controller");

            int numberOfDays = Random.Shared.Next(1, 7);
            var result = await _service.GetNextForecast(numberOfDays);

            _logger.LogTrace("End GetWeatherForecast Controller");
            return result;
        }

    }
}
