using System.Collections.Generic;
using DtCollect.Core.Entity;

namespace DtCollect.Core.Service
{
    public interface ILogService
    {
        Log Create(Log Created);
        List<Log> ReadAll();
        List<Log> ReadByUser(string user);
        List<Log> ReadbySuccess(bool success);
    }
}