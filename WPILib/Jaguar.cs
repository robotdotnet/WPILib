

using System;
using System.Collections.Generic;
using System.Text;
using WPILib.Interfaces;
using HAL_FRC;

namespace WPILib
{
    public class Jaguar : SafePWM, SpeedController
    {
        private void InitJaguar()
        {
            SetBounds(2.31, 1.55, 1.507, 1.454, .697);
            SetPeriodMultiplier(PeriodMultiplier.k1x_val);
            SetRaw(GetCenterPwm());
            SetZeroLatch();

            HAL.Report(ResourceType.kResourceType_Jaguar, (byte)GetChannel());
        }

        public Jaguar(int channel)
            : base(channel)
        {
            InitJaguar();
        }


        public void PidWrite(double output)
        {
            Set(output);
        }

        public double Get()
        {
            return GetSpeed();
        }

        public void Set(double speed, byte syncGroup)
        {
            SetSpeed(speed);
            Feed();
        }

        public void Set(double speed)
        {
            SetSpeed(speed);
            Feed();
        }
    }
}
