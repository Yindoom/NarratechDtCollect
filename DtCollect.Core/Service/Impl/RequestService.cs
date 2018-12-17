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
        // it checks for two if conditions. if from date is earlier than to date and if date is set to the min/default value.
        // in which case we log that the request failed because of wrong input. 
        //It will try to create the request if there are no exceptions throwns. and this will then be logged as a successful data request. 
        
        public Request Create(Request Created)
        {
            Created.Logs = new List<Log>();
            if (Created.From > Created.To)
            {
                 Created.Logs.Add(_log.Create(new Log()
                 {
                     Date = DateTime.Now,
                     Message = "From cannot be later than To",
                     Success = false
                 }));             
                throw new InvalidDataException("From must be earlier than To");
            }

            if ((Created.From == DateTimeOffset.MinValue) || Created.To == DateTimeOffset.MinValue)
            {
                var log2 = new Log()            
                {                                       
                    Message = "Date input missing",   
                    Date = DateTime.Now,       
                    Success = false           
                };                             
                Created.Logs.Add(log2);
                throw new InvalidDataException("No date entered");
                
            }

            var log = new Log()
            {
                Message = "Date request success",
                Date = DateTime.Now,
                Success = true
            };
            Created.Logs.Add(log);
            
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
        // everytime a request is deleted, we log the deletion of the request. 
        public Request Delete(int id)
        {
            var request = _repo.Get(id);
            
            var log = new Log()            
            {    
                request = request,
                Message = "Deletion",   
                Date = DateTime.Now,       
                Success = true             
            };                             
            _log.Create(log);              
            return _repo.Delete(request);
        }
    }
}