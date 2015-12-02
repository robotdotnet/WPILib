//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALInterrupts
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALInterrupts.InitializeInterrupts = (global::HAL.HALInterrupts.InitializeInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeInterrupts"), typeof(global::HAL.HALInterrupts.InitializeInterruptsDelegate));

            global::HAL.HALInterrupts.CleanInterrupts = (global::HAL.HALInterrupts.CleanInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "cleanInterrupts"), typeof(global::HAL.HALInterrupts.CleanInterruptsDelegate));

            global::HAL.HALInterrupts.WaitForInterrupt = (global::HAL.HALInterrupts.WaitForInterruptDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "waitForInterrupt"), typeof(global::HAL.HALInterrupts.WaitForInterruptDelegate));

            global::HAL.HALInterrupts.EnableInterrupts = (global::HAL.HALInterrupts.EnableInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "enableInterrupts"), typeof(global::HAL.HALInterrupts.EnableInterruptsDelegate));

            global::HAL.HALInterrupts.DisableInterrupts = (global::HAL.HALInterrupts.DisableInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "disableInterrupts"), typeof(global::HAL.HALInterrupts.DisableInterruptsDelegate));

            global::HAL.HALInterrupts.ReadRisingTimestamp = (global::HAL.HALInterrupts.ReadRisingTimestampDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "readRisingTimestamp"), typeof(global::HAL.HALInterrupts.ReadRisingTimestampDelegate));

            global::HAL.HALInterrupts.ReadFallingTimestamp = (global::HAL.HALInterrupts.ReadFallingTimestampDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "readFallingTimestamp"), typeof(global::HAL.HALInterrupts.ReadFallingTimestampDelegate));

            global::HAL.HALInterrupts.RequestInterrupts = (global::HAL.HALInterrupts.RequestInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "requestInterrupts"), typeof(global::HAL.HALInterrupts.RequestInterruptsDelegate));

            global::HAL.HALInterrupts.AttachInterruptHandler = (global::HAL.HALInterrupts.AttachInterruptHandlerDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "attachInterruptHandler"), typeof(global::HAL.HALInterrupts.AttachInterruptHandlerDelegate));

            global::HAL.HALInterrupts.SetInterruptUpSourceEdge = (global::HAL.HALInterrupts.SetInterruptUpSourceEdgeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setInterruptUpSourceEdge"), typeof(global::HAL.HALInterrupts.SetInterruptUpSourceEdgeDelegate));

        }
    }
}
