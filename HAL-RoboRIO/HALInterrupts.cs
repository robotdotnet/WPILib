//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using HAL_Base;
namespace HAL_RoboRIO
{
    public class HALInterrupts
    {

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "initializeInterrupts")]
        public static extern System.IntPtr initializeInterrupts(uint interruptIndex, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool watcher, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "cleanInterrupts")]
        public static extern void cleanInterrupts(System.IntPtr interrupt_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "waitForInterrupt")]
        public static extern uint waitForInterrupt(System.IntPtr interrupt_pointer, double timeout, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool ignorePrevious, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "enableInterrupts")]
        public static extern void enableInterrupts(System.IntPtr interrupt_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "disableInterrupts")]
        public static extern void disableInterrupts(System.IntPtr interrupt_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "readRisingTimestamp")]
        public static extern double readRisingTimestamp(System.IntPtr interrupt_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "readFallingTimestamp")]
        public static extern double readFallingTimestamp(System.IntPtr interrupt_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "requestInterrupts")]
        public static extern void requestInterrupts(System.IntPtr interrupt_pointer, byte routing_module, uint routing_pin, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool routing_analog_trigger, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "attachInterruptHandler")]
        public static extern void attachInterruptHandler(System.IntPtr interrupt_pointer, Action<uint, IntPtr> handler, System.IntPtr param, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setInterruptUpSourceEdge")]
        public static extern void setInterruptUpSourceEdge(System.IntPtr interrupt_pointer, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool risingEdge, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool fallingEdge, ref int status);
    }
}
