using System;
using HAL.Base;
using WPILib.Exceptions;
using HALInterrupts = HAL.Base.HALInterrupts;

namespace WPILib
{
    /// <summary>
    /// The Result returned from a synchronous interrupt
    /// </summary>
    public enum WaitResult
    {
        /// The interrupt timed out
        Timeout = 0x0,
        /// The interrupt occured on the rising edge
        RisingEdge = 0x1,
        /// The interrupt occured on the falling edge
        FallingEdge = 0x100,
        /// The interrupt occured on both the rising and falling edge
        Both = 0x101,
    }

    /// <summary>
    /// Base for sensors to be used with interrupts.
    /// </summary>
    public abstract class InterruptableSensorBase : SensorBase
    {
        /// <summary>
        /// The interrupt resource.
        /// </summary>
        protected int Interrupt;

        /// <summary>
        /// Flags if the interrupt being allocated is synchronous.
        /// </summary>
        protected bool IsSynchronousInterrupt = false;

        /// <summary>
        /// Creates a new <see cref="InterruptableSensorBase"/>.
        /// </summary>
        protected InterruptableSensorBase()
        {
            Interrupt = 0;
        }

        private Action<uint, IntPtr> m_function;
        private object m_param;

        /// <summary>
        /// Get the port handle for routing.
        /// </summary>
        public abstract int PortHandleForRouting { get; }
        /// <summary>
        /// Get the analog trigger type.
        /// </summary>
        public abstract AnalogTriggerType AnalogTriggerTypeForRouting { get; }

        /// <summary>
        /// Gets the index of the Interrupt
        /// </summary>
        public int InterruptIndex { get; protected set; }

        /// <summary>
        /// Requests one of the 8 interrupts asynchronously on this input.
        /// </summary>
        /// <remarks>Request interrupts in asynchronous mode where the user program interrupt handler will be 
        /// called when an interrupt occurs. The default is interrupt on rising edges only.</remarks>
        /// <param name="handler">The callback that will be called whenever 
        /// there is an interrupt on this device.</param>
        public void RequestInterrupts(Action handler)
        {
            m_function = (mask, t) => handler();
            if (Interrupt != 0)
            {
                throw new AllocationException("The interrupt has already been allocated");
            }
            AllocateInterrupts(false);

            m_param = null;

            int status = 0;
            HALInterrupts.HAL_RequestInterrupts(Interrupt, PortHandleForRouting, (HALAnalogTriggerType) AnalogTriggerTypeForRouting, ref status);
            Utility.CheckStatus(status);
            SetUpSourceEdge(true, false);
            HALInterrupts.HAL_AttachInterruptHandler(Interrupt, m_function, IntPtr.Zero, ref status);
            Utility.CheckStatus(status);
        }

        /// <summary>
        /// Requests one of the 8 interrupts asynchronously on this input.
        /// </summary>
        /// <remarks>Request interrupts in asynchronous mode where the user program interrupt handler will be 
        /// called when an interrupt occurs. The default is interrupt on rising edges only.</remarks>
        /// <param name="handler">The callback that will be called whenever 
        /// there is an interrupt on this device.</param>
        /// <param name="param">The object returned when the interrupt occurs.</param>
        public void RequestInterrupts(Action<uint, object> handler, object param)
        {
            m_function = (mask, t) => handler(mask, m_param);
            if (Interrupt != 0)
            {
                throw new AllocationException("The interrupt has already been allocated");
            }
            AllocateInterrupts(false);

            m_param = param;

            int status = 0;
            HALInterrupts.HAL_RequestInterrupts(Interrupt, PortHandleForRouting, (HALAnalogTriggerType)AnalogTriggerTypeForRouting, ref status);
            Utility.CheckStatus(status);
            SetUpSourceEdge(true, false);
            HALInterrupts.HAL_AttachInterruptHandler(Interrupt, m_function, IntPtr.Zero, ref status);
            Utility.CheckStatus(status);
        }

        /// <summary>
        /// Requests one of the 8 interrupts synchronously on this input.
        /// </summary>
        /// <remarks>Request interrupts in synchronous mode where the user program will have to explicitly 
        /// wait for the interrupt to occur using <see cref="WaitForInterrupt(double)">WaitForInterrupt</see>. 
        /// The default is interrupt on rising edges only.</remarks>
        public void RequestInterrupts()
        {
            if (Interrupt != 0)
            {
                throw new AllocationException("The interrupt has already been allocated");
            }

            AllocateInterrupts(true);

            int status = 0;
            HALInterrupts.HAL_RequestInterrupts(Interrupt, PortHandleForRouting, (HALAnalogTriggerType)AnalogTriggerTypeForRouting, ref status);
            Utility.CheckStatus(status);
            SetUpSourceEdge(true, false);
        }

