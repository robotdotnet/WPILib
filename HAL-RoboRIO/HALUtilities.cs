//File automatically generated using robotdotnet-tools. Please do not modify.
using HAL_Base;
namespace HAL_RoboRIO
{
    public class HALUtilities
    {

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "delayTicks")]
        public static extern void delayTicks(int ticks);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "delayMillis")]
        public static extern void delayMillis(double ms);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "delaySeconds")]
        public static extern void delaySeconds(double s);
    }
}
