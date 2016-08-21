using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
    internal class HALInterrupts
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {

            Base.HALInterrupts.HAL_InitializeInterrupts = (Base.HALInterrupts.HAL_InitializeInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializeInterrupts"), typeof(Base.HALInterrupts.HAL_InitializeInterruptsDelegate));

            Base.HALInterrupts.HAL_CleanInterrupts = (Base.HALInterrupts.HAL_CleanInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_CleanInterrupts"), typeof(Base.HALInterrupts.HAL_CleanInterruptsDelegate));

            Base.HALInterrupts.HAL_WaitForInterrupt = (Base.HALInterrupts.HAL_WaitForInterruptDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_WaitForInterrupt"), typeof(Base.HALInterrupts.HAL_WaitForInterruptDelegate));

            Base.HALInterrupts.HAL_EnableInterrupts = (Base.HALInterrupts.HAL_EnableInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_EnableInterrupts"), typeof(Base.HALInterrupts.HAL_EnableInterruptsDelegate));

            Base.HALInterrupts.HAL_DisableInterrupts = (Base.HALInterrupts.HAL_DisableInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_DisableInterrupts"), typeof(Base.HALInterrupts.HAL_DisableInterruptsDelegate));

            Base.HALInterrupts.HAL_ReadInterruptRisingTimestamp = (Base.HALInterrupts.HAL_ReadInterruptRisingTimestampDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ReadInterruptRisingTimestamp"), typeof(Base.HALInterrupts.HAL_ReadInterruptRisingTimestampDelegate));

            Base.HALInterrupts.HAL_ReadInterruptFallingTimestamp = (Base.HALInterrupts.HAL_ReadInterruptFallingTimestampDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ReadInterruptFallingTimestamp"), typeof(Base.HALInterrupts.HAL_ReadInterruptFallingTimestampDelegate));

            Base.HALInterrupts.HAL_RequestInterrupts = (Base.HALInterrupts.HAL_RequestInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_RequestInterrupts"), typeof(Base.HALInterrupts.HAL_RequestInterruptsDelegate));

            Base.HALInterrupts.HAL_AttachInterruptHandler = (Base.HALInterrupts.HAL_AttachInterruptHandlerDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_AttachInterruptHandlerShim"), typeof(Base.HALInterrupts.HAL_AttachInterruptHandlerDelegate));

            Base.HALInterrupts.HAL_SetInterruptUpSourceEdge = (Base.HALInterrupts.HAL_SetInterruptUpSourceEdgeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetInterruptUpSourceEdge"), typeof(Base.HALInterrupts.HAL_SetInterruptUpSourceEdgeDelegate));
        }
    }
}

