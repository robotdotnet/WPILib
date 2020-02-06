using System;
using Hal;

namespace WPILib
{



    public abstract class InterruptibleSensorBase : IDisposable
    {

        [Flags]
        public enum WaitResult
        {
            Timeout = 0x0,
            RisingEdge = 0x1,
            FallingEdge = 0x101,
            Both = RisingEdge | FallingEdge
        }

        protected int m_interrupt = 0;
        protected bool m_isSynchronousInterrupt;


        public void Dispose()
        {
            if (m_interrupt != 0)
            {
                CancelInterrupts();
            }
        }

        public abstract AnalogTriggerType AnalogTriggerTypeForRouting { get; }
        public abstract int PortHandleForRouting { get; }

        public void RequestInterrupts(Action<WaitResult> handler)
        {
            // if (m_interrupt != 0) {
            //     throw new ArgumentException("The interrupt has already been allocated");
            // }

            // AllocateInterrupts(false);

            // if (m_interrupt != 0) {
            //     throw new InvalidOperationException("Interrupt shouldn't be unset here");
            // }

            // Hal.Interrupts.Request(m_interrupt, PortHandleForRouting, AnalogTriggerTypeForRouting);
            // SetUpSourceEdge(true, false);
            // Hal.Interrupts.AttachInterruptHandlerThreaded(m_interrupt, )
        }

        public void RequestInterrupts()
        {

        }

        protected void AllocateInterrupts()
        {

        }

        public void CancelInterrupts()
        {

        }

        public WaitResult WaitForInterrupt(TimeSpan timeout, bool ignorePrevious = true)
        {
            return WaitResult.Timeout;
        }

        public void EnableInterrupts()
        {

        }

        public void DisableInterrupts()
        {

        }

        public TimeSpan RisingTimestamp => TimeSpan.Zero;

        public TimeSpan FallingTimestamp => TimeSpan.Zero;

        public void SetInterruptEdges(bool risingEdge, bool fallingEdge)
        {

        }
    }
}
