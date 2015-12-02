//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace

namespace HAL.Base
{
    public partial class HALSemaphore
    {
        static HALSemaphore()
        {
            HAL.Initialize();
        }

        public delegate IntPtr InitializeMutexNormalDelegate();
        public static InitializeMutexNormalDelegate InitializeMutexNormal;

        public delegate void DeleteMutexDelegate(IntPtr sem);
        public static DeleteMutexDelegate DeleteMutex;

        public delegate void TakeMutexDelegate(IntPtr sem);
        public static TakeMutexDelegate TakeMutex;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool TryTakeMutexDelegate(IntPtr sem);
        public static TryTakeMutexDelegate TryTakeMutex;

        public delegate void GiveMutexDelegate(IntPtr sem);
        public static GiveMutexDelegate GiveMutex;

        public delegate IntPtr InitializeMultiWaitDelegate();
        public static InitializeMultiWaitDelegate InitializeMultiWait;

        public delegate void DeleteMultiWaitDelegate(IntPtr sem);
        public static DeleteMultiWaitDelegate DeleteMultiWait;

        public delegate void TakeMultiWaitDelegate(IntPtr sem, IntPtr m);
        public static TakeMultiWaitDelegate TakeMultiWait;

        public delegate void GiveMultiWaitDelegate(IntPtr sem);
        public static GiveMultiWaitDelegate GiveMultiWait;
    }
}
