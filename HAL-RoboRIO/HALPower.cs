//File automatically generated using robotdotnet-tools. Please do not modify.
using HAL_Base;
namespace HAL_RoboRIO
{
    public class HALPower
    {

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getVinVoltage")]
        public static extern float getVinVoltage(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getVinCurrent")]
        public static extern float getVinCurrent(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getUserVoltage6V")]
        public static extern float getUserVoltage6V(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getUserCurrent6V")]
        public static extern float getUserCurrent6V(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getUserActive6V")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getUserActive6V(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getUserCurrentFaults6V")]
        public static extern int getUserCurrentFaults6V(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getUserVoltage5V")]
        public static extern float getUserVoltage5V(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getUserCurrent5V")]
        public static extern float getUserCurrent5V(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getUserActive5V")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getUserActive5V(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getUserCurrentFaults5V")]
        public static extern int getUserCurrentFaults5V(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getUserVoltage3V3")]
        public static extern float getUserVoltage3V3(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getUserCurrent3V3")]
        public static extern float getUserCurrent3V3(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getUserActive3V3")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getUserActive3V3(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getUserCurrentFaults3V3")]
        public static extern int getUserCurrentFaults3V3(ref int status);
    }
}
