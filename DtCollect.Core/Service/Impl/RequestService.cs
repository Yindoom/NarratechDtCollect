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
        private ILogService _log;

        public RequestService(IRepo<Request> repo, ILogService log)
        {
            _repo = repo;
            _log = log;
        }
        
        public Request Create(Request Created)
        {
            if (Created.From > Created.To)
            {
                var log1 = new Log()            
                {                              
                    request = Created,         
                    Action = "Data Request",   
                    Date = DateTime.Now,       
                    Success = false             
                };                             
                _log.Create(log1);              
                throw new InvalidDataException("From must be earlier than To");
            }

            if ((Created.From == DateTimeOffset.MinValue) || Created.To == DateTimeOffset.MinValue)
            {
                var log2 = new Log()            
                {                              
                    request = Created,         
                    Action = "Data Request",   
                    Date = DateTime.Now,       
                    Success = false           
                };                             
                _log.Create(log2);              
                throw new InvalidDataException("No date entered");
                
            }

            var log = new Log()
            {
                request = Created,
                Action = "Data Request",
                Date = DateTime.Now,
                Success = true
            };
            _log.Create(log);
            
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

        public Request GetById(int id)
        {
            return _repo.Get(id);
        }

        public Request Delete(int id)
        {
            var request = _repo.Get(id);
            
            var log = new Log()            
            {    
                request = request,
                Action = "Deletion",   
                Date = DateTime.Now,       
                Success = true             
            };                             
            _log.Create(log);              
            return _repo.Delete(request);
        }
    }
}