using System;
using System.Collections.Generic;
using System.Linq;
using DtCollect.Core.Entity;
using MockHistorian;

namespace DtCollect.Core.Service.Impl
{
    public class SampleService: ISampleService
    {
        readonly IHistorian _historian;

        public SampleService(IHistorian historian)
        {
            _historian = historian;
        }
        public List<HistorianSample> Get(Request request)
        {
            SampleType.TryParse(request.SampleType, out SampleType typeSample);
            
            return _historian.GetSamples(request.From, request.To, request.Interval , typeSample).ToList();
        }
    }
}