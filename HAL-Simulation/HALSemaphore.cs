

using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace HAL_FRC
{
    public class HALSemaphore
    {
        /// Return Type: MUTEX_ID->void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeMutexRecursive")]
        public static extern System.IntPtr initializeMutexRecursive();


        /// Return Type: MUTEX_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "initializeMutexNormal")]
        //public static extern System.IntPtr initializeMutexNormal();

        public static IntPtr initializeMutexNormal()
        {
            MUTEX_ID p = new MUTEX_ID();
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

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
        public static extern System.IntPtr initializeSemaphore(uint initial_value);


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
        //[DllImport("libHALAthena_shared.so", EntryPoint = "initializeMultiWait")]
        //public static extern System.IntPtr initializeMultiWait();
        public static IntPtr initializeMultiWait()
        {
            MULTIWAIT_ID p = new MULTIWAIT_ID();
            p.lockObject = new object();
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        /// Return Type: void
        ///sem: MULTIWAIT_ID->void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "deleteMultiWait")]
        public static extern void deleteMultiWait(System.IntPtr sem);


        /// Return Type: byte
        ///sem: MULTIWAIT_ID->void*
        ///m: MUTEX_ID->void*
        ///timeout: int
        //[DllImport("libHALAthena_shared.so", EntryPoint = "takeMultiWait")]
        //public static extern byte takeMultiWait(System.IntPtr sem, System.IntPtr m, int timeout);
        public static byte takeMultiWait(IntPtr sem, IntPtr m, int timeout)
        {
            lock (HAL.NewDataSem)
            {
                try
                {
                    Monitor.Wait(HAL.NewDataSem);
                }
                catch (ThreadInterruptedException ex)
                {
                }
            }
            return 0;
        }

        /// Return Type: byte
        ///sem: MULTIWAIT_ID->void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "giveMultiWait")]
        public static extern byte giveMultiWait(System.IntPtr sem);
    }
}
