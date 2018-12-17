using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using DtCollect.Core.Entity;
using DtCollect.Core.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace DtCollect.Core.Service.Impl
{
    public class AuthenticationHelper: IAuthenticationHelper
    {
        private byte[] secretBytes;
        // SecretBytes get an array of bytes called secret that we generate in the startup. 
        public AuthenticationHelper(byte[] secret)
        {
            secretBytes = secret;
        }

        // It creates a password salt and hashes the password.
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        // To verify the password it converts it to a hash value using the salt. It then checks both arrays
        // to see if they match, if they do then it returns true if not then it returns false. 
        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
        //Generate a token that is valid for 10 minutes. The token is used between the server and the client,
        // to check wether the user is logged in or not. 
        public string GenerateToken(User user)
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
                    new SymmetricSecurityKey(secretBytes),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(null,
                    null,
                    claims.ToArray(),
                    DateTime.Now,
                    DateTime.Now.AddMinutes(10)));
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}