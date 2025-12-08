using Kosmos.Dtos;
using Kosmos.Models;

namespace Kosmos.Business.Mappers
{
    public static class ForecastMappers
    {

        public static WeatherForecastDto MapToDto(this WeatherForecastModel model)
        {
            var dto = new WeatherForecastDto();
            dto.Date = DateOnly.FromDateTime(model.Date);
            dto.TemperatureC = model.TemperatureC;
            dto.Summary = model.Summary;
            return dto;
        }

        public static IList<WeatherForecastDto> MapToDtos(this IList<WeatherForecastModel> models)
        {
            return models.Select(model => model.MapToDto()).ToList();
        }

        public static WeatherForecastModel MapToModel(this WeatherForecastDto dto)
        {
            var model = new WeatherForecastModel();
            model.Date = new DateTime(dto.Date, new TimeOnly());
            model.TemperatureC = dto.TemperatureC;
            model.Summary = dto.Summary;
            return model;
        }

        public static IList<WeatherForecastModel> MapToModels(this IList<WeatherForecastDto> dtos)
        {
            return dtos.Select(dto => dto.MapToModel()).ToList();
        }

    }
}
