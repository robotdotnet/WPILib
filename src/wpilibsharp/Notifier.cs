using System;
using System.Threading;

namespace WPILib
{
    public class Notifier : IDisposable
    {
        private readonly Thread m_thread;
        private readonly object m_processMutex;
        private readonly int m_notifier;
        private Action m_handler;
        private double m_expirationTime;
        private double m_period;
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
                }
            });
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

            }
        }

        public void StartSingle(TimeSpan time)
        {

        }

        public void StartPeriodic(TimeSpan time)
        {

        }

        public void Stop()
        {

        }

        private void UpdateAlarm()
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
