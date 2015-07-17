using System;
using HAL_Base;
using NetworkTables.Tables;
using WPILib.Exceptions;
using WPILib.LiveWindows;
using static WPILib.Utility;
using static HAL_Base.HAL;
using static HAL_Base.HALDigital;

namespace WPILib
{
    /// <summary>
    /// Represents the amount to multiply the minimum servo-pulse pwm period by.
    /// </summary>
    public enum PeriodMultiplier
    {
        K1X = 1,
        K2X = 2,
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
        private IntPtr m_port;

        /// <summary>
        /// DefaultPWMPeriod is in milliseconds.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><description> 20ms periods (50 Hz) are the "safest" setting in that this works for all devices</description></item>
        /// <item><description> 20ms periods seem to be desirable for Vex Motors</description></item>
        /// <item><description> 20ms periods are the specified period for HS-322HD servos, but work reliably down
        /// to 10.0 ms; starting at about 8.5ms, the servo sometimes hums and get hot;
        ///	by 5.0ms the hum is nearly continuous</description></item>
        /// <item><description> 10ms periods work well for Victor 884</description></item>
        /// <item><description> 5ms periods allows higher update rates for Luminary Micro Jaguar speed controllers.
        /// Due to the shipping firmware on the Jaguar, we can't run the update period less
        ///	  than 5.05 ms. </description></item></list>
        ///
        /// <para/>DefaultPwmPeriod is the 1x period(5.05 ms).  In hardware, the period scaling is implemented as an
        /// output squelch to get longer periods for old devices.
        /// </remarks>
        protected static readonly double DefaultPwmPeriod = 5.05;

        /// <summary>
        /// DefaultPWMCenter is the PWM range center in ms
        /// </summary>
        protected static readonly double DefaultPwmCenter = 1.5;

        /// <summary>
        /// DefaultPWMStepsDown is the number of PWM steps below the centerpoint
        /// </summary>
        protected const int DefaultPwmStepsDown = 1000;
        ///The output value for disabled.
        public const int PwmDisabled = 0;
        private int m_deadbandMaxPwm;
        private int m_deadbandMinPwm;

        /// <summary>
        /// Initialize PWMs given a channel.
        /// </summary>
        /// <param name="channel">The PWM channel number. 0-9 are on-board, 10-19 are on the MXP port</param>
        private void InitPwm(int channel)
        {

            CheckPwmChannel(channel);
            Channel = channel;

            int status = 0;
            m_port = InitializeDigitalPort(GetPort((byte)channel), ref status);
            CheckStatus(status);
            if (!AllocatePWMChannel(m_port, ref status))
            {
                throw new AllocationException("PWM channel " + channel + " is already allocated");
            }
            CheckStatus(status);
            SetPWM(m_port, 0, ref status);
            CheckStatus(status);
            DeadbandElimination = false;

            Report(ResourceType.kResourceType_PWM, (byte)channel);
        }

        /// <summary>
        /// Allocate a PWM given a channel
        /// </summary>
        /// <param name="channel">The PWM Channel.</param>
        public PWM(int channel)
        {
            InitPwm(channel);
        }

