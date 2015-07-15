using System;
using System.Runtime.InteropServices;
using System.Threading;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    public class HALSemaphore
    {
        public static IntPtr initializeMutexRecursive()
        {
            MUTEX_ID p = new MUTEX_ID {lockObject = new object()};
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        public static IntPtr initializeMutexNormal()
        {
            MUTEX_ID p = new MUTEX_ID {lockObject = new object()};
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        public static void deleteMutex(IntPtr sem)
        {
            Marshal.FreeHGlobal(sem);
            sem = IntPtr.Zero;
        }

        public static sbyte takeMutex(IntPtr sem)
        {
            var temp = (MUTEX_ID)Marshal.PtrToStructure(sem, typeof (MUTEX_ID));
            Monitor.Enter(temp.lockObject);

            return 0;
        }

        public static sbyte tryTakeMutex(IntPtr sem)
        {
            var temp = (MUTEX_ID)Marshal.PtrToStructure(sem, typeof(MUTEX_ID));
            bool retVal = Monitor.TryEnter(temp.lockObject);
            return retVal ? (sbyte)1 : (sbyte)0;
        }

        public static sbyte giveMutex(IntPtr sem)
        {
            var temp = (MUTEX_ID)Marshal.PtrToStructure(sem, typeof(MUTEX_ID));
            Monitor.Exit(temp.lockObject);
            return 0;
        }

        public static IntPtr initializeSemaphore(uint initial_value)
        {
            var p = new MULTIWAIT_ID();
            p.lockObject = new object();
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }


        public static void deleteSemaphore(IntPtr sem)
        {
            Marshal.FreeHGlobal(sem);
            sem = IntPtr.Zero;
        }

        public static sbyte takeSemaphore(IntPtr sem)
        {
            var temp = (MULTIWAIT_ID) Marshal.PtrToStructure(sem, typeof (MULTIWAIT_ID));
            Monitor.Enter(temp.lockObject);//temp.semaphore.WaitOne();
            return 0;
        }

        public static sbyte tryTakeSemaphore(IntPtr sem)
        {
            var temp = (MULTIWAIT_ID)Marshal.PtrToStructure(sem, typeof(MULTIWAIT_ID));
            bool retVal = Monitor.TryEnter(temp.lockObject);
            
            return retVal ? (sbyte)0 : (sbyte)1;
        }

        public static sbyte giveSemaphore(IntPtr sem)
        {
            var temp = (MULTIWAIT_ID)Marshal.PtrToStructure(sem, typeof(MULTIWAIT_ID));
            if (Monitor.IsEntered(temp.lockObject))
                Monitor.Exit(temp.lockObject);
            return 0;
        }

        public static IntPtr initializeMultiWait()
        {
            MULTIWAIT_ID p = new MULTIWAIT_ID {lockObject = new object()};
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        public static void deleteMultiWait(IntPtr sem)
        {
            Marshal.FreeHGlobal(sem);
            sem = IntPtr.Zero;
        }

        public static sbyte takeMultiWait(IntPtr sem, IntPtr m, int timeout)
        {
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
