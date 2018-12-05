using System.Collections.Generic;
using System.Linq;
using DtCollect.Core.Domain;
using DtCollect.Core.Entity;

namespace DtCollect.Infrastructure.Data.Repos
{
    public class LogRepo : IRepo<Log>
    {
        private readonly DbContextDtCollect _ctx;

        public LogRepo(DbContextDtCollect ctx)
        {
            _ctx = ctx;
        }
        
        
        public IEnumerable<Log> ReadAll()
        {
            return _ctx.Logs;
        }

        public Log Get(int id)
        {
            return _ctx.Logs.FirstOrDefault(l => l.Id == id);
        }

        public Log Create(Log created)
        {
            var add = _ctx.Logs.Add(created).Entity;
            _ctx.SaveChanges();
            return add;
        }

        public Log Update(Log update)
        {
            throw new System.NotImplementedException();
        }

        public Log Delete(Log delete)
        {
            throw new System.NotImplementedException();
        }
    }
}