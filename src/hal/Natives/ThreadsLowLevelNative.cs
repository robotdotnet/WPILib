using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class ThreadsLowLevelNative
    {
        public ThreadsLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_GetCurrentThreadPriorityFunc = (delegate* unmanaged[Cdecl] < System.Int32 *, int *, System.Int32 >)loader.GetProcAddress("HAL_GetCurrentThreadPriority");
            HAL_GetThreadPriorityFunc = (delegate* unmanaged[Cdecl] < void *, System.Int32 *, int *, System.Int32 >)loader.GetProcAddress("HAL_GetThreadPriority");
            HAL_SetCurrentThreadPriorityFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_SetCurrentThreadPriority");
            HAL_SetThreadPriorityFunc = (delegate* unmanaged[Cdecl] < void *, System.Int32, System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_SetThreadPriority");
        }

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
