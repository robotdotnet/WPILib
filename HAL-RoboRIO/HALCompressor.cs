namespace HAL_RoboRIO
{
    public class HALCompressor
    {
        /// Return Type: void*
        ///module: byte
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "initializeCompressor")]
        public static extern System.IntPtr initializeCompressor(byte module);


        /// Return Type: boolean
        ///module: byte
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "checkCompressorModule")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool checkCompressorModule(byte module);


        /// Return Type: boolean
        ///pcm_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getCompressor")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCompressor(System.IntPtr pcmPointer, ref int status);


        /// Return Type: void
        ///pcm_pointer: void*
        ///value: boolean
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "setClosedLoopControl")]
        public static extern void setClosedLoopControl(System.IntPtr pcmPointer, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool value, ref int status);


        /// Return Type: boolean
        ///pcm_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getClosedLoopControl")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getClosedLoopControl(System.IntPtr pcmPointer, ref int status);


        /// Return Type: boolean
        ///pcm_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getPressureSwitch")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getPressureSwitch(System.IntPtr pcmPointer, ref int status);


        /// Return Type: float
        ///pcm_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getCompressorCurrent")]
        public static extern float getCompressorCurrent(System.IntPtr pcmPointer, ref int status);


        /// Return Type: boolean
        ///pcm_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getCompressorCurrentTooHighFault")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCompressorCurrentTooHighFault(System.IntPtr pcmPointer, ref int status);


        /// Return Type: boolean
        ///pcm_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getCompressorCurrentTooHighStickyFault")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCompressorCurrentTooHighStickyFault(System.IntPtr pcmPointer, ref int status);


        /// Return Type: boolean
        ///pcm_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getCompressorShortedStickyFault")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCompressorShortedStickyFault(System.IntPtr pcmPointer, ref int status);


        /// Return Type: boolean
        ///pcm_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getCompressorShortedFault")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCompressorShortedFault(System.IntPtr pcmPointer, ref int status);


        /// Return Type: boolean
        ///pcm_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getCompressorNotConnectedStickyFault")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCompressorNotConnectedStickyFault(System.IntPtr pcmPointer, ref int status);


        /// Return Type: boolean
        ///pcm_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getCompressorNotConnectedFault")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCompressorNotConnectedFault(System.IntPtr pcmPointer, ref int status);


        /// Return Type: void
        ///pcm_pointer: void*
        ///status: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "clearAllPCMStickyFaults")]
        public static extern void clearAllPCMStickyFaults(System.IntPtr pcmPointer, ref int status);
    }
}
