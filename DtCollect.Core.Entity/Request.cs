using System;

namespace DtCollect.Core.Entity
{
    public class Request
    {
        public string TagName { get; set; }
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To{ get; set; }
        public TimeSpan Interval { get; set; }
        public string SampleType { get; set; }
    }
}