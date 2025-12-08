using Kosmos.Dtos;

namespace Kosmos.Business
{
    public interface ITechnologyService
    {
        public Task<int> CountTechnologies();
        public Task<IList<TechnologyDto>> GetAllTechnologies();
        public Task<TechnologyDto> GetTechnologyById(int id);
        public Task<TechnologyDto> AddTechnology(TechnologyDto technology);
    }
}
