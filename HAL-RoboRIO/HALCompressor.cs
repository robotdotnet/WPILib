//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;

namespace HAL_RoboRIO
{
    internal class HALCompressor
    {

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeCompressor")]
        internal static extern IntPtr initializeCompressor(byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "checkCompressorModule")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool checkCompressorModule(byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCompressor")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getCompressor(IntPtr pcm_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setClosedLoopControl")]
        internal static extern void setClosedLoopControl(IntPtr pcm_pointer, [MarshalAs(UnmanagedType.I1)] bool value, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getClosedLoopControl")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getClosedLoopControl(IntPtr pcm_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPressureSwitch")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getPressureSwitch(IntPtr pcm_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCompressorCurrent")]
        internal static extern float getCompressorCurrent(IntPtr pcm_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCompressorCurrentTooHighFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getCompressorCurrentTooHighFault(IntPtr pcm_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCompressorCurrentTooHighStickyFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getCompressorCurrentTooHighStickyFault(IntPtr pcm_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCompressorShortedStickyFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getCompressorShortedStickyFault(IntPtr pcm_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCompressorShortedFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getCompressorShortedFault(IntPtr pcm_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCompressorNotConnectedStickyFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getCompressorNotConnectedStickyFault(IntPtr pcm_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCompressorNotConnectedFault")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getCompressorNotConnectedFault(IntPtr pcm_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "clearAllPCMStickyFaults")]
        internal static extern void clearAllPCMStickyFaults(IntPtr pcm_pointer, ref int status);
    }
}
