using System;
using HAL_Base;

namespace WPILib
{
    /// <summary>
    /// Cross the Road Electronics (CTRE) Talon and Talon SR Speed Controller
    /// </summary>
    public class Talon : SafePWM, ISpeedController
    {
        /// <summary>
        /// Common initialization code called by all constructors.
        /// <para> </para>
        /// Note that the VictorSP uses the following bounds for PWM values. These values should work reasonably well for
        /// <para />most controllers, but if users experience issues such as asymmetric behavior around
        /// <para />the deadband or inability to saturate the controller in either direction, calibration is recommended.
        /// <para />The calibration procedure can be found in the VictorSP User Manual available from CTRE.
        /// <para> </para>
        /// <para />  - 2.037ms = full "forward"
        /// <para />  - 1.539ms = the "high end" of the deadband range
        /// <para />  - 1.513ms = center of the deadband range (off)
        /// <para />  - 1.487ms = the "low end" of the deadband range
        /// <para />  - .989ms = full "reverse"
        /// </summary>
        private void InitTalon()
        {
            SetBounds(2.037, 1.539, 1.513, 1.487, 0.989);
            PeriodMultiplier = PeriodMultiplier.K1X;
            Raw = CenterPwm;
            SetZeroLatch();

            //TODO: Add Live Actuator
            HAL.Report(ResourceType.kResourceType_Talon, (byte)Channel);
        }

        /// <summary>
        /// Constructor for a Talon (original or Talon SR)
        /// </summary>
        /// <param name="channel">The PWM Channel that the VictorSP is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
        public Talon(int channel)
            : base(channel)
        {
            InitTalon();
        }

        /// <summary>
        /// Write out the PID value as seen in the PIDOutput base object.
        /// </summary>
        /// <param name="value">Write out the PWM value at it was found in the PID Controller</param>
        public void PidWrite(double value)
        {
            Set(value);
        }

        /// <summary>
        /// Get the recently set value of the PWM.
        /// </summary>
        /// <param name="value">The most recently set value for the PWM between -1.0 and 1.0</param>
        public void Set(double value)
        {
            Speed = value;
            Feed();
        }

        /// <summary>
        /// Get the recently set value of the PWM.
        /// </summary>
        /// <returns>The most recently set value for the PWM between -1.0 and 1.0</returns>
        public double Get()
        {
            return Speed;
        }

        /// <summary>
        /// Set the PWM value.
        /// <para> </para>
        /// The PWM value is set using a range of -1.0 to 1.0, appropriately
        /// scaling the value for the FPGA.
        /// </summary>
        /// <param name="value">The speed to set. Value should be between -1.0 and 1.0</param>
        /// <param name="syncGroup">The update group to add this Set() to, pending UpdateSyncGroup(). If 0, update immediately.</param>
        [Obsolete("For compatibility with CAN Jaguar")]
        public void Set(double value, byte syncGroup)
        {
            Speed = value;
            Feed();
        }
    }
}
