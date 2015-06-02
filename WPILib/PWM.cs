

using System;
using NetworkTablesDotNet.Tables;
using WPILib.Util;
using HAL_Base;
using WPILib.livewindow;
using NetworkTablesDotNet;

namespace WPILib
{
    public class PWM : SensorBase, LiveWindowSendable, ITableListener
    {
        public enum PeriodMultiplier
        {
            K1X = 1,
            K2X = 2,
            K4X = 4,
        }
        private int m_channel;
        private IntPtr m_port;

        protected static readonly double DefaultPwmPeriod = 5.05;

        protected static readonly double DefaultPwmCenter = 1.5;

        protected static readonly int DefaultPwmStepsDown = 1000;
        public static readonly int PwmDisabled = 0;
        private bool m_eliminateDeadband;
        private int m_maxPwm;
        private int m_deadbandMaxPwm;
        private int m_centerPwm;
        private int m_deadbandMinPwm;
        private int m_minPwm;

        private void InitPwm(int channel)
        {
            CheckPwmChannel(channel);
            m_channel = channel;

            int status = 0;
            m_port = HALDigital.InitializeDigitalPort(HAL.GetPort((byte)channel), ref status);
            Utility.CheckStatus(status);
            if (!HALDigital.AllocatePWMChannel(m_port, ref status))
            {
                throw new AllocationException("PWM channel " + channel + " is already allocated");
            }
            Utility.CheckStatus(status);
            HALDigital.SetPWM(m_port, 0, ref status);
            Utility.CheckStatus(status);
            m_eliminateDeadband = false;

            HAL.Report(ResourceType.kResourceType_PWM, (byte)channel);
        }

        public PWM(int channel)
        {
            InitPwm(channel);
        }

        public override void Free()
        {
            int status = 0;
            HALDigital.SetPWM(m_port, 0, ref status);
            Utility.CheckStatus(status);
            HALDigital.FreePWMChannel(m_port, ref status);
            Utility.CheckStatus(status);
            HALDigital.FreeDIO(m_port, ref status);
            Utility.CheckStatus(status);
        }

        public void EnableDeadbandElimination(bool eliminateDeadband)
        {
            m_eliminateDeadband = eliminateDeadband;
        }

        public void SetBounds(int max, int deadbandMax, int center, int deadbandMin, int min)
        {
            m_maxPwm = max;
            m_deadbandMaxPwm = deadbandMax;
            m_centerPwm = center;
            m_deadbandMinPwm = deadbandMin;
            m_minPwm = min;
        }

