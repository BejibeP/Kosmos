using Kosmos.Models;
using Microsoft.Extensions.Logging;

namespace Kosmos.Domain
{
    public class DroitRepository : IDroitRepository
    {

        private static List<DroitModel> Droits =
        [
            new DroitModel() { Guid = Guid.NewGuid().ToString(), Name = "Lecture des droits" },
            new DroitModel() { Guid = Guid.NewGuid().ToString(), Name = "Accès au technologies" },
            new DroitModel() { Guid = Guid.NewGuid().ToString(), Name = "Accès au rapport météo" }
        ];

        private readonly ILogger<DroitRepository> _logger;

        public DroitRepository(ILogger<DroitRepository> logger)
        {
            _logger = logger;
        }

        public async Task<IList<DroitModel>> GetAllDroits()
        {
            _logger.LogTrace("GetAllDroits - DAO call");
            return Droits;
        }

        public async Task<DroitModel> GetDroitById(string id)
        {
            _logger.LogTrace($"GetDroit n°{id} - DAO call");
            return Droits.FirstOrDefault(x => x.Guid == id) ?? new DroitModel();
        }

        public async Task<DroitModel> AddDroit(DroitModel droit)
        {
            _logger.LogTrace($"Add Droit n°{droit.Guid} - DAO call");
            Droits.Add(droit);
            return droit;
        }

    }
}
