//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace

namespace HAL.Base
{
    public partial class HALInterrupts
    {
        static HALInterrupts()
        {
            HAL.Initialize();
        }

        public delegate InterruptSafeHandle InitializeInterruptsDelegate(uint interruptIndex, [MarshalAs(UnmanagedType.I1)]bool watcher, ref int status);
        public static InitializeInterruptsDelegate InitializeInterrupts;

        public delegate void CleanInterruptsDelegate(InterruptSafeHandle interrupt_pointer, ref int status);
        public static CleanInterruptsDelegate CleanInterrupts;

        public delegate uint WaitForInterruptDelegate(InterruptSafeHandle interrupt_pointer, double timeout, [MarshalAs(UnmanagedType.I1)]bool ignorePrevious, ref int status);
        public static WaitForInterruptDelegate WaitForInterrupt;

        public delegate void EnableInterruptsDelegate(InterruptSafeHandle interrupt_pointer, ref int status);
        public static EnableInterruptsDelegate EnableInterrupts;

        public delegate void DisableInterruptsDelegate(InterruptSafeHandle interrupt_pointer, ref int status);
        public static DisableInterruptsDelegate DisableInterrupts;

        public delegate double ReadRisingTimestampDelegate(InterruptSafeHandle interrupt_pointer, ref int status);
        public static ReadRisingTimestampDelegate ReadRisingTimestamp;

        public delegate double ReadFallingTimestampDelegate(InterruptSafeHandle interrupt_pointer, ref int status);
        public static ReadFallingTimestampDelegate ReadFallingTimestamp;

        public delegate void RequestInterruptsDelegate(InterruptSafeHandle interrupt_pointer, byte routing_module, uint routing_pin, [MarshalAs(UnmanagedType.I1)]bool routing_analog_trigger, ref int status);
        public static RequestInterruptsDelegate RequestInterrupts;

        public delegate void AttachInterruptHandlerDelegate(InterruptSafeHandle interrupt_pointer, Action<uint, IntPtr> handler, IntPtr param, ref int status);
        public static AttachInterruptHandlerDelegate AttachInterruptHandler;

        public delegate void SetInterruptUpSourceEdgeDelegate(InterruptSafeHandle interrupt_pointer, [MarshalAs(UnmanagedType.I1)]bool risingEdge, [MarshalAs(UnmanagedType.I1)]bool fallingEdge, ref int status);
        public static SetInterruptUpSourceEdgeDelegate SetInterruptUpSourceEdge;
    }
}
