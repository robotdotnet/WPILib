using System;
using System.IO;

namespace WPILib
{
    /// <summary>
    /// The MotorSafetyHelper object is constructed for every object that wants to implement the Motor
    /// Safety protocol. The helper object has the code to actually do the timing and call the
    /// motors Stop() method when the timeout expires. The motor object is expected to call the
    /// Feed() method whenever the motors value is updated.
    /// </summary>
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

        /// <summary>
        /// The constructor for a MotorSafetyHelper object
        /// <para />
        /// The helper object is constructed for every object that wants to implement the Motor
        /// Safety protocol. The helper object has the code to actually do the timing and call the
        /// motors Stop() method when the timeout expires. The motor object is expected to call the
        /// Feed() method whenever the motors value is updated.
        /// </summary>
        /// <param name="safeObject">A pointer to the motor object implementing MotorSafety. This is used
        /// to call the Stop() method on the motor</param>
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

        /// <summary>
        /// Feed the motor safety object.
        /// <para />Resets the timer on this object that is used to do the timeouts.
        /// </summary>
        public void Feed()
        {
            m_stopTime = Timer.GetFPGATimestamp() + m_expiration;
        }

        /// <summary>
        /// Set the expiration time for the corresponding motor safety object.
        /// </summary>
        /// <param name="expirationTime">The timeout value in seconds.</param>
        public void SetExpiration(double expirationTime)
        {
            m_expiration = expirationTime;
        }

        /// <summary>
        /// Retrieve the timeout value for the corresponding motor safety object.
        /// </summary>
        /// <returns>The timeout value in seconds.</returns>
        public double GetExpiration()
        {
            return m_expiration;
        }

        /// <summary>
        /// Determine if the motor is still operating or has timed out.
        /// </summary>
        /// <returns>A true value if the motor is still operating normally and hasn't timed out.</returns>
        public bool IsAlive()
        {
            return !m_enabled || m_stopTime > Timer.GetFPGATimestamp();
        }

        /// <summary>
        /// Check if this motor has exceeded its timeout.
        /// <para />This method is called periodicallty to determine if this motor has exceeded its timeout value.
        /// If it has, the stop method is called, and the motor is shut down until its value is
        /// updated again.
        /// </summary>
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

        /// <summary>
        /// Enable/disable motor safety for this device.
        /// <para />Turn on and off the motor safety option for this PWM device.
        /// </summary>
        /// <param name="enabled">True if motor safety is enforced for this object</param>
        public void SetSafetyEnabled(bool enabled)
        {
            m_enabled = enabled;
        }

        /// <summary>
        /// Return the state of the motor safety enabled flag.
        /// <para />Return if the motor safety is currently enabled for this device.
        /// </summary>
        /// <returns>True if motor safety is enforced for this device</returns>
        public bool IsSafetyEnabled()
        {
            return m_enabled;
        }

        /// <summary>
        /// Check the motors to see if any have timed out.
        /// <para />This static method is called periodically to poll all motors and stop any that have timed out.
        /// </summary>
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
