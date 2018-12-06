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
        Log ReadByUser(string user);
        Log ReadbySuccess(bool success);
    }
}