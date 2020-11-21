using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class ThreadsLowLevelNative
    {
        
        private readonly delegate* unmanaged[Cdecl]<int*, int*, int> HAL_GetCurrentThreadPriorityFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetCurrentThreadPriority(int* isRealTime)
        {
            int status = 0;
            var retVal = HAL_GetCurrentThreadPriorityFunc(isRealTime, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        
        private readonly delegate* unmanaged[Cdecl]<void*, int*, int*, int> HAL_GetThreadPriorityFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetThreadPriority(void* handle, int* isRealTime)
        {
            int status = 0;
            var retVal = HAL_GetThreadPriorityFunc(handle, isRealTime, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, int, int*, int> HAL_SetCurrentThreadPriorityFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_SetCurrentThreadPriority(int realTime, int priority)
        {
            int status = 0;
            var retVal = HAL_SetCurrentThreadPriorityFunc(realTime, priority, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        
        private readonly delegate* unmanaged[Cdecl]<void*, int, int, int*, int> HAL_SetThreadPriorityFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_SetThreadPriority(void* handle, int realTime, int priority)
        {
            int status = 0;
            var retVal = HAL_SetThreadPriorityFunc(handle, realTime, priority, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



    }
}
