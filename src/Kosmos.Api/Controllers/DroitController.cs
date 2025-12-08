using Kosmos.Business;
using Kosmos.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bejibe.Kosmos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DroitController : ControllerBase
    {

        private readonly ILogger<DroitController> _logger;

        private readonly IDroitService _service;

        public DroitController(ILogger<DroitController> logger, IDroitService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<DroitDto>> GetAll()
        {
            _logger.LogTrace("Start GetAllDroits Controller");

            var result = await _service.GetAllDroits();

            _logger.LogTrace("End GetAllDroits Controller");
            return result;
        }

        [HttpGet("{id}")]
        public async Task<DroitDto> GetById(string id)
        {
            _logger.LogTrace("Start GetDroitById Controller");

            var result = await _service.GetDroitById(id);

            _logger.LogTrace("End GetDroitById Controller");
            return result;
        }

        [HttpPost]
        public async Task<object> Add([FromBody] DroitDto droit)
        {
            _logger.LogTrace("Start GetDroitById Controller");

            var result = await _service.AddDroit(droit);

            _logger.LogTrace("End GetDroitById Controller");
            return result;
        }

    }
}
