using Kosmos.Business.Mappers;
using Kosmos.Domain;
using Kosmos.Dtos;
using Microsoft.Extensions.Logging;

namespace Kosmos.Business
{
    public class TechnologyService : ITechnologyService
    {

        private readonly ILogger<TechnologyService> _logger;

        private readonly ITechnologyRepository _repository;

        public TechnologyService(ILogger<TechnologyService> logger, ITechnologyRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<int> CountTechnologies()
        {
            _logger.LogTrace("CountTechnologies - Service call");
            return await _repository.CountTechnologies();
        }

        public async Task<IList<TechnologyDto>> GetAllTechnologies()
        {
            _logger.LogTrace("GetAllTechnologies - Service call");
            var models = await _repository.GetAllTechnologies();

            _logger.LogInformation("GetAllTechnologies - Service mapping");
            return models.MapToDtos();
        }

        public async Task<TechnologyDto> GetTechnologyById(int id)
        {
            _logger.LogTrace($"GetTechnologyById n°{id} - Service call");
            var model = await _repository.GetTechnologyById(id);

            _logger.LogInformation("GetTechnologyById - Service Mapping");
            return model.MapToDto();
        }

        public async Task<TechnologyDto> AddTechnology(TechnologyDto technology)
        {
            _logger.LogTrace($"AddTechnology n°{technology.Id} - Service call");

            _logger.LogInformation("AddTechnology - Service Mapping to Model");
            var result = await _repository.AddTechnology(technology.MapToModel());

            _logger.LogInformation("AddTechnology - Service Mapping to Dto");
            return result.MapToDto();
        }

    }
}
