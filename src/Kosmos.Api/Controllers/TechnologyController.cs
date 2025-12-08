using Kosmos.Api.Controllers;
using Kosmos.Business;
using Kosmos.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bejibe.Kosmos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologyController : ControllerBase
    {
        private readonly ILogger<TechnologyController> _logger;

        private readonly ITechnologyService _service;

        public TechnologyController(ILogger<TechnologyController> logger, ITechnologyService service)
        {
            _logger = logger;
            _service = service;
        }

        // from https://dotnettutorials.net/lesson/http-head-method-in-asp-net-core-web-api/
        [HttpHead]
        public async Task<IActionResult> Count()
        {
            _logger.LogTrace("Start CountTechnologies Controller");

            var count = await _service.CountTechnologies();

            Response.Headers.Append("Last-Modified", DateTime.UtcNow.ToString());
            Response.Headers.Append("Count", count.ToString());

            _logger.LogTrace("End CountTechnologies Controller");
            return Ok();
        }

        [HttpHead("{Id}")]
        public async Task<IActionResult> Exists(int Id)
        {
            _logger.LogTrace("Start Exists Controller");

            var techs = await _service.GetAllTechnologies();


            bool exists = techs.Any(x => x.Id == Id);
            if (!exists)
                return NotFound();

            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<TechnologyDto>> GetAll()
        {
            _logger.LogTrace("Start GetAllTechnologies Controller");

            var result = await _service.GetAllTechnologies();

            _logger.LogTrace("End GetAllTechnologies Controller");
            return result;
        }

        [HttpGet("{id}")]
        public async Task<TechnologyDto> GetById(int id)
        {
            _logger.LogTrace("Start GetTechnologyById Controller");

            var result = await _service.GetTechnologyById(id);

            _logger.LogTrace("End GetTechnologyById Controller");
            return result;
        }

        [HttpPost]
        public async Task<TechnologyDto> Add([FromBody] TechnologyDto technology)
        {
            _logger.LogTrace("Start AddTechnology Controller");

            var result = await _service.AddTechnology(technology);

            _logger.LogTrace("End AddTechnology Controller");
            return result;
        }

    }
}
