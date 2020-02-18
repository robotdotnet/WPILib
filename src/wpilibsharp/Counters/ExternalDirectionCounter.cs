using System;
using WPILib.SmartDashboardNS;

namespace WPILib.Counters
{
    public sealed class ExternalDirectionCounter : ISendable, IDisposable
    {
        private IDigitalSource m_countSource;
        private IDigitalSource m_directionSource;
        private readonly int m_handle;
        private readonly int m_index;

        public ExternalDirectionCounter(IDigitalSource countSource, IDigitalSource directionSource)
        {
            m_countSource = countSource ?? throw new ArgumentNullException(nameof(countSource));
            m_directionSource = directionSource ?? throw new ArgumentNullException(nameof(directionSource));
            m_handle = Hal.CounterLowLevel.Initialize(Hal.CounterMode.kExternalDirection, out m_index);

            Hal.CounterLowLevel.SetUpSource(m_handle, m_countSource.PortHandleForRouting, m_countSource.AnalogTriggerTypeForRouting);
            Hal.CounterLowLevel.SetUpSourceEdge(m_handle, true, false);
            Hal.CounterLowLevel.SetDownSource(m_handle, m_directionSource.PortHandleForRouting, m_directionSource.AnalogTriggerTypeForRouting);
            Hal.CounterLowLevel.SetDownSourceEdge(m_handle, false, true);
            Hal.CounterLowLevel.Reset(m_handle);
        }

        public IDigitalSource CountSource
        {
            set
            {
                m_countSource = value ?? throw new ArgumentNullException(nameof(value));
                Hal.CounterLowLevel.SetUpSource(m_handle, m_countSource.PortHandleForRouting, m_countSource.AnalogTriggerTypeForRouting);
                Hal.CounterLowLevel.SetUpSourceEdge(m_handle, true, false);
            }
        }

        public EdgeConfiguration EdgeConfiguration
        {
            set
            {
                Hal.CounterLowLevel.SetUpSourceEdge(m_handle, (value & EdgeConfiguration.kRisingEdge) != 0, (value & EdgeConfiguration.kFallingEdge) != 0);
            }
        }

        public IDigitalSource DirectionSource
        {
            set
            {
                m_directionSource = value ?? throw new ArgumentNullException(nameof(value));
                Hal.CounterLowLevel.SetDownSource(m_handle, m_directionSource.PortHandleForRouting, m_directionSource.AnalogTriggerTypeForRouting);
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
