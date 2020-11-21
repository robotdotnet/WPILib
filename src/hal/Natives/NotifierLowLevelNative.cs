using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class NotifierLowLevelNative : INotifier
    {
        [NativeFunctionPointer("HAL_CancelNotifierAlarm")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_CancelNotifierAlarmFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_CancelNotifierAlarm(int notifierHandle)
        {
            int status = 0;
            HAL_CancelNotifierAlarmFunc(notifierHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_CleanNotifier")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_CleanNotifierFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_CleanNotifier(int notifierHandle)
        {
            int status = 0;
            HAL_CleanNotifierFunc(notifierHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_InitializeNotifier")]
        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_InitializeNotifierFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeNotifier()
        {
            int status = 0;
            var retVal = HAL_InitializeNotifierFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_SetNotifierName")]
        private readonly delegate* unmanaged[Cdecl]<int, byte*, int*, void> HAL_SetNotifierNameFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetNotifierName(int notifierHandle, byte* name)
        {
            int status = 0;
            HAL_SetNotifierNameFunc(notifierHandle, name, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_StopNotifier")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_StopNotifierFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_StopNotifier(int notifierHandle)
        {
            int status = 0;
            HAL_StopNotifierFunc(notifierHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_UpdateNotifierAlarm")]
        private readonly delegate* unmanaged[Cdecl]<int, ulong, int*, void> HAL_UpdateNotifierAlarmFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_UpdateNotifierAlarm(int notifierHandle, ulong triggerTime)
        {
            int status = 0;
            HAL_UpdateNotifierAlarmFunc(notifierHandle, triggerTime, &status);
            Hal.StatusHandling.StatusCheck(status);
        }


        [NativeFunctionPointer("HAL_WaitForNotifierAlarm")]
        private readonly delegate* unmanaged[Cdecl]<int, int*, ulong> HAL_WaitForNotifierAlarmFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong HAL_WaitForNotifierAlarm(int notifierHandle)
        {
            int status = 0;
            var retVal = HAL_WaitForNotifierAlarmFunc(notifierHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong HAL_WaitForNotifierAlarm(int notifierHandle, int* status)
        {
            return HAL_WaitForNotifierAlarmFunc(notifierHandle, status);
        }



    }
}
