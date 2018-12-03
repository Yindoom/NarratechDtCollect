using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DtCollect.Core.Entity;
using DtCollect.Core.Helpers;
using DtCollect.Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace NarraTechDtCollect.Controllers
{
    [Route("/token")]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;

        public TokenController(IUserService userService)
        {
            _userService = userService;
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

            if (!VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized();
            }

            return Ok(new
            {
                username = user.Username,
                isAdmin = user.IsAdmin,
                token = GenerateToken(user)
            });
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash((System.Text.Encoding.UTF8.GetBytes(password)));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };
            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }
            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    JwtSecurityKey.Key, 
                    SecurityAlgorithms.HmacSha256)),
                
                new JwtPayload(null,
                    null,
                    claims.ToArray(),
                    DateTime.Now,
                    DateTime.Now.AddMinutes(5)));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}