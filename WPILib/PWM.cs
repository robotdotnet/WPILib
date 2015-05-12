using System;
using System.Collections.Generic;
using System.Text;
using HAL_RoboRIO;
using WPILib.Util;

namespace WPILib
{
    public class PWM : SensorBase
    {
        public enum PeriodMultiplier
        {
            k1x_val = 1,
            k2x_val = 2,
            k4x_val = 4,
        }


        //Use enums for period multiplier
        private int _channel;
        private IntPtr _port;
        //bytebuffer m_port

        protected static readonly double DefaultPwmPeriod = 5.05;

        protected static readonly double DefaultPwmCenter = 1.5;

        protected static readonly int DefaultPwmStepsDown = 1000;
        public static readonly int PwmDisabled = 0;
        private bool _eliminateDeadband;
        private int _maxPwm;
        private int _deadbandMaxPwm;
        private int _centerPwm;
        private int _deadbandMinPwm;
        private int _minPwm;

        private void InitPwm(int channel)
        {
            CheckPwmChannel(channel);
            this._channel = channel;

            int status = 0;
            _port = HALDigital.initializeDigitalPort(HAL.GetPort((byte)channel), ref status);
            if (!HALDigital.allocatePWMChannel(_port, ref status))
            {
                throw new AllocationException("PWM channel " + channel + " is already allocated");
            }

            HALDigital.setPWM(_port, 0, ref status);

            _eliminateDeadband = false;

            HAL.Report(ResourceType.kResourceType_PWM, (byte)channel);


        }

        public PWM(int channel)
        {
            InitPwm(channel);
        }

        public override void Free()
        {
            int status = 0;
            HALDigital.setPWM(_port, 0, ref status);
            HALDigital.freePWMChannel(_port, ref status);
            HALDigital.freeDIO(_port, ref status);
        }

        public void EnableDeadbandElimination(bool eliminateDeadband)
        {
            this._eliminateDeadband = eliminateDeadband;
        }

        public void SetBounds(int max, int deadbandMax, int center, int deadbandMin, int min)
        {
            _maxPwm = max;
            _deadbandMaxPwm = deadbandMax;
            _centerPwm = center;
            _deadbandMinPwm = deadbandMin;
            _minPwm = min;
        }

        protected void SetBounds(double max, double deadbandMax, double center, double deadbandMin, double min)
        {
            int status = 0;

            double loopTime = HALDigital.getLoopTiming(ref status) / (SystemClockTicksPerMicrosecond * 1e3);

            _maxPwm = (int)((max - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
            _deadbandMaxPwm = (int)((deadbandMax - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
            _centerPwm = (int)((center - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
            _deadbandMinPwm = (int)((deadbandMin - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
            _minPwm = (int)((min - DefaultPwmCenter) / loopTime + DefaultPwmStepsDown - 1);
        }

        public int GetChannel()
        {
            return _channel;
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
            HALDigital.setPWM(_port, (ushort)value, ref status);
        }

        public int GetRaw()
        {
            int status = 0;
            int value = HALDigital.getPWM(_port, ref status);

            return value;
        }

        public void SetPeriodMultiplier(PeriodMultiplier mult)
        {
            int status = 0;

            switch (mult)
            {
                case PeriodMultiplier.k1x_val:
                    HALDigital.setPWMPeriodScale(_port, 3, ref status);
                    break;
                case PeriodMultiplier.k2x_val:
                    HALDigital.setPWMPeriodScale(_port, 1, ref status);
                    break;
                case PeriodMultiplier.k4x_val:
                    HALDigital.setPWMPeriodScale(_port, 0, ref status);
                    break;
                default:
                    break;
            }
        }

        protected void SetZeroLatch()
        {
            int status = 0;
            HALDigital.latchPWMZero(_port, ref status);
        }

        protected int GetMaxPositivePwm()
        {
            return _maxPwm;
        }

        protected int GetMinPositivePwm()
        {
            return _eliminateDeadband ? _deadbandMaxPwm : _centerPwm + 1;
        }

        protected int GetCenterPwm()
        {
            return _centerPwm;
        }

        protected int GetMaxNegativePwm()
        {
            return _eliminateDeadband ? _deadbandMinPwm : _centerPwm - 1;
        }

        protected int GetMinNegativePwm()
        {
            return _minPwm;
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
    }
}
