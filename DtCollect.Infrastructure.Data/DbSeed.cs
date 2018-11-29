using DtCollect.Core.Entity;

namespace DtCollect.Infrastructure.Data
{
    public class DbSeed : IDbSeed
    {
        public void SeedDb(DbContextDtCollect ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            ctx.Users.Add(new User()
            {
                Id = 1,
                Username = "Admin",
                IsAdmin = true
            });
            ctx.Users.Add(new User()
            {
                Id = 2,
                Username = "User1",
                IsAdmin = false
            });
            ctx.Users.Add(new User()
            {
                Id = 3,
                Username = "User2",
                IsAdmin = false
            });

            ctx.SaveChanges();
        }
    }
}