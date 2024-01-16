using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalNotifier
{
    public static void CancelNotifierAlarm(HalNotifierHandle notifierHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        CancelNotifierAlarmRefShim(notifierHandle, ref status);
    }
    public static void CleanNotifier(HalNotifierHandle notifierHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        CleanNotifierRefShim(notifierHandle, ref status);
    }
    public static HalNotifierHandle InitializeNotifier(out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeNotifierRefShim(ref status);
    }
    public static void SetNotifierName(HalNotifierHandle notifierHandle, string name, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetNotifierNameRefShim(notifierHandle, name, ref status);
    }
    public static void StopNotifier(HalNotifierHandle notifierHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        StopNotifierRefShim(notifierHandle, ref status);
    }
    public static void UpdateNotifierAlarm(HalNotifierHandle notifierHandle, ulong triggerTime, out HalStatus status)
    {
        status = HalStatus.Ok;
        UpdateNotifierAlarmRefShim(notifierHandle, triggerTime, ref status);
    }
    public static int SetNotifierThreadPriority(int realTime, int priority, out HalStatus status)
    {
        status = HalStatus.Ok;
        return SetNotifierThreadPriorityRefShim(realTime, priority, ref status);
    }
    public static ulong WaitForNotifierAlarm(HalNotifierHandle notifierHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return WaitForNotifierAlarmRefShim(notifierHandle, ref status);
    }
}
