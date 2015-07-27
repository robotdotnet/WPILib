//File automatically generated using robotdotnet-tools. Please do not modify.

using System.Runtime.InteropServices;

namespace HAL_RoboRIO
{
    internal class HALUtilities
    {

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "delayTicks")]
        internal static extern void delayTicks(int ticks);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "delayMillis")]
        internal static extern void delayMillis(double ms);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "delaySeconds")]
        internal static extern void delaySeconds(double s);
    }
}
