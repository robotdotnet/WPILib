//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;

namespace HAL_RoboRIO
{
    internal class HALInterrupts
    {

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeInterrupts")]
        internal static extern IntPtr initializeInterrupts(uint interruptIndex, [MarshalAs(UnmanagedType.I1)] bool watcher, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "cleanInterrupts")]
        internal static extern void cleanInterrupts(IntPtr interrupt_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "waitForInterrupt")]
        internal static extern uint waitForInterrupt(IntPtr interrupt_pointer, double timeout, [MarshalAs(UnmanagedType.I1)] bool ignorePrevious, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "enableInterrupts")]
        internal static extern void enableInterrupts(IntPtr interrupt_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "disableInterrupts")]
        internal static extern void disableInterrupts(IntPtr interrupt_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "readRisingTimestamp")]
        internal static extern double readRisingTimestamp(IntPtr interrupt_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "readFallingTimestamp")]
        internal static extern double readFallingTimestamp(IntPtr interrupt_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "requestInterrupts")]
        internal static extern void requestInterrupts(IntPtr interrupt_pointer, byte routing_module, uint routing_pin, [MarshalAs(UnmanagedType.I1)] bool routing_analog_trigger, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "attachInterruptHandler")]
        internal static extern void attachInterruptHandler(IntPtr interrupt_pointer, Action<uint, IntPtr> handler, IntPtr param, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setInterruptUpSourceEdge")]
        internal static extern void setInterruptUpSourceEdge(IntPtr interrupt_pointer, [MarshalAs(UnmanagedType.I1)] bool risingEdge, [MarshalAs(UnmanagedType.I1)] bool fallingEdge, ref int status);
    }
}
