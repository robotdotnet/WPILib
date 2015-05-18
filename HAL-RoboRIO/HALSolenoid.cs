namespace HAL_RoboRIO
{
    public class HALSolenoid
    {
        /// Return Type: void*
        ///port_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "initializeSolenoidPort")]
        public static extern System.IntPtr initializeSolenoidPort(System.IntPtr portPointer, ref int status);


        /// Return Type: boolean
        ///module: byte
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "checkSolenoidModule")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool checkSolenoidModule(byte module);


        /// Return Type: boolean
        ///solenoid_port_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getSolenoid")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getSolenoid(System.IntPtr solenoidPortPointer, ref int status);


        /// Return Type: void
        ///solenoid_port_pointer: void*
        ///value: boolean
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "setSolenoid")]
        public static extern void setSolenoid(System.IntPtr solenoidPortPointer, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool value, ref int status);


        /// Return Type: int
        ///solenoid_port_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getPCMSolenoidBlackList")]
        public static extern int getPCMSolenoidBlackList(System.IntPtr solenoidPortPointer, ref int status);


        /// Return Type: boolean
        ///solenoid_port_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getPCMSolenoidVoltageStickyFault")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getPCMSolenoidVoltageStickyFault(System.IntPtr solenoidPortPointer, ref int status);


        /// Return Type: boolean
        ///solenoid_port_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getPCMSolenoidVoltageFault")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getPCMSolenoidVoltageFault(System.IntPtr solenoidPortPointer, ref int status);


        /// Return Type: void
        ///solenoid_port_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "clearAllPCMStickyFaults_sol")]
        public static extern void clearAllPCMStickyFaults_sol(System.IntPtr solenoidPortPointer, ref int status);
    }
}

