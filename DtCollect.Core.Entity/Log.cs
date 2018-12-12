using System;
using System.Collections.Generic;

namespace DtCollect.Core.Entity
{
    public class Log
    {
        

        public int Id { get; set; }
        public Request request { get; set; }
        public bool Success { get; set; }
        public string Action { get; set; }
        public DateTime Date { get; set; }
    }
}