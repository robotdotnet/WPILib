using System;
using WPILib.SmartDashboard;

namespace WPILib.Counters
{
    public class UpDownCounter : ISendable, IDisposable
    {

        private IDigitalSource? m_upSource;
        private IDigitalSource? m_downSource;
        private readonly int m_handle;
        private readonly int m_index;

        public UpDownCounter()
        {
            m_handle = Hal.CounterLowLevel.Initialize(Hal.CounterMode.kTwoPulse, out m_index);
            Hal.CounterLowLevel.Reset(m_handle);
        }

        public IDigitalSource? UpSource
        {
            set
            {
                if (value == null)
                {
                    Hal.CounterLowLevel.ClearUpSource(m_handle);
                    m_upSource = null;
                }
                else
                {
                    m_upSource = value;
                    Hal.CounterLowLevel.SetUpSource(m_handle, m_upSource.PortHandleForRouting, m_upSource.AnalogTriggerTypeForRouting);
                    Hal.CounterLowLevel.SetUpSourceEdge(m_handle, true, false);
                }
            }
        }

        public IDigitalSource? DownSource
        {
            set
            {
                if (value == null)
                {
                    Hal.CounterLowLevel.ClearDownSource(m_handle);
                    m_downSource = null;
                }
                else
                {
                    m_downSource = value;
                    Hal.CounterLowLevel.SetDownSource(m_handle, m_downSource.PortHandleForRouting, m_downSource.AnalogTriggerTypeForRouting);
                    Hal.CounterLowLevel.SetDownSourceEdge(m_handle, true, false);
                }
            }
        }

        public EdgeConfiguration UpEdgeConfiguration
        {
            set
            {
                Hal.CounterLowLevel.SetUpSourceEdge(m_handle, (value & EdgeConfiguration.kRisingEdge) != 0, (value & EdgeConfiguration.kFallingEdge) != 0);
            }
        }

        public EdgeConfiguration DownEdgeConfiguration
        {
            set
            {
                Hal.CounterLowLevel.SetDownSourceEdge(m_handle, (value & EdgeConfiguration.kRisingEdge) != 0, (value & EdgeConfiguration.kFallingEdge) != 0);
            }
        }


        public void Reset()
        {
            Hal.CounterLowLevel.Reset(m_handle);
        }

        public bool ReverseDirection
        {
            set
            {
                Hal.CounterLowLevel.SetReverseDirection(m_handle, value);
            }
        }

        public int Count => Hal.CounterLowLevel.Get(m_handle);

        public void Dispose()
        {
            Hal.CounterLowLevel.Free(m_handle);
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
        }
    }
}
