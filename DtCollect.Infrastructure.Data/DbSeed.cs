using System.Collections.Generic;
using DtCollect.Core.Entity;
using System.Linq;
namespace DtCollect.Infrastructure.Data
{
    public class DbSeed : IDbSeed
    {
        public void SeedDb(DbContextDtCollect ctx)
        {
            ctx.Database.EnsureCreated();
            if (!ctx.Users.Any())
            {
                string password = "1234";
                byte[] passwordHashJoe, passwordSaltJoe, passwordHashAnn, passwordSaltAnn;
                CreatePasswordHash(password, out passwordHashJoe, out passwordSaltJoe);
                CreatePasswordHash(password, out passwordHashAnn, out passwordSaltAnn);
                List<User> users = new List<User>
                {
                    new User
                    {
                        Username = "UserJoe",
                        PasswordHash = passwordHashJoe,
                        PasswordSalt = passwordSaltJoe,
                        IsAdmin = false
                    },
                    new User
                    {
                        Username = "AdminAnn",
                        PasswordHash = passwordHashAnn,
                        PasswordSalt = passwordSaltAnn,
                        IsAdmin = true
                    }
                };
                ctx.Users.AddRange(users);
            }
            

            ctx.SaveChanges();
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}