using System;
using System.Collections.Generic;
using DtCollect.Core.Entity;

namespace DtCollect.Core.Service
{
    public interface IRequestService
    {
        Request Create(Request Created);
        List<Request> ReadAll();
        List<Request> ReadByUser(String user);
        Request GetById(int id);
        Request Delete(int id);

    }
}