        /// <summary>
        /// Allocate the interrupt.
        /// </summary>
        /// <param name="watcher">True if the interrupt should be called in synchronous mode
        /// where the user program will have to explicitly wait for the interrupt
        /// to occur.</param>
        protected void AllocateInterrupts(bool watcher)
        {
            if (Interrupt != 0)
            {
                throw new AllocationException("The interrupt has already been allocated");
            }


            IsSynchronousInterrupt = watcher;

            int status = 0;
            Interrupt = HALInterrupts.HAL_InitializeInterrupts(watcher, ref status);
            Utility.CheckStatus(status);
        }

        /// <summary>
        /// Cancel interrupts on this device.
        /// </summary>
        /// <remarks>This deallocates all the structures and disables any interrupts.</remarks>
        public void CancelInterrupts()
        {
            if (Interrupt == 0)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            int status = 0;
            HALInterrupts.HAL_CleanInterrupts(Interrupt, ref status);
            Utility.CheckStatus(status);

            Interrupt = 0;
        }

        /// /// <summary>
        /// In synchronous mode, wait for the defined interrupt to occur.
        /// </summary>
        /// <param name="timeout">Timeout in seconds</param>
        /// /// <param name="ignorePrevious">If true, ignore previous interrupts that 
        /// happened before this was called.</param>
        /// <returns>The <see cref="WaitResult"/> of the interrupt</returns>
        public WaitResult WaitForInterrupt(double timeout, bool ignorePrevious)
        {
            if (Interrupt == 0)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            int status = 0;
            long value = HALInterrupts.HAL_WaitForInterrupt(Interrupt, timeout, ignorePrevious, ref status);
            Utility.CheckStatus(status);
            return (WaitResult)value;
        }

        /// <summary>
        /// In synchronous mode, wait for the defined interrupt to occur.
        /// </summary>
        /// <param name="timeout">Timeout in seconds</param>
        /// <returns>The <see cref="WaitResult"/> of the interrupt</returns>
        public WaitResult WaitForInterrupt(double timeout)
        {
            return WaitForInterrupt(timeout, true);
        }

        /// <summary>
        /// Enable interrupts to occur on this input.
        /// </summary>
        /// <remarks>Interrupts are disabled when the RequestInterrup call is made.
        /// This gives time to do the setup of the other options before starting
        /// the field interrupts.</remarks>
        public void EnableInterrupts()
        {
            if (Interrupt == 0)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            if (IsSynchronousInterrupt)
            {
                throw new InvalidOperationException("You do not need to enable synchronous interrupts");
            }
            int status = 0;
            HALInterrupts.HAL_EnableInterrupts(Interrupt, ref status);
            Utility.CheckStatus(status);
        }

        /// <summary>
        /// Disable Interrupts without deallocating structures.
        /// </summary>
        public void DisableInterrupts()
        {
            if (Interrupt == 0)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            if (IsSynchronousInterrupt)
            {
                throw new InvalidOperationException("You do not need to enable synchronous interrupts");
            }
            int status = 0;
            HALInterrupts.HAL_DisableInterrupts(Interrupt, ref status);
            Utility.CheckStatus(status);
        }

        /// <summary>
        /// Return the timestamp for the rising interrupt that occurred most recently.
        /// </summary>
        /// <returns>Timestamp in seconds since boot.</returns>
        /// <remarks>This is in the same time domain as GetClock(). The rising edge interrupt
        /// should be enabled with <see cref="SetUpSourceEdge"/></remarks>
        public double ReadRisingTimestanp()
        {
            if (Interrupt == 0)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            int status = 0;
            double timestamp = HALInterrupts.HAL_ReadInterruptRisingTimestamp(Interrupt, ref status);
            Utility.CheckStatus(status);
            return timestamp;
        }

        /// <summary>
        /// Return the timestamp for the falling interrupt that occurred most recently.
        /// </summary>
        /// <returns>Timestamp in seconds since boot.</returns>
        /// <remarks>This is in the same time domain as GetClock(). The rising edge interrupt
        /// should be enabled with <see cref="SetUpSourceEdge"/></remarks>
        public double ReadFallingTimestanp()
        {
            if (Interrupt == 0)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            int status = 0;
            double timestamp = HALInterrupts.HAL_ReadInterruptFallingTimestamp(Interrupt, ref status);
            Utility.CheckStatus(status);
            return timestamp;
        }

        /// <summary>
        /// Set which edge to trigger interrupts on.
        /// </summary>
        /// <param name="risingEdge">True to interrupt on the rising edge</param>
        /// <param name="fallingEdge">True to interrupt on the falling edge</param>
        public void SetUpSourceEdge(bool risingEdge, bool fallingEdge)
        {
            if (Interrupt != 0)
            {
                int status = 0;
                HALInterrupts.HAL_SetInterruptUpSourceEdge(Interrupt, risingEdge, fallingEdge, ref status);
                Utility.CheckStatus(status);
            }
            else
            {
                throw new InvalidOperationException("You must call RequestInterrupts before SetUpSourceEdge");
            }
        }

        ///<inheritdoc/>
        public override void Dispose()
        {
            if (Interrupt != 0)
            {
                CancelInterrupts();
            }
        }
    }
}
