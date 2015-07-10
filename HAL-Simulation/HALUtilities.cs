
using System;
using System.Runtime.InteropServices;

namespace HAL_Simulator
{
    public class HALUtilities
    {
        /// Return Type: void
        ///ticks: int
        public static void delayTicks(int ticks)
        {
            throw new NotImplementedException();
        }


        /// Return Type: void
        ///ms: double
        public static void delayMillis(double ms)
        {
            SimHooks.DelayMillis(ms);
        }


        /// Return Type: void
        ///s: double
        public static void delaySeconds(double s)
        {
            SimHooks.DelaySeconds(s);
        }
    }
}
