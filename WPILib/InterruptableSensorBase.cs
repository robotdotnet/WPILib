

using System;
using WPILib.Util;
using HAL_Base;

namespace WPILib
{
    public enum WaitResult
    {
        Timeout = 0x0,
        RisingEdge = 0x1,
        FallingEdge = 0x100,
        Both = 0x101,
    }

    public delegate void InterruptHandlerFunctionWPILib();

    public abstract class InterruptableSensorBase : SensorBase
    {
        //figure this out
        protected IntPtr m_interrupt = IntPtr.Zero;

        protected uint m_interruptIndex;

        protected static Resource s_interrupts = new Resource(8);

        protected bool m_isSynchronousInterrupt = false;

        public InterruptableSensorBase()
        {
            m_interrupt = IntPtr.Zero;
        }

        public abstract bool GetAnalogTriggerForRouting();
        public abstract int GetChannelForRouting();
        public abstract byte GetModuleForRouting();

        

        public void RequestInterrupts(InterruptHandlerFunctionWPILib handler)
        {
            InterruptHandlerFunction function = new InterruptHandlerFunction(delegate(uint mask, IntPtr param)
            {
                handler();
            });
            if (m_interrupt != IntPtr.Zero)
            {
                throw new AllocationException("The interrupt has already been allocated");
            }
            AllocateInterrupts(false);

            int status = 0;
            HALInterrupts.RequestInterrupts(m_interrupt, GetModuleForRouting(), (uint)GetChannelForRouting(), GetAnalogTriggerForRouting(), ref status);
            SetUpSourceEdge(true, false);
            HALInterrupts.AttachInterruptHandler(m_interrupt, function, IntPtr.Zero, ref status);
            //allocate Interupts
        }

        public void RequestInterrupts()
        {
            if (m_interrupt != IntPtr.Zero)
            {
                throw new AllocationException("The interrupt has already been allocated");
            }

            AllocateInterrupts(true);

            int status = 0;
            HALInterrupts.RequestInterrupts(m_interrupt, GetModuleForRouting(), (uint)GetChannelForRouting(),
                GetAnalogTriggerForRouting(), ref status);
            SetUpSourceEdge(true, false);
            //Setup Source Edge
        }

        protected void AllocateInterrupts(bool watcher)
        {
            try
            {
                m_interruptIndex = (uint)s_interrupts.Allocate();
            }
            catch (CheckedAllocationException e)
            {
                throw new AllocationException("No interrupts are left to be allocated");
            }
            m_isSynchronousInterrupt = watcher;

            int status = 0;
            m_interrupt = HALInterrupts.InitializeInterrupts(m_interruptIndex, watcher, ref status);
        }

        public void CancelInterrupts()
        {
            if (m_interrupt == IntPtr.Zero)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            int status = 0;
            HALInterrupts.CleanInterrupts(m_interrupt, ref status);

            m_interrupt = IntPtr.Zero;
            s_interrupts.Free((int)m_interruptIndex);
        }

        public WaitResult WaitForInterrupt(double timeout, bool ignorePrevious)
        {
            if (m_interrupt == IntPtr.Zero)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            int status = 0;
            uint value = HALInterrupts.WaitForInterrupt(m_interrupt, timeout, ignorePrevious, ref status);
            return (WaitResult)value;
        }

        public WaitResult WaitForInterrupt(double timeout)
        {
            return WaitForInterrupt(timeout, true);
        }

        public void EnableInterrupts()
        {
            if (m_interrupt == IntPtr.Zero)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            if (m_isSynchronousInterrupt)
            {
                throw new InvalidOperationException("You do not need to enable sychronous interrupts");
            }
            int status = 0;
            HALInterrupts.EnableInterrupts(m_interrupt, ref status);
        }

        public void DisableInterrupts()
        {
            if (m_interrupt == IntPtr.Zero)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            if (m_isSynchronousInterrupt)
            {
                throw new InvalidOperationException("You do not need to enable sychronous interrupts");
            }
            int status = 0;
            HALInterrupts.DisableInterrupts(m_interrupt, ref status);
        }

        public double ReadRisingTimestanp()
        {
            if (m_interrupt == IntPtr.Zero)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            int status = 0;
            double timestamp = HALInterrupts.ReadRisingTimestamp(m_interrupt, ref status);
            return timestamp;
        }
        public double ReadFallingTimestanp()
        {
            if (m_interrupt == IntPtr.Zero)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            int status = 0;
            double timestamp = HALInterrupts.ReadFallingTimestamp(m_interrupt, ref status);
            return timestamp;
        }

        public void SetUpSourceEdge(bool risingEdge, bool fallingEdge)
        {
            if (m_interrupt != IntPtr.Zero)
            {
                int status = 0;
                HALInterrupts.SetInterruptUpSourceEdge(m_interrupt, risingEdge, fallingEdge, ref status);
            }
            else
            {
                throw new InvalidOperationException("You must call RequestInterrupts beofre SetUpSourceEdge");
            }
        }
    }
}
