//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;

namespace HAL.Athena
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALSemaphore
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALSemaphore.InitializeMutexNormal = (Base.HALSemaphore.InitializeMutexNormalDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeMutexNormal"), typeof(Base.HALSemaphore.InitializeMutexNormalDelegate));

            Base.HALSemaphore.DeleteMutex = (Base.HALSemaphore.DeleteMutexDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "deleteMutex"), typeof(Base.HALSemaphore.DeleteMutexDelegate));

            Base.HALSemaphore.TakeMutex = (Base.HALSemaphore.TakeMutexDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "takeMutex"), typeof(Base.HALSemaphore.TakeMutexDelegate));

            Base.HALSemaphore.TryTakeMutex = (Base.HALSemaphore.TryTakeMutexDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "tryTakeMutex"), typeof(Base.HALSemaphore.TryTakeMutexDelegate));

            Base.HALSemaphore.GiveMutex = (Base.HALSemaphore.GiveMutexDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "giveMutex"), typeof(Base.HALSemaphore.GiveMutexDelegate));

            Base.HALSemaphore.InitializeMultiWait = (Base.HALSemaphore.InitializeMultiWaitDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeMultiWait"), typeof(Base.HALSemaphore.InitializeMultiWaitDelegate));

            Base.HALSemaphore.DeleteMultiWait = (Base.HALSemaphore.DeleteMultiWaitDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "deleteMultiWait"), typeof(Base.HALSemaphore.DeleteMultiWaitDelegate));

            Base.HALSemaphore.TakeMultiWait = (Base.HALSemaphore.TakeMultiWaitDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "takeMultiWait"), typeof(Base.HALSemaphore.TakeMultiWaitDelegate));

            Base.HALSemaphore.GiveMultiWait = (Base.HALSemaphore.GiveMultiWaitDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "giveMultiWait"), typeof(Base.HALSemaphore.GiveMultiWaitDelegate));

        }
    }
}
