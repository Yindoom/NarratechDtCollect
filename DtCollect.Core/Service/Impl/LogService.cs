using System.Collections.Generic;
using System.Linq;
using DtCollect.Core.Domain;
using DtCollect.Core.Entity;

namespace DtCollect.Core.Service.Impl
{
    public class LogService : ILogService
    {
        private readonly IRepo<Log> _repo;

        public LogService(IRepo<Log> repo)
        {
            _repo = repo;

        }
        public Log Create(Log Created)
        {
            return _repo.Create(Created);
        }

        public List<Log> ReadAll()
        {
            return _repo.ReadAll().ToList();
        }

        public List<Log> ReadByUser(string user)
        {
            return _repo.ReadAll().Where(l => l.User.Username == user).ToList();
        }

        public List<Log> ReadbySuccess(bool success)
        {   
            return _repo.ReadAll().Where(l => l.Success == success).ToList();
        }
    }
}