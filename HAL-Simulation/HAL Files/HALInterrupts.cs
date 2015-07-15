using System;
using System.Threading;
using static HAL_Simulator.SimData;
using static HAL_Simulator.HALErrorConstants;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    public class HALInterrupts
    {
        private enum WaitResult
        {
            Timeout = 0x0,
            RisingEdge = 0x1,
            FallingEdge = 0x100,
            Both = 0x101,
        }

        public const int NumInterrupts = 8;

        //Holds a list of our interrupts
        private static Interrupt[] Interrupts = new Interrupt[NumInterrupts];

        //Gets an interrupt from and IntPtr
        //Since 0 is equal to IntPtr.Zero, and we check for that in the WPILib,
        //We have to use index's 1-8, instead of 0-7. So when we create in 
        //initializeInterrupt, we add one to the index, and when we get the interrupt
        //here, we subtract 1.
        private static Interrupt GetInterrupt(IntPtr ptr)
        {
            return Interrupts[ptr.ToInt32() -1];
        }

        /// <summary>
        /// Initialized an Interrupt.
        /// </summary>
        /// <param name="interruptIndex">The Interrupt index (0-7)</param>
        /// <param name="watcher">This tells if we are using synchronous interrupts</param>
        /// <param name="status">Return Status</param>
        /// <returns>Interrupt Pointer</returns>
        public static IntPtr initializeInterrupts(uint interruptIndex, bool watcher,
            ref int status)
        {
            //Check to see if we are already allocated, or if we have already allocated 8.
            if (Interrupts[interruptIndex] != null)
            {
                status = RESOURCE_IS_ALLOCATED;
                return IntPtr.Zero;
            }
            if (interruptIndex > 7)
            {
                status = NO_AVAILABLE_RESOURCES;
                return IntPtr.Zero;
            }

            Interrupt interrupt = new Interrupt
            {
                Callback = null,
                Watcher = watcher,
                DIOPin = 0,
            };
            status = NiFpga_Status_Success;
            Interrupts[interruptIndex] = interrupt;
            //Returns + 1. See GetInterrupts comments for the reason.
            return (IntPtr)interruptIndex + 1;
        }


        /// <summary>
        /// Cleans an Interrupt
        /// </summary>
        /// <param name="interrupt_pointer">The Interrupt Pointer</param>
        /// <param name="status">Return Status</param>
        public static void cleanInterrupts(IntPtr interrupt_pointer, ref int status)
        {
            status = NiFpga_Status_Success;
            Interrupt interrupt = GetInterrupt(interrupt_pointer);
            if (interrupt.DictCallback != null)
            {
                halData["dio"][interrupt.DIOPin].Cancel("value", interrupt.DictCallback);
            }
            interrupt.DictCallback = null;
            Interrupts[interrupt_pointer.ToInt32() - 1] = null;
        }

        /// <summary>
        /// Wait for an Interrupt Synchronously.
        /// </summary>
        /// <param name="interrupt_pointer"></param>
        /// <param name="timeout"></param>
        /// <param name="ignorePrevious"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static uint waitForInterrupt(IntPtr interrupt_pointer, double timeout, bool ignorePrevious,
            ref int status)
        {
            
            status = NiFpga_Status_Success;
            Interrupt interrupt = GetInterrupt(interrupt_pointer);

            //Throw invalid parameter if we are called, but are an async interrupt.
            //This matches what the fpga does
            if (!interrupt.Watcher)
            {
                status = NiFpga_Status_InvalidParameter;
                return (uint)WaitResult.Timeout;
            }
            object lockObject = new object();

            //Store the previous state of the interrupt, so we can check for rising or falling edges
            interrupt.PreviousState = halData["dio"][interrupt.DIOPin]["value"];

            interrupt.DictCallback = (k, v) =>
            {
                //If no change, do nothing
                if (v == interrupt.PreviousState)
                    return;
                //If its a falling change, and we dont fire on falling return
                if (interrupt.PreviousState && !interrupt.FireOnDown)
                    return;
                //If its a rising change, and we dont fire on rising return.
                if (!interrupt.PreviousState && !interrupt.FireOnUp)
                    return;
                //Otherwise pulse the lock.
                lock (lockObject)
                {
                    Monitor.PulseAll(lockObject);
                }
            };

            //Register our interrupt with the NotifyDict
            halData["dio"][interrupt.DIOPin].Register("value", interrupt.DictCallback);

            WaitResult retVal = WaitResult.Timeout;
            //We are using a lock to wait for the interrupt.
            lock (lockObject)
            {
                bool timedout = !Monitor.Wait(lockObject, TimeSpan.FromSeconds(timeout));
                //Cancel the interrupt, because we don't want it to fire again.
                halData["dio"][interrupt.DIOPin].Cancel("value", interrupt.DictCallback);
                if (timedout)
                {
                    retVal = WaitResult.Timeout;
                }
                else
                {
                    //True => false, Falling
                    if (interrupt.PreviousState)
                    {
                        //Set our return value and our timestamps
                        retVal = WaitResult.FallingEdge;
                        interrupt.FallingTimestamp = SimHooks.GetFPGATimestamp();
                        interrupt.RisingTimestamp = 0;
                    }
                    else
                    {
                        retVal = WaitResult.RisingEdge;
                        interrupt.RisingTimestamp = SimHooks.GetFPGATimestamp();
                        interrupt.FallingTimestamp = 0;
                    }
                }
            }

            return (uint)retVal;

        }

        /// <summary>
        /// Enable async interrupts
        /// </summary>
        /// <param name="interrupt_pointer"></param>
        /// <param name="status"></param>
        public static void enableInterrupts(IntPtr interrupt_pointer, ref int status)
        {
            status = NiFpga_Status_Success;

            Interrupt interrupt = GetInterrupt(interrupt_pointer);
            if (interrupt == null)
            {
                //We throw an error here instead of returning a status since the HAL doesnt check for
                //null, and would instead just segfault.
                throw new ArgumentNullException(nameof(interrupt_pointer), "The interrupt cannot be null");
            }
            interrupt.DictCallback = (k, v) =>
            {
                //Since the NotifyDict will fire even if the state is the same,
                //We ignore if the state is the same.
                if (v == interrupt.PreviousState)
                    return;
                //True => false, Falling
                if (interrupt.PreviousState)
                {
                    //Set previous state, so next trigger will work.
                    interrupt.PreviousState = v;
                    //If we dont fire on down, return
                    if (!interrupt.FireOnDown)
                        return;
                    //Set out timestamps
                    interrupt.FallingTimestamp = SimHooks.GetFPGATimestamp();
                    interrupt.RisingTimestamp = 0;
                }
                else
                {
                    interrupt.PreviousState = v;
                    //If we dont fire on up, return
                    if (!interrupt.FireOnUp)
                        return;
                    interrupt.RisingTimestamp = SimHooks.GetFPGATimestamp();
                    interrupt.FallingTimestamp = 0;
                }
                //Call our callback in a new thread. This is what the FPGA does as well.
                new Thread(() => {
                    interrupt.Callback((uint) interrupt.DIOPin, interrupt.Param);
                }).Start();
            };
            //Set our previous state, and register
            interrupt.PreviousState = halData["dio"][interrupt.DIOPin]["value"];
            halData["dio"][interrupt.DIOPin].Register("value", interrupt.DictCallback, false);
        }

        /// <summary>
        /// Disable interrupts without deallocating it.
        /// </summary>
        /// <param name="interrupt_pointer"></param>
        /// <param name="status"></param>
        public static void disableInterrupts(IntPtr interrupt_pointer, ref int status)
        {
            status = NiFpga_Status_Success;
            Interrupt interrupt = GetInterrupt(interrupt_pointer);
            halData["dio"][interrupt.DIOPin].Cancel("value", interrupt.DictCallback);
        }

        /// <summary>
        /// Return rising timestamp
        /// </summary>
        /// <param name="interrupt_pointer"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static double readRisingTimestamp(IntPtr interrupt_pointer, ref int status)
        {
            status = NiFpga_Status_Success;
            return GetInterrupt(interrupt_pointer).RisingTimestamp;
        }

        /// <summary>
        /// Return falling timestamp
        /// </summary>
        /// <param name="interrupt_pointer"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static double readFallingTimestamp(IntPtr interrupt_pointer, ref int status)
        {
            status = NiFpga_Status_Success;
            return GetInterrupt(interrupt_pointer).FallingTimestamp;
        }

        /// <summary>
        /// Request our interrupts on a specific port.
        /// Currently, we do not support analog triggers. They are going to be
        /// much harder to implement.
        /// </summary>
        /// <param name="interrupt_pointer"></param>
        /// <param name="routing_module">Our module (must be 0)</param>
        /// <param name="routing_pin">Our DIO Pin</param>
        /// <param name="routing_analog_trigger">If analog trigger (must be false)</param>
        /// <param name="status"></param>
        public static void requestInterrupts(IntPtr interrupt_pointer, byte routing_module, uint routing_pin,
            bool routing_analog_trigger, ref int status)
        {
            Interrupt interrupt = GetInterrupt(interrupt_pointer);
            if (routing_analog_trigger)
                throw new NotImplementedException("We currently do not support interrupting on an analog port.");
            if (routing_module != 0)
                throw new ArgumentOutOfRangeException(nameof(routing_module), "Routing module must be 0.");
            status = NiFpga_Status_Success;
            interrupt.DIOPin = (int)routing_pin;
        }

        /// <summary>
        /// Attach a handler to our interrupt.
        /// </summary>
        /// <param name="interrupt_pointer"></param>
        /// <param name="handler"></param>
        /// <param name="param"></param>
        /// <param name="status"></param>
        public static void attachInterruptHandler(IntPtr interrupt_pointer, Action<uint, IntPtr> handler, IntPtr param,
            ref int status)
        {
            status = NiFpga_Status_Success;
            Interrupt interrupt = GetInterrupt(interrupt_pointer);
            interrupt.Callback = handler;
            interrupt.Param = param;
        }

        /// <summary>
        /// Sets if our interrupt runs on the up or down source.
        /// </summary>
        /// <param name="interrupt_pointer"></param>
        /// <param name="risingEdge"></param>
        /// <param name="fallingEdge"></param>
        /// <param name="status"></param>
        public static void setInterruptUpSourceEdge(IntPtr interrupt_pointer, bool risingEdge, bool fallingEdge,
            ref int status)
        {
            status = NiFpga_Status_Success;
            Interrupt interrupt = GetInterrupt(interrupt_pointer);
            interrupt.FireOnUp = risingEdge;
            interrupt.FireOnDown = fallingEdge;
        }
    }
}
