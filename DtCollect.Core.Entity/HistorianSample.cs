using System;

namespace MockHistorian
{
    //@NARRATECH
    public class HistorianSample
    {
        public enum SampleQuality {
            Good = 0,
            Suspect = 1,
            Bad = 2
        }

        public HistorianSample(DateTimeOffset time, SampleQuality quality, double value)
        {
            Time = time;
            Quality = quality;
            Value = value;
        }

        public DateTimeOffset Time { get; }

        public SampleQuality Quality { get; }

        public double Value { get; }
    }
}