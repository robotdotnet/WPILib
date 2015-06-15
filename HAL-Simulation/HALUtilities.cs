
using System.Runtime.InteropServices;

namespace HAL_Simulator
{
    public class HALUtilities
    {
        /// Return Type: void
        ///ticks: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "delayTicks")]
        public static extern void delayTicks(int ticks);


        /// Return Type: void
        ///ms: double
        [DllImport("libHALAthena_shared.so", EntryPoint = "delayMillis")]
        public static extern void delayMillis(double ms);


        /// Return Type: void
        ///s: double
        [DllImport("libHALAthena_shared.so", EntryPoint = "delaySeconds")]
        public static extern void delaySeconds(double s);
    }
}
