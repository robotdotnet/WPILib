namespace WPIHal.Natives;

public static unsafe partial class HalThreads
{
    public static int GetCurrentThreadPriority(out int isRealTime, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetCurrentThreadPriorityRefShim(out isRealTime, ref status);
    }
    public static unsafe int GetThreadPriority(void* handle, out int isRealTime, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetThreadPriorityRefShim(handle, out isRealTime, ref status);
    }
    public static int SetCurrentThreadPriority(int realTime, int priority, out HalStatus status)
    {
        status = HalStatus.Ok;
        return SetCurrentThreadPriorityRefShim(realTime, priority, ref status);
    }
    public static unsafe int SetThreadPriority(void* handle, int realTime, int priority, out HalStatus status)
    {
        status = HalStatus.Ok;
        return SetThreadPriorityRefShim(handle, realTime, priority, ref status);
    }
}
