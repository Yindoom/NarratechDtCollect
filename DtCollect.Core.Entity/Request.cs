using System;
using System.Collections.Generic;

namespace DtCollect.Core.Entity
{
    public class Request
    {
        public string TagName { get; set; }
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To{ get; set; }
        public TimeSpan Interval { get; set; }
        public string SampleType { get; set; }
        public int Id { get; set; }
        public User User { get; set; }
        public List<Log> Logs { get; set; }
        
    }
}