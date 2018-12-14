using System.Collections.Generic;
using DtCollect.Core.Entity;
using DtCollect.Core.Service;
using Microsoft.AspNetCore.Mvc;
using MockHistorian;

namespace NarraTechDtCollect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesController : ControllerBase
    {
        
        private ISampleService _sampleService;
        private IRequestService _requestService;

        public SamplesController(ISampleService sampleService, IRequestService requestService)
        {
            _sampleService = sampleService;
            _requestService = requestService;

        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<HistorianSample>> GetData(int id)
        {
            var req = _requestService.GetById(id);

            return Ok(_sampleService.Get(req));
        }
    }
}