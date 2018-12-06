using System;
using System.Collections.Generic;
using DtCollect.Core.Entity;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;

namespace DtCollect.Infrastructure.Data
{
    public class DbSeed : IDbSeed
    {
        public void SeedDb(DbContextDtCollect ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            if (!ctx.Users.Any() && !ctx.Logs.Any())
            {
                string password = "1234";
                byte[] passwordHashJoe, passwordSaltJoe, passwordHashAnn, passwordSaltAnn;
                CreatePasswordHash(password, out passwordHashJoe, out passwordSaltJoe);
                CreatePasswordHash(password, out passwordHashAnn, out passwordSaltAnn);

                var user = ctx.Users.Add(new User()
                {

                    Username = "UserJoe",
                    PasswordHash = passwordHashJoe,
                    PasswordSalt = passwordSaltJoe,
                    IsAdmin = false
                }).Entity;

                var user2 = ctx.Users.Add(new User()
                {
                    Username = "AdminAnn",
                    PasswordHash = passwordHashAnn,
                    PasswordSalt = passwordSaltAnn,
                    IsAdmin = true
                }).Entity;

                ctx.Logs.Add(new Log()
                {
                    Id = 1,
                    User = user,
                    Success = true,
                    Action = "Data Request",
                    Date = DateTime.Now
                });
                
                ctx.Logs.Add(new Log()
                {
                    Id = 2,
                    User = user,
                    Success = true,
                    Action = "Data Request",
                    Date = DateTime.Now
                });
                
                ctx.Logs.Add(new Log()
                {
                    Id = 3,
                    User = user,
                    Success = false,
                    Action = "Data Request",
                    Date = DateTime.Now
                });
                
                ctx.Logs.Add(new Log()
                {
                    Id = 4,
                    User = user2,
                    Success = false,
                    Action = "Data Request",
                    Date = DateTime.Now
                });
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