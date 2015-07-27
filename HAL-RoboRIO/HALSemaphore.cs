//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;

namespace HAL_RoboRIO
{
    internal class HALSemaphore
    {

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeMutexRecursive")]
        internal static extern IntPtr initializeMutexRecursive();

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeMutexNormal")]
        internal static extern IntPtr initializeMutexNormal();

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "deleteMutex")]
        internal static extern void deleteMutex(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "takeMutex")]
        internal static extern sbyte takeMutex(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "tryTakeMutex")]
        internal static extern sbyte tryTakeMutex(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "giveMutex")]
        internal static extern sbyte giveMutex(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeSemaphore")]
        internal static extern IntPtr initializeSemaphore(uint initial_value);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "deleteSemaphore")]
        internal static extern void deleteSemaphore(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "takeSemaphore")]
        internal static extern sbyte takeSemaphore(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "tryTakeSemaphore")]
        internal static extern sbyte tryTakeSemaphore(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "giveSemaphore")]
        internal static extern sbyte giveSemaphore(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeMultiWait")]
        internal static extern IntPtr initializeMultiWait();

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "deleteMultiWait")]
        internal static extern void deleteMultiWait(IntPtr sem);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "takeMultiWait")]
        internal static extern sbyte takeMultiWait(IntPtr sem, IntPtr m, int timeout);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "giveMultiWait")]
        internal static extern sbyte giveMultiWait(IntPtr sem);
    }
}
