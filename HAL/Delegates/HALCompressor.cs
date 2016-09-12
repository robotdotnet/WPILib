using System.Runtime.InteropServices;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALCompressor
    {
        static HALCompressor()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALCompressor>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_InitializeCompressorDelegate(int module, ref int status);
        [NativeDelegate] public static HAL_InitializeCompressorDelegate HAL_InitializeCompressor;

        [return: MarshalAs(UnmanagedType.I4)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_CheckCompressorModuleDelegate(int module);
        [NativeDelegate] public static HAL_CheckCompressorModuleDelegate HAL_CheckCompressorModule;

        [return: MarshalAs(UnmanagedType.I4)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetCompressorDelegate(int compressor_handle, ref int status);
        [NativeDelegate] public static HAL_GetCompressorDelegate HAL_GetCompressor;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetCompressorClosedLoopControlDelegate(int compressor_handle, [MarshalAs(UnmanagedType.Bool)]bool value, ref int status);
        [NativeDelegate] public static HAL_SetCompressorClosedLoopControlDelegate HAL_SetCompressorClosedLoopControl;

        [return: MarshalAs(UnmanagedType.I4)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetCompressorClosedLoopControlDelegate(int compressor_handle, ref int status);
        [NativeDelegate] public static HAL_GetCompressorClosedLoopControlDelegate HAL_GetCompressorClosedLoopControl;

        [return: MarshalAs(UnmanagedType.I4)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetCompressorPressureSwitchDelegate(int compressor_handle, ref int status);
        [NativeDelegate] public static HAL_GetCompressorPressureSwitchDelegate HAL_GetCompressorPressureSwitch;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetCompressorCurrentDelegate(int compressor_handle, ref int status);
        [NativeDelegate] public static HAL_GetCompressorCurrentDelegate HAL_GetCompressorCurrent;

        [return: MarshalAs(UnmanagedType.I4)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetCompressorCurrentTooHighFaultDelegate(int compressor_handle, ref int status);
        [NativeDelegate] public static HAL_GetCompressorCurrentTooHighFaultDelegate HAL_GetCompressorCurrentTooHighFault;

        [return: MarshalAs(UnmanagedType.I4)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetCompressorCurrentTooHighStickyFaultDelegate(int compressor_handle, ref int status);
        [NativeDelegate] public static HAL_GetCompressorCurrentTooHighStickyFaultDelegate HAL_GetCompressorCurrentTooHighStickyFault;

        [return: MarshalAs(UnmanagedType.I4)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetCompressorShortedStickyFaultDelegate(int compressor_handle, ref int status);
        [NativeDelegate] public static HAL_GetCompressorShortedStickyFaultDelegate HAL_GetCompressorShortedStickyFault;

        [return: MarshalAs(UnmanagedType.I4)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetCompressorShortedFaultDelegate(int compressor_handle, ref int status);
        [NativeDelegate] public static HAL_GetCompressorShortedFaultDelegate HAL_GetCompressorShortedFault;

        [return: MarshalAs(UnmanagedType.I4)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetCompressorNotConnectedStickyFaultDelegate(int compressor_handle, ref int status);
        [NativeDelegate] public static HAL_GetCompressorNotConnectedStickyFaultDelegate HAL_GetCompressorNotConnectedStickyFault;

        [return: MarshalAs(UnmanagedType.I4)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetCompressorNotConnectedFaultDelegate(int compressor_handle, ref int status);
        [NativeDelegate] public static HAL_GetCompressorNotConnectedFaultDelegate HAL_GetCompressorNotConnectedFault;
    }
}

