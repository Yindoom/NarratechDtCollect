using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DtCollect.Core.Entity;
using DtCollect.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NarraTechDtCollect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        
        //Generic CRUD
        
        
        public UsersController(IUserService service)
        {
            _service = service;
        }

        // GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_service.ReadAll());
        }
        
        //Get api/users/{id}
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            return Ok(_service.GetById(id));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            return Ok(_service.Create(user));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<User> Put([FromBody] User user, int id)
        {
            return Ok(_service.Update(id, user));
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            return Ok(_service.Delete(id));
        }
    }
}