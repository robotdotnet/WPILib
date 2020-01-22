using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib
{
    public class Timer
    {
        public static TimeSpan FPGATimestamp => TimeSpan.FromMilliseconds(Hal.HalBase.GetFPGATimestamp() / 1000.0);
    }
}
