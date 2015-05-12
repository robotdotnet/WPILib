using System;
using System.Collections.Generic;
using System.Text;
using WPILib.Interfaces;
using HAL_RoboRIO;

namespace WPILib
{
    public class VictorSP : SafePWM, SpeedController
    {
        private void InitVictorSP()
        {
            SetBounds(2.004, 1.52, 1.50, 1.48, .997);
            SetPeriodMultiplier(PeriodMultiplier.k1x_val);
            SetRaw(GetCenterPwm());
            SetZeroLatch();

            HAL.Report(ResourceType.kResourceType_Victor, (byte)GetChannel());
        }

        public VictorSP(int channel)
            : base(channel)
        {
            InitVictorSP();
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
