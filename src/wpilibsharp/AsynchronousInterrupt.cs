using System;
using System.Threading;

namespace WPILib
{
    public sealed class AsynchronousInterrupt : IDisposable
    {
        private readonly SynchronousInterrupt m_synchronousInterrupt;

        private readonly Action<bool, bool> m_callback;

        private Thread? m_interruptThread;
        private CancellationTokenSource m_threadRunSource = new CancellationTokenSource();

        public AsynchronousInterrupt(IDigitalSource source, Action<bool, bool> callback)
        {
            m_synchronousInterrupt = new SynchronousInterrupt(source);
            m_callback = callback;
        }

        private void ThreadMain()
        {
            while (!m_threadRunSource.IsCancellationRequested)
            {
                var result = m_synchronousInterrupt.WaitForInterrupt(TimeSpan.FromSeconds(1));
                if (m_threadRunSource.IsCancellationRequested)
                {
                    break;
                }
                if (result == 0)
                {
                    continue;
                }
                m_callback((result & EdgeConfiguration.kRisingEdge) != 0,
                           (result & EdgeConfiguration.kFallingEdge) != 0);
            }

        }

        public void Enable()
        {
            if (m_interruptThread != null) return;

            m_threadRunSource = new CancellationTokenSource();
            m_interruptThread = new Thread(ThreadMain)
            {
                Name = $"Interrupt Thread",
                IsBackground = true
            };
            m_interruptThread.Start();
        }

        public void Disable()
        {
            m_threadRunSource.Cancel();
            m_synchronousInterrupt.WakeupWaitingInterrupt();
            m_interruptThread?.Join();
            m_interruptThread = null;
        }

        public void SetInterruptEdges(bool risingEdge, bool fallingEdge)
        {
            m_synchronousInterrupt.SetInterruptEdges(risingEdge, fallingEdge);
        }

        public TimeSpan RisingTimestamp => m_synchronousInterrupt.RisingTimestamp;

        public TimeSpan FallingTimestamp => m_synchronousInterrupt.FallingTimestamp;

        public void Dispose()
        {
            Disable();
            m_synchronousInterrupt.Dispose();
            m_threadRunSource.Dispose();
        }
    }
}
