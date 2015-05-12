using System;
using System.Collections.Generic;
using System.Text;
using WPILib.Interfaces;
using HAL_RoboRIO;

namespace WPILib
{
    public class Victor : SafePWM, SpeedController
    {
        private void InitVictor()
        {
            SetBounds(2.027, 1.525, 1.507, 1.49, 1.026);
            SetPeriodMultiplier(PeriodMultiplier.k2x_val);
            SetRaw(GetCenterPwm());
            SetZeroLatch();

            HAL.Report(ResourceType.kResourceType_Victor, (byte)GetChannel());
        }

        public Victor(int channel)
            : base(channel)
        {
            InitVictor();
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
