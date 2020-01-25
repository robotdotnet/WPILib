using WPIUtil.ILGeneration;

namespace Hal.Natives
{
   public unsafe interface IThreads
    {
        [StatusCheckLastParameter]  int HAL_GetCurrentThreadPriority(int* isRealTime);

        [StatusCheckLastParameter]  int HAL_GetThreadPriority(NativeThreadHandle handle, int* isRealTime);

        [StatusCheckLastParameter]  int HAL_SetCurrentThreadPriority(int realTime, int priority);

        [StatusCheckLastParameter]  int HAL_SetThreadPriority(NativeThreadHandle handle, int realTime, int priority);

    }
}
