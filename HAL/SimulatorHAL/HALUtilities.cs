using System;
using HAL.Base;
using HAL.Simulator;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming

#pragma warning disable 1591

namespace HAL.SimulatorHAL
{
    ///<inheritdoc cref="HAL"/>
    internal class HALUtilities
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALUtilities.DelayTicks = delayTicks;
            Base.HALUtilities.DelayMillis = delayMillis;
            Base.HALUtilities.DelaySeconds = delaySeconds;
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
