//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALSolenoid
    {
        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeSolenoidPort")]
        public static extern IntPtr initializeSolenoidPort(IntPtr port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "checkSolenoidModule")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkSolenoidModule(byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getSolenoid")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getSolenoid(IntPtr solenoid_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setSolenoid")]
        public static extern void setSolenoid(IntPtr solenoid_port_pointer, [MarshalAs(UnmanagedType.I1)] bool value, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPCMSolenoidBlackList")]
        public static extern int getPCMSolenoidBlackList(IntPtr solenoid_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPCMSolenoidVoltageStickyFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getPCMSolenoidVoltageStickyFault(IntPtr solenoid_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPCMSolenoidVoltageFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getPCMSolenoidVoltageFault(IntPtr solenoid_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "clearAllPCMStickyFaults_sol")]
        public static extern void clearAllPCMStickyFaults_sol(IntPtr solenoid_port_pointer, ref int status);
    }
}
