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
        // convert the request sampletype string to a samepletype enum, which we use to generate the data for the request. 
        public List<HistorianSample> Get(Request request)
        {
            SampleType.TryParse(request.SampleType, out SampleType typeSample);

            return _historian.GetSamples(request.From, request.To, request.Interval, typeSample).ToList();
        }
    }
}
