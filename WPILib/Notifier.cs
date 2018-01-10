using System;
using static WPILib.Timer;
using static HAL.Base.HALNotifier;
using static WPILib.Utility;
using System.Threading;

namespace WPILib
{
    /// <summary>
    /// The Notifier class is used to create alarms from the FPGA.
    /// </summary>
    public class Notifier : IDisposable
    {
        private Thread m_thread;
        private object m_processLock = new object();
        private int m_notifier = 0;
        private double m_expirationTime = 0;
        private Action m_handler;
        private bool m_periodic = false;
        private double m_period = 0;
        private volatile bool m_isRunning = false;

        public void Dispose()
        {
            int status = 0;
            int handle = Interlocked.Exchange(ref m_notifier, 0);
            m_isRunning = false;
            HAL_StopNotifier(handle, ref status);
            if (m_thread != null && m_thread.IsAlive)
            {
                m_thread.Join();
            }
            HAL_CleanNotifier(handle, ref status);
        }

        private void UpdateAlarm()
        {
            int handle = Interlocked.Add(ref m_notifier, 0);
            if (handle == 0) return;
            int status = 0;
            HAL_UpdateNotifierAlarm(handle, (ulong)(m_expirationTime * 1e6), ref status);
            CheckStatus(status);
        }

        public Notifier(Action run)
        {
            m_handler = run;
            int status = 0;
            int handle = HAL_InitializeNotifier(ref status);
            CheckStatusForceThrow(status);
            Interlocked.Exchange(ref m_notifier, handle);
            m_isRunning = true;

            m_thread = new Thread(() =>
            {
                while (m_isRunning)
                {
                    int sstatus = 0;
                    int notifier = Interlocked.Add(ref m_notifier, 0);
                    if (notifier == 0 || !m_isRunning) break;
                    ulong curTime = HAL_WaitForNotifierAlarm(notifier, ref sstatus);
                    if (curTime == 0 || !m_isRunning) break;
                    Action handler = null;
                    lock (m_processLock)
                    {
                        handler = m_handler;
                        if (m_periodic)
                        {
                            m_expirationTime += m_period;
                            UpdateAlarm();
                        }
                    }

                    handler?.Invoke();

                }
            });
            m_thread.IsBackground = true;
            m_thread.Start();
        }

        public void SetHandler(Action handler)
        {
            lock(m_processLock)
            {
                m_handler = handler;
            }
        }

        public void StartSingle(double delay)
        {
            lock(m_processLock)
            {
                m_periodic = false;
                m_period = delay;
                m_expirationTime = Utility.GetFPGATime() * 1e-6 + delay;
                UpdateAlarm();
            }
        }

        public void StartPeriodic(double period)
        {
            lock (m_processLock)
            {
                m_periodic = true;
                m_period = period;
                m_expirationTime = Utility.GetFPGATime() * 1e-6 + period;
                UpdateAlarm();
            }
        }

        public void Stop()
        {
            int status = 0;
            HAL_CancelNotifierAlarm(Interlocked.Add(ref m_notifier, 0), ref status);

        }
    }
}
