using System;
using System.Collections.Generic;
using System.Text;
using HAL_RoboRIO;
using WPILib.Util;

namespace WPILib
{
    public enum WaitResult
    {
        kTimeout = 0x0,
        kRisingEdge = 0x1,
        kFallingEdge = 0x100,
        kBoth = 0x101,
    }

    public abstract class InterruptableSensorBase : SensorBase
    {
        //figure this out
        protected IntPtr Interrupt = IntPtr.Zero;

        protected uint InterruptIndex;

        protected static Resource Interrupts = new Resource(8);

        protected bool IsSynchronousInterrupt = false;

        public InterruptableSensorBase()
        {
            Interrupt = IntPtr.Zero;
        }

        public abstract bool GetAnalogTriggerForRouting();
        public abstract int GetChannelForRouting();
        public abstract byte GetModuleForRouting();

        public void RequestInterupts(InterruptHandlerFunction handler)
        {
            if (Interrupt != IntPtr.Zero)
            {
                throw new AllocationException("The interrupt has already been allocated");
            }
            AllocateInterrupts(false);

            int status = 0;
            HALInterrupts.requestInterrupts(Interrupt, GetModuleForRouting(), (uint)GetChannelForRouting(), GetAnalogTriggerForRouting(), ref status);
            //Set Up Source Edge
            HALInterrupts.attachInterruptHandler(Interrupt, handler, IntPtr.Zero, ref status);
            //allocate Interupts
        }

        public void RequestInterrupts()
        {
            if (Interrupt != IntPtr.Zero)
            {
                throw new AllocationException("The interrupt has already been allocated");
            }

            AllocateInterrupts(true);

            int status = 0;
            HALInterrupts.requestInterrupts(Interrupt, GetModuleForRouting(), (uint)GetChannelForRouting(),
                GetAnalogTriggerForRouting(), ref status);
            //Setup Source Edge
        }

        protected void AllocateInterrupts(bool watcher)
        {
            try
            {
                InterruptIndex = (uint)Interrupts.Allocate();
            }
            catch (CheckedAllocationException e)
            {
                throw new AllocationException("No interrupts are left to be allocated");

            }
            IsSynchronousInterrupt = watcher;

            int status = 0;
            Interrupt = HALInterrupts.initializeInterrupts(InterruptIndex, watcher, ref status);
        }

        public void CancelInterrupts()
        {
            if (Interrupt == IntPtr.Zero)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            int status = 0;
            HALInterrupts.cleanInterrupts(Interrupt, ref status);

            Interrupt = IntPtr.Zero;
            Interrupts.Free((int)InterruptIndex);
        }

        public WaitResult WaitForInterrupt(double timeout, bool ignorePrevious)
        {
            if (Interrupt == IntPtr.Zero)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            int status = 0;
            uint value = HALInterrupts.waitForInterrupt(Interrupt, timeout, ignorePrevious, ref status);
            return (WaitResult)value;
        }

        public WaitResult WaitForInterrupt(double timeout)
        {
            return WaitForInterrupt(timeout, true);
        }

        public void EnableInterrupts()
        {
            if (Interrupt == IntPtr.Zero)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            if (IsSynchronousInterrupt)
            {
                throw new InvalidOperationException("You do not need to enable sychronous interrupts");
            }
            int status = 0;
            HALInterrupts.enableInterrupts(Interrupt, ref status);
        }

        public void DisableInterrupts()
        {
            if (Interrupt == IntPtr.Zero)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            if (IsSynchronousInterrupt)
            {
                throw new InvalidOperationException("You do not need to enable sychronous interrupts");
            }
            int status = 0;
            HALInterrupts.disableInterrupts(Interrupt, ref status);
        }

        public double ReadRisingTimestanp()
        {
            if (Interrupt == IntPtr.Zero)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            int status = 0;
            double timestamp = HALInterrupts.readRisingTimestamp(Interrupt, ref status);
            return timestamp;
        }
        public double ReadFallingTimestanp()
        {
            if (Interrupt == IntPtr.Zero)
            {
                throw new InvalidOperationException("The interrupt is not allocated.");
            }
            int status = 0;
            double timestamp = HALInterrupts.readFallingTimestamp(Interrupt, ref status);
            return timestamp;
        }

        public void SetUpSourceEdge(bool risingEdge, bool fallingEdge)
        {
            if (Interrupt != IntPtr.Zero)
            {
                int status = 0;
                HALInterrupts.setInterruptUpSourceEdge(Interrupt, risingEdge, fallingEdge, ref status);
            }
            else
            {
                throw new InvalidOperationException("You must call RequestInterrupts beofre SetUpSourceEdge");
            }
        }
    }
}
