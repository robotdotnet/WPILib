//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Linq;
using System.Reflection;

// ReSharper disable CheckNamespace

namespace HAL_Base
{
    public partial class HALSemaphore
    {
        static HALSemaphore()
        {
            HAL.Initialize();
        }

        public delegate IntPtr InitializeMutexRecursiveDelegate();
        public static InitializeMutexRecursiveDelegate InitializeMutexRecursive;

        public delegate IntPtr InitializeMutexNormalDelegate();
        public static InitializeMutexNormalDelegate InitializeMutexNormal;

        public delegate void DeleteMutexDelegate(IntPtr sem);
        public static DeleteMutexDelegate DeleteMutex;

        public delegate sbyte TakeMutexDelegate(IntPtr sem);
        public static TakeMutexDelegate TakeMutex;

        public delegate sbyte TryTakeMutexDelegate(IntPtr sem);
        public static TryTakeMutexDelegate TryTakeMutex;

        public delegate sbyte GiveMutexDelegate(IntPtr sem);
        public static GiveMutexDelegate GiveMutex;

        public delegate IntPtr InitializeSemaphoreDelegate(uint initial_value);
        public static InitializeSemaphoreDelegate InitializeSemaphore;

        public delegate void DeleteSemaphoreDelegate(IntPtr sem);
        public static DeleteSemaphoreDelegate DeleteSemaphore;

        public delegate sbyte TakeSemaphoreDelegate(IntPtr sem);
        public static TakeSemaphoreDelegate TakeSemaphore;

        public delegate sbyte TryTakeSemaphoreDelegate(IntPtr sem);
        public static TryTakeSemaphoreDelegate TryTakeSemaphore;

        public delegate sbyte GiveSemaphoreDelegate(IntPtr sem);
        public static GiveSemaphoreDelegate GiveSemaphore;

        public delegate IntPtr InitializeMultiWaitDelegate();
        public static InitializeMultiWaitDelegate InitializeMultiWait;

        public delegate void DeleteMultiWaitDelegate(IntPtr sem);
        public static DeleteMultiWaitDelegate DeleteMultiWait;

        public delegate sbyte TakeMultiWaitDelegate(IntPtr sem, IntPtr m, int timeout);
        public static TakeMultiWaitDelegate TakeMultiWait;

        public delegate sbyte GiveMultiWaitDelegate(IntPtr sem);
        public static GiveMultiWaitDelegate GiveMultiWait;
    }
}
