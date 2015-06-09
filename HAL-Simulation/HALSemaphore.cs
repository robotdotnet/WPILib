

using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace HAL_FRC
{
    public class HALSemaphore
    {
        /// Return Type: MUTEX_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "initializeMutexRecursive")]
        public static IntPtr initializeMutexRecursive()
        {
            MUTEX_ID p = new MUTEX_ID();
            p.lockObject = new object();
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }


        /// Return Type: MUTEX_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "initializeMutexNormal")]
        //public static extern System.IntPtr initializeMutexNormal();

        public static IntPtr initializeMutexNormal()
        {
            MUTEX_ID p = new MUTEX_ID();
            p.lockObject = new object();
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        /// Return Type: void
        ///sem: MUTEX_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "deleteMutex")]
        public static void deleteMutex(IntPtr sem)
        {
            Marshal.FreeHGlobal(sem);
            sem = IntPtr.Zero;
        }


        /// Return Type: byte
        ///sem: MUTEX_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "takeMutex")]
        public static sbyte takeMutex(IntPtr sem)
        {
            var temp = (MUTEX_ID)Marshal.PtrToStructure(sem, typeof (MUTEX_ID));
            Monitor.Enter(temp.lockObject);

            return 0;
        }


        /// Return Type: byte
        ///sem: MUTEX_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "tryTakeMutex")]
        public static sbyte tryTakeMutex(IntPtr sem)
        {
            var temp = (MUTEX_ID)Marshal.PtrToStructure(sem, typeof(MUTEX_ID));
            bool retVal = Monitor.TryEnter(temp.lockObject);
            return retVal ? (sbyte)1 : (sbyte)0;
        }


        /// Return Type: byte
        ///sem: MUTEX_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "giveMutex")]
        public static sbyte giveMutex(IntPtr sem)
        {
            var temp = (MUTEX_ID)Marshal.PtrToStructure(sem, typeof(MUTEX_ID));
            Monitor.Exit(temp.lockObject);
            return 0;
        }


        /// Return Type: SEMAPHORE_ID->void*
        ///initial_value: unsigned int
        //[DllImport("libHALAthena_shared.so", EntryPoint = "initializeSemaphore")]
        public static IntPtr initializeSemaphore(uint initial_value)
        {
            SEMAPHORE_ID p = new SEMAPHORE_ID();
            p.semaphore = new Semaphore((int)initial_value, 1);
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }


        /// Return Type: void
        ///sem: SEMAPHORE_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "deleteSemaphore")]
        public static void deleteSemaphore(IntPtr sem)
        {
            Marshal.FreeHGlobal(sem);
            sem = IntPtr.Zero;
        }


        /// Return Type: byte
        ///sem: SEMAPHORE_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "takeSemaphore")]
        public static sbyte takeSemaphore(IntPtr sem)
        {
            var temp = (SEMAPHORE_ID) Marshal.PtrToStructure(sem, typeof (SEMAPHORE_ID));
            temp.semaphore.WaitOne();
            return 0;
        }


        /// Return Type: byte
        ///sem: SEMAPHORE_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "tryTakeSemaphore")]
        public static sbyte tryTakeSemaphore(IntPtr sem)
        {
            var temp = (SEMAPHORE_ID)Marshal.PtrToStructure(sem, typeof(SEMAPHORE_ID));
            bool retVal = temp.semaphore.WaitOne(0);
            return retVal ? (sbyte)1 : (sbyte)0;
        }


        /// Return Type: byte
        ///sem: SEMAPHORE_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "giveSemaphore")]
        public static sbyte giveSemaphore(IntPtr sem)
        {
            var temp = (SEMAPHORE_ID)Marshal.PtrToStructure(sem, typeof(SEMAPHORE_ID));
            temp.semaphore.Release();
            return 0;
        }


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
        //[DllImport("libHALAthena_shared.so", EntryPoint = "deleteMultiWait")]
        public static void deleteMultiWait(IntPtr sem)
        {
            Marshal.FreeHGlobal(sem);
            sem = IntPtr.Zero;
        }


        /// Return Type: byte
        ///sem: MULTIWAIT_ID->void*
        ///m: MUTEX_ID->void*
        ///timeout: int
        //[DllImport("libHALAthena_shared.so", EntryPoint = "takeMultiWait")]
        //public static extern byte takeMultiWait(System.IntPtr sem, System.IntPtr m, int timeout);
        public static sbyte takeMultiWait(IntPtr sem, IntPtr m, int timeout)
        {
           /*
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
             */
            var temp = (MULTIWAIT_ID)Marshal.PtrToStructure(sem, typeof(MULTIWAIT_ID));
            lock (temp.lockObject)
            {
                try
                {
                    Monitor.Wait(temp.lockObject);
                }
                catch (ThreadInterruptedException)
                {
                }
            }
            return 0;
        }

        /// Return Type: byte
        ///sem: MULTIWAIT_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "giveMultiWait")]
        public static sbyte giveMultiWait(IntPtr sem)
        {
            var temp = (MULTIWAIT_ID)Marshal.PtrToStructure(sem, typeof(MULTIWAIT_ID));
            lock (temp.lockObject)
            {
                try
                {
                    Monitor.PulseAll(temp.lockObject);
                }
                catch (ThreadInterruptedException)
                {
                    
                }
            }
            return 0;
        }
    }
}
