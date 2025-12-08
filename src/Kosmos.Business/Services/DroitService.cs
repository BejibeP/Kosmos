using Kosmos.Business.Mappers;
using Kosmos.Domain;
using Kosmos.Dtos;
using Microsoft.Extensions.Logging;

namespace Kosmos.Business
{
    public class DroitService : IDroitService
    {
        private readonly ILogger<DroitService> _logger;

        private readonly IDroitRepository _repository;

        public DroitService(ILogger<DroitService> logger, IDroitRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IList<DroitDto>> GetAllDroits()
        {
            _logger.LogTrace("GetAllDroits - Service call");
            var models = await _repository.GetAllDroits();

            _logger.LogInformation("GetAllDroits - Service mapping");
            return models.MapToDtos();
        }

        public async Task<DroitDto> GetDroitById(string id)
        {
            _logger.LogTrace($"GetDroitById n°{id} - Service call");
            var model = await _repository.GetDroitById(id);

            _logger.LogInformation("GetDroitById - Service Mapping");
            return model.MapToDto();
        }

        public async Task<DroitDto> AddDroit(DroitDto droit)
        {
            _logger.LogTrace($"AddDroit n°{droit.Guid} - Service call");

            _logger.LogInformation("AddDroit - Service Mapping to Model");
            var result = await _repository.AddDroit(droit.MapToModel());

            _logger.LogInformation("AddDroit - Service Mapping to Dto");
            return result.MapToDto();
        }

    }
}
