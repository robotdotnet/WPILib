using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            m_alarmThread.Start();
        }

        private readonly object m_mutex = new object();

        private bool m_enabled = false;

        public void Run()
        {
            lock (m_mutex)
            {
                while (true)
                {
                    while (!m_enabled) Thread.Yield();
                    m_enabled = false;

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
                    }
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
            //Not implemented.
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
            m_alarmThread.Abort();
            m_alarmThread.Join();
        }
    }
}
