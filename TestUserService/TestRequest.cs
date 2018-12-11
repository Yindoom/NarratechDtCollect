using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DtCollect.Core.Domain;
using DtCollect.Core.Entity;
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
            var Request = GetRequests().FirstOrDefault();
            var repo = new Mock<IRepo<Request>>();

            var service = new RequestService(repo.Object);
            
            service.Create(Request);
            repo.Verify(x => x.Create(Request), Times.Once);
        }

        [Fact]
        public void TestDateTimeOffsetException()
        {
            var request = GetRequests().FirstOrDefault();      
            var repo = new Mock<IRepo<Request>>();             
                                                    
            var service = new RequestService(repo.Object);     
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
                                                                                            
            var service = new RequestService(repo.Object);                                       
            request.From = DateTimeOffset.MinValue;                             
            request.To = DateTimeOffset.Parse("2018/12/31 00:00");                               
            Exception ex = Assert.Throws<InvalidDataException>(() => service.Create(request));   
            Assert.True(ex.Message.Equals("No date entered"));                      
                                                                                            
        }                                                                                        
    }
}