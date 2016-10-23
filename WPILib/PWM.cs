using System;
using HAL.Base;
using NetworkTables;
using NetworkTables.Tables;
using WPILib.Exceptions;
using WPILib.LiveWindow;
using static HAL.Base.HALPorts;
using static WPILib.Utility;
using static HAL.Base.HAL;
using static HAL.Base.HALPWM;

namespace WPILib
{
    /// <summary>
    /// Represents the amount to multiply the minimum servo-pulse pwm period by.
    /// </summary>
    public enum PeriodMultiplier
    {
        /// Don't skip pulses
        K1X = 1,
        /// Skip every other pulse
        K2X = 2,
        /// Skip three out of 4 pulses
        K4X = 4,
    }

    /// <summary>
    /// Class implements the PWM generation in the FPGA.
    /// </summary>
    /// <remarks>
    /// The values supplied as arguments for PWM outputs range from -1.0 to 1.0. They are mapped
    /// <para/> to the hardware dependent values, in this case 0-2000 for the FPGA.
    /// <para/> Changes are immediately sent to the FPGA, and the update occurs at the next
    /// <para/> FPGA cycle. There is no delay. 
    /// <para>As of revision 0.1.10 of the FPGA, the FPGA interprets the 0-2000 values as follows:
    /// <list type="bullet">
    /// <item><description>2000 = maximum pulse width</description></item>
    /// <item><description>1999 to 1001 = linear scaling from "full forward" to "center"</description></item>
    /// <item><description>1000 = center value</description></item>
    /// <item><description>999 to 2 = linear scaling from "center" to "full reverse"</description></item>
    /// <item><description>1 = minimum pulse width (currently .5ms)</description></item>
    /// <item><description>0 = disabled (i.e. PWM output is held low)</description></item></list></para>   
    /// </remarks>
    public class PWM : SensorBase, ILiveWindowSendable, ITableListener
    {
        private int m_handle;

        

      

        /// <summary>
        /// Allocate a PWM given a channel
        /// </summary>
        /// <param name="channel">The PWM Channel.</param>
        public PWM(int channel)
        {
            CheckPwmChannel(channel);
            Channel = channel;

            int status = 0;
            m_handle = HAL_InitializePWMPort(HAL_GetPort(channel), ref status);
            CheckStatusRange(status, 0, HAL_GetNumPWMChannels(), channel);

            SetDisabled();

            status = 0;
            HAL_SetPWMEliminateDeadband(m_handle, false, ref status);

            CheckStatus(status);

            Report(ResourceType.kResourceType_PWM, channel);
        }

        /// <summary>
        /// Free the PWM Channel
        /// </summary>
        /// <remarks>Free the resource associated with the PWM channel and set the value to 0.</remarks>
        public override void Dispose()
        {
            if (m_handle == 0) return;
            SetDisabled();

            int status = 0;
            HAL_FreePWMPort(m_handle, ref status);
            CheckStatus(status);
            m_handle = 0;
        }

        /// <summary>
        /// Get or set whether or not to eliminate deadband from the speed contruller.
        /// </summary>
        public bool DeadbandElimination {
            set
            {
                int status = 0;
                HAL_SetPWMEliminateDeadband(m_handle, value, ref status);
                CheckStatus(status);
            }
        }

