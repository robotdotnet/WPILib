//File automatically generated using robotdotnet-tools. Please do not modify.
using HAL_Base;
namespace HAL_RoboRIO
{
    public class HALCompressor
    {

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "initializeCompressor")]
        public static extern System.IntPtr initializeCompressor(byte module);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "checkCompressorModule")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool checkCompressorModule(byte module);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getCompressor")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCompressor(System.IntPtr pcm_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setClosedLoopControl")]
        public static extern void setClosedLoopControl(System.IntPtr pcm_pointer, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool value, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getClosedLoopControl")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getClosedLoopControl(System.IntPtr pcm_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getPressureSwitch")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getPressureSwitch(System.IntPtr pcm_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getCompressorCurrent")]
        public static extern float getCompressorCurrent(System.IntPtr pcm_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getCompressorCurrentTooHighFault")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCompressorCurrentTooHighFault(System.IntPtr pcm_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getCompressorCurrentTooHighStickyFault")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCompressorCurrentTooHighStickyFault(System.IntPtr pcm_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getCompressorShortedStickyFault")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCompressorShortedStickyFault(System.IntPtr pcm_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getCompressorShortedFault")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCompressorShortedFault(System.IntPtr pcm_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getCompressorNotConnectedStickyFault")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCompressorNotConnectedStickyFault(System.IntPtr pcm_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getCompressorNotConnectedFault")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCompressorNotConnectedFault(System.IntPtr pcm_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "clearAllPCMStickyFaults")]
        public static extern void clearAllPCMStickyFaults(System.IntPtr pcm_pointer, ref int status);
    }
}
