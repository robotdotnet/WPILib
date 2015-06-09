using System;
using System.Runtime.InteropServices;

namespace HAL_FRC
{
    public class HALSolenoid
    {
        /// Return Type: void*
        ///port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeSolenoidPort")]
        public static extern IntPtr initializeSolenoidPort(IntPtr port_pointer, ref int status);


        /// Return Type: boolean
        ///module: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "checkSolenoidModule")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkSolenoidModule(byte module);


        /// Return Type: boolean
        ///solenoid_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getSolenoid")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getSolenoid(IntPtr solenoid_port_pointer, ref int status);


        /// Return Type: void
        ///solenoid_port_pointer: void*
        ///value: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setSolenoid")]
        public static extern void setSolenoid(IntPtr solenoid_port_pointer, [MarshalAs(UnmanagedType.I1)] bool value, ref int status);


        /// Return Type: int
        ///solenoid_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPCMSolenoidBlackList")]
        public static extern int getPCMSolenoidBlackList(IntPtr solenoid_port_pointer, ref int status);


        /// Return Type: boolean
        ///solenoid_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPCMSolenoidVoltageStickyFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getPCMSolenoidVoltageStickyFault(IntPtr solenoid_port_pointer, ref int status);


        /// Return Type: boolean
        ///solenoid_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPCMSolenoidVoltageFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getPCMSolenoidVoltageFault(IntPtr solenoid_port_pointer, ref int status);


        /// Return Type: void
        ///solenoid_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "clearAllPCMStickyFaults_sol")]
        public static extern void clearAllPCMStickyFaults_sol(IntPtr solenoid_port_pointer, ref int status);
    }
}
