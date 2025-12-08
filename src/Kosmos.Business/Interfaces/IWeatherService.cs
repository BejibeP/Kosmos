using Kosmos.Dtos;

namespace Kosmos.Business
{
    public interface IWeatherService
    {
        public Task<IList<WeatherForecastDto>> GetNextForecast(int numberOfDays);
    }
}
