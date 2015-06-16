//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;

namespace HAL_RoboRIO
{
    public class HALCompressor
    {

        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeCompressor")]
        public static extern IntPtr initializeCompressor(byte module);

        [DllImport("libHALAthena_shared.so", EntryPoint = "checkCompressorModule")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkCompressorModule(byte module);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getCompressor")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getCompressor(IntPtr pcm_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "setClosedLoopControl")]
        public static extern void setClosedLoopControl(IntPtr pcm_pointer, [MarshalAs(UnmanagedType.I1)] bool value, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getClosedLoopControl")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getClosedLoopControl(IntPtr pcm_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getPressureSwitch")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getPressureSwitch(IntPtr pcm_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getCompressorCurrent")]
        public static extern float getCompressorCurrent(IntPtr pcm_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getCompressorCurrentTooHighFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getCompressorCurrentTooHighFault(IntPtr pcm_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getCompressorCurrentTooHighStickyFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getCompressorCurrentTooHighStickyFault(IntPtr pcm_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getCompressorShortedStickyFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getCompressorShortedStickyFault(IntPtr pcm_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getCompressorShortedFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getCompressorShortedFault(IntPtr pcm_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getCompressorNotConnectedStickyFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getCompressorNotConnectedStickyFault(IntPtr pcm_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getCompressorNotConnectedFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getCompressorNotConnectedFault(IntPtr pcm_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "clearAllPCMStickyFaults")]
        public static extern void clearAllPCMStickyFaults(IntPtr pcm_pointer, ref int status);
    }
}
