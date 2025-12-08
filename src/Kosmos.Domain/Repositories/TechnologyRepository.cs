using Kosmos.Models;
using Microsoft.Extensions.Logging;

namespace Kosmos.Domain
{
    public class TechnologyRepository : ITechnologyRepository
    {
        private static List<TechnologyModel> Technologies =
        [
            new TechnologyModel() { Id = 0, Name = "C#", Description = "Langage POO de Microsoft" },
            new TechnologyModel() { Id = 1, Name = "Java", Description = "Langage POO de Oracle" },
            new TechnologyModel() { Id = 2, Name = "Python", Description = "Langage dynamique" }
        ];

        private readonly ILogger<TechnologyRepository> _logger;

        public TechnologyRepository(ILogger<TechnologyRepository> logger)
        {
            _logger = logger;
        }

        public async Task<int> CountTechnologies()
        {
            _logger.LogTrace("CountTechnologies - DAO call");
            return Technologies.Count();
        }

        public async Task<IList<TechnologyModel>> GetAllTechnologies()
        {
            _logger.LogTrace("GetAllTechnologies - DAO call");
            return Technologies;
        }

        public async Task<TechnologyModel> GetTechnologyById(int id)
        {
            _logger.LogTrace($"GetTechnology n°{id} - DAO call");

            bool exists = Technologies.Any(x => x.Id == id);
            if (!exists) throw new Exception($"The Technology n°{id} does not exists");

            return Technologies.FirstOrDefault(x => x.Id == id) ?? new TechnologyModel();
        }

        public async Task<TechnologyModel> AddTechnology(TechnologyModel technology)
        {
            _logger.LogTrace($"Add Technology n°{technology.Id} - DAO call");
            Technologies.Add(technology);
            return technology;
        }

    }
}
