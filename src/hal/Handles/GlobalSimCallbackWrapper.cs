using System.Runtime.InteropServices;

using unsafe HalNativeNotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Handles;

public readonly struct GlobalSimCallbackWrapper : IDisposable
{
    public required GCHandle DelegateHandle { get; init; }
    public required int NativeHandle { get; init; }
    public unsafe required delegate* managed<int, void> Cancel { get; init; }

    public unsafe void Dispose()
    {
        Cancel(NativeHandle);
        DelegateHandle.Free();
    }

    public unsafe GlobalSimCallbackWrapper(NotifyCallback callback, bool immediateNotify, delegate* managed<HalNativeNotifyCallback, void*, bool, int> register, delegate* managed<int, void> cancel)
    {
        DelegateHandle = GCHandle.Alloc(callback);
        NativeHandle = register(&SimCallbacks.HalNotifyCallback, (void*)GCHandle.ToIntPtr(DelegateHandle), immediateNotify);
        Cancel = cancel;
    }

    public unsafe GlobalSimCallbackWrapper(NotifyCallbackUtf8 callback, bool immediateNotify, delegate* managed<HalNativeNotifyCallback, void*, bool, int> register, delegate* managed<int, void> cancel)
    {
        DelegateHandle = GCHandle.Alloc(callback);
        NativeHandle = register(&SimCallbacks.HalNotifyCallback, (void*)GCHandle.ToIntPtr(DelegateHandle), immediateNotify);
        Cancel = cancel;
    }
}
