using System;

namespace WPILib
{
    public sealed class SynchronousInterrupt : IDisposable
    {
        private readonly IDigitalSource m_source;

        private readonly int m_interruptHandle;


        public SynchronousInterrupt(IDigitalSource source)
        {
            m_source = source ?? throw new ArgumentNullException(nameof(source));
            m_interruptHandle = Hal.InterruptsLowLevel.Initialize(true);
            Hal.InterruptsLowLevel.Request(m_interruptHandle, m_source.PortHandleForRouting, m_source.AnalogTriggerTypeForRouting);
        }

        public EdgeConfiguration WaitForInterrupt(TimeSpan timeout, bool ignorePrevious = true)
        {
            var result = Hal.InterruptsLowLevel.WaitForInterrupt(m_interruptHandle, timeout.TotalSeconds, ignorePrevious);

            var rising = ((result & 0xFF) != 0) ? EdgeConfiguration.kRisingEdge : EdgeConfiguration.kNone;
            var falling = ((result & 0xFF00) != 0) ? EdgeConfiguration.kFallingEdge : EdgeConfiguration.kNone;
            return rising | falling;
        }

        public void SetInterruptEdges(bool risingEdge, bool fallingEdge)
        {
            Hal.InterruptsLowLevel.SetInterruptUpSourceEdge(m_interruptHandle, risingEdge, fallingEdge);
        }

        public TimeSpan RisingTimestamp => TimeSpan.FromTicks((long)(Hal.InterruptsLowLevel.ReadInterruptRisingTimestamp(m_interruptHandle) * Timer.TicksPerMicrosecond));

        public TimeSpan FallingTimestamp => TimeSpan.FromTicks((long)(Hal.InterruptsLowLevel.ReadInterruptFallingTimestamp(m_interruptHandle) * Timer.TicksPerMicrosecond));

        public void WakeupWaitingInterrupt()
        {
            Hal.InterruptsLowLevel.ReleaseWaitingInterrupt(m_interruptHandle);
        }

        public unsafe void Dispose()
        {
            // Clean will wake up any waiting interrupts
            Hal.InterruptsLowLevel.Clean(m_interruptHandle);
        }
    }
}
