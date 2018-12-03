using System;
using DtCollect.Core.Entity;
using DtCollect.Core.Service.Impl;
using MockHistorian;
using Moq;
using Xunit;

namespace TestUserService
{
    public class TestSampleService
    {
        [Fact]
        public void TestGetSamples()
        {
            var repo = new Mock<IHistorian>();
            var req = new Request()
            {
                From = new DateTimeOffset(DateTime.UtcNow),
                To = new DateTimeOffset(DateTime.UtcNow.Add(TimeSpan.FromDays(5))),
                Interval = TimeSpan.FromHours(5),
                SampleType = "Average",
                TagName = "Henlo"
            };

            var service = new SampleService(repo.Object);

            service.Get(req);
            
            repo.Verify(m => m.GetSamples(req.From, req.To, req.Interval, SampleType.Average), Times.Once);
        }

        [Fact]
        public void TestWrongGetSamples()
        {
            var repo = new Mock<IHistorian>();
            var req = new Request()
            {
                From = new DateTimeOffset(DateTime.UtcNow),
                To = new DateTimeOffset(DateTime.UtcNow.Add(TimeSpan.FromDays(5))),
                Interval = TimeSpan.FromHours(5),
                SampleType = "Average",
                TagName = "Henlo"
            };

            var service = new SampleService(repo.Object);

            service.Get(req);
            
            repo.Verify(m => m.GetSamples(req.From, req.To, req.Interval, SampleType.Point), Times.Never);
        }
    }
}