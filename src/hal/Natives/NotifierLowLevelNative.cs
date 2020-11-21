using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class NotifierLowLevelNative
    {
        public NotifierLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_CancelNotifierAlarmFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_CancelNotifierAlarm");
            HAL_CleanNotifierFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_CleanNotifier");
            HAL_InitializeNotifierFunc = (delegate* unmanaged[Cdecl] < int *, System.Int32 >)loader.GetProcAddress("HAL_InitializeNotifier");
            HAL_SetNotifierNameFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Byte *, int *, void >)loader.GetProcAddress("HAL_SetNotifierName");
            HAL_StopNotifierFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_StopNotifier");
            HAL_UpdateNotifierAlarmFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.UInt64, int *, void >)loader.GetProcAddress("HAL_UpdateNotifierAlarm");
            HAL_WaitForNotifierAlarmFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.UInt64 >)loader.GetProcAddress("HAL_WaitForNotifierAlarm");
            HAL_WaitForNotifierAlarmFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32 *, System.UInt64 >)loader.GetProcAddress("HAL_WaitForNotifierAlarm");
        }

        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_CancelNotifierAlarmFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_CancelNotifierAlarm(int notifierHandle)
        {
            int status = 0;
            HAL_CancelNotifierAlarmFunc(notifierHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_CleanNotifierFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_CleanNotifier(int notifierHandle)
        {
            int status = 0;
            HAL_CleanNotifierFunc(notifierHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_InitializeNotifierFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_InitializeNotifier()
        {
            int status = 0;
            var retVal = HAL_InitializeNotifierFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int, byte*, int*, void> HAL_SetNotifierNameFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_SetNotifierName(int notifierHandle, byte* name)
        {
            int status = 0;
            HAL_SetNotifierNameFunc(notifierHandle, name, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_StopNotifierFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_StopNotifier(int notifierHandle)
        {
            int status = 0;
            HAL_StopNotifierFunc(notifierHandle, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



        private readonly delegate* unmanaged[Cdecl]<int, ulong, int*, void> HAL_UpdateNotifierAlarmFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_UpdateNotifierAlarm(int notifierHandle, ulong triggerTime)
        {
            int status = 0;
            HAL_UpdateNotifierAlarmFunc(notifierHandle, triggerTime, &status);
            Hal.StatusHandling.StatusCheck(status);
        }



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
