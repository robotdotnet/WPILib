﻿

using System;
using HAL_Base;
using WPILib.Util;

namespace WPILib
{
    public enum WaitResult
    {
        Timeout = 0x0,
        RisingEdge = 0x1,
        FallingEdge = 0x100,
        Both = 0x101,
    }

    public abstract class InterruptableSensorBase : SensorBase
    {
        protected IntPtr m_interrupt = IntPtr.Zero;

        protected uint m_interruptIndex;

        protected static Resource s_interrupts = new Resource(8);

        protected bool m_isSynchronousInterrupt = false;

        protected InterruptableSensorBase()
        {
            m_interrupt = IntPtr.Zero;
        }

        private Action<uint, IntPtr> m_function;
        private object m_param;

        public abstract bool AnalogTriggerForRouting { get; }
        public abstract int ChannelForRouting { get; }
        public abstract byte ModuleForRouting { get; }


        public void RequestInterrupts(Action handler)
        {
            m_function = (mask, t) => handler();
            if (m_interrupt != IntPtr.Zero)
            {
                throw new AllocationException("The interrupt has already been allocated");
            }
            AllocateInterrupts(false);

            m_param = null;

            int status = 0;
            HALInterrupts.RequestInterrupts(m_interrupt, ModuleForRouting, (uint)ChannelForRouting, AnalogTriggerForRouting, ref status);
            SetUpSourceEdge(true, false);
            HALInterrupts.AttachInterruptHandler(m_interrupt, m_function, IntPtr.Zero, ref status);
        }

        public void RequestInterrupts(Action<uint, object> handler, object param)
        {
            m_function = (mask, t) => handler(mask, m_param);
            if (m_interrupt != IntPtr.Zero)
            {
                throw new AllocationException("The interrupt has already been allocated");
            }
            AllocateInterrupts(false);

            m_param = param;

            int status = 0;
            HALInterrupts.RequestInterrupts(m_interrupt, ModuleForRouting, (uint)ChannelForRouting, AnalogTriggerForRouting, ref status);
            SetUpSourceEdge(true, false);
            HALInterrupts.AttachInterruptHandler(m_interrupt, m_function, IntPtr.Zero, ref status);
        }


        public void RequestInterrupts()
        {
            if (m_interrupt != IntPtr.Zero)
            {
                throw new AllocationException("The interrupt has already been allocated");
            }

            AllocateInterrupts(true);

            int status = 0;
            HALInterrupts.RequestInterrupts(m_interrupt, ModuleForRouting, (uint)ChannelForRouting,
                AnalogTriggerForRouting, ref status);
            SetUpSourceEdge(true, false);
        }

        protected void AllocateInterrupts(bool watcher)
        {
            try
            {
                m_interruptIndex = (uint)s_interrupts.Allocate();
            }
            catch (CheckedAllocationException)
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
            s_interrupts.Dispose((int)m_interruptIndex);
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

        public double RisingTimestanp
        {
            get
            {
                if (m_interrupt == IntPtr.Zero)
                {
                    throw new InvalidOperationException("The interrupt is not allocated.");
                }
                int status = 0;
                double timestamp = HALInterrupts.ReadRisingTimestamp(m_interrupt, ref status);
                return timestamp;
            }
        }

        public double FallingTimestanp
        {
            get
            {
                if (m_interrupt == IntPtr.Zero)
                {
                    throw new InvalidOperationException("The interrupt is not allocated.");
                }
                int status = 0;
                double timestamp = HALInterrupts.ReadFallingTimestamp(m_interrupt, ref status);
                return timestamp;
            }
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
                throw new InvalidOperationException("You must call RequestInterrupts before SetUpSourceEdge");
            }
        }
    }
}
