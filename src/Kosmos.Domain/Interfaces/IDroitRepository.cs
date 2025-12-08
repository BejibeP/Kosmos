using Kosmos.Models;

namespace Kosmos.Domain
{
    public interface IDroitRepository
    {
        public Task<IList<DroitModel>> GetAllDroits();
        public Task<DroitModel> GetDroitById(string id);
        public Task<DroitModel> AddDroit(DroitModel droit);
    }
}
