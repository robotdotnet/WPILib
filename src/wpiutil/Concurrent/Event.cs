using System;
using WPIUtil.Natives;

namespace WPIUtil.Concurrent;

public sealed class Event(bool manualReset = false, bool initialState = false) : IDisposable
{
    public int Handle { get; private set; } = SynchronizationNative.CreateEvent(manualReset, initialState);

    public void Dispose()
    {
        if (Handle != 0)
        {
            SynchronizationNative.DestroyEvent(Handle);
            Handle = 0;
        }
    }

    public void Set()
    {
        SynchronizationNative.SetEvent(Handle);
    }

    public void Reset()
    {
        SynchronizationNative.ResetEvent(Handle);
    }
}
