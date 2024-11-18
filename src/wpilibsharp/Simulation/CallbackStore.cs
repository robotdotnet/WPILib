using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using unsafe HalConstBufferCreate = delegate* managed<int, delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>, void*, int>;
using unsafe HalGlobalCreate = delegate* managed<delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>, void*, bool, int>;
using unsafe HalGlobalFree = delegate* managed<int, void>;
using unsafe HalIndexedCreate = delegate* managed<int, delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>, void*, bool, int>;
using unsafe HalIndexedFree = delegate* managed<int, int, void>;

namespace WPILib.Simulation;

public delegate void NotifyCallback(string name, HalValue value);

public delegate void ConstBufferCallback(string name, ReadOnlySpan<byte> buffer);

public sealed class CallbackStore : IDisposable
{
    private GCHandle delegateHandle;
    private readonly int nativeHandle;
    private readonly int? index;
    private readonly unsafe HalIndexedFree indexedFree;
    private readonly unsafe HalGlobalFree globalFree;

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe void HalNotifyCallback(byte* name, void* param, HalValue* value)
    {
        GCHandle handle = GCHandle.FromIntPtr((nint)param);
        if (handle.Target is NotifyCallback stringCallback)
        {
            string n = Marshal.PtrToStringUTF8((nint)name) ?? "";
            stringCallback(n, *value);
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe void HalConstBufferCallback(byte* name, void* param, byte* buffer, uint len)
    {
        GCHandle handle = GCHandle.FromIntPtr((nint)param);
        if (handle.Target is ConstBufferCallback stringCallback)
        {
            string n = Marshal.PtrToStringUTF8((nint)name) ?? "";
            stringCallback(n, new ReadOnlySpan<byte>(buffer, (int)len));
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

    public unsafe CallbackStore(NotifyCallback callback, bool immediateNotify, HalGlobalCreate create, HalGlobalFree free)
    {
        delegateHandle = GCHandle.Alloc(callback);
        nativeHandle = create(&HalNotifyCallback, (void*)(nint)delegateHandle, immediateNotify);
        index = null;
        globalFree = free;
    }

    public unsafe CallbackStore(NotifyCallback callback, int index, bool immediateNotify, HalIndexedCreate create, HalIndexedFree free)
    {
        delegateHandle = GCHandle.Alloc(callback);
        nativeHandle = create(index, &HalNotifyCallback, (void*)(nint)delegateHandle, immediateNotify);
        this.index = index;
        indexedFree = free;
    }

    public unsafe CallbackStore(ConstBufferCallback callback, int index, HalConstBufferCreate create, HalIndexedFree free)
    {
        delegateHandle = GCHandle.Alloc(callback);
        nativeHandle = create(index, &HalConstBufferCallback, (void*)(nint)delegateHandle);
        this.index = index;
        indexedFree = free;
    }
}
