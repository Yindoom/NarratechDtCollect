using System.Collections.Generic;
using System.Security.AccessControl;
using DtCollect.Core.Entity;
using DtCollect.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NarraTechDtCollect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private IUserService _userService;

        public RequestController(IRequestService requestService, IUserService userService)
        {
            _requestService = requestService;
            _userService = userService;

        }
       
        //Saves a request, after adding a user based on the username of the user who sent the request in
        // Post 
        [Authorize]
        [HttpPost]
        public ActionResult<Request> PostRequest([FromBody] Request request, [FromQuery] string username)
        {
            var user = _userService.GetUser(username);
            request.User = new User() {Id = user.Id};
            
            return Ok(_requestService.Create(request));    
        }

        //Reads requests by users, if a username was sent in the query
        //Otherwise, reads all requests
        [HttpGet]
        public ActionResult<IEnumerable<Request>> GetRequests([FromQuery] string user)
        {
            if(!string.IsNullOrEmpty(user))
            {
                return Ok(_requestService.ReadByUser(user));
            }
            
            return Ok(_requestService.ReadAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Request> GetReqById(int id)
        {
            return _requestService.GetById(id);
            
        }
            
    }
}