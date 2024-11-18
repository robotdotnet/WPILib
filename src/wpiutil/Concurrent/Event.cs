using WPIUtil.Handles;
using WPIUtil.Natives;

namespace WPIUtil.Concurrent;

public sealed class WpiEvent(bool manualReset = false, bool initialState = false) : IDisposable
{
    public WpiEventHandle Handle { get; private set; } = SynchronizationNative.CreateEvent(manualReset, initialState);

    public void Dispose()
    {
        if (Handle.Handle != 0)
        {
            SynchronizationNative.DestroyEvent(Handle);
            Handle = default;
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
