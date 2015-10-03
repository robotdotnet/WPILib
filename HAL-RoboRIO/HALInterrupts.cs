//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALInterrupts
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALInterrupts.InitializeInterrupts = (HAL_Base.HALInterrupts.InitializeInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeInterrupts"), typeof(HAL_Base.HALInterrupts.InitializeInterruptsDelegate));

            HAL_Base.HALInterrupts.CleanInterrupts = (HAL_Base.HALInterrupts.CleanInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "cleanInterrupts"), typeof(HAL_Base.HALInterrupts.CleanInterruptsDelegate));

            HAL_Base.HALInterrupts.WaitForInterrupt = (HAL_Base.HALInterrupts.WaitForInterruptDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "waitForInterrupt"), typeof(HAL_Base.HALInterrupts.WaitForInterruptDelegate));

            HAL_Base.HALInterrupts.EnableInterrupts = (HAL_Base.HALInterrupts.EnableInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "enableInterrupts"), typeof(HAL_Base.HALInterrupts.EnableInterruptsDelegate));

            HAL_Base.HALInterrupts.DisableInterrupts = (HAL_Base.HALInterrupts.DisableInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "disableInterrupts"), typeof(HAL_Base.HALInterrupts.DisableInterruptsDelegate));

            HAL_Base.HALInterrupts.ReadRisingTimestamp = (HAL_Base.HALInterrupts.ReadRisingTimestampDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "readRisingTimestamp"), typeof(HAL_Base.HALInterrupts.ReadRisingTimestampDelegate));

            HAL_Base.HALInterrupts.ReadFallingTimestamp = (HAL_Base.HALInterrupts.ReadFallingTimestampDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "readFallingTimestamp"), typeof(HAL_Base.HALInterrupts.ReadFallingTimestampDelegate));

            HAL_Base.HALInterrupts.RequestInterrupts = (HAL_Base.HALInterrupts.RequestInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "requestInterrupts"), typeof(HAL_Base.HALInterrupts.RequestInterruptsDelegate));

            HAL_Base.HALInterrupts.AttachInterruptHandler = (HAL_Base.HALInterrupts.AttachInterruptHandlerDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "attachInterruptHandler"), typeof(HAL_Base.HALInterrupts.AttachInterruptHandlerDelegate));

            HAL_Base.HALInterrupts.SetInterruptUpSourceEdge = (HAL_Base.HALInterrupts.SetInterruptUpSourceEdgeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setInterruptUpSourceEdge"), typeof(HAL_Base.HALInterrupts.SetInterruptUpSourceEdgeDelegate));

        }
    }
}
