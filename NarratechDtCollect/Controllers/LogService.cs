using System.Collections.Generic;
using DtCollect.Core.Entity;
using DtCollect.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace NarraTechDtCollect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;
        private IUserService _userService;

        public LogController(ILogService logService, IUserService userService)
        {
            _logService = logService;
            _userService = userService;
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<List<Log>> Get()
        {
            return _logService.ReadAll();
        }
        
        // POST api/values
        [HttpPost]
        public Log Post([FromBody] Log log)
        {
            return _logService.Create(log);
        }
        
       //GET api/values/{users}
        [HttpGet("{user}")]
        public ActionResult<List<Log>> Get(string user)
        {
            return Ok(_logService.ReadByUser(user));
        }
        
        //GET api/values/{success}
        [HttpGet("{success}")]
        public ActionResult<List<Log>> Get(bool success)
        {
            return Ok(_logService.ReadbySuccess(success));
        }
    }
}