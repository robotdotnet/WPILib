//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace

namespace HAL_Base
{
    public partial class HALCompressor
    {
        static HALCompressor()
        {
            HAL.Initialize();
        }

        public delegate IntPtr InitializeCompressorDelegate(byte module);
        public static InitializeCompressorDelegate InitializeCompressor;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool CheckCompressorModuleDelegate(byte module);
        public static CheckCompressorModuleDelegate CheckCompressorModule;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetCompressorDelegate(IntPtr pcm_pointer, ref int status);
        public static GetCompressorDelegate GetCompressor;

        public delegate void SetClosedLoopControlDelegate(IntPtr pcm_pointer, [MarshalAs(UnmanagedType.I1)]bool value, ref int status);
        public static SetClosedLoopControlDelegate SetClosedLoopControl;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetClosedLoopControlDelegate(IntPtr pcm_pointer, ref int status);
        public static GetClosedLoopControlDelegate GetClosedLoopControl;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetPressureSwitchDelegate(IntPtr pcm_pointer, ref int status);
        public static GetPressureSwitchDelegate GetPressureSwitch;

        public delegate float GetCompressorCurrentDelegate(IntPtr pcm_pointer, ref int status);
        public static GetCompressorCurrentDelegate GetCompressorCurrent;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetCompressorCurrentTooHighFaultDelegate(IntPtr pcm_pointer, ref int status);
        public static GetCompressorCurrentTooHighFaultDelegate GetCompressorCurrentTooHighFault;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetCompressorCurrentTooHighStickyFaultDelegate(IntPtr pcm_pointer, ref int status);
        public static GetCompressorCurrentTooHighStickyFaultDelegate GetCompressorCurrentTooHighStickyFault;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetCompressorShortedStickyFaultDelegate(IntPtr pcm_pointer, ref int status);
        public static GetCompressorShortedStickyFaultDelegate GetCompressorShortedStickyFault;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetCompressorShortedFaultDelegate(IntPtr pcm_pointer, ref int status);
        public static GetCompressorShortedFaultDelegate GetCompressorShortedFault;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetCompressorNotConnectedStickyFaultDelegate(IntPtr pcm_pointer, ref int status);
        public static GetCompressorNotConnectedStickyFaultDelegate GetCompressorNotConnectedStickyFault;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetCompressorNotConnectedFaultDelegate(IntPtr pcm_pointer, ref int status);
        public static GetCompressorNotConnectedFaultDelegate GetCompressorNotConnectedFault;

        public delegate void ClearAllPCMStickyFaultsDelegate(IntPtr pcm_pointer, ref int status);
        public static ClearAllPCMStickyFaultsDelegate ClearAllPCMStickyFaults;
    }
}
