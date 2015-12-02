//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;

namespace HAL.Athena
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALInterrupts
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALInterrupts.InitializeInterrupts = (Base.HALInterrupts.InitializeInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeInterrupts"), typeof(Base.HALInterrupts.InitializeInterruptsDelegate));

            Base.HALInterrupts.CleanInterrupts = (Base.HALInterrupts.CleanInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "cleanInterrupts"), typeof(Base.HALInterrupts.CleanInterruptsDelegate));

            Base.HALInterrupts.WaitForInterrupt = (Base.HALInterrupts.WaitForInterruptDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "waitForInterrupt"), typeof(Base.HALInterrupts.WaitForInterruptDelegate));

            Base.HALInterrupts.EnableInterrupts = (Base.HALInterrupts.EnableInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "enableInterrupts"), typeof(Base.HALInterrupts.EnableInterruptsDelegate));

            Base.HALInterrupts.DisableInterrupts = (Base.HALInterrupts.DisableInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "disableInterrupts"), typeof(Base.HALInterrupts.DisableInterruptsDelegate));

            Base.HALInterrupts.ReadRisingTimestamp = (Base.HALInterrupts.ReadRisingTimestampDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "readRisingTimestamp"), typeof(Base.HALInterrupts.ReadRisingTimestampDelegate));

            Base.HALInterrupts.ReadFallingTimestamp = (Base.HALInterrupts.ReadFallingTimestampDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "readFallingTimestamp"), typeof(Base.HALInterrupts.ReadFallingTimestampDelegate));

            Base.HALInterrupts.RequestInterrupts = (Base.HALInterrupts.RequestInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "requestInterrupts"), typeof(Base.HALInterrupts.RequestInterruptsDelegate));

            Base.HALInterrupts.AttachInterruptHandler = (Base.HALInterrupts.AttachInterruptHandlerDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "attachInterruptHandler"), typeof(Base.HALInterrupts.AttachInterruptHandlerDelegate));

            Base.HALInterrupts.SetInterruptUpSourceEdge = (Base.HALInterrupts.SetInterruptUpSourceEdgeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setInterruptUpSourceEdge"), typeof(Base.HALInterrupts.SetInterruptUpSourceEdgeDelegate));

        }
    }
}
