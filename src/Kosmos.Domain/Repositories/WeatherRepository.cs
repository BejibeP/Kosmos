using Kosmos.Models;
using Microsoft.Extensions.Logging;

namespace Kosmos.Domain
{
    public class WeatherRepository : IWeatherRepository
    {

        private static string[] Summaries =
        [
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"
        ];

        private readonly ILogger<WeatherRepository> _logger;

        public WeatherRepository(ILogger<WeatherRepository> logger)
        {
            _logger = logger;
        }

        public async Task<IList<WeatherForecastModel>> GetNextForecast(int numberOfDays)
        {
            _logger.LogTrace("Start NextForecast");

            var forecasts = new List<WeatherForecastModel>();
            for (int i = 0; i < numberOfDays; i++)
            {
                var forecast = new WeatherForecastModel()
                {
                    Date = DateTime.Now.AddDays(i),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                };
                forecasts.Add(forecast);
            }

            _logger.LogTrace("Stop NextForecast");
            return forecasts;
        }

    }
}
