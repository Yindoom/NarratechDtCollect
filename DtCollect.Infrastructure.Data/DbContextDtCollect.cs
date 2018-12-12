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
                .HasOne(l => l.request)
                .WithMany(u => u.log);

            model.Entity<Request>()
                .HasOne(u => u.User)
                .WithMany(r => r.requests);
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Log> Logs { get; set; }
        
        public DbSet<Request> Requests { get; set; }
    }
}