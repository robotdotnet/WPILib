using System;
using HAL_Base;
using NetworkTablesDotNet.Tables;
using WPILib.livewindow;
using WPILib.Util;
using static WPILib.Utility;
using static HAL_Base.HAL;
using static HAL_Base.HALDigital;

namespace WPILib
{
    public enum PeriodMultiplier
    {
        K1X = 1,
        K2X = 2,
        K4X = 4,
    }
    public class PWM : SensorBase, LiveWindowSendable, ITableListener
    {
        private IntPtr m_port;

        protected static readonly double DefaultPwmPeriod = 5.05;

        protected static readonly double DefaultPwmCenter = 1.5;

        protected static readonly int DefaultPwmStepsDown = 1000;
        public static readonly int PwmDisabled = 0;
        private bool m_eliminateDeadband;
        private int m_deadbandMaxPwm;
        private int m_deadbandMinPwm;

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
            m_eliminateDeadband = false;

            Report(ResourceType.kResourceType_PWM, (byte)channel);
        }

        public PWM(int channel)
        {
            InitPwm(channel);
        }

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

        public bool DeadbandElimination
        {
            set { m_eliminateDeadband = value; }
        }

        public void SetBounds(int max, int deadbandMax, int center, int deadbandMin, int min)
        {
            MaxPositivePwm = max;
            m_deadbandMaxPwm = deadbandMax;
            CenterPwm = center;
            m_deadbandMinPwm = deadbandMin;
            MinNegativePwm = min;
        }

        protected void SetBounds(double max, double deadbandMax, double center, double deadbandMin, double min)
        {
            int status = 0;

            double loopTime = GetLoopTiming(ref status) / (SystemClockTicksPerMicrosecond * 1e3);

            MaxPositivePwm = (int)((max - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
            m_deadbandMaxPwm = (int)((deadbandMax - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
            CenterPwm = (int)((center - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
            m_deadbandMinPwm = (int)((deadbandMin - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
            MinNegativePwm = (int)((min - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
        }

        public int Channel { get; private set; }

        public double Position
        {
            get
            {
                int value = Raw;
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
                    return (double) (value - MinNegativePwm)/(double) FullRangeScaleFactor;
                }
            }
            set
            {
                if (value < 0.0)
                {
                    value = 0.0;
                }
                else if (value > 1.0)
                {
                    value = 1.0;
                }

                int rawValue;

                // note, need to perform the multiplication below as floating point before converting to int
                rawValue = (int) ((value*(double) FullRangeScaleFactor) + MinNegativePwm);

                // send the computed pwm value to the FPGA
                Raw = rawValue;
            }
        }

        public double Speed
        {
            get
            {
                int value = Raw;
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
                    return (double) (value - MinPositivePwm)/(double) PositiveScaleFactor;
                }
                else if (value < MaxNegativePwm)
                {
                    return (double) (value - MaxNegativePwm)/(double) NegativeScaleFactor;
                }
                else
                {
                    return 0.0;
                }
            }
            set
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
                    rawValue = (int) (value*((double) PositiveScaleFactor) +
                                      ((double) MinPositivePwm) + 0.5);
                }
                else
                {
                    rawValue = (int) (value*((double) NegativeScaleFactor) +
                                      ((double) MaxNegativePwm) + 0.5);
                }

                // send the computed pwm value to the FPGA
                Raw = rawValue;
            }
        }

        public int Raw
        {
            get
            {
                int status = 0;
                int value = GetPWM(m_port, ref status);
                CheckStatus(status);

                return value;
            }
            set
            {
                int status = 0;
                SetPWM(m_port, (ushort) value, ref status);
                CheckStatus(status);
            }
        }

        public PeriodMultiplier PeriodMultiplier
        {
            set
            {
                int status = 0;

                switch (value)
                {
                    case PeriodMultiplier.K1X:
                        SetPWMPeriodScale(m_port, 3, ref status);
                        break;
                    case PeriodMultiplier.K2X:
                        SetPWMPeriodScale(m_port, 1, ref status);
                        break;
                    case PeriodMultiplier.K4X:
                        SetPWMPeriodScale(m_port, 0, ref status);
                        break;
                    default:
                        break;
                }
                CheckStatus(status);
            }
        }

        protected void SetZeroLatch()
        {
            int status = 0;
            LatchPWMZero(m_port, ref status);
            CheckStatus(status);
        }

        protected int MaxPositivePwm { get; private set; }

        protected int MinPositivePwm => m_eliminateDeadband ? m_deadbandMaxPwm : CenterPwm + 1;

        protected int CenterPwm { get; private set; }

        protected int MaxNegativePwm => m_eliminateDeadband ? m_deadbandMinPwm : CenterPwm - 1;

        protected int MinNegativePwm { get; private set; }

        protected int PositiveScaleFactor => MaxPositivePwm - MinPositivePwm;

        protected int NegativeScaleFactor => MaxNegativePwm - MinNegativePwm;

        protected int FullRangeScaleFactor => MaxPositivePwm - MinNegativePwm;

        public string SmartDashboardType => "Speed Controller";

        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }

        public void UpdateTable()
        {
            Table?.PutNumber("Value", Speed);
        }

        public ITable Table { get; private set; }

        public void StartLiveWindowMode()
        {
            Speed = 0;
            Table?.AddTableListener("Value", this, true);
        }

        public void ValueChanged(ITable table, string key, object value, bool bin)
        {
            Speed = (double) value;
        }

        public void StopLiveWindowMode()
        {
            Speed = 0;
            Table?.RemoveTableListener(this);
        }
    }
}
