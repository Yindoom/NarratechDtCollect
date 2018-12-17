using System;
using System.Collections.Generic;

namespace DtCollect.Core.Entity
{
    public class Log
    {
        

        public int Id { get; set; }
        public Request request { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}