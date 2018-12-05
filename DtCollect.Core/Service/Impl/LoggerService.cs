using System;
using System.Collections.Generic;
using System.Linq;
using DtCollect.Core.Domain;
using DtCollect.Core.Entity;

namespace DtCollect.Core.Service.Impl
{
    public class LoggerService : ILoggerService
    {
        private readonly IRepo<Log> _repo;

        public LoggerService(IRepo<Log> repo)
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

        public Log ReadByUser(Log log)
        {
            return _repo.ReadAll().FirstOrDefault(l => l.User == log.User);
        }

        public Log ReadbySuccess(bool success)
        {   
            return _repo.ReadAll().FirstOrDefault(l => l.Success == success );
        }
    }
}