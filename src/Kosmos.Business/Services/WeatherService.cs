using Kosmos.Business.Mappers;
using Kosmos.Domain;
using Kosmos.Dtos;
using Microsoft.Extensions.Logging;

namespace Kosmos.Business
{
    public class WeatherService : IWeatherService
    {

        private readonly ILogger<WeatherService> _logger;

        private readonly IWeatherRepository _repository;

        public WeatherService(ILogger<WeatherService> logger, IWeatherRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IList<WeatherForecastDto>> GetNextForecast(int numberOfDays)
        {
            _logger.LogTrace("Start NextForecast Service");
            var models = await _repository.GetNextForecast(numberOfDays);

            _logger.LogTrace("Start NextForecast Mapping");
            return models.MapToDtos();
        }

    }
}