        /// <summary>
        /// Set the bounds on the PWM values.
        /// </summary>
        /// <remarks>
        /// This sets the bounds on the PWM values for a particular each type of controller. The values
	    /// determine the upper and lower speeds as well as the deadband bracket.
        /// </remarks>
        /// <param name="max">The minimum PWM value</param>
        /// <param name="deadbandMax">The high end of the deadband range</param>
        /// <param name="center">The center speed (off)</param>
        /// <param name="deadbandMin">The low end of the deadband range</param>
        /// <param name="min">The minimum PWM value</param>
        [Obsolete("Recommended to set bounds in ms using SetBounds(double, double, double, double, double")]
        public void SetRawBounds(int max, int deadbandMax, int center, int deadbandMin, int min)
        {
            int status = 0;
            HAL_SetPWMConfigRaw(m_handle, max, deadbandMax, center, deadbandMin, min, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Set the bounds on the PWM pulse widths.
        /// </summary>
        /// <remarks>This sets the bounds on the PWM values for a particular type of controller. The values
        /// determine the upper and lower speeds as well as the deadband bracket.</remarks>
        /// <param name="max">The minimum PWM value</param>
        /// <param name="deadbandMax">The high end of the deadband range</param>
        /// <param name="center">The center speed (off)</param>
        /// <param name="deadbandMin">The low end of the deadband range</param>
        /// <param name="min">The minimum PWM value</param>
        public void SetBounds(double max, double deadbandMax, double center, double deadbandMin, double min)
        {
            int status = 0;
            HAL_SetPWMConfig(m_handle, max, deadbandMax, center, deadbandMin, min, ref status);
            CheckStatus(status);
        }


        /// <summary>
        /// Gets the bounds on the PWM pulse widths.
        /// </summary>
        /// <remarks>This gets the bounds on the PWM values for a particular type of controller. The values
        /// determine the upper and lower speeds as well as the deadband bracket.</remarks>
        /// <param name="max">The minimum PWM value</param>
        /// <param name="deadbandMax">The high end of the deadband range</param>
        /// <param name="center">The center speed (off)</param>
        /// <param name="deadbandMin">The low end of the deadband range</param>
        /// <param name="min">The minimum PWM value</param>
        public void GetRawBounds(out int max, out int deadbandMax, out int center, out int deadbandMin, out int min)
        {
            int status = 0;
            max = 0;
            deadbandMax = 0;
            center = 0;
            deadbandMin = 0;
            min = 0;
            HAL_GetPWMConfigRaw(m_handle, ref max, ref deadbandMax, ref center, ref deadbandMin, ref min, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Gets the channel number associated with the PWM Object.
        /// </summary>
        public int Channel { get; private set; }

        /// <summary>
        /// Set the PWM value based on a position
        /// </summary>
        /// <remarks>This is intended to be used by servos</remarks>
        /// <param name="pos">The position to set the servo between 0.0 and 1.0</param>
        public void SetPosition(double pos)
        {
            int status = 0;
            HAL_SetPWMPosition(m_handle, pos, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Get the PWM value in terms of a position
        /// </summary>
        /// <remarks>This is intended to be used by servos.</remarks>
        /// <returns>The position the server is set to between 0.0 and 1.0</returns>
        public double GetPosition()
        {
            int status = 0;
            double pos = HAL_GetPWMPosition(m_handle, ref status);
            CheckStatus(status);
            return pos;
        }

        /// <summary>
        /// Set the PWM value based on speed
        /// </summary>
        /// <remarks>This is intended to be used by speed controllers.</remarks>
        /// <param name="value">The speed to set the speed controller between -1.0 and 1.0</param>
        public void SetSpeed(double value)
        {
            int status = 0;
            HAL_SetPWMSpeed(m_handle, value, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Gets the latest PWM value in terms of speed
        /// </summary>
        /// <remarks>This is intended to be used by speed controllers.</remarks>
        /// <returns>The most recently set speed between -1.0 and 1.0</returns>
        public double GetSpeed()
        {
            int status = 0;
            double speed = HAL_GetPWMSpeed(m_handle, ref status);
            CheckStatus(status);
            return speed;
        }

        /// <summary>
        /// Sets the PWM value directly to the FPGA.
        /// </summary>
        /// <param name="value">Raw PWM value between 0 and 2000</param>
        public void SetRaw(int value)
        {
            int status = 0;
            HAL_SetPWMRaw(m_handle, value, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Gets the latest PWM value directly from the FPGA.
        /// </summary>
        /// <returns>Raw PWM value between 0 and 2000</returns>
        public int GetRaw()
        {
            int status = 0;
            int value = HAL_GetPWMRaw(m_handle, ref status);
            CheckStatus(status);
            return value;
        }

        /// <summary>
        /// Sets the Period Multiplier. This is used to slow down the signal for
        /// old PWM devices.
        /// </summary>
        public PeriodMultiplier PeriodMultiplier
        {
            set
            {
                int status = 0;

                switch (value)
                {
                    case PeriodMultiplier.K4X:
                        HAL_SetPWMPeriodScale(m_handle, 3, ref status);
                        break;
                    case PeriodMultiplier.K2X:
                        HAL_SetPWMPeriodScale(m_handle, 1, ref status);
                        break;
                    case PeriodMultiplier.K1X:
                        HAL_SetPWMPeriodScale(m_handle, 0, ref status);
                        break;
                    default:
                        break;
                }
                CheckStatus(status);
            }
        }

        /// <summary>
        /// Latch Zero on the PWM Port.
        /// </summary>
        protected void SetZeroLatch()
        {
            int status = 0;
            HAL_LatchPWMZero(m_handle, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Disables the PWM output.
        /// </summary>
        public void SetDisabled()
        {
            int status = 0;
            HAL_SetPWMDisabled(m_handle, ref status);
            CheckStatus(status);
        }

        ///<inheritdoc/>
        public virtual string SmartDashboardType => "Speed Controller";
        ///<inheritdoc/>
        public virtual void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }
        ///<inheritdoc/>
        public virtual void UpdateTable()
        {
            Table?.PutNumber("Value", GetSpeed());
        }
        ///<inheritdoc/>
        public ITable Table { get; protected set; }
        ///<inheritdoc/>
        public virtual void StartLiveWindowMode()
        {
            SetSpeed(0);
            Table?.AddTableListener("Value", this, true);
        }

        ///<inheritdoc/>
        public virtual void ValueChanged(ITable table, string key, Value value, NotifyFlags flags)
        {
            SetSpeed(value.GetDouble());
        }
        ///<inheritdoc/>
        public virtual void StopLiveWindowMode()
        {
            SetSpeed(0);
            Table?.RemoveTableListener(this);
        }
    }
}
