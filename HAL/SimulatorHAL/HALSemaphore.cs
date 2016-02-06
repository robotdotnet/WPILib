using System;
using System.Runtime.InteropServices;
using System.Threading;
using HAL.Base;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming

#pragma warning disable 1591

namespace HAL.SimulatorHAL
{
    ///<inheritdoc cref="HAL"/>
    internal class HALSemaphore
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALSemaphore.InitializeMutexNormal = initializeMutexNormal;
            Base.HALSemaphore.DeleteMutex = deleteMutex;
            Base.HALSemaphore.TakeMutex = takeMutex;
            Base.HALSemaphore.TryTakeMutex = tryTakeMutex;
            Base.HALSemaphore.GiveMutex = giveMutex;
            Base.HALSemaphore.InitializeMultiWait = initializeMultiWait;
            Base.HALSemaphore.DeleteMultiWait = deleteMultiWait;
            Base.HALSemaphore.TakeMultiWait = takeMultiWait;
            Base.HALSemaphore.GiveMultiWait = giveMultiWait;
        }

        [CalledSimFunction]
        public static MutexSafeHandle initializeMutexNormal()
        {
            MUTEX_ID p = new MUTEX_ID { lockObject = new object() };
            return new MutexSafeHandle(p);
        }

        [CalledSimFunction]
        public static void deleteMutex(MutexSafeHandle sem)
        {
        }

        [CalledSimFunction]
        public static void takeMutex(MutexSafeHandle sem)
        {
            var temp = sem.GetSimulatorPort();
            Monitor.Enter(temp.lockObject);
        }

        [CalledSimFunction]
        public static bool tryTakeMutex(MutexSafeHandle sem)
        {
            var temp = sem.GetSimulatorPort();
            bool retVal = Monitor.TryEnter(temp.lockObject);
            return retVal;
        }

        [CalledSimFunction]
        public static void giveMutex(MutexSafeHandle sem)
        {
            var temp = sem.GetSimulatorPort();
            Monitor.Exit(temp.lockObject);
        }

        [CalledSimFunction]
        public static MultiWaitSafeHandle initializeMultiWait()
        {
            MULTIWAIT_ID p = new MULTIWAIT_ID { lockObject = new object() };
            return new MultiWaitSafeHandle(p);
        }

        [CalledSimFunction]
        public static void deleteMultiWait(MultiWaitSafeHandle sem)
        {
        }

        [CalledSimFunction]
        public static void takeMultiWait(MultiWaitSafeHandle sem, MutexSafeHandle m)
        {
            var temp = sem.GetSimulatorPort();

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
        }

        [CalledSimFunction]
        public static void giveMultiWait(MultiWaitSafeHandle sem)
        {
            var temp = sem.GetSimulatorPort();
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
        }
    }
}
