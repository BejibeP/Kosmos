using Kosmos.Models;

namespace Kosmos.Domain
{
    public interface ITechnologyRepository
    {
        public Task<int> CountTechnologies();
        public Task<IList<TechnologyModel>> GetAllTechnologies();
        public Task<TechnologyModel> GetTechnologyById(int id);
        public Task<TechnologyModel> AddTechnology(TechnologyModel technology);
    }
}
