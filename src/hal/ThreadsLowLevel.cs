
using Hal.Natives;
using WPIUtil.ILGeneration;

namespace Hal
{

    public static unsafe class ThreadsLowLevel
    {
        internal static ThreadsLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

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
