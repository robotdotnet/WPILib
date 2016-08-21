using System;
using System.Runtime.InteropServices;
using HAL.Base;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALInterrupts
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALInterrupts.HAL_InitializeInterrupts = HAL_InitializeInterrupts;
            Base.HALInterrupts.HAL_CleanInterrupts = HAL_CleanInterrupts;
            Base.HALInterrupts.HAL_WaitForInterrupt = HAL_WaitForInterrupt;
            Base.HALInterrupts.HAL_EnableInterrupts = HAL_EnableInterrupts;
            Base.HALInterrupts.HAL_DisableInterrupts = HAL_DisableInterrupts;
            Base.HALInterrupts.HAL_ReadInterruptRisingTimestamp = HAL_ReadInterruptRisingTimestamp;
            Base.HALInterrupts.HAL_ReadInterruptFallingTimestamp = HAL_ReadInterruptFallingTimestamp;
            Base.HALInterrupts.HAL_RequestInterrupts = HAL_RequestInterrupts;
            Base.HALInterrupts.HAL_AttachInterruptHandler = HAL_AttachInterruptHandler;
            Base.HALInterrupts.HAL_SetInterruptUpSourceEdge = HAL_SetInterruptUpSourceEdge;
        }

        public static int HAL_InitializeInterrupts(bool watcher, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_CleanInterrupts(int interrupt_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static long HAL_WaitForInterrupt(int interrupt_handle, double timeout, bool ignorePrevious, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_EnableInterrupts(int interrupt_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_DisableInterrupts(int interrupt_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_ReadInterruptRisingTimestamp(int interrupt_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_ReadInterruptFallingTimestamp(int interrupt_handle, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_RequestInterrupts(int interrupt_handle, int digitalSourceHandle, HALAnalogTriggerType analogTriggerType, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_AttachInterruptHandler(int interrupt_handle, Action<uint, IntPtr> handler, IntPtr param, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_SetInterruptUpSourceEdge(int interrupt_handle, bool risingEdge, bool fallingEdge, ref int status)
        {
        }
    }
}

