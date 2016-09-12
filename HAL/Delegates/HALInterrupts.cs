using System;
using System.Runtime.InteropServices;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALInterrupts
    {
        static HALInterrupts()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALInterrupts>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_InitializeInterruptsDelegate([MarshalAs(UnmanagedType.Bool)]bool watcher, ref int status);
        [NativeDelegate] public static HAL_InitializeInterruptsDelegate HAL_InitializeInterrupts;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_CleanInterruptsDelegate(int interrupt_handle, ref int status);
        [NativeDelegate] public static HAL_CleanInterruptsDelegate HAL_CleanInterrupts;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate long HAL_WaitForInterruptDelegate(int interrupt_handle, double timeout, [MarshalAs(UnmanagedType.Bool)]bool ignorePrevious, ref int status);
        [NativeDelegate] public static HAL_WaitForInterruptDelegate HAL_WaitForInterrupt;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_EnableInterruptsDelegate(int interrupt_handle, ref int status);
        [NativeDelegate] public static HAL_EnableInterruptsDelegate HAL_EnableInterrupts;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_DisableInterruptsDelegate(int interrupt_handle, ref int status);
        [NativeDelegate] public static HAL_DisableInterruptsDelegate HAL_DisableInterrupts;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_ReadInterruptRisingTimestampDelegate(int interrupt_handle, ref int status);
        [NativeDelegate] public static HAL_ReadInterruptRisingTimestampDelegate HAL_ReadInterruptRisingTimestamp;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_ReadInterruptFallingTimestampDelegate(int interrupt_handle, ref int status);
        [NativeDelegate] public static HAL_ReadInterruptFallingTimestampDelegate HAL_ReadInterruptFallingTimestamp;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_RequestInterruptsDelegate(int interrupt_handle, int digitalSourceHandle, HALAnalogTriggerType analogTriggerType, ref int status);
        [NativeDelegate] public static HAL_RequestInterruptsDelegate HAL_RequestInterrupts;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_AttachInterruptHandlerDelegate(int interrupt_handle, Action<uint, IntPtr> handler, IntPtr param, ref int status);
        [NativeDelegate] public static HAL_AttachInterruptHandlerDelegate HAL_AttachInterruptHandler;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetInterruptUpSourceEdgeDelegate(int interrupt_handle, [MarshalAs(UnmanagedType.Bool)]bool risingEdge, [MarshalAs(UnmanagedType.Bool)]bool fallingEdge, ref int status);
        [NativeDelegate] public static HAL_SetInterruptUpSourceEdgeDelegate HAL_SetInterruptUpSourceEdge;
    }
}