        /// <summary>
        /// Free the PWM Channel
        /// </summary>
        /// <remarks>Free the resource associated with the PWM channel and set the value to 0.</remarks>
        public override void Dispose()
        {
            int status = 0;
            SetPWM(m_port, 0, ref status);
            CheckStatus(status);
            FreePWMChannel(m_port, ref status);
            CheckStatus(status);
            FreeDIO(m_port, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Get or set whether or not to eliminate deadband from the speed contruller.
        /// </summary>
        public bool DeadbandElimination { get; set; }

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
        [Obsolete("Recommended so set bounds in ms using SetBounds(double, double, double, double, double")]
        public void SetBounds(int max, int deadbandMax, int center, int deadbandMin, int min)
        {
            MaxPositivePwm = max;
            m_deadbandMaxPwm = deadbandMax;
            CenterPwm = center;
            m_deadbandMinPwm = deadbandMin;
            MinNegativePwm = min;
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

            double loopTime = GetLoopTiming(ref status) / (SystemClockTicksPerMicrosecond * 1e3);

            MaxPositivePwm = (int)((max - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
            m_deadbandMaxPwm = (int)((deadbandMax - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
            CenterPwm = (int)((center - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
            m_deadbandMinPwm = (int)((deadbandMin - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
            MinNegativePwm = (int)((min - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
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
            if (pos < 0.0)
            {
                pos = 0.0;
            }
            else if (pos > 1.0)
            {
                pos = 1.0;
            }

            // note, need to perform the multiplication below as floating point before converting to int
            var rawValue = (int)((pos * (double)FullRangeScaleFactor) + MinNegativePwm);

            // send the computed pwm value to the FPGA
            SetRaw(rawValue);
        }

        /// <summary>
        /// Get the PWM value in terms of a position
        /// </summary>
        /// <remarks>This is intended to be used by servos.</remarks>
        /// <returns>The position the server is set to between 0.0 and 1.0</returns>
        public double GetPosition()
        {
            int value = GetRaw();
            if (value < MinNegativePwm)
            {
                return 0.0;
            }
            else if (value > MaxPositivePwm)
            {
                return 1.0;
            }
            else
            {
                return (double)(value - MinNegativePwm) / (double)FullRangeScaleFactor;
            }
        }

        /// <summary>
        /// Set the PWM value based on speed
        /// </summary>
        /// <remarks>This is intended to be used by speed controllers.</remarks>
        /// <param name="value">The speed to set the speed controller between -1.0 and 1.0</param>
        public void SetSpeed(double value)
        {
            // clamp speed to be in the range 1.0 >= speed >= -1.0
            if (value < -1.0)
            {
                value = -1.0;
            }
            else if (value > 1.0)
            {
                value = 1.0;
            }

            // calculate the desired output pwm value by scaling the speed appropriately
            int rawValue;
            if (value == 0.0)
            {
                rawValue = CenterPwm;
            }
            else if (value > 0.0)
            {
                rawValue = (int)(value * ((double)PositiveScaleFactor) +
                                  ((double)MinPositivePwm) + 0.5);
            }
            else
            {
                rawValue = (int)(value * ((double)NegativeScaleFactor) +
                                  ((double)MaxNegativePwm) + 0.5);
            }

            // send the computed pwm value to the FPGA
            SetRaw(rawValue);
        }

        /// <summary>
        /// Gets the latest PWM value in terms of speed
        /// </summary>
        /// <remarks>This is intended to be used by speed controllers.</remarks>
        /// <returns>The most recently set speed between -1.0 and 1.0</returns>
        public double GetSpeed()
        {
            int value = GetRaw();
            if (value > MaxPositivePwm)
            {
                return 1.0;
            }
            else if (value < MinNegativePwm)
            {
                return -1.0;
            }
            else if (value > MinPositivePwm)
            {
                return (double)(value - MinPositivePwm) / (double)PositiveScaleFactor;
            }
            else if (value < MaxNegativePwm)
            {
                return (double)(value - MaxNegativePwm) / (double)NegativeScaleFactor;
            }
            else
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Sets the PWM value directly to the FPGA.
        /// </summary>
        /// <param name="value">Raw PWM value between 0 and 2000</param>
        public void SetRaw(int value)
        {
            int status = 0;
            SetPWM(m_port, (ushort)value, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Gets the latest PWM value directly from the FPGA.
        /// </summary>
        /// <returns>Raw PWM value between 0 and 2000</returns>
        public int GetRaw()
        {
            int status = 0;
            int value = GetPWM(m_port, ref status);
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
                        SetPWMPeriodScale(m_port, 3, ref status);
                        break;
                    case PeriodMultiplier.K2X:
                        SetPWMPeriodScale(m_port, 1, ref status);
                        break;
                    case PeriodMultiplier.K1X:
                        SetPWMPeriodScale(m_port, 0, ref status);
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
            LatchPWMZero(m_port, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Get the Max Positive PWM allowed.
        /// </summary>
        public int MaxPositivePwm { get; private set; }

        /// <summary>
        /// Gets the Min Positive PWM allowed. This is the high end of the deadzone.
        /// </summary>
        public int MinPositivePwm => DeadbandElimination ? m_deadbandMaxPwm : CenterPwm + 1;

        /// <summary>
        /// Gets the Center PWM value.
        /// </summary>
        public int CenterPwm { get; private set; }

        /// <summary>
        /// Gets the Max Negative PWM allowed. This is the low end of the deadband.
        /// </summary>
        public int MaxNegativePwm => DeadbandElimination ? m_deadbandMinPwm : CenterPwm - 1;

        /// <summary>
        /// Gets the Min Negative PWM allowed.
        /// </summary>
        public int MinNegativePwm { get; private set; }

        /// <summary>
        /// Gets the scale factor for positive values
        /// </summary>
        protected int PositiveScaleFactor => MaxPositivePwm - MinPositivePwm;

        /// <summary>
        /// Gets the scale factor for negative values
        /// </summary>
        protected int NegativeScaleFactor => MaxNegativePwm - MinNegativePwm;

        /// <summary>
        /// Gets the scale factor for all values
        /// </summary>
        protected int FullRangeScaleFactor => MaxPositivePwm - MinNegativePwm;

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
        public virtual void ValueChanged(ITable table, string key, object value, bool bin)
        {
            SetSpeed((double)value);
        }
        ///<inheritdoc/>
        public virtual void StopLiveWindowMode()
        {
            SetSpeed(0);
            Table?.RemoveTableListener(this);
        }
    }
}
