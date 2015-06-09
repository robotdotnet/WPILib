//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;

namespace HAL_RoboRIO
{
    public class HALSolenoid
    {

        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeSolenoidPort")]
        public static extern IntPtr initializeSolenoidPort(IntPtr port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "checkSolenoidModule")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkSolenoidModule(byte module);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getSolenoid")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getSolenoid(IntPtr solenoid_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "setSolenoid")]
        public static extern void setSolenoid(IntPtr solenoid_port_pointer, [MarshalAs(UnmanagedType.I1)] bool value, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getPCMSolenoidBlackList")]
        public static extern int getPCMSolenoidBlackList(IntPtr solenoid_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getPCMSolenoidVoltageStickyFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getPCMSolenoidVoltageStickyFault(IntPtr solenoid_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getPCMSolenoidVoltageFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getPCMSolenoidVoltageFault(IntPtr solenoid_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "clearAllPCMStickyFaults_sol")]
        public static extern void clearAllPCMStickyFaults_sol(IntPtr solenoid_port_pointer, ref int status);
    }
}
