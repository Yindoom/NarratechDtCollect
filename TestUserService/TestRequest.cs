using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DtCollect.Core.Domain;
using DtCollect.Core.Entity;
using DtCollect.Core.Service;
using DtCollect.Core.Service.Impl;
using Moq;
using Xunit;

namespace TestUserService
{
    public class TestRequest
    {
        public List<Request> GetRequests()
        {
            var Requests = new List<Request>
            {
                new Request()
                {
                    User = new User(),
                    From = new DateTimeOffset(),
                    To = new DateTimeOffset(),
                    Interval = new TimeSpan(),
                    SampleType = "Good",
                    TagName = "Average"
                },
                
                new Request()                    
                {                                
                    User = new User(),           
                    From = new DateTimeOffset(), 
                    To = new DateTimeOffset(),   
                    Interval = new TimeSpan(),   
                    SampleType = "Good",         
                    TagName = "Average"          
                },
                new Request()                    
                {                                
                    User = new User(),           
                    From = new DateTimeOffset(), 
                    To = new DateTimeOffset(),   
                    Interval = new TimeSpan(),   
                    SampleType = "Good",         
                    TagName = "Max"          
                },
                new Request()                    
                {                                
                    User = new User(),           
                    From = new DateTimeOffset(), 
                    To = new DateTimeOffset(),   
                    Interval = new TimeSpan(),   
                    SampleType = "Good",         
                    TagName = "Min"          
                }                              
            };
            return Requests;
        }


        [Fact]
        public void TestCreate()
        {
            var request = GetRequests().FirstOrDefault();
            var repo = new Mock<IRepo<Request>>();
            var log = new Mock<ILogService>();
            request.From = DateTimeOffset.Parse("2018/01/01 00:00");
            request.To = DateTimeOffset.Parse("2018/12/31 00:00");

            var service = new RequestService(repo.Object, log.Object);
            
            service.Create(request);
            repo.Verify(x => x.Create(request), Times.Once);
        }

        [Fact]
        public void TestDateTimeOffsetException()
        {
            var request = GetRequests().FirstOrDefault();      
            var repo = new Mock<IRepo<Request>>();             
            var log = new Mock<ILogService>();                                        
            var service = new RequestService(repo.Object, log.Object);     
            request.From = DateTimeOffset.Parse("2019/01/01 00:00");
            request.To = DateTimeOffset.Parse("2018/12/31 00:00");            
            Exception ex = Assert.Throws<InvalidDataException>(() => service.Create(request));
            Assert.True(ex.Message.Equals("From must be earlier than To")); 
            
        }
        
        [Fact]                                                                                   
        public void TestDateTimeOffsetNotEntered()                                                
        {                                                                                        
            var request = GetRequests().FirstOrDefault();                                        
            var repo = new Mock<IRepo<Request>>();                                               
            var log = new Mock<ILogService>();                                                                                
            var service = new RequestService(repo.Object, log.Object);                                       
            request.From = DateTimeOffset.MinValue;                             
            request.To = DateTimeOffset.Parse("2018/12/31 00:00");                               
            Exception ex = Assert.Throws<InvalidDataException>(() => service.Create(request));   
            Assert.True(ex.Message.Equals("No date entered"));                      
                                                                                            
        }  
        
        [Fact]
        public void TestReadAll()
        {
            var repo = new Mock<IRepo<Request>>();
            var log = new Mock<ILogService>();
            var service = new RequestService(repo.Object, log.Object);
            service.ReadAll();
            repo.Verify(l => l.ReadAll(), Times.Once);
            
        }
        
        
        [Fact]
        public void TestReadByUser()
        {
            var repo = new Mock<IRepo<Request>>();
            var log = new Mock<ILogService>();
            var service = new RequestService(repo.Object, log.Object);
            service.ReadByUser(ToString());
            repo.Verify(l => l.ReadAll(), Times.Once);
            
        }
        
        
        [Fact]
        public void TestGetById()
        {
            var repo = new Mock<IRepo<Request>>();
            var log = new Mock<ILogService>();
            var service = new RequestService(repo.Object, log.Object);

            int id = 1;
            
            service.GetById(id);
            
            repo.Verify(l => l.Get(id), Times.Once);
        }
        
        [Fact]
        public void TestDelete()
        {
            var repo = new Mock<IRepo<Request>>();
            var log = new Mock<ILogService>();
            var requests = GetRequests();
            int id = 1;
            var request = requests.FirstOrDefault(r => r.Id == id) ;
            repo.Setup(f => f.Get(id)).Returns(request);
            
            var service = new RequestService(repo.Object, log.Object);
            service.Delete(id);
            repo.Verify(f => f.Get(id), Times.Once);
            repo.Verify(x => x.Delete(request), Times.Once);
        }
        
    }
}