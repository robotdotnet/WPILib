using WPIUtil.ILGeneration;

namespace Hal.Natives
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface INotifier
    {
        [StatusCheckLastParameter] void HAL_CancelNotifierAlarm(int notifierHandle);

        [StatusCheckLastParameter] void HAL_CleanNotifier(int notifierHandle);

        [StatusCheckedBy(typeof(StatusHandling), nameof(StatusHandling.StatusCheckForce))]
        [StatusCheckLastParameter]
        int HAL_InitializeNotifier();

        [StatusCheckLastParameter] void HAL_SetNotifierName(int notifierHandle, byte* name);

        [StatusCheckLastParameter] void HAL_StopNotifier(int notifierHandle);

        [StatusCheckLastParameter] void HAL_UpdateNotifierAlarm(int notifierHandle, ulong triggerTime);

        [StatusCheckLastParameter]
        ulong HAL_WaitForNotifierAlarm(int notifierHandle);

        unsafe ulong HAL_WaitForNotifierAlarm(int notifierHandle, int* status);

    }
}
