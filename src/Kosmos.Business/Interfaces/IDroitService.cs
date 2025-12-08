using Kosmos.Dtos;

namespace Kosmos.Business
{
    public interface IDroitService
    {
        public Task<IList<DroitDto>> GetAllDroits();
        public Task<DroitDto> GetDroitById(string id);
        public Task<DroitDto> AddDroit(DroitDto droit);
    }
}
