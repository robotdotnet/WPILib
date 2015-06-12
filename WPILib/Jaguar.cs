using System;
using HAL_Base;

namespace WPILib
{
    /// <summary>
    /// Texas Instruments / Vex Robotics Jaguar Speed Controller as a PWM Device.
    /// </summary>
    /// <seealso cref="CANJaguar"> CANJaguar for CAN Control</seealso>
    public class Jaguar : SafePWM, ISpeedController
    {
        /// <summary>
        /// Common initialization code called by all constructors.
        /// </summary>
        protected void InitJaguar()
        {
            /*
             * Input profile defined by Luminary Micro.
             *
             * Full reverse ranges from 0.671325ms to 0.6972211ms
             * Proportional reverse ranges from 0.6972211ms to 1.4482078ms
             * Neutral ranges from 1.4482078ms to 1.5517922ms
             * Proportional forward ranges from 1.5517922ms to 2.3027789ms
             * Full forward ranges from 2.3027789ms to 2.328675ms
             */
            SetBounds(2.31, 1.55, 1.507, 1.454, .697);
            PeriodMultiplier = PeriodMultiplier.K1X;
            SetRaw(CenterPwm);
            SetZeroLatch();

            HAL.Report(ResourceType.kResourceType_Jaguar, (byte)Channel);
            //TODO: Add Actuator
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="channel">The PWM channel that the Jaguar is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
        public Jaguar(int channel)
            : base(channel)
        {
            InitJaguar();
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
            SetSpeed(value);
            Feed();
        }

        /// <summary>
        /// Get the recently set value of the PWM.
        /// </summary>
        /// <returns>The most recently set value for the PWM between -1.0 and 1.0</returns>
        public double Get()
        {
            return GetSpeed();
        }

        /// <summary>
        /// Set the PWM value.
        /// </summary> <remarks>
        /// <para> </para>
        /// The PWM value is set using a range of -1.0 to 1.0, appropriately
        /// scaling the value for the FPGA.
        /// </remarks>
        /// <param name="value">The speed to set. Value should be between -1.0 and 1.0</param>
        /// <param name="syncGroup">The update group to add this Set() to, pending UpdateSyncGroup(). If 0, update immediately.</param>
        public void Set(double value, byte syncGroup)
        {
            SetSpeed(value);
            Feed();
        }
    }
}
