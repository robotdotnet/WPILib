using System;
using System.IO;
using WPILib.Interfaces;
using static WPILib.Timer;

namespace WPILib
{
    /// <summary>
    /// The <see cref="MotorSafetyHelper"/> object is constructed for every object that wants to implement the Motor
    /// Safety protocol. </summary>
    /// <remarks>The helper object has the code to actually do the timing and call the
    /// motors Stop() method when the timeout expires. The motor object is expected to call the
    /// Feed() method whenever the motors value is updated.
    /// </remarks>
    public class MotorSafetyHelper
    {
        /// <summary>
        /// The default safety expiration time.
        /// </summary>
        public const double DefaultSafetyExpiration = 0.1;

        private double m_stopTime;
        private IMotorSafety m_safeObject;
        private MotorSafetyHelper m_nextHelper;
        private static MotorSafetyHelper s_headHelper;
        private static object s_lockObject = new object();

        /// <summary>
        /// The constructor for a <see cref="MotorSafetyHelper"/> object
        /// </summary><remarks>
        /// The helper object is constructed for every object that wants to implement the Motor
        /// Safety protocol. The helper object has the code to actually do the timing and call the
        /// motors Stop() method when the timeout expires. The motor object is expected to call the
        /// Feed() method whenever the motors value is updated.
        /// </remarks>
        /// <param name="safeObject">A pointer to the motor object implementing <see cref="IMotorSafety"/>. This is used
        /// to call the Stop() method on the motor</param>
        public MotorSafetyHelper(IMotorSafety safeObject)
        {
            m_safeObject = safeObject;
            SafetyEnabled = false;
            Expiration = DefaultSafetyExpiration;
            m_stopTime = GetFPGATimestamp();
            lock (s_lockObject)
            {
                m_nextHelper = s_headHelper;
                s_headHelper = this;
            }
        }

        /// <summary>
        /// Feed the motor safety object. </summary><remarks>
        /// Resets the timer on this object that is used to do the timeouts.
        /// </remarks>
        public void Feed()
        {
            m_stopTime = GetFPGATimestamp() + Expiration;
        }

        /// <summary>
        /// Set the expiration time for the corresponding motor safety object.
        /// </summary>
        /// <value>The timeout value in seconds.</value>
        public double Expiration { set; get; }

        /// <summary>
        /// Determine if the motor is still operating or has timed out.
        /// </summary>
        /// <value>A true value if the motor is still operating normally and hasn't timed out.</value>
        public bool Alive => !SafetyEnabled || m_stopTime > GetFPGATimestamp();

        /// <summary>
        /// Check if this motor has exceeded its timeout. </summary> 
        /// <remarks>This method is called periodically to determine if this motor has exceeded its timeout value.
        /// If it has, the stop method is called, and the motor is shut down until its value is
        /// updated again.
        /// </remarks>
        public void Check()
        {
            if (!SafetyEnabled || RobotState.Disabled || RobotState.Test)
                return;
            if (m_stopTime < GetFPGATimestamp())
            {
                TextWriter errorWriter = Console.Error;
                errorWriter.WriteLine(m_safeObject.Description + "... Output not updated often enough.");
                errorWriter.Close();

                m_safeObject.StopMotor();
            }
        }

        /// <summary>
        /// Enable/disable motor safety for this device. </summary><remarks>
        /// Turn on and off the motor safety option for this PWM device.
        /// </remarks>
        /// <value>True if motor safety is enforced for this object</value>
        public bool SafetyEnabled { set; get; }

        /// <summary>
        /// Check the motors to see if any have timed out. </summary><remarks>
        /// This static method is called periodically to poll all motors and stop any that have timed out.
        /// </remarks>
        public static void CheckMotors()
        {
            lock (s_lockObject)
            {
                for (MotorSafetyHelper msh = s_headHelper; msh != null; msh = msh.m_nextHelper)
                {
                    msh.Check();
                }
            }
        }
    }
}
