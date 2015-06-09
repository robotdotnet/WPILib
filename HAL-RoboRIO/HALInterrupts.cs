//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;

namespace HAL_RoboRIO
{
    public class HALInterrupts
    {

        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeInterrupts")]
        public static extern IntPtr initializeInterrupts(uint interruptIndex, [MarshalAs(UnmanagedType.I1)] bool watcher, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "cleanInterrupts")]
        public static extern void cleanInterrupts(IntPtr interrupt_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "waitForInterrupt")]
        public static extern uint waitForInterrupt(IntPtr interrupt_pointer, double timeout, [MarshalAs(UnmanagedType.I1)] bool ignorePrevious, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "enableInterrupts")]
        public static extern void enableInterrupts(IntPtr interrupt_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "disableInterrupts")]
        public static extern void disableInterrupts(IntPtr interrupt_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "readRisingTimestamp")]
        public static extern double readRisingTimestamp(IntPtr interrupt_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "readFallingTimestamp")]
        public static extern double readFallingTimestamp(IntPtr interrupt_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "requestInterrupts")]
        public static extern void requestInterrupts(IntPtr interrupt_pointer, byte routing_module, uint routing_pin, [MarshalAs(UnmanagedType.I1)] bool routing_analog_trigger, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "attachInterruptHandler")]
        public static extern void attachInterruptHandler(IntPtr interrupt_pointer, Action<uint, IntPtr> handler, IntPtr param, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "setInterruptUpSourceEdge")]
        public static extern void setInterruptUpSourceEdge(IntPtr interrupt_pointer, [MarshalAs(UnmanagedType.I1)] bool risingEdge, [MarshalAs(UnmanagedType.I1)] bool fallingEdge, ref int status);
    }
}
