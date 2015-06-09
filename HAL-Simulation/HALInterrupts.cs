
using System;
using System.Runtime.InteropServices;

namespace HAL_FRC
{
    /// Return Type: void
    ///interruptAssertedMask: unsigned int
    ///param: void*
    //public delegate void InterruptHandlerFunctionHAL(uint interruptAssertedMask, System.IntPtr param);

    public class HALInterrupts
    {
        /// Return Type: void*
        ///interruptIndex: unsigned int
        ///watcher: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeInterrupts")]
        public static extern IntPtr initializeInterrupts(uint interruptIndex, [MarshalAs(UnmanagedType.I1)] bool watcher, ref int status);


        /// Return Type: void
        ///interrupt_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "cleanInterrupts")]
        public static extern void cleanInterrupts(IntPtr interrupt_pointer, ref int status);


        /// Return Type: unsigned int
        ///interrupt_pointer: void*
        ///timeout: double
        ///ignorePrevious: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "waitForInterrupt")]
        public static extern uint waitForInterrupt(IntPtr interrupt_pointer, double timeout, [MarshalAs(UnmanagedType.I1)] bool ignorePrevious, ref int status);


        /// Return Type: void
        ///interrupt_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "enableInterrupts")]
        public static extern void enableInterrupts(IntPtr interrupt_pointer, ref int status);


        /// Return Type: void
        ///interrupt_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "disableInterrupts")]
        public static extern void disableInterrupts(IntPtr interrupt_pointer, ref int status);


        /// Return Type: double
        ///interrupt_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "readRisingTimestamp")]
        public static extern double readRisingTimestamp(IntPtr interrupt_pointer, ref int status);


        /// Return Type: double
        ///interrupt_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "readFallingTimestamp")]
        public static extern double readFallingTimestamp(IntPtr interrupt_pointer, ref int status);


        /// Return Type: void
        ///interrupt_pointer: void*
        ///routing_module: byte
        ///routing_pin: unsigned int
        ///routing_analog_trigger: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "requestInterrupts")]
        public static extern void requestInterrupts(IntPtr interrupt_pointer, byte routing_module, uint routing_pin, [MarshalAs(UnmanagedType.I1)] bool routing_analog_trigger, ref int status);


        /// Return Type: void
        ///interrupt_pointer: void*
        ///handler: InterruptHandlerFunction
        ///param: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "attachInterruptHandler")]
        public static extern void attachInterruptHandler(IntPtr interrupt_pointer, Action<uint, IntPtr> handler, IntPtr param, ref int status);


        /// Return Type: void
        ///interrupt_pointer: void*
        ///risingEdge: boolean
        ///fallingEdge: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setInterruptUpSourceEdge")]
        public static extern void setInterruptUpSourceEdge(IntPtr interrupt_pointer, [MarshalAs(UnmanagedType.I1)] bool risingEdge, [MarshalAs(UnmanagedType.I1)] bool fallingEdge, ref int status);
    }
}
