using Kosmos.Models;

namespace Kosmos.Domain
{
    public interface IWeatherRepository
    {
        public Task<IList<WeatherForecastModel>> GetNextForecast(int numberOfDays);
    }
}
