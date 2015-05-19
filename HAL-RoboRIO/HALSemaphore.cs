//File automatically generated using robotdotnet-tools. Please do not modify.
using HAL_Base;
namespace HAL_RoboRIO
{
    public class HALSemaphore
    {

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "initializeMutexRecursive")]
        public static extern System.IntPtr initializeMutexRecursive();

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "initializeMutexNormal")]
        public static extern System.IntPtr initializeMutexNormal();

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "deleteMutex")]
        public static extern void deleteMutex(System.IntPtr sem);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "takeMutex")]
        public static extern sbyte takeMutex(System.IntPtr sem);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "tryTakeMutex")]
        public static extern sbyte tryTakeMutex(System.IntPtr sem);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "giveMutex")]
        public static extern sbyte giveMutex(System.IntPtr sem);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "initializeSemaphore")]
        public static extern System.IntPtr initializeSemaphore(uint initial_value);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "deleteSemaphore")]
        public static extern void deleteSemaphore(System.IntPtr sem);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "takeSemaphore")]
        public static extern sbyte takeSemaphore(System.IntPtr sem);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "tryTakeSemaphore")]
        public static extern sbyte tryTakeSemaphore(System.IntPtr sem);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "giveSemaphore")]
        public static extern sbyte giveSemaphore(System.IntPtr sem);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "initializeMultiWait")]
        public static extern System.IntPtr initializeMultiWait();

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "deleteMultiWait")]
        public static extern void deleteMultiWait(System.IntPtr sem);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "takeMultiWait")]
        public static extern sbyte takeMultiWait(System.IntPtr sem, System.IntPtr m, int timeout);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "giveMultiWait")]
        public static extern sbyte giveMultiWait(System.IntPtr sem);
    }
}
