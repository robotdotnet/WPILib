using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    public unsafe interface IThreads
    {
        [StatusCheckLastParameter] int HAL_GetCurrentThreadPriority(int* isRealTime);

        [StatusCheckLastParameter] int HAL_GetThreadPriority(void* handle, int* isRealTime);

        [StatusCheckLastParameter] int HAL_SetCurrentThreadPriority(int realTime, int priority);

        [StatusCheckLastParameter] int HAL_SetThreadPriority(void* handle, int realTime, int priority);

    }
}
