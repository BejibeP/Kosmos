using Kosmos.Dtos;
using Kosmos.Models;

namespace Kosmos.Business.Mappers
{
    public static class DroitMappers
    {

        public static DroitDto MapToDto(this DroitModel model)
        {
            var dto = new DroitDto();
            dto.Guid = model.Guid;
            dto.Name = model.Name;
            return dto;
        }

        public static IList<DroitDto> MapToDtos(this IList<DroitModel> models)
        {
            return models.Select(model => model.MapToDto()).ToList();
        }

        public static DroitModel MapToModel(this DroitDto dto)
        {
            var model = new DroitModel();
            model.Guid = dto.Guid;
            model.Name = dto.Name;
            return model;
        }

        public static IList<DroitModel> MapToModels(this IList<DroitDto> dtos)
        {
            return dtos.Select(dto => dto.MapToModel()).ToList();
        }

    }
}
