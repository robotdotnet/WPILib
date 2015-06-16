//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;

namespace HAL_RoboRIO
{
    public class HALSemaphore
    {

        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeMutexRecursive")]
        public static extern IntPtr initializeMutexRecursive();

        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeMutexNormal")]
        public static extern IntPtr initializeMutexNormal();

        [DllImport("libHALAthena_shared.so", EntryPoint = "deleteMutex")]
        public static extern void deleteMutex(IntPtr sem);

        [DllImport("libHALAthena_shared.so", EntryPoint = "takeMutex")]
        public static extern sbyte takeMutex(IntPtr sem);

        [DllImport("libHALAthena_shared.so", EntryPoint = "tryTakeMutex")]
        public static extern sbyte tryTakeMutex(IntPtr sem);

        [DllImport("libHALAthena_shared.so", EntryPoint = "giveMutex")]
        public static extern sbyte giveMutex(IntPtr sem);

        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeSemaphore")]
        public static extern IntPtr initializeSemaphore(uint initial_value);

        [DllImport("libHALAthena_shared.so", EntryPoint = "deleteSemaphore")]
        public static extern void deleteSemaphore(IntPtr sem);

        [DllImport("libHALAthena_shared.so", EntryPoint = "takeSemaphore")]
        public static extern sbyte takeSemaphore(IntPtr sem);

        [DllImport("libHALAthena_shared.so", EntryPoint = "tryTakeSemaphore")]
        public static extern sbyte tryTakeSemaphore(IntPtr sem);

        [DllImport("libHALAthena_shared.so", EntryPoint = "giveSemaphore")]
        public static extern sbyte giveSemaphore(IntPtr sem);

        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeMultiWait")]
        public static extern IntPtr initializeMultiWait();

        [DllImport("libHALAthena_shared.so", EntryPoint = "deleteMultiWait")]
        public static extern void deleteMultiWait(IntPtr sem);

        [DllImport("libHALAthena_shared.so", EntryPoint = "takeMultiWait")]
        public static extern sbyte takeMultiWait(IntPtr sem, IntPtr m, int timeout);

        [DllImport("libHALAthena_shared.so", EntryPoint = "giveMultiWait")]
        public static extern sbyte giveMultiWait(IntPtr sem);
    }
}
