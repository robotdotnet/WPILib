using System;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
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
