using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using unsafe HalGlobalCreate = delegate* managed<delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>, void*, bool, int>;
using unsafe HalGlobalFree = delegate* managed<int, void>;
using unsafe HalIndexedCreate = delegate* managed<int, delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>, void*, bool, int>;
using unsafe HalIndexedFree = delegate* managed<int, int, void>;
using unsafe HalNativeNotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPILib.Simulation;

public class CallbackStore : IDisposable
{
    private GCHandle delegateHandle;
    private readonly int nativeHandle;
    private readonly int? index;
    private unsafe readonly HalIndexedFree indexedFree;
    private unsafe readonly HalGlobalFree globalFree;

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe void HalNotifyCallback(byte* name, void* param, HalValue* value)
    {
        GCHandle handle = GCHandle.FromIntPtr((nint)param);
        if (handle.Target is Action<string, HalValue> stringCallback)
        {
            string n = Marshal.PtrToStringUTF8((nint)name) ?? "";
            stringCallback(n, *value);
        }
    }

    public unsafe void Dispose()
    {
        GC.SuppressFinalize(this);
        if (index is { } x)
        {
            indexedFree(x, nativeHandle);
        }
        else
        {
            globalFree(nativeHandle);
        }
        delegateHandle.Free();
    }

    public unsafe CallbackStore(Action<string, HalValue> callback, bool immediateNotify, HalGlobalCreate create, HalGlobalFree free)
    {
        delegateHandle = GCHandle.Alloc(callback);
        nativeHandle = create(&HalNotifyCallback, (void*)(nint)delegateHandle, immediateNotify);
        index = null;
        globalFree = free;
    }

    public unsafe CallbackStore(Action<string, HalValue> callback, int index, bool immediateNotify, HalIndexedCreate create, HalIndexedFree free)
    {
        delegateHandle = GCHandle.Alloc(callback);
        nativeHandle = create(index, &HalNotifyCallback, (void*)(nint)delegateHandle, immediateNotify);
        this.index = index;
        indexedFree = free;
    }
}
