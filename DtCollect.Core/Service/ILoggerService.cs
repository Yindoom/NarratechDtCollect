using System;
using System.Collections.Generic;
using DtCollect.Core.Entity;
using DtCollect.Core.Service.Impl;

namespace DtCollect.Core.Service
{
    public interface ILoggerService
    {
        Log Create(Log Created);
        List<Log> ReadAll();
        List<Log> ReadByUser(string user);
        List<Log> ReadbySuccess(bool success);
    }
}