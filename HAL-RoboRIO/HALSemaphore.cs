using System.Runtime.InteropServices;

namespace HAL_FRC
{
    public class HALSemaphore
    {
        /// Return Type: MUTEX_ID->void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeMutexRecursive")]
        public static extern System.IntPtr InitializeMutexRecursive();


        /// Return Type: MUTEX_ID->void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "nitializeMutexNormal")]
        public static extern System.IntPtr initializeMutexNormal();


        /// Return Type: void
        ///sem: MUTEX_ID->void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "deleteMutex")]
        public static extern void deleteMutex(System.IntPtr sem);


        /// Return Type: byte
        ///sem: MUTEX_ID->void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "takeMutex")]
        public static extern byte takeMutex(System.IntPtr sem);


        /// Return Type: byte
        ///sem: MUTEX_ID->void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "tryTakeMutex")]
        public static extern byte tryTakeMutex(System.IntPtr sem);


        /// Return Type: byte
        ///sem: MUTEX_ID->void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "giveMutex")]
        public static extern byte giveMutex(System.IntPtr sem);


        /// Return Type: SEMAPHORE_ID->void*
        ///initial_value: unsigned int
        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeSemaphore")]
        public static extern System.IntPtr initializeSemaphore(uint initialValue);


        /// Return Type: void
        ///sem: SEMAPHORE_ID->void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "deleteSemaphore")]
        public static extern void deleteSemaphore(System.IntPtr sem);


        /// Return Type: byte
        ///sem: SEMAPHORE_ID->void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "takeSemaphore")]
        public static extern byte takeSemaphore(System.IntPtr sem);


        /// Return Type: byte
        ///sem: SEMAPHORE_ID->void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "tryTakeSemaphore")]
        public static extern byte tryTakeSemaphore(System.IntPtr sem);


        /// Return Type: byte
        ///sem: SEMAPHORE_ID->void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "giveSemaphore")]
        public static extern byte giveSemaphore(System.IntPtr sem);


        /// Return Type: MULTIWAIT_ID->void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeMultiWait")]
        public static extern System.IntPtr initializeMultiWait();


        /// Return Type: void
        ///sem: MULTIWAIT_ID->void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "deleteMultiWait")]
        public static extern void deleteMultiWait(System.IntPtr sem);


        /// Return Type: byte
        ///sem: MULTIWAIT_ID->void*
        ///m: MUTEX_ID->void*
        ///timeout: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "takeMultiWait")]
        public static extern byte takeMultiWait(System.IntPtr sem, System.IntPtr m, int timeout);


        /// Return Type: byte
        ///sem: MULTIWAIT_ID->void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "giveMultiWait")]
        public static extern byte giveMultiWait(System.IntPtr sem);
    }
}
