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
            HAL_Base.HALSemaphore.InitializeMutexRecursive = (HAL_Base.HALSemaphore.InitializeMutexRecursiveDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeMutexRecursive"), typeof(HAL_Base.HALSemaphore.InitializeMutexRecursiveDelegate));

            HAL_Base.HALSemaphore.InitializeMutexNormal = (HAL_Base.HALSemaphore.InitializeMutexNormalDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeMutexNormal"), typeof(HAL_Base.HALSemaphore.InitializeMutexNormalDelegate));

            HAL_Base.HALSemaphore.DeleteMutex = (HAL_Base.HALSemaphore.DeleteMutexDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "deleteMutex"), typeof(HAL_Base.HALSemaphore.DeleteMutexDelegate));

            HAL_Base.HALSemaphore.TakeMutex = (HAL_Base.HALSemaphore.TakeMutexDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "takeMutex"), typeof(HAL_Base.HALSemaphore.TakeMutexDelegate));

            HAL_Base.HALSemaphore.TryTakeMutex = (HAL_Base.HALSemaphore.TryTakeMutexDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "tryTakeMutex"), typeof(HAL_Base.HALSemaphore.TryTakeMutexDelegate));

            HAL_Base.HALSemaphore.GiveMutex = (HAL_Base.HALSemaphore.GiveMutexDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "giveMutex"), typeof(HAL_Base.HALSemaphore.GiveMutexDelegate));

            HAL_Base.HALSemaphore.InitializeSemaphore = (HAL_Base.HALSemaphore.InitializeSemaphoreDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeSemaphore"), typeof(HAL_Base.HALSemaphore.InitializeSemaphoreDelegate));

            HAL_Base.HALSemaphore.DeleteSemaphore = (HAL_Base.HALSemaphore.DeleteSemaphoreDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "deleteSemaphore"), typeof(HAL_Base.HALSemaphore.DeleteSemaphoreDelegate));

            HAL_Base.HALSemaphore.TakeSemaphore = (HAL_Base.HALSemaphore.TakeSemaphoreDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "takeSemaphore"), typeof(HAL_Base.HALSemaphore.TakeSemaphoreDelegate));

            HAL_Base.HALSemaphore.TryTakeSemaphore = (HAL_Base.HALSemaphore.TryTakeSemaphoreDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "tryTakeSemaphore"), typeof(HAL_Base.HALSemaphore.TryTakeSemaphoreDelegate));

            HAL_Base.HALSemaphore.GiveSemaphore = (HAL_Base.HALSemaphore.GiveSemaphoreDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "giveSemaphore"), typeof(HAL_Base.HALSemaphore.GiveSemaphoreDelegate));

            HAL_Base.HALSemaphore.InitializeMultiWait = (HAL_Base.HALSemaphore.InitializeMultiWaitDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeMultiWait"), typeof(HAL_Base.HALSemaphore.InitializeMultiWaitDelegate));

            HAL_Base.HALSemaphore.DeleteMultiWait = (HAL_Base.HALSemaphore.DeleteMultiWaitDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "deleteMultiWait"), typeof(HAL_Base.HALSemaphore.DeleteMultiWaitDelegate));

            HAL_Base.HALSemaphore.TakeMultiWait = (HAL_Base.HALSemaphore.TakeMultiWaitDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "takeMultiWait"), typeof(HAL_Base.HALSemaphore.TakeMultiWaitDelegate));

            HAL_Base.HALSemaphore.GiveMultiWait = (HAL_Base.HALSemaphore.GiveMultiWaitDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "giveMultiWait"), typeof(HAL_Base.HALSemaphore.GiveMultiWaitDelegate));

        }

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeMutexRecursive")]
        public static extern IntPtr initializeMutexRecursive();

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeMutexNormal")]
        public static extern IntPtr initializeMutexNormal();

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "deleteMutex")]
        public static extern void deleteMutex(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "takeMutex")]
        public static extern sbyte takeMutex(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "tryTakeMutex")]
        public static extern sbyte tryTakeMutex(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "giveMutex")]
        public static extern sbyte giveMutex(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeSemaphore")]
        public static extern IntPtr initializeSemaphore(uint initial_value);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "deleteSemaphore")]
        public static extern void deleteSemaphore(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "takeSemaphore")]
        public static extern sbyte takeSemaphore(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "tryTakeSemaphore")]
        public static extern sbyte tryTakeSemaphore(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "giveSemaphore")]
        public static extern sbyte giveSemaphore(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeMultiWait")]
        public static extern IntPtr initializeMultiWait();

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "deleteMultiWait")]
        public static extern void deleteMultiWait(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "takeMultiWait")]
        public static extern sbyte takeMultiWait(IntPtr sem, IntPtr m, int timeout);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "giveMultiWait")]
        public static extern sbyte giveMultiWait(IntPtr sem);
    }
}
