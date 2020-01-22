using System;
using System.Threading;

namespace WPILib
{
    public class Notifier : IDisposable
    {
        private readonly Thread m_thread;
        private readonly object m_processMutex = new object();
        private int m_notifier;
        private Action m_handler;
        private TimeSpan m_expirationTime;
        private TimeSpan m_period;
        private bool m_periodic;
        

        public Notifier(Action handler)
        {
            m_handler = handler;
            m_notifier = Hal.Notifier.Initialize();

            m_thread = new Thread(() =>
            {
                while (true)
                {
                    var notifier = m_notifier;
                    if (notifier == 0) break;
                    var curTime = Hal.Notifier.WaitForAlarm(m_notifier, out var status);
                    if (curTime == 0 || status != 0) break;

                    Action handler;
                    lock (m_processMutex)
                    {
                        handler = m_handler;
                        if (m_periodic)
                        {
                            m_expirationTime += m_period;
                            UpdateAlarm();
                        }
                        else
                        {
                            UpdateAlarm(ulong.MaxValue);
                        }
                    }
                    handler?.Invoke();
                }
            });

            m_thread.Start();
        }

        public string Name
        {
            set
            {
                if (m_thread.Name == null)
                {
                    m_thread.Name = value;
                }
                Hal.Notifier.SetName(m_notifier, value);
            }
        }

        public Action Handler
        {
            set
            {
                lock (m_processMutex)
                {
                    m_handler = value;
                }
            }
        }

        public void StartSingle(TimeSpan time)
        {
            lock (m_processMutex)
            {
                m_periodic = false;
                m_expirationTime = Timer.FPGATimestamp + time;
                UpdateAlarm();
            }
        }

        public void StartPeriodic(TimeSpan time)
        {
            lock (m_processMutex)
            {
                m_periodic = true;
                m_period = time;
                m_expirationTime = Timer.FPGATimestamp + time;
                UpdateAlarm();
            }
        }

        public void Stop()
        {
            Hal.Notifier.CancelAlarm(m_notifier);
        }

        private void UpdateAlarm()
        {
            UpdateAlarm((ulong)(m_expirationTime.TotalMilliseconds * 1000));
        }

        private void UpdateAlarm(ulong triggerTime)
        {
            var notifier = m_notifier;
            if (notifier == 0) return;

            Hal.Notifier.UpdateAlarm(notifier, triggerTime);
        }

        public void Dispose()
        {
            var handle = Interlocked.Exchange(ref m_notifier, 0);
            Hal.Notifier.Stop(handle);
            m_thread.Join();

            Hal.Notifier.Clean(handle);
        }
    }
}
