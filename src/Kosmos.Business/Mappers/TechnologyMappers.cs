using Kosmos.Dtos;
using Kosmos.Models;

namespace Kosmos.Business.Mappers
{
    public static class TechnologyMappers
    {

        public static TechnologyDto MapToDto(this TechnologyModel model)
        {
            var dto = new TechnologyDto();
            dto.Id = model.Id;
            dto.Name = model.Name;
            dto.Description = model.Description;
            return dto;
        }

        public static IList<TechnologyDto> MapToDtos(this IList<TechnologyModel> models)
        {
            return models.Select(model => model.MapToDto()).ToList();
        }

        public static TechnologyModel MapToModel(this TechnologyDto dto)
        {
            var model = new TechnologyModel();
            model.Id = dto.Id;
            model.Name = dto.Name;
            model.Description = dto.Description;
            return model;
        }

        public static IList<TechnologyModel> MapToModels(this IList<TechnologyDto> dtos)
        {
            return dtos.Select(dto => dto.MapToModel()).ToList();
        }

    }
}
