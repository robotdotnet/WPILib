

using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace HAL_FRC
{
    public class HALSemaphore
    {
        /// Return Type: MUTEX_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "initializeMutexRecursive")]
        public static System.IntPtr initializeMutexRecursive()
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
        public static void deleteMutex(System.IntPtr sem)
        {
            Marshal.FreeHGlobal(sem);
            sem = IntPtr.Zero;
        }


        /// Return Type: byte
        ///sem: MUTEX_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "takeMutex")]
        public static sbyte takeMutex(System.IntPtr sem)
        {
            var temp = (MUTEX_ID)Marshal.PtrToStructure(sem, typeof (MUTEX_ID));
            Monitor.Enter(temp.lockObject);

            return 0;
        }


        /// Return Type: byte
        ///sem: MUTEX_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "tryTakeMutex")]
        public static sbyte tryTakeMutex(System.IntPtr sem)
        {
            var temp = (MUTEX_ID)Marshal.PtrToStructure(sem, typeof(MUTEX_ID));
            bool retVal = Monitor.TryEnter(temp.lockObject);
            return retVal ? (sbyte)1 : (sbyte)0;
        }


        /// Return Type: byte
        ///sem: MUTEX_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "giveMutex")]
        public static sbyte giveMutex(System.IntPtr sem)
        {
            var temp = (MUTEX_ID)Marshal.PtrToStructure(sem, typeof(MUTEX_ID));
            Monitor.Exit(temp.lockObject);
            return 0;
        }


        /// Return Type: SEMAPHORE_ID->void*
        ///initial_value: unsigned int
        //[DllImport("libHALAthena_shared.so", EntryPoint = "initializeSemaphore")]
        public static System.IntPtr initializeSemaphore(uint initial_value)
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
        public static void deleteSemaphore(System.IntPtr sem)
        {
            Marshal.FreeHGlobal(sem);
            sem = IntPtr.Zero;
        }


        /// Return Type: byte
        ///sem: SEMAPHORE_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "takeSemaphore")]
        public static sbyte takeSemaphore(System.IntPtr sem)
        {
            var temp = (SEMAPHORE_ID) Marshal.PtrToStructure(sem, typeof (SEMAPHORE_ID));
            temp.semaphore.WaitOne();
            return 0;
        }


        /// Return Type: byte
        ///sem: SEMAPHORE_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "tryTakeSemaphore")]
        public static sbyte tryTakeSemaphore(System.IntPtr sem)
        {
            var temp = (SEMAPHORE_ID)Marshal.PtrToStructure(sem, typeof(SEMAPHORE_ID));
            bool retVal = temp.semaphore.WaitOne(0);
            return retVal ? (sbyte)1 : (sbyte)0;
        }


        /// Return Type: byte
        ///sem: SEMAPHORE_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "giveSemaphore")]
        public static sbyte giveSemaphore(System.IntPtr sem)
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
        public static void deleteMultiWait(System.IntPtr sem)
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
                catch (ThreadInterruptedException ex)
                {
                }
            }
            return 0;
        }

        /// Return Type: byte
        ///sem: MULTIWAIT_ID->void*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "giveMultiWait")]
        public static sbyte giveMultiWait(System.IntPtr sem)
        {
            var temp = (MULTIWAIT_ID)Marshal.PtrToStructure(sem, typeof(MULTIWAIT_ID));
            lock (temp.lockObject)
            {
                try
                {
                    Monitor.PulseAll(temp.lockObject);
                }
                catch (ThreadInterruptedException e)
                {
                    
                }
            }
            return 0;
        }
    }
}
