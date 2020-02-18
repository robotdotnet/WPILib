using System;
using UnitsNet;
using WPILib.SmartDashboardNS;

namespace WPILib.Counters
{
    public sealed class Tachometer : IDisposable, ISendable
    {
        private readonly IDigitalSource m_source;
        private readonly int m_handle;
        private readonly int m_index;

        public Tachometer(IDigitalSource source)
        {
            m_source = source ?? throw new ArgumentNullException(nameof(source));
            m_handle = Hal.CounterLowLevel.Initialize(Hal.CounterMode.kTwoPulse, out m_index);

            Hal.CounterLowLevel.SetUpSource(m_handle, m_source.PortHandleForRouting, m_source.AnalogTriggerTypeForRouting);
            Hal.CounterLowLevel.SetUpSourceEdge(m_handle, true, false);
        }

        public TimeSpan Period => TimeSpan.FromSeconds(Hal.CounterLowLevel.GetPeriod(m_handle));

        public Frequency Frequency => Frequency.FromHertz(1 / Hal.CounterLowLevel.GetPeriod(m_handle));

        public int EdgesPerRevolution { get; set; } = 1;

        public RotationalSpeed RotationalSpeed
        {
            get
            {
                var period = Hal.CounterLowLevel.GetPeriod(m_handle);
                if (period == 0)
                {
                    return RotationalSpeed.MaxValue;
                }
                return RotationalSpeed.FromRevolutionsPerSecond((1.0 / EdgesPerRevolution) / period);
            }
        }

        public bool Stopped => Hal.CounterLowLevel.GetStopped(m_handle);

        public int SamplesToAverage
        {
            get => Hal.CounterLowLevel.GetSamplesToAverage(m_handle);
            set => Hal.CounterLowLevel.SetSamplesToAverage(m_handle, value);
        }

        public TimeSpan MaxPeriod
        {
            set => Hal.CounterLowLevel.SetMaxPeriod(m_handle, value.TotalSeconds);
        }

        public bool UpdateWhenEmpty
        {
            set => Hal.CounterLowLevel.SetUpdateWhenEmpty(m_handle, value);
        }

        public void Dispose()
        {
            Hal.CounterLowLevel.Free(m_handle);
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
        }
    }
}
