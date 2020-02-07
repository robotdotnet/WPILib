using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WPILib
{
    public class SynchronousInterrupt : IDisposable
    {
        [Flags]
        public enum InterruptType
        {
            kNone = 0x0,
            kRisingEdge = 0x1,
            kFallingEdge = 0x10,
            kBoth = kRisingEdge | kFallingEdge
        }

        private readonly DigitalSource m_source;

        private readonly int m_interruptHandle;


        public SynchronousInterrupt(DigitalSource source)
        {
            m_source = source;
            m_interruptHandle = Hal.Interrupts.Initialize(false);
            Hal.Interrupts.Request(m_interruptHandle, m_source.PortHandleForRouting, m_source.AnalogTriggerTypeForRouting);
        }

        public InterruptType WaitForInterrupt(bool ignorePrevious = true)
        {
            var result = Hal.Interrupts.WaitForInterrupt(m_interruptHandle, 0, ignorePrevious);
            var rising = ((result & 0xFF) != 0) ? InterruptType.kRisingEdge : InterruptType.kNone;
            var falling = ((result & 0xFF00) != 0) ? InterruptType.kFallingEdge : InterruptType.kNone;
            return rising | falling;
        }

        public InterruptType WaitForInterrupt(TimeSpan timeout, bool ignorePrevious = true)
        {
            if (timeout == TimeSpan.Zero)
            {
                return WaitForInterrupt(ignorePrevious);
            }
            var result = Hal.Interrupts.WaitForInterrupt(m_interruptHandle, timeout.TotalSeconds, ignorePrevious);

            var rising = ((result & 0xFF) != 0) ? InterruptType.kRisingEdge : InterruptType.kNone;
            var falling = ((result & 0xFF00) != 0) ? InterruptType.kFallingEdge : InterruptType.kNone;
            return rising | falling;
        }

        public void SetInterruptEdges(bool risingEdge, bool fallingEdge)
        {
            Hal.Interrupts.SetInterruptUpSourceEdge(m_interruptHandle, risingEdge, fallingEdge);
        }

        public TimeSpan RisingTimestamp => TimeSpan.FromTicks((long)(Hal.Interrupts.ReadInterruptRisingTimestamp(m_interruptHandle) * Timer.TicksPerMicrosecond));

        public TimeSpan FallingTimestamp => TimeSpan.FromTicks((long)(Hal.Interrupts.ReadInterruptFallingTimestamp(m_interruptHandle) * Timer.TicksPerMicrosecond));

        public void WakeupWaitingInterrupt()
        {

        }

        public unsafe void Dispose()
        {
            // Clean will wake up any waiting interrupts
            Hal.Interrupts.Clean(m_interruptHandle);
        }
    }
}
