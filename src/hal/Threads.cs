
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IThreads))]
    public unsafe static class Threads
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IThreads lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public static int GetCurrentThreadPriority(int* isRealTime)
        {
            return lowLevel.HAL_GetCurrentThreadPriority(isRealTime);
        }

        public static int GetThreadPriority(void* handle, int* isRealTime)
        {
            return lowLevel.HAL_GetThreadPriority(handle, isRealTime);
        }

        public static int SetCurrentThreadPriority(int realTime, int priority)
        {
            return lowLevel.HAL_SetCurrentThreadPriority(realTime, priority);
        }

        public static int SetThreadPriority(void* handle, int realTime, int priority)
        {
            return lowLevel.HAL_SetThreadPriority(handle, realTime, priority);
        }

    }
}
