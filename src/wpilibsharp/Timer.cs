using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WPILib
{
    public class Timer
    {
        private const long TicksPerMicrosecond = TimeSpan.TicksPerMillisecond / 1000;

        
        public static TimeSpan FPGATimestamp => TimeSpan.FromTicks((long)(Hal.HalBase.GetFPGATimestamp() * TicksPerMicrosecond));
    }
}
