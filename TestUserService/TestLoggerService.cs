using System;
using System.Collections.Generic;
using System.Linq;
using DtCollect.Core.Domain;
using DtCollect.Core.Entity;
using DtCollect.Core.Service.Impl;
using Moq;
using Xunit;

namespace TestUserService
{
    public class TestLoggerService
    {
        public List<User> GetUsers()
        {
            var Users = new List<User>
            {

                new User()
                {
                    Id = 1,
                    Username = "Mohammad",
                    IsAdmin = true
                },
                new User()
                {
                    Id = 2,
                    Username = "User 2",
                    IsAdmin = false
                },
                new User()
                {
                    Id = 3,
                    Username = "User 3",
                    IsAdmin = false
                }
            };
            return Users;
        }

        public List<Log> GetLogs()
        {
            var Logs = new List<Log>
            {
                new Log()
                {
                    Id = 1,
                    User = new User(),
                    Success = false,
                    Action = "401",
                    Date = DateTime.Now
                },
                new Log()
                {
                    Id = 2,
                    User = new User(),
                    Success = true,
                    Action = "Extract information form data",
                    Date = DateTime.Now
                },
                new Log()
                {
                    Id = 3,
                    User = new User(),
                    Success = false,
                    Action = "Failed to extract data",
                    Date = DateTime.Now 
                }
            };
            return Logs;
        }

        [Fact]
        public void TestGetAll()
        {
            var repo = new Mock<IRepo<Log>>();

            var Logs = GetLogs();
            repo.Setup(me => me.ReadAll()).Returns(Logs);

            var service = new LoggerService(repo.Object);

            Assert.True(service.ReadAll().Count == 3);
        }
        
        [Fact]
        public void TestCreate()
        {
            var Log = GetLogs().FirstOrDefault();
            var repo = new Mock<IRepo<Log>>();

            var service = new LoggerService(repo.Object);
            
            service.Create(Log);
            repo.Verify(x => x.Create(Log), Times.Once);
        }

        [Fact]
        public void TestReadByUser()
        {
            var repo = new Mock<IRepo<Log>>();
            
            var service = new LoggerService(repo.Object);
            service.ReadByUser(ToString());
            repo.Verify(l => l.ReadAll(), Times.Once);
            
        }

        [Fact]
        
        public void TestReadByType()
        {
            var repo = new Mock<IRepo<Log>>();
            
            var service = new LoggerService(repo.Object);

            bool error = false;
            
            service.ReadbySuccess(error);
            
            repo.Verify(l => l.ReadAll(), Times.Once );
        }
    }
}