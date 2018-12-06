using System.Collections.Generic;

namespace DtCollect.Core.Entity
{
    public class User
    {
        public int Id { get; set; }
        
        public string Username { get; set; }
        
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        
        public bool IsAdmin { get; set; }
        
        public List<Log> Logs { get; set; }
    }
}