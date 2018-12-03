using System;
using System.Collections.Generic;

namespace MockHistorian
{
    //@NARRATECH
    public enum SampleType {
        Average,
        Point,
        Minimum,
        Maximum
    }

    public interface IHistorian : IDisposable
    {
        IEnumerable<HistorianSample> GetSamples(DateTimeOffset from, DateTimeOffset to, TimeSpan interval, SampleType sampleType);
    }
}
