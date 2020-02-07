using System;
using System.Collections.Generic;
using System.Text;
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
            m_handle = Hal.Counter.Initialize(Hal.CounterMode.kTwoPulse, out m_index);
            Hal.Counter.Reset(m_handle);
        }

        public IDigitalSource? UpSource
        {
            set
            {
                if (value == null)
                {
                    Hal.Counter.ClearUpSource(m_handle);
                    m_upSource = null;
                }
                else
                {
                    m_upSource = value;
                    Hal.Counter.SetUpSource(m_handle, m_upSource.PortHandleForRouting, m_upSource.AnalogTriggerTypeForRouting);
                    Hal.Counter.SetUpSourceEdge(m_handle, true, false);
                }
            }
        }

        public IDigitalSource? DownSource
        {
            set
            {
                if (value == null)
                {
                    Hal.Counter.ClearDownSource(m_handle);
                    m_downSource = null;
                }
                else
                {
                    m_downSource = value;
                    Hal.Counter.SetDownSource(m_handle, m_downSource.PortHandleForRouting, m_downSource.AnalogTriggerTypeForRouting);
                    Hal.Counter.SetDownSourceEdge(m_handle, true, false);
                }
            }
        }

        public EdgeConfiguration UpEdgeConfiguration
        {
            set
            {
                Hal.Counter.SetUpSourceEdge(m_handle, (value & EdgeConfiguration.kRisingEdge) != 0, (value & EdgeConfiguration.kFallingEdge) != 0);
            }
        }

        public EdgeConfiguration DownEdgeConfiguration
        {
            set
            {
                Hal.Counter.SetDownSourceEdge(m_handle, (value & EdgeConfiguration.kRisingEdge) != 0, (value & EdgeConfiguration.kFallingEdge) != 0);
            }
        }


        public void Reset()
        {
            Hal.Counter.Reset(m_handle);
        }

        public bool ReverseDirection
        {
            set
            {
                Hal.Counter.SetReverseDirection(m_handle, value);
            }
        }

        public int Count => Hal.Counter.Get(m_handle);

        public void Dispose()
        {
            Hal.Counter.Free(m_handle);
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
        }
    }
}
