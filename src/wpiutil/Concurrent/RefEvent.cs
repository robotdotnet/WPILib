using WPIUtil.Handles;
using WPIUtil.Natives;

namespace WPIUtil.Concurrent;

public readonly ref struct RefEvent
{
    public WpiEventHandle Handle { get; }

    public readonly void Dispose()
    {
        if (Handle.Handle != 0)
        {
            SynchronizationNative.DestroyEvent(Handle);
        }
    }

    public readonly void Set()
    {
        SynchronizationNative.SetEvent(Handle);
    }

    public readonly void Reset()
    {
        SynchronizationNative.SetEvent(Handle);
    }
}
