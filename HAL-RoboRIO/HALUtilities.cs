//File automatically generated using robotdotnet-tools. Please do not modify.

using System.Runtime.InteropServices;

namespace HAL_RoboRIO
{
    public class HALUtilities
    {

        [DllImport("libHALAthena_shared.so", EntryPoint = "delayTicks")]
        public static extern void delayTicks(int ticks);

        [DllImport("libHALAthena_shared.so", EntryPoint = "delayMillis")]
        public static extern void delayMillis(double ms);

        [DllImport("libHALAthena_shared.so", EntryPoint = "delaySeconds")]
        public static extern void delaySeconds(double s);
    }
}