        protected void SetBounds(double max, double deadbandMax, double center, double deadbandMin, double min)
        {
            int status = 0;

            double loopTime = HALDigital.GetLoopTiming(ref status) / (SystemClockTicksPerMicrosecond * 1e3);

            m_maxPwm = (int)((max - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
            m_deadbandMaxPwm = (int)((deadbandMax - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
            m_centerPwm = (int)((center - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
            m_deadbandMinPwm = (int)((deadbandMin - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
            m_minPwm = (int)((min - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
        }

        public int GetChannel()
        {
            return m_channel;
        }

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

            int rawValue;

            // note, need to perform the multiplication below as floating point before converting to int
            rawValue = (int)((pos * (double)GetFullRangeScaleFactor()) + GetMinNegativePwm());

            // send the computed pwm value to the FPGA
            SetRaw(rawValue);
        }

        public double GetPosition()
        {
            int value = GetRaw();
            if (value < GetMinNegativePwm())
            {
                return 0.0;
            }
            else if (value > GetMaxPositivePwm())
            {
                return 1.0;
            }
            else
            {
                return (double)(value - GetMinNegativePwm()) / (double)GetFullRangeScaleFactor();
            }
        }

        public void SetSpeed(double speed)
        {
            // clamp speed to be in the range 1.0 >= speed >= -1.0
            if (speed < -1.0)
            {
                speed = -1.0;
            }
            else if (speed > 1.0)
            {
                speed = 1.0;
            }

            // calculate the desired output pwm value by scaling the speed appropriately
            int rawValue;
            if (speed == 0.0)
            {
                rawValue = GetCenterPwm();
            }
            else if (speed > 0.0)
            {
                rawValue = (int)(speed * ((double)GetPositiveScaleFactor()) +
                                  ((double)GetMinPositivePwm()) + 0.5);
            }
            else
            {
                rawValue = (int)(speed * ((double)GetNegativeScaleFactor()) +
                                  ((double)GetMaxNegativePwm()) + 0.5);
            }

            // send the computed pwm value to the FPGA
            SetRaw(rawValue);
        }

        public double GetSpeed()
        {
            int value = GetRaw();
            if (value > GetMaxPositivePwm())
            {
                return 1.0;
            }
            else if (value < GetMinNegativePwm())
            {
                return -1.0;
            }
            else if (value > GetMinPositivePwm())
            {
                return (double)(value - GetMinPositivePwm()) / (double)GetPositiveScaleFactor();
            }
            else if (value < GetMaxNegativePwm())
            {
                return (double)(value - GetMaxNegativePwm()) / (double)GetNegativeScaleFactor();
            }
            else
            {
                return 0.0;
            }
        }

        public void SetRaw(int value)
        {
            int status = 0;
            HALDigital.SetPWM(m_port, (ushort)value, ref status);
            Utility.CheckStatus(status);
        }

        public int GetRaw()
        {
            int status = 0;
            int value = HALDigital.GetPWM(m_port, ref status);
            Utility.CheckStatus(status);

            return value;
        }

        public void SetPeriodMultiplier(PeriodMultiplier mult)
        {
            int status = 0;

            switch (mult)
            {
                case PeriodMultiplier.K1X:
                    HALDigital.SetPWMPeriodScale(m_port, 3, ref status);
                    break;
                case PeriodMultiplier.K2X:
                    HALDigital.SetPWMPeriodScale(m_port, 1, ref status);
                    break;
                case PeriodMultiplier.K4X:
                    HALDigital.SetPWMPeriodScale(m_port, 0, ref status);
                    break;
                default:
                    break;
            }
            Utility.CheckStatus(status);
        }

        protected void SetZeroLatch()
        {
            int status = 0;
            HALDigital.LatchPWMZero(m_port, ref status);
            Utility.CheckStatus(status);
        }

        protected int GetMaxPositivePwm()
        {
            return m_maxPwm;
        }

        protected int GetMinPositivePwm()
        {
            return m_eliminateDeadband ? m_deadbandMaxPwm : m_centerPwm + 1;
        }

        protected int GetCenterPwm()
        {
            return m_centerPwm;
        }

        protected int GetMaxNegativePwm()
        {
            return m_eliminateDeadband ? m_deadbandMinPwm : m_centerPwm - 1;
        }

        protected int GetMinNegativePwm()
        {
            return m_minPwm;
        }

        protected int GetPositiveScaleFactor()
        {
            return GetMaxPositivePwm() - GetMinPositivePwm();
        }

        protected int GetNegativeScaleFactor()
        {
            return GetMaxNegativePwm() - GetMinNegativePwm();
        }

        protected int GetFullRangeScaleFactor()
        {
            return GetMaxPositivePwm() - GetMinNegativePwm();
        }

        public string GetSmartDashboardType()
        {
            return "Speed Controller";
        }

        private ITable m_table;

        public void InitTable(ITable subtable)
        {
            m_table = subtable;
            UpdateTable();
        }

        public void UpdateTable()
        {
            if (m_table != null)
            {
                m_table.PutNumber("Value", GetSpeed());
            }
        }

        public ITable GetTable()
        {
            return m_table;
        }

        public void StartLiveWindowMode()
        {
            SetSpeed(0);
            if (m_table != null)
            {
                m_table.AddTableListener("Value", this, true);
            }
        }

        public void ValueChanged(ITable table, string key, object value, bool bin)
        {
            SetSpeed((double) value);
        }

        public void StopLiveWindowMode()
        {
            SetSpeed(0);
            if (m_table != null)
            {
                m_table.RemoveTableListener(this);
            }
        }
    }
}
