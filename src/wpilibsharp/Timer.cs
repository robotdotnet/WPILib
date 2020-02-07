using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WPILib
{
    public class Timer
    {
        public const long TicksPerMicrosecond = TimeSpan.TicksPerMillisecond / 1000;


        public static TimeSpan FPGATimestamp => TimeSpan.FromTicks((long)(Hal.HalBase.GetFPGATimestamp() * TicksPerMicrosecond));

        private TimeSpan m_startTime;
        private TimeSpan m_accumulatedTime;
        private bool m_running;
        private readonly object m_lockObject = new object();

        public Timer()
        {
            Reset();
        }

        public TimeSpan Get()
        {
            lock (m_lockObject)
            {
                if (m_running)
                {
                    return m_accumulatedTime + (FPGATimestamp - m_startTime);
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
                m_accumulatedTime = TimeSpan.Zero;
                m_startTime = FPGATimestamp;
            }
        }

        public void Start()
        {
            lock (m_lockObject)
            {
                m_startTime = FPGATimestamp;
                m_running = true;
            }
        }

        public void Stop()
        {
            lock (m_lockObject)
            {
                m_accumulatedTime = Get();
                m_running = false;
            }
        }

        public bool HasPeriodPassed(TimeSpan period)
        {
            if (Get() > period)
            {
                m_startTime += period;
                return true;
            }
            return false;
        }
    }
}
