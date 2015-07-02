
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace HAL_Simulator
{
    /// Return Type: void
    ///interruptAssertedMask: unsigned int
    ///param: void*
    //public delegate void InterruptHandlerFunctionHAL(uint interruptAssertedMask, System.IntPtr param);

    public class HALInterrupts
    {
        private static List<Interrupt> Interrupts = new List<Interrupt>(); 

        /// Return Type: void*
        ///interruptIndex: unsigned int
        ///watcher: boolean
        ///status: int*
        public static IntPtr initializeInterrupts(uint interruptIndex, bool watcher,
            ref int status)
        {
            Interrupt interrupt = new Interrupt
            {
                idx = interruptIndex,
                Callback = null,
                isSynchronous = watcher
            };
            status = 0;
            Interrupts.Add(interrupt);
            return (IntPtr)Interrupts.IndexOf(interrupt);
        }


        /// Return Type: void
        ///interrupt_pointer: void*
        ///status: int*
        public static void cleanInterrupts(IntPtr interrupt_pointer, ref int status)
        {
            status = 0;
            Interrupt interrupt = Interrupts[interrupt_pointer.ToInt32()];
            interrupt.Callback = null;
            Interrupts.Remove(interrupt);
        }


        /// Return Type: unsigned int
        ///interrupt_pointer: void*
        ///timeout: double
        ///ignorePrevious: boolean
        ///status: int*
        public static uint waitForInterrupt(IntPtr interrupt_pointer, double timeout, bool ignorePrevious,
            ref int status)
        {
            Interrupt interrupt = Interrupts[interrupt_pointer.ToInt32()];

            
        }
            


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
