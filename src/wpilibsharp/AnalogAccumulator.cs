using System;
using System.Threading;

namespace WPILib
{
    public class AnalogAccumulator
    {
        private readonly AnalogInput m_analogInput;
        private readonly int m_port;
        private long m_accumulatorOffset;

        public readonly struct AccumulatorOutput
        {
            public long Count { get; }
            public long Value { get; }

            public AccumulatorOutput(long count, long value)
            {
                Count = count;
                Value = value;
            }
        }

        public AnalogAccumulator(AnalogInput analogInput)
        {
            m_analogInput = analogInput;
            if (!analogInput.IsAccumulatorChannel)
            {
                throw new ArgumentException();
            }
            m_port = analogInput.m_port;

            m_accumulatorOffset = 0;
            Hal.AnalogAccumulatorLowLevel.InitAccumulator(m_port);
        }

        public long InitialValue
        {
            set => m_accumulatorOffset = value;
        }

        public void Reset()
        {
            Hal.AnalogAccumulatorLowLevel.ResetAccumulator(m_port);

            // Wait until the next sample, so the next call to getAccumulator*()
            // won't have old values.
            double sampleTime = 1.0 / AnalogInput.GlobalSampleRate;
            double overSamples = 1 << m_analogInput.OversampleBits;
            double averageSamples = 1 << m_analogInput.AverageBits;
            Thread.Sleep(TimeSpan.FromSeconds(sampleTime * overSamples * averageSamples));
        }

        public int Center
        {
            set => Hal.AnalogAccumulatorLowLevel.SetAccumulatorCenter(m_port, value);
        }

        public int Deadband
        {
            set => Hal.AnalogAccumulatorLowLevel.SetAccumulatorDeadband(m_port, value);
        }

        public long Value => Hal.AnalogAccumulatorLowLevel.GetAccumulatorValue(m_port);

        public long Count => Hal.AnalogAccumulatorLowLevel.GetAccumulatorCount(m_port);

        public AccumulatorOutput Output
        {
            get
            {
                Hal.AnalogAccumulatorLowLevel.GetAccumulatorOutput(m_port, out var value, out var count);
                return new AccumulatorOutput(count, value + m_accumulatorOffset);
            }
        }
    }
}
