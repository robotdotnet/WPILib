using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib
{
    public abstract class MotorSafety
    {
        public static readonly TimeSpan DefaultSafetyExpiration = TimeSpan.FromMilliseconds(100);

        private TimeSpan m_expiration = DefaultSafetyExpiration;
        private bool m_enabled;
        private TimeSpan m_stopTime = Timer.FPGATimestamp;
        private readonly object m_thisMutex = new object();
        private static readonly HashSet<MotorSafety> m_instanceList = new HashSet<MotorSafety>();
        private static readonly object m_listMutex = new object();

        public MotorSafety()
        {
            lock(m_listMutex)
            {
                m_instanceList.Add(this);
            }
        }

        public void Feed()
        {
            lock (m_thisMutex)
            {
                m_stopTime = Timer.FPGATimestamp + m_expiration;
            }
        }

        public TimeSpan Expiration
        {
            get
            {
                lock (m_thisMutex)
                {
                    return m_expiration;
                }
            }
            set
            {
                lock (m_thisMutex)
                {
                    m_expiration = value;
                }
            }
        }

        public bool IsAlive
        {
            get
            {
                lock (m_thisMutex)
                {
                    return !m_enabled || m_stopTime > Timer.FPGATimestamp;
                }
            }
        }

        public void Check()
        {
            bool enabled;
            TimeSpan stopTime;

            lock (m_thisMutex)
            {
                enabled = m_enabled;
                stopTime = m_stopTime;
            }

            if (!enabled || RobotState.IsDisabled || RobotState.IsTest)
            {
                return;
            }

            if (stopTime < Timer.FPGATimestamp)
            {
                DriverStation.ReportError(Description + "... Output not updated often enough", false);
                StopMotor();
            }
        }

        public bool SafetyEnabled
        {
            get
            {
                lock (m_thisMutex)
                {
                    return m_enabled;
                }
            }
            set
            {
                lock (m_thisMutex)
                {
                    m_enabled = value;
                }
            }
        }

        public static void CheckMotors()
        {
            lock (m_listMutex)
            {
                foreach (var elem in m_instanceList)
                {
                    elem.Check();
                }
            }
        }

        public abstract void StopMotor();

        public abstract string Description { get; }
    }
}
