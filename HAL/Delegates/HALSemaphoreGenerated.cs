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

        public delegate MutexSafeHandle InitializeMutexNormalDelegate();
        public static InitializeMutexNormalDelegate InitializeMutexNormal;

        public delegate void DeleteMutexDelegate(MutexSafeHandle sem);
        public static DeleteMutexDelegate DeleteMutex;

        public delegate void TakeMutexDelegate(MutexSafeHandle sem);
        public static TakeMutexDelegate TakeMutex;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool TryTakeMutexDelegate(MutexSafeHandle sem);
        public static TryTakeMutexDelegate TryTakeMutex;

        public delegate void GiveMutexDelegate(MutexSafeHandle sem);
        public static GiveMutexDelegate GiveMutex;

        public delegate MultiWaitSafeHandle InitializeMultiWaitDelegate();
        public static InitializeMultiWaitDelegate InitializeMultiWait;

        public delegate void DeleteMultiWaitDelegate(MultiWaitSafeHandle sem);
        public static DeleteMultiWaitDelegate DeleteMultiWait;

        public delegate void TakeMultiWaitDelegate(MultiWaitSafeHandle sem, MutexSafeHandle m);
        public static TakeMultiWaitDelegate TakeMultiWait;

        public delegate void GiveMultiWaitDelegate(MultiWaitSafeHandle sem);
        public static GiveMultiWaitDelegate GiveMultiWait;
    }
}
