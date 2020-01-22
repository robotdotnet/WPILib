using System;
using System.Collections.Generic;
using System.Text;
using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    public interface INotifier
    {
        [StatusCheckLastParameter]
        int HAL_InitializeNotifier();

        [StatusCheckLastParameter]
        unsafe void HAL_SetNotifierName(int notifierHandle, byte* name);

        [StatusCheckLastParameter]
        void HAL_StopNotifier(int notifierHandle);

        [StatusCheckLastParameter]
        void HAL_CleanNotifier(int notifierHandle);

        [StatusCheckLastParameter]
        void HAL_UpdateNotifierAlarm(int notifierHandle, ulong triggerTime);

        [StatusCheckLastParameter]
        void HAL_CancelNotifierAlarm(int notifierHandle);

        [StatusCheckLastParameter]
        ulong HAL_WaitForNotifierAlarm(int notifierHandle);

        unsafe ulong HAL_WaitForNotifierAlarm(int notifierHandle, int* status);
    }
}
