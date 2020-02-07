using System;
using System.Collections.Generic;
using System.Text;
using UnitsNet;
using WPILib.SmartDashboard;

namespace WPILib.Counters
{
    public class Tachometer : IDisposable, ISendable
    {
        private readonly IDigitalSource m_source;
        private readonly int m_handle;
        private readonly int m_index;

        public Tachometer(IDigitalSource source)
        {
            m_source = source;
            m_handle = Hal.Counter.Initialize(Hal.CounterMode.kTwoPulse, out m_index);

            Hal.Counter.SetUpSource(m_handle, m_source.PortHandleForRouting, m_source.AnalogTriggerTypeForRouting);
            Hal.Counter.SetUpSourceEdge(m_handle, true, false);
        }

        public TimeSpan Period => TimeSpan.FromSeconds(Hal.Counter.GetPeriod(m_handle));

        public Frequency Frequency => Frequency.FromHertz(1 / Hal.Counter.GetPeriod(m_handle));

        public int EdgesPerRevolution { get; set; } = 1;

        public RotationalSpeed RotationalSpeed
        {
            get
            {
                var period = Hal.Counter.GetPeriod(m_handle);
                if (period == 0)
                {
                    return RotationalSpeed.MaxValue;
                }
                return RotationalSpeed.FromRevolutionsPerSecond((1.0 / EdgesPerRevolution) / period);
            }
        }

        public bool Stopped => Hal.Counter.GetStopped(m_handle);
        
        public int SamplesToAverage
        {
            get => Hal.Counter.GetSamplesToAverage(m_handle);
            set => Hal.Counter.SetSamplesToAverage(m_handle, value);
        }

        public TimeSpan MaxPeriod
        {
            set => Hal.Counter.SetMaxPeriod(m_handle, value.TotalSeconds);
        }

        public bool UpdateWhenEmpty
        {
            set => Hal.Counter.SetUpdateWhenEmpty(m_handle, value);
        }

        public void Dispose()
        {
            Hal.Counter.Free(m_handle);
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
        }
    }
}
