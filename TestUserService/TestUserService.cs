using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using DtCollect.Core.Domain;
using DtCollect.Core.Entity;
using DtCollect.Core.Service.Impl;
using Moq;
using Xunit;

namespace TestUserService
{
    public class TestUserService
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

        //Tests the Get All method in the service 
        [Fact]
        public void TestGetAll()
        {
            var repo = new Mock<IRepo<User>>();

            var Users = GetUsers();
            repo.Setup(me => me.ReadAll()).Returns(Users);

            var service = new UserService(repo.Object);

            Assert.True(service.ReadAll().Count == 3);
        }

        [Theory]
        [InlineData(1, "Mohammad")]
        [InlineData(2, "User 2")]
        [InlineData(3, "User 3")]
        public void TestGetById(int id, string name)
        {
            var repo = new Mock<IRepo<User>>();

            repo.Setup(me => me.Get(id)).Returns(GetUsers().FirstOrDefault(u => u.Id == id));
            var service = new UserService(repo.Object);

            Assert.True(service.GetById(id).Username == name);
        }

        [Fact]
        public void TestCreate()
        {
            var user = GetUsers().FirstOrDefault();
            var repo = new Mock<IRepo<User>>();

            var service = new UserService(repo.Object);
            
            service.Create(user);
            repo.Verify(x => x.Create(user), Times.Once);
        }

        [Fact]
        public void TestCreateExceptionUserName()
        {
            var repo = new Mock<IRepo<User>>();
            var user = new User()
            {
                Id = 1,
                IsAdmin = false
            };
            
            var service = new UserService(repo.Object);
            Exception ex = Assert.Throws<InvalidDataException>(() => service.Create(user));
            Assert.NotNull(ex);
            Assert.IsType<InvalidDataException>(ex);
            Assert.Equal(ex.Message, "Username is missing");
        }
        
        [Fact]
        public void TestCreateExceptionUserNameEmpty()
        {
            var repo = new Mock<IRepo<User>>();
            var user = new User()
            {
                Id = 1,
                Username = "",
                IsAdmin = false
            };
            
            var service = new UserService(repo.Object);
            Exception ex = Assert.Throws<InvalidDataException>(() => service.Create(user));
            Assert.NotNull(ex);
            Assert.IsType<InvalidDataException>(ex);
            Assert.Equal(ex.Message, "Username is missing");
        }
        
        [Fact]
        public void TestCreateExceptionUserNameWhiteSpace()
        {
            var repo = new Mock<IRepo<User>>();
            var user = new User()
            {
                Id = 1,
                Username = " ",
                IsAdmin = false
            };
            
            var service = new UserService(repo.Object);
            Exception ex = Assert.Throws<InvalidDataException>(() => service.Create(user));
            Assert.NotNull(ex);
            Assert.IsType<InvalidDataException>(ex);
            Assert.Equal(ex.Message, "Username is missing");
        }

        [Fact]
        public void TestCreateUserWithNoAdmin()
        {
            var repo = new Mock<IRepo<User>>();
            var user = new User()
            {
                Id = 1,
                Username = "Jarl Balgruuf"
            };

            repo.Setup(me => me.Create(user)).Returns(user);
            
            var service = new UserService(repo.Object);
            Assert.True(!service.Create(user).IsAdmin);
            
        }

        [Fact]
        public void TestCreateUserWithNoAdminFalse()
        {
            var repo = new Mock<IRepo<User>>();
            var user = new User()
            {
                Id = 1,
                Username = "Jarl Balgruuf"
            };

            repo.Setup(me => me.Create(user)).Returns(user);
            
            var service = new UserService(repo.Object);
            Assert.False(service.Create(user).IsAdmin);
        }

        [Fact]
        public void TestUpdate()
        {
            var repo = new Mock<IRepo<User>>();
            var users = GetUsers();
            var user = new User()
            {
                Id = 1,
                Username = "Mohammed",
                IsAdmin = true
            };
            
            var service = new UserService(repo.Object);
            service.Update(user.Id, user);
            
            repo.Verify(x => x.Update(user), Times.Once);
        }

        [Fact]
        public void TestUpdateNoUserName()
        {
            var repo = new Mock<IRepo<User>>();
            var user = new User();
            
            var service = new UserService(repo.Object);

            Exception ex = Assert.Throws<InvalidDataException>(() => service.Update(user.Id, user));
            
            Assert.Equal(ex.Message, "Username is missing");
            repo.Verify(x => x.Update(user), Times.Never);
        }

        [Fact]
        public void TestDelete()
        {
            var repo = new Mock<IRepo<User>>();
            var users = GetUsers();
            int id = 1;
            var user = users.FirstOrDefault(o => o.Id == id);
            repo.Setup(f => f.Get(id)).Returns(user);
            
            var service = new UserService(repo.Object);
            service.Delete(id);
            repo.Verify(f => f.Get(id), Times.Once);
            repo.Verify(x => x.Delete(user), Times.Once);
        }
    }
}