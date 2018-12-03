using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DtCollect.Core.Entity;
using DtCollect.Core.Helpers;
using DtCollect.Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SQLitePCL;

namespace NarraTechDtCollect.Controllers
{
    [Route("/token")]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationHelper _auth;

        public TokenController(IUserService userService, IAuthenticationHelper auth)
        {
            _userService = userService;
            _auth = auth;
        }


        // GET api/values/5
        [HttpPost]
        public ActionResult Login([FromBody] LoginInput login)
        {   
           var user = _userService.GetUser(login);
            if (user == null)
            {
                return Unauthorized();
            }

            if (!_auth.VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized();
            }

            return Ok(new
            {
                username = user.Username,
                isAdmin = user.IsAdmin,
                token = _auth.GenerateToken(user)
            });
        }
    }
}