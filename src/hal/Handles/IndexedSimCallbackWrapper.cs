using System.Runtime.InteropServices;
using unsafe HalNativeNotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Handles;

public readonly struct IndexedSimCallbackWrapper : IDisposable
{
    public required GCHandle DelegateHandle { get; init; }
    public required int NativeHandle { get; init; }
    public required int Index { get; init; }
    public unsafe required delegate* managed<int, int, void> Cancel { get; init; }

    public unsafe IndexedSimCallbackWrapper(int index, NotifyCallback callback, bool immediateNotify, delegate* managed<int, HalNativeNotifyCallback, void*, bool, int> register, delegate* managed<int, int, void> cancel)
    {
        DelegateHandle = GCHandle.Alloc(callback);
        NativeHandle = register(index, &SimCallbacks.HalNotifyCallback, (void*)GCHandle.ToIntPtr(DelegateHandle), immediateNotify);
        Index = index;
        Cancel = cancel;
    }

    public unsafe IndexedSimCallbackWrapper(int index, NotifyCallbackUtf8 callback, bool immediateNotify, delegate* managed<int, HalNativeNotifyCallback, void*, bool, int> register, delegate* managed<int, int, void> cancel)
    {
        DelegateHandle = GCHandle.Alloc(callback);
        NativeHandle = register(index, &SimCallbacks.HalNotifyCallback, (void*)GCHandle.ToIntPtr(DelegateHandle), immediateNotify);
        Index = index;
        Cancel = cancel;
    }

    public unsafe void Dispose()
    {
        Cancel(Index, NativeHandle);
        DelegateHandle.Free();
    }
}
