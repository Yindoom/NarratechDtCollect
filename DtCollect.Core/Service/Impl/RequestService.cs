using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DtCollect.Core.Domain;
using DtCollect.Core.Entity;

namespace DtCollect.Core.Service.Impl
{
    public class RequestService : IRequestService
    {
        private readonly IRepo<Request> _repo;

        public RequestService(IRepo<Request> repo)
        {
            _repo = repo;
        }
        
        public Request Create(Request Created)
        {
            if (Created.From > Created.To)
            {
                throw new InvalidDataException("From must be earlier than To");
            }

            if ((Created.From == DateTimeOffset.MinValue) || Created.To == DateTimeOffset.MinValue)
            {
                throw new InvalidDataException("No date entered");
            }
            return _repo.Create(Created);
        }

        public List<Request> ReadAll()
        {
            return _repo.ReadAll().ToList();
        }

        public List<Request> ReadByUser(String user)
        {
            return _repo.ReadAll().Where(r => r.User.Username == user).ToList();
        }

        public Request ReadById(int id)
        {
            return _repo.Get(id);
        }
    }
}