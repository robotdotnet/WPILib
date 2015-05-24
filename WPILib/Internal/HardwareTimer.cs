using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WPILib.Internal
{
    public class HardwareTimer : Timer.StaticInterface
    {

        public double GetFPGATimestamp()
        {
            return Utility.GetFPGATime() / 1000000.0;
        }

        public double GetMatchTime()
        {
            return DriverStation.GetInstance().GetMatchTime();
        }

        public void Delay(double seconds)
        {
            try
            {
                Thread.Sleep((int)(seconds * 1e3));
            }
            catch(ThreadInterruptedException e)
            {

            }
        }

        public Timer.Interface NewTimer()
        {
            return new TimerImpl();
        }

        public class TimerImpl : Timer.Interface
        {
            private long m_startTime;
            private double m_accumulatedTime;
            private bool m_running;

            private object m_lockObject = new object();

            public TimerImpl()
            {
                Reset();
            }

            private long GetMsClock()
            {
                return Utility.GetFPGATime() / 1000;
            }

            public double Get()
            {
                lock(m_lockObject)
                {
                    if (m_running)
                    {
                        return ((double)((GetMsClock() - m_startTime) + m_accumulatedTime)) / 1000.0;
                    }
                    else
                    {
                        return m_accumulatedTime;
                    }
                }
            }

            public void Reset()
            {
                lock (m_lockObject)
                {
                    m_accumulatedTime = 0;
                    m_startTime = GetMsClock();
                }
            }

            public void Start()
            {
                lock(m_lockObject)
                {
                    m_startTime = GetMsClock();
                    m_running = true;
                }
            }

            public void Stop()
            {
                lock(m_lockObject)
                {
                    double temp = Get();
                    m_accumulatedTime = temp;
                    m_running = false;
                }
            }

            public bool HasPeriodPassed(double period)
            {
                lock (m_lockObject)
                {
                    if (Get() > period)
                    {
                        m_startTime += (long)(period * 1000);
                        return true;
                    }
                    return false;
                }
            }
        }
    }
}
