using System;
using System.Collections.Generic;
using System.Text;
using WPILib.SmartDashboard;

namespace WPILib.Counters
{
    public class ExternalDirectionCounter : ISendable, IDisposable
    {
        private IDigitalSource m_countSource;
        private IDigitalSource m_directionSource;
        private readonly int m_handle;
        private readonly int m_index;

        public ExternalDirectionCounter(IDigitalSource countSource, IDigitalSource directionSource)
        {
            m_countSource = countSource;
            m_directionSource = directionSource;
            m_handle = Hal.Counter.Initialize(Hal.CounterMode.kExternalDirection, out m_index);

            Hal.Counter.SetUpSource(m_handle, m_countSource.PortHandleForRouting, m_countSource.AnalogTriggerTypeForRouting);
            Hal.Counter.SetUpSourceEdge(m_handle, true, false);
            Hal.Counter.SetDownSource(m_handle, m_directionSource.PortHandleForRouting, m_directionSource.AnalogTriggerTypeForRouting);
            Hal.Counter.SetDownSourceEdge(m_handle, false, true);
            Hal.Counter.Reset(m_handle);
        }

        public IDigitalSource CountSource
        {
            set
            {
                    m_countSource = value;
                    Hal.Counter.SetUpSource(m_handle, m_countSource.PortHandleForRouting, m_countSource.AnalogTriggerTypeForRouting);
                    Hal.Counter.SetUpSourceEdge(m_handle, true, false);
            }
        }

        public EdgeConfiguration EdgeConfiguration
        {
            set
            {
                Hal.Counter.SetUpSourceEdge(m_handle, (value & EdgeConfiguration.kRisingEdge) != 0, (value & EdgeConfiguration.kFallingEdge) != 0);
            }
        }

        public IDigitalSource DirectionSource
        {
            set
            {

                m_directionSource = value;
                Hal.Counter.SetDownSource(m_handle, m_directionSource.PortHandleForRouting, m_directionSource.AnalogTriggerTypeForRouting);
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
