
namespace HAL_FRC
{
    public class HALUtilities
    {
        /// Return Type: void
        ///ticks: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "delayTicks")]
        public static extern void delayTicks(int ticks);


        /// Return Type: void
        ///ms: double
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "delayMillis")]
        public static extern void delayMillis(double ms);


        /// Return Type: void
        ///s: double
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "delaySeconds")]
        public static extern void delaySeconds(double s);

        public static int PARAMETER_OUT_OF_RANGE = -1028;
    }
}
