using System;
using System.Collections.Generic;

namespace MockHistorian
{
    //@NARRATECH
    public class MockIp21 : IHistorian
    {
        public void Dispose()
        {
        }

        //We did not write this code, but it makes a new IEnumerable of samples
        public IEnumerable<HistorianSample> GetSamples(DateTimeOffset from, DateTimeOffset to, TimeSpan interval, SampleType sampleType)
        {
            var random = new Random();

            var requestLength = to - from;

            var sampleCount = (int)(requestLength.TotalSeconds / interval.TotalSeconds);

            var samples = new HistorianSample[sampleCount];

            for (var currentSample = 0; currentSample < sampleCount; currentSample++)
            {
                var currentSampleTime = from.AddMilliseconds(interval.TotalMilliseconds * currentSample);
                var value = random.NextDouble();

                var quality = HistorianSample.SampleQuality.Good;

                switch (random.Next(0, 1000))
                {
                    case int i when i == 0:
                    quality = HistorianSample.SampleQuality.Bad;
                    break;
                    case int i when i < 10:
                    quality = HistorianSample.SampleQuality.Suspect;
                    break;
                }

                var sample = new HistorianSample(currentSampleTime, quality, value);
                samples[currentSample] = sample;
            }

            return samples;
        }
    }
}