using System.Collections.Generic;
using DtCollect.Core.Entity;
using DtCollect.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace NarraTechDtCollect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_service.ReadAll());
        }

    }
}