using System;
using System.Threading;
using HAL.Simulator;

namespace HAL.SimulatorHAL
{
    internal class NotifierAlarm : IDisposable
    {
        private SpinLock m_lockObject = new SpinLock();

        private uint m_triggerTime = 0;

        private readonly Action<uint, IntPtr> m_callback = null;

        private Thread m_alarmThread;

        public NotifierAlarm(Action<uint, IntPtr> alarmCallback)
        {
            if (alarmCallback == null)
            {
                throw new ArgumentNullException(nameof(alarmCallback), "Alarm Callback cannot be null");
            }
            m_callback = alarmCallback;

            m_alarmThread = new Thread(Run);
            m_alarmThread.Name = "Notifier Alarm";
            m_alarmThread.Start();
        }

        private bool m_enabled = false;
        private bool m_continue = true;

        public void Run()
        {
            while (true)
            {
                while (!m_enabled)
                {
                    Thread.Yield();
                }

                if (!m_continue) return;

                uint triggerTime = m_triggerTime;
                while (triggerTime > SimHooks.GetFPGATime())
                {

                    bool gotLock = false;
                    try
                    {
                        m_lockObject.Enter(ref gotLock);
                        triggerTime = m_triggerTime;
                    }
                    finally
                    {
                        if (gotLock) m_lockObject.Exit();
                    }
                    if (!m_continue) return;
                }
                if (m_enabled)
                {
                    m_enabled = false;
                    m_callback?.Invoke((uint)SimHooks.GetFPGATime(), IntPtr.Zero);
                }

               
            }

        }

        public void EnableAlarm()
        {
            m_enabled = true;
        }

        public void DisableAlarm()
        {
            m_enabled = false;
        }

        public void WriteTriggerTime(uint triggerTime)
        {
            bool gotLock = false;
            try
            {
                m_lockObject.Enter(ref gotLock);
                m_triggerTime = triggerTime;
            }
            finally
            {
                if (gotLock) m_lockObject.Exit();
            }
        }

        public void Dispose()
        {
            m_continue = false;
            m_enabled = true;
            m_alarmThread.Join();
        }
    }
}
