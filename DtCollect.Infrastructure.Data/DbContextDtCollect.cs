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
            
        }

        public DbSet<User> Users { get; set; }
    }
}