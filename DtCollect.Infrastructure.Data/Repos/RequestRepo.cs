using System.Collections.Generic;
using System.Linq;
using DtCollect.Core.Domain;
using DtCollect.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace DtCollect.Infrastructure.Data.Repos
{
    public class RequestRepo : IRepo<Request>
    {
        
        private readonly DbContextDtCollect _ctx;

        public RequestRepo(DbContextDtCollect ctx)
        {
            _ctx = ctx;
        }
        
        public IEnumerable<Request> ReadAll()
        {
            return _ctx.Requests.Include(r => r.User).Include(r => r.Logs);
        }

        public Request Get(int id)
        {
            return _ctx.Requests.FirstOrDefault(r => r.Id == id);
        }

        public Request Create(Request created)
        {
            var add = _ctx.Requests.Add(created).Entity;
            _ctx.SaveChanges();
            return add;
        }

        public Request Update(Request update)
        {
            throw new System.NotImplementedException();
        }

        public Request Delete(Request delete)
        {
            var del = _ctx.Remove(delete).Entity;
            _ctx.SaveChanges();
            return del;
        }
    }
}