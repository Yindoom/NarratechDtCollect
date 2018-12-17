using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DtCollect.Core.Domain;
using DtCollect.Core.Entity;

namespace DtCollect.Infrastructure.Data.Repos
{
    public class UserRepo: IRepo<User>
    {
        private readonly DbContextDtCollect _ctx;

        public UserRepo(DbContextDtCollect ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<User> ReadAll()
        {
            return _ctx.Users;
        }



        public User Get(int id)
        {
            return _ctx.Users.FirstOrDefault(u => u.Id == id);
        }

        public User Create(User created)
        {
            var add = _ctx.Users.Add(created).Entity;
            _ctx.SaveChanges();
            return add;
        }

        public User Update(User update)
        {
            var upd = _ctx.Users.Update(update).Entity;
            _ctx.SaveChanges();
            return upd;
        }

        public User Delete(User deleted)
        {
            var del = _ctx.Remove(deleted).Entity;
            _ctx.SaveChanges();
            return del;
        }
    }
}