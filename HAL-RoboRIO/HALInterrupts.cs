//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALInterrupts
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALInterrupts.InitializeInterrupts = (HAL_Base.HALInterrupts.InitializeInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeInterrupts"), typeof(HAL_Base.HALInterrupts.InitializeInterruptsDelegate));

            HAL_Base.HALInterrupts.CleanInterrupts = (HAL_Base.HALInterrupts.CleanInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "cleanInterrupts"), typeof(HAL_Base.HALInterrupts.CleanInterruptsDelegate));

            HAL_Base.HALInterrupts.WaitForInterrupt = (HAL_Base.HALInterrupts.WaitForInterruptDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "waitForInterrupt"), typeof(HAL_Base.HALInterrupts.WaitForInterruptDelegate));

            HAL_Base.HALInterrupts.EnableInterrupts = (HAL_Base.HALInterrupts.EnableInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "enableInterrupts"), typeof(HAL_Base.HALInterrupts.EnableInterruptsDelegate));

            HAL_Base.HALInterrupts.DisableInterrupts = (HAL_Base.HALInterrupts.DisableInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "disableInterrupts"), typeof(HAL_Base.HALInterrupts.DisableInterruptsDelegate));

            HAL_Base.HALInterrupts.ReadRisingTimestamp = (HAL_Base.HALInterrupts.ReadRisingTimestampDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "readRisingTimestamp"), typeof(HAL_Base.HALInterrupts.ReadRisingTimestampDelegate));

            HAL_Base.HALInterrupts.ReadFallingTimestamp = (HAL_Base.HALInterrupts.ReadFallingTimestampDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "readFallingTimestamp"), typeof(HAL_Base.HALInterrupts.ReadFallingTimestampDelegate));

            HAL_Base.HALInterrupts.RequestInterrupts = (HAL_Base.HALInterrupts.RequestInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "requestInterrupts"), typeof(HAL_Base.HALInterrupts.RequestInterruptsDelegate));

            HAL_Base.HALInterrupts.AttachInterruptHandler = (HAL_Base.HALInterrupts.AttachInterruptHandlerDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "attachInterruptHandler"), typeof(HAL_Base.HALInterrupts.AttachInterruptHandlerDelegate));

            HAL_Base.HALInterrupts.SetInterruptUpSourceEdge = (HAL_Base.HALInterrupts.SetInterruptUpSourceEdgeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setInterruptUpSourceEdge"), typeof(HAL_Base.HALInterrupts.SetInterruptUpSourceEdgeDelegate));

        }



        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeInterrupts")]
        public static extern IntPtr initializeInterrupts(uint interruptIndex, [MarshalAs(UnmanagedType.I1)] bool watcher, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "cleanInterrupts")]
        public static extern void cleanInterrupts(IntPtr interrupt_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "waitForInterrupt")]
        public static extern uint waitForInterrupt(IntPtr interrupt_pointer, double timeout, [MarshalAs(UnmanagedType.I1)] bool ignorePrevious, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "enableInterrupts")]
        public static extern void enableInterrupts(IntPtr interrupt_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "disableInterrupts")]
        public static extern void disableInterrupts(IntPtr interrupt_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "readRisingTimestamp")]
        public static extern double readRisingTimestamp(IntPtr interrupt_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "readFallingTimestamp")]
        public static extern double readFallingTimestamp(IntPtr interrupt_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "requestInterrupts")]
        public static extern void requestInterrupts(IntPtr interrupt_pointer, byte routing_module, uint routing_pin, [MarshalAs(UnmanagedType.I1)] bool routing_analog_trigger, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "attachInterruptHandler")]
        public static extern void attachInterruptHandler(IntPtr interrupt_pointer, Action<uint, IntPtr> handler, IntPtr param, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setInterruptUpSourceEdge")]
        public static extern void setInterruptUpSourceEdge(IntPtr interrupt_pointer, [MarshalAs(UnmanagedType.I1)] bool risingEdge, [MarshalAs(UnmanagedType.I1)] bool fallingEdge, ref int status);
    }
}
