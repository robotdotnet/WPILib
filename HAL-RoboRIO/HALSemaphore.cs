//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALSemaphore
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALSemaphore.InitializeMutexNormal = (HAL_Base.HALSemaphore.InitializeMutexNormalDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeMutexNormal"), typeof(HAL_Base.HALSemaphore.InitializeMutexNormalDelegate));

            HAL_Base.HALSemaphore.DeleteMutex = (HAL_Base.HALSemaphore.DeleteMutexDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "deleteMutex"), typeof(HAL_Base.HALSemaphore.DeleteMutexDelegate));

            HAL_Base.HALSemaphore.TakeMutex = (HAL_Base.HALSemaphore.TakeMutexDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "takeMutex"), typeof(HAL_Base.HALSemaphore.TakeMutexDelegate));

            HAL_Base.HALSemaphore.TryTakeMutex = (HAL_Base.HALSemaphore.TryTakeMutexDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "tryTakeMutex"), typeof(HAL_Base.HALSemaphore.TryTakeMutexDelegate));

            HAL_Base.HALSemaphore.GiveMutex = (HAL_Base.HALSemaphore.GiveMutexDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "giveMutex"), typeof(HAL_Base.HALSemaphore.GiveMutexDelegate));

            HAL_Base.HALSemaphore.InitializeMultiWait = (HAL_Base.HALSemaphore.InitializeMultiWaitDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeMultiWait"), typeof(HAL_Base.HALSemaphore.InitializeMultiWaitDelegate));

            HAL_Base.HALSemaphore.DeleteMultiWait = (HAL_Base.HALSemaphore.DeleteMultiWaitDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "deleteMultiWait"), typeof(HAL_Base.HALSemaphore.DeleteMultiWaitDelegate));

            HAL_Base.HALSemaphore.TakeMultiWait = (HAL_Base.HALSemaphore.TakeMultiWaitDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "takeMultiWait"), typeof(HAL_Base.HALSemaphore.TakeMultiWaitDelegate));

            HAL_Base.HALSemaphore.GiveMultiWait = (HAL_Base.HALSemaphore.GiveMultiWaitDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "giveMultiWait"), typeof(HAL_Base.HALSemaphore.GiveMultiWaitDelegate));

        }
    }
}
