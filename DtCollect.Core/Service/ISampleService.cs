using System.Collections.Generic;
using DtCollect.Core.Entity;
using MockHistorian;

namespace DtCollect.Core.Service
{
    public interface ISampleService
    {
        List<HistorianSample> Get(Request request);
    }
}