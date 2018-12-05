using System;

namespace DtCollect.Core.Entity
{
    public class Log
    {
        public int Id { get; set; }
        public User User { get; set; }
        public bool Success { get; set; }
        public string Action { get; set; }
        public DateTime Date { get; set; }
    }
}