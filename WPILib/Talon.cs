using System;
using System.Collections.Generic;
using System.Text;
using WPILib.Interfaces;
using HAL_RoboRIO;

namespace WPILib
{
    public class Talon : SafePWM, SpeedController
    {
        private void InitTalon()
        {
            SetBounds(2.037, 1.539, 1.513, 1.487, 0.989);
            SetPeriodMultiplier(PeriodMultiplier.k1x_val);
            SetRaw(GetCenterPwm());
            SetZeroLatch();

            HAL.Report(ResourceType.kResourceType_Talon, (byte)GetChannel());
        }

        public Talon(int channel)
            : base(channel)
        {
            InitTalon();
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
