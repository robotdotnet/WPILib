//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;

namespace HAL_RoboRIO
{
    internal class HALSemaphore
    {

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
