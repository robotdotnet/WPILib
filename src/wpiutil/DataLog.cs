using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIUtil.Natives;

namespace WPIUtil;

public sealed unsafe class DataLog : IDisposable {
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void NativeDataLogCallback(void* ptr, byte* data, nuint len) {
        GCHandle handle = GCHandle.FromIntPtr((nint)ptr);
        if (handle.Target is DataLog datalog)
        {
            datalog.callback?.Invoke(new ReadOnlySpan<byte>(data, (int)len));
        }
    }

    private unsafe DataLogHandle dataLogHandle = null;
    private GCHandle? gcHandle = null;
    private DataLogCallback? callback = null;

    public delegate void DataLogCallback(ReadOnlySpan<byte> data);

    public DataLog(string? dir = null, string? filename = null, double period = 0.25, string? extraHeader = null) {
        dataLogHandle = DataLogNative.DataLogCreate(dir, filename, period, extraHeader);
    }

    public DataLog(DataLogCallback callback, double period = 0.25, string? extraHeader = null) {
        gcHandle = GCHandle.Alloc(this);
        this.callback = callback;
        dataLogHandle = DataLogNative.DataLogCreateFunc(&NativeDataLogCallback, (void*)GCHandle.ToIntPtr(gcHandle.Value), period, extraHeader);
    }

    public void Dispose()
    {
        DataLogNative.DataLogRelease(dataLogHandle);
        if (gcHandle.HasValue) {
            gcHandle.Value.Free();
        }
    }
}
