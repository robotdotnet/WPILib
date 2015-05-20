

using System;
using System.IO;
using WPILib.Interfaces;

namespace WPILib
{
    public class MotorSafetyHelper
    {
        public const double DefaultSafetyExpiration = 0.1;

        private double m_expiration;
        private bool m_enabled;
        private double m_stopTime;
        private MotorSafety _safeObject;
        private MotorSafetyHelper _nextHelper;
        private static MotorSafetyHelper s_headHelper = null;
        private static DriverStation s_ds;
        private static object m_lockObject = new object();

        public MotorSafetyHelper(MotorSafety safeObject)
        {
            _safeObject = safeObject;
            s_ds = DriverStation.GetInstance();
            m_enabled = false;
            m_expiration = DefaultSafetyExpiration;
            m_stopTime = Timer.GetFPGATimestamp();
            lock (m_lockObject)
            {
                _nextHelper = s_headHelper;
                s_headHelper = this;
            }
        }

        public void Feed()
        {
            m_stopTime = Timer.GetFPGATimestamp() + m_expiration;
        }

        public void SetExpiration(double expirationTime)
        {
            m_expiration = expirationTime;
        }

        public double GetExpiration()
        {
            return m_expiration;
        }

        public bool IsAlive()
        {
            return !m_enabled || m_stopTime > Timer.GetFPGATimestamp();
        }

        public void Check()
        {
            if (!m_enabled || RobotState.IsDisabled() || RobotState.IsTest())
                return;
            if (m_stopTime < Timer.GetFPGATimestamp())
            {
                TextWriter errorWriter = Console.Error;
                errorWriter.WriteLine(_safeObject.GetDescription() + "... Output not updated often enough.");
                errorWriter.Close();

                _safeObject.StopMotor();
            }
        }

        public void SetSafetyEnabled(bool enabled)
        {
            m_enabled = enabled;
        }

        public bool IsSafetyEnabled()
        {
            return m_enabled;
        }

        public static void CheckMotors()
        {
            lock (m_lockObject)
            {
                for (MotorSafetyHelper msh = s_headHelper; msh != null; msh = msh._nextHelper)
                {
                    msh.Check();
                }
            }
        }
    }
}
