using System;
using System.Threading;
using HAL.Base;
using HAL.Simulator;
using HAL.Simulator.Data;
using static HAL.Simulator.SimData;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming

#pragma warning disable 1591

namespace HAL.SimulatorHAL
{
    ///<inheritdoc cref="HAL"/>
    internal class HALInterrupts
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
        internal static readonly bool[] AllocatedInterrupts = new bool[NumInterrupts];

        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            for (int i = 0; i < AllocatedInterrupts.Length; i++)
            {
                AllocatedInterrupts[i] = false;
            }

            Base.HALInterrupts.InitializeInterrupts = initializeInterrupts;
            Base.HALInterrupts.CleanInterrupts = cleanInterrupts;
            Base.HALInterrupts.WaitForInterrupt = waitForInterrupt;
            Base.HALInterrupts.EnableInterrupts = enableInterrupts;
            Base.HALInterrupts.DisableInterrupts = disableInterrupts;
            Base.HALInterrupts.ReadRisingTimestamp = readRisingTimestamp;
            Base.HALInterrupts.ReadFallingTimestamp = readFallingTimestamp;
            Base.HALInterrupts.RequestInterrupts = requestInterrupts;
            Base.HALInterrupts.AttachInterruptHandler = attachInterruptHandler;
            Base.HALInterrupts.SetInterruptUpSourceEdge = setInterruptUpSourceEdge;
        }

        //Gets an interrupt from an IntPtr
        //Since 0 is equal to IntPtr.Zero, and we check for that in the WPILib,
        //We have to use index's 1-8, instead of 0-7. So when we create in 
        //initializeInterrupt, we add one to the index, and when we get the interrupt
        //here, we subtract 1.
        private static Interrupt GetInterrupt(InterruptSafeHandle ptr)
        {
            return ptr.GetSimulatorPort();
        }

        /// <summary>
        /// Initialized an Interrupt.
        /// </summary>
        /// <param name="interruptIndex">The Interrupt index (0-7)</param>
        /// <param name="watcher">This tells if we are using synchronous interrupts</param>
        /// <param name="status">Return Status</param>
        /// <returns>Interrupt Pointer</returns>
        [CalledSimFunction]
        public static InterruptSafeHandle initializeInterrupts(uint interruptIndex, bool watcher,
            ref int status)
        {
            //Check to see if we are already allocated, or if we have already allocated 8.
            if (AllocatedInterrupts[interruptIndex])
            {
                status = HALErrorConstants.RESOURCE_IS_ALLOCATED;
                return null;
            }
            if (interruptIndex >= NumInterrupts)
            {
                status = HALErrorConstants.NO_AVAILABLE_RESOURCES;
                return null;
            }

            Interrupt interrupt = new Interrupt
            {
                Callback = null,
                Watcher = watcher,
                Pin = -1,
                Index = interruptIndex,
            };
            status = HALErrorConstants.NiFpga_Status_Success;
            AllocatedInterrupts[interruptIndex] = true;
            //Interrupts[interruptIndex] = interrupt;
            //Returns + 1. See GetInterrupts comments for the reason.
            return new InterruptSafeHandle(interrupt);
        }


        /// <summary>
        /// Cleans an Interrupt
        /// </summary>
        /// <param name="interrupt_pointer">The Interrupt Pointer</param>
        /// <param name="status">Return Status</param>
        [CalledSimFunction]
        public static void cleanInterrupts(InterruptSafeHandle interrupt_pointer, ref int status)
        {
            status = HALErrorConstants.NiFpga_Status_Success;
            Interrupt interrupt = GetInterrupt(interrupt_pointer);
            if (interrupt.DictCallback != null)
            {
                if (interrupt.IsAnalog)
                {
                    AnalogTriggerData trigData = SimData.AnalogTrigger[interrupt.Pin];
                    var ain = AnalogIn[trigData.AnalogPin];
                    ain.Cancel(nameof(ain.Voltage), interrupt.DictCallback);
                }
                else
                {
                    var dio = DIO[interrupt.Pin];
                    dio.Cancel(nameof(dio.Value), interrupt.DictCallback);
                }
            }
            interrupt.DictCallback = null;
            AllocatedInterrupts[interrupt.Index] = false;
        }

        private static uint WaitForInterruptDigital(Interrupt interrupt, double timeout, bool ignorePrevious)
        {
            object lockObject = new object();

            //Store the previous state of the interrupt, so we can check for rising or falling edges
            interrupt.PreviousState = DIO[interrupt.Pin].Value;

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
            var dio = DIO[interrupt.Pin];
            dio.Register(nameof(dio.Value), interrupt.DictCallback);

            WaitResult retVal = WaitResult.Timeout;
            //We are using a lock to wait for the interrupt.
            lock (lockObject)
            {
                bool timedout = !Monitor.Wait(lockObject, TimeSpan.FromSeconds(timeout));
                //Cancel the interrupt, because we don't want it to fire again.
                dio.Cancel(nameof(dio.Value), interrupt.DictCallback);
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

        private static uint WaitForInterruptAnalog(Interrupt interrupt, double timeout, bool ignorePrevious, ref int status)
        {
            object lockObject = new object();

            AnalogTriggerData trigData = SimData.AnalogTrigger[interrupt.Pin];

            //Store the previous state of the interrupt, so we can check for rising or falling edges
            interrupt.PreviousState = trigData.GetTriggerValue(interrupt.AnalogType, ref status);


            interrupt.DictCallback = (k, val) =>
            {
                //V is new analog value
                int status2 = 0;
                bool v = trigData.GetTriggerValue(interrupt.AnalogType, ref status2);
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
            var aIn = AnalogIn[trigData.AnalogPin];
            aIn.Register(nameof(aIn.Voltage), interrupt.DictCallback);

            WaitResult retVal = WaitResult.Timeout;
            //We are using a lock to wait for the interrupt.
            lock (lockObject)
            {
                bool timedout = !Monitor.Wait(lockObject, TimeSpan.FromSeconds(timeout));
                //Cancel the interrupt, because we don't want it to fire again.
                aIn.Cancel(nameof(aIn.Voltage), interrupt.DictCallback);
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
        /// Wait for an Interrupt Synchronously.
        /// </summary>
        /// <param name="interrupt_pointer"></param>
        /// <param name="timeout"></param>
        /// <param name="ignorePrevious"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [CalledSimFunction]
        public static uint waitForInterrupt(InterruptSafeHandle interrupt_pointer, double timeout, bool ignorePrevious,
            ref int status)
        {

            status = HALErrorConstants.NiFpga_Status_Success;
            Interrupt interrupt = GetInterrupt(interrupt_pointer);

            //Throw invalid parameter if we are called, but are an async interrupt.
            //This matches what the fpga does
            if (!interrupt.Watcher)
            {
                status = HALErrorConstants.NiFpga_Status_InvalidParameter;
                return (uint)WaitResult.Timeout;
            }
            if (interrupt.IsAnalog)
            {
                return WaitForInterruptAnalog(interrupt, timeout, ignorePrevious, ref status);
            }
            else
            {
                return WaitForInterruptDigital(interrupt, timeout, ignorePrevious);
            }

        }

        private static void enableInterruptsAnalog(Interrupt interrupt, ref int status)
        {
            AnalogTriggerData trigData = SimData.AnalogTrigger[interrupt.Pin];

            interrupt.DictCallback = (k, val) =>
            {

                int status2 = 0;
                bool v = trigData.GetTriggerValue(interrupt.AnalogType, ref status2);
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
                new Thread(() =>
                {
                    interrupt.Callback((uint)interrupt.Pin, interrupt.Param);
                }).Start();
            };
            //Set our previous state, and register
            interrupt.PreviousState = trigData.GetTriggerValue(interrupt.AnalogType, ref status);
            var aIn = AnalogIn[trigData.AnalogPin];
            aIn.Register(nameof(aIn.Voltage), interrupt.DictCallback);
        }

        private static void enableInterruptsDigital(Interrupt interrupt, ref int status)
        {
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
                new Thread(() =>
                {
                    interrupt.Callback((uint)interrupt.Pin, interrupt.Param);
                }).Start();
            };
            //Set our previous state, and register
            interrupt.PreviousState = DIO[interrupt.Pin].Value;
            var dio = DIO[interrupt.Pin];
            dio.Register(nameof(dio.Value), interrupt.DictCallback);
        }

        /// <summary>
        /// Enable async interrupts
        /// </summary>
        /// <param name="interrupt_pointer"></param>
        /// <param name="status"></param>
        [CalledSimFunction]
        public static void enableInterrupts(InterruptSafeHandle interrupt_pointer, ref int status)
        {
            status = HALErrorConstants.NiFpga_Status_Success;

            Interrupt interrupt = GetInterrupt(interrupt_pointer);
            if (interrupt == null)
            {
                //We throw an error here instead of returning a status since the HAL doesnt check for
                //null, and would instead just segfault.
                throw new ArgumentNullException(nameof(interrupt_pointer), "The interrupt cannot be null");
            }

            if (interrupt.IsAnalog)
            {
                enableInterruptsAnalog(interrupt, ref status);
            }
            else
            {
                enableInterruptsDigital(interrupt, ref status);
            }
        }

        /// <summary>
        /// Disable interrupts without deallocating it.
        /// </summary>
        /// <param name="interrupt_pointer"></param>
        /// <param name="status"></param>
        [CalledSimFunction]
        public static void disableInterrupts(InterruptSafeHandle interrupt_pointer, ref int status)
        {
            status = HALErrorConstants.NiFpga_Status_Success;
            Interrupt interrupt = GetInterrupt(interrupt_pointer);


            if (interrupt.IsAnalog)
            {
                AnalogTriggerData trigData = SimData.AnalogTrigger[interrupt.Pin];
                var ain = AnalogIn[trigData.AnalogPin];
                ain.Cancel(nameof(ain.Voltage), interrupt.DictCallback);
            }
            else
            {
                var dio = DIO[interrupt.Pin];
                dio.Cancel(nameof(dio.Value), interrupt.DictCallback);
            }
        }

        /// <summary>
        /// Return rising timestamp
        /// </summary>
        /// <param name="interrupt_pointer"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [CalledSimFunction]
        public static double readRisingTimestamp(InterruptSafeHandle interrupt_pointer, ref int status)
        {
            status = HALErrorConstants.NiFpga_Status_Success;
            return GetInterrupt(interrupt_pointer).RisingTimestamp;
        }

        /// <summary>
        /// Return falling timestamp
        /// </summary>
        /// <param name="interrupt_pointer"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [CalledSimFunction]
        public static double readFallingTimestamp(InterruptSafeHandle interrupt_pointer, ref int status)
        {
            status = HALErrorConstants.NiFpga_Status_Success;
            return GetInterrupt(interrupt_pointer).FallingTimestamp;
        }

        /// <summary>
        /// Request our interrupts on a specific port.
        /// Currently, we do not support IsAnalog triggers. They are going to be
        /// much harder to implement.
        /// </summary>
        /// <param name="interrupt_pointer"></param>
        /// <param name="routing_module">Our module (must be 0)</param>
        /// <param name="routing_pin">Our DIO AnalogPin</param>
        /// <param name="routing_analog_trigger">If IsAnalog trigger (must be false)</param>
        /// <param name="status"></param>
        [CalledSimFunction]
        public static void requestInterrupts(InterruptSafeHandle interrupt_pointer, byte routing_module, uint routing_pin,
            bool routing_analog_trigger, ref int status)
        {
            Interrupt interrupt = GetInterrupt(interrupt_pointer);

            if (routing_module != 0)
                throw new ArgumentOutOfRangeException(nameof(routing_module), "Routing module must be 0.");

            if (routing_analog_trigger)
            {
                uint mask = (1 << 2) - 1;
                AnalogTriggerType triggerType = (AnalogTriggerType)(routing_pin & mask);
                int index = routing_module >> 2;

                interrupt.IsAnalog = true;

                if (triggerType == AnalogTriggerType.InWindow || triggerType == AnalogTriggerType.State)
                {
                    interrupt.Pin = index;
                    interrupt.AnalogType = triggerType;
                }
                else
                {
                    //What the other states do is complicated. It actually triggers if a 
                    //continous output rolls over. 
                    throw new InvalidOperationException("We do not support rollover analog triggers yet.");
                }


            }
            else
            {
                interrupt.Pin = (int)routing_pin;
            }
            status = HALErrorConstants.NiFpga_Status_Success;
        }

        /// <summary>
        /// Attach a handler to our interrupt.
        /// </summary>
        /// <param name="interrupt_pointer"></param>
        /// <param name="handler"></param>
        /// <param name="param"></param>
        /// <param name="status"></param>
        [CalledSimFunction]
        public static void attachInterruptHandler(InterruptSafeHandle interrupt_pointer, Action<uint, IntPtr> handler, IntPtr param,
            ref int status)
        {
            status = HALErrorConstants.NiFpga_Status_Success;
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
        [CalledSimFunction]
        public static void setInterruptUpSourceEdge(InterruptSafeHandle interrupt_pointer, bool risingEdge, bool fallingEdge,
            ref int status)
        {
            status = HALErrorConstants.NiFpga_Status_Success;
            Interrupt interrupt = GetInterrupt(interrupt_pointer);
            interrupt.FireOnUp = risingEdge;
            interrupt.FireOnDown = fallingEdge;
        }
    }
}
