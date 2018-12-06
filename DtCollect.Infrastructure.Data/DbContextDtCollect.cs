using DtCollect.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace DtCollect.Infrastructure.Data
{
    public class DbContextDtCollect : DbContext
    {
        public DbContextDtCollect(DbContextOptions<DbContextDtCollect> opt) : base(opt)
        {    }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Log>()
                .HasOne(l => l.User)
                .WithMany(u => u.Logs);
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Log> Logs { get; set; }
    }
}