

using System;
using System.Threading;
using HAL_Base;

namespace WPILib
{
    public class Timer
    {
        public static double GetFPGATimestamp()
        {
            int status = 0;
            return HAL.GetFPGATime(ref status) * 1.0e-6;
        }

        public static double GetMatchTime()
        {
            return DriverStation.GetInstance().GetMatchTime();
        }

        public static void Delay(double seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }
    }
}
