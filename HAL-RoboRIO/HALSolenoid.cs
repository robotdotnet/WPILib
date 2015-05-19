//File automatically generated using robotdotnet-tools. Please do not modify.
using HAL_Base;
namespace HAL_RoboRIO
{
    public class HALSolenoid
    {

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "initializeSolenoidPort")]
        public static extern System.IntPtr initializeSolenoidPort(System.IntPtr port_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "checkSolenoidModule")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool checkSolenoidModule(byte module);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getSolenoid")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getSolenoid(System.IntPtr solenoid_port_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setSolenoid")]
        public static extern void setSolenoid(System.IntPtr solenoid_port_pointer, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool value, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getPCMSolenoidBlackList")]
        public static extern int getPCMSolenoidBlackList(System.IntPtr solenoid_port_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getPCMSolenoidVoltageStickyFault")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getPCMSolenoidVoltageStickyFault(System.IntPtr solenoid_port_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getPCMSolenoidVoltageFault")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getPCMSolenoidVoltageFault(System.IntPtr solenoid_port_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "clearAllPCMStickyFaults_sol")]
        public static extern void clearAllPCMStickyFaults_sol(System.IntPtr solenoid_port_pointer, ref int status);
    }
}
