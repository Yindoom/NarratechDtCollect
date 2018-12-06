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
        private readonly ISampleService _service;

        public SamplesController(ISampleService service)
        {
            _service = service;
            
        }

        // GET
        [HttpPost]
        public ActionResult<IEnumerable<HistorianSample>> GetData([FromBody] Request request)
        {
            return Ok(_service.Get(request));
        }
    }
}