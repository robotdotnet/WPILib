using System;
using HAL;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    internal class HALUtilities
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALUtilities.DelayTicks = delayTicks;
            global::HAL.HALUtilities.DelayMillis = delayMillis;
            global::HAL.HALUtilities.DelaySeconds = delaySeconds;
        }

        /// Return Type: void
        ///ticks: int
        [CalledSimFunction]
        public static void delayTicks(int ticks)
        {
            throw new NotImplementedException();
        }


        /// Return Type: void
        ///ms: double
        [CalledSimFunction]
        public static void delayMillis(double ms)
        {
            SimHooks.DelayMillis(ms);
        }


        /// Return Type: void
        ///s: double
        [CalledSimFunction]
        public static void delaySeconds(double s)
        {
            SimHooks.DelaySeconds(s);
        }
    }
}
