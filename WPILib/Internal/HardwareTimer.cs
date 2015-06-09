using System.Threading;
using static WPILib.Utility;

namespace WPILib.Internal
{
    public class HardwareTimer : Timer.IStaticInterface
    {

        public double FPGATimestamp => FPGATime / 1000000.0;

        public double MatchTime => DriverStation.Instance.MatchTime;

        public void Delay(double seconds)
        {
            try
            {
                Thread.Sleep((int)(seconds * 1e3));
            }
            catch (ThreadInterruptedException)
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

            private static long MsClock => FPGATime / 1000;

            public double Get()
            {
                lock (m_lockObject)
                {
                    if (m_running)
                    {
                        return ((MsClock - m_startTime) + m_accumulatedTime) / 1000.0;
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
                    m_startTime = MsClock;
                }
            }

            public void Start()
            {
                lock (m_lockObject)
                {
                    m_startTime = MsClock;
                    m_running = true;
                }
            }

            public void Stop()
            {
                lock (m_lockObject)
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
