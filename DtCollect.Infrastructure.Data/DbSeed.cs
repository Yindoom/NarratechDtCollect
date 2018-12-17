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
            
            //ENsures the Database is created, and checks all tables for entries. If empty, fills out with data
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            if (!ctx.Users.Any() && !ctx.Logs.Any() && !ctx.Requests.Any())
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

                var request1 = ctx.Requests.Add(new Request()
                {
                    From = new DateTimeOffset(),
                    To = new DateTimeOffset(),
                    User = user,
                    Id = 1,
                    Logs = new List<Log>(),
                    Interval = TimeSpan.Parse("00:00:30"),
                    TagName = "ewrwer",
                    SampleType = "Point"

                }).Entity;
                
                var request2 = ctx.Requests.Add(new Request()
                {
                    From = new DateTimeOffset(),
                    To = new DateTimeOffset(),
                    User = user2,
                    Id = 2,
                    Logs = new List<Log>(),
                    Interval = TimeSpan.Parse("00:00:30"),
                    TagName = "ewrwer2",
                    SampleType = "Average"

                }).Entity;

                ctx.Logs.Add(new Log()
                {
                    Id = 1,
                    request = request1,
                    Success = true,
                    Message = "Data Request",
                    Date = DateTime.Now
                });
                
                ctx.Logs.Add(new Log()
                {
                    Id = 2,
                    request = request1,
                    Success = true,
                    Message = "Data Request",
                    Date = DateTime.Now
                });
                
                ctx.Logs.Add(new Log()
                {
                    Id = 3,
                    request = request2,
                    Success = false,
                    Message = "Data Request",
                    Date = DateTime.Now
                });
                
                ctx.Logs.Add(new Log()
                {
                    Id = 4,
                    request = request2,
                    Success = false,
                    Message = "Data Request",
                    Date = DateTime.Now
                });
            }
            ctx.SaveChanges();
        }
        
        //Stand in passwordhash method 
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