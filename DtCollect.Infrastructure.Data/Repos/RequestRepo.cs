using System.Collections.Generic;
using DtCollect.Core.Domain;
using DtCollect.Core.Entity;

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
            throw new System.NotImplementedException();
        }

        public Request Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Request Create(Request created)
        {
            throw new System.NotImplementedException();
        }

        public Request Update(Request update)
        {
            throw new System.NotImplementedException();
        }

        public Request Delete(Request delete)
        {
            throw new System.NotImplementedException();
        }
    }
}