using System.Collections.Generic;
using DtCollect.Core.Entity;
using DtCollect.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace NarraTechDtCollect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        private readonly ILoggerService _loggerService;
        private IUserService _userService;

        public LoggerController(ILoggerService loggerService, IUserService userService)
        {
            _loggerService = loggerService;
            _userService = userService;
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<List<Log>> Get()
        {
            return _loggerService.ReadAll();
        }
        
        // POST api/values
        [HttpPost]
        public Log Post([FromBody] Log log)
        {
            return _loggerService.Create(log);
        }
        
       //GET api/values/{users}
        [HttpGet("{user}")]
        public ActionResult<List<Log>> Get(string user)
        {
            return Ok(_loggerService.ReadByUser(user));
        }
    }
}