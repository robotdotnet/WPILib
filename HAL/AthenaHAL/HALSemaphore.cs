//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace HAL.Athena
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALSemaphore
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALSemaphore.InitializeMutexNormal = (global::HAL.HALSemaphore.InitializeMutexNormalDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeMutexNormal"), typeof(global::HAL.HALSemaphore.InitializeMutexNormalDelegate));

            global::HAL.HALSemaphore.DeleteMutex = (global::HAL.HALSemaphore.DeleteMutexDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "deleteMutex"), typeof(global::HAL.HALSemaphore.DeleteMutexDelegate));

            global::HAL.HALSemaphore.TakeMutex = (global::HAL.HALSemaphore.TakeMutexDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "takeMutex"), typeof(global::HAL.HALSemaphore.TakeMutexDelegate));

            global::HAL.HALSemaphore.TryTakeMutex = (global::HAL.HALSemaphore.TryTakeMutexDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "tryTakeMutex"), typeof(global::HAL.HALSemaphore.TryTakeMutexDelegate));

            global::HAL.HALSemaphore.GiveMutex = (global::HAL.HALSemaphore.GiveMutexDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "giveMutex"), typeof(global::HAL.HALSemaphore.GiveMutexDelegate));

            global::HAL.HALSemaphore.InitializeMultiWait = (global::HAL.HALSemaphore.InitializeMultiWaitDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeMultiWait"), typeof(global::HAL.HALSemaphore.InitializeMultiWaitDelegate));

            global::HAL.HALSemaphore.DeleteMultiWait = (global::HAL.HALSemaphore.DeleteMultiWaitDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "deleteMultiWait"), typeof(global::HAL.HALSemaphore.DeleteMultiWaitDelegate));

            global::HAL.HALSemaphore.TakeMultiWait = (global::HAL.HALSemaphore.TakeMultiWaitDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "takeMultiWait"), typeof(global::HAL.HALSemaphore.TakeMultiWaitDelegate));

            global::HAL.HALSemaphore.GiveMultiWait = (global::HAL.HALSemaphore.GiveMultiWaitDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "giveMultiWait"), typeof(global::HAL.HALSemaphore.GiveMultiWaitDelegate));

        }
    }
}
