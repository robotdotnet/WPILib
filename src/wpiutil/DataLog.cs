global using EntryHandle = int;

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIUtil.Handles;
using WPIUtil.Natives;
using WPIUtil.Serialization;

namespace WPIUtil;

public sealed unsafe class DataLog : IDisposable
{
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void NativeDataLogCallback(void* ptr, byte* data, nuint len)
    {
        GCHandle handle = GCHandle.FromIntPtr((nint)ptr);
        if (handle.Target is DataLog datalog)
        {
            datalog.callback?.Invoke(new ReadOnlySpan<byte>(data, (int)len));
        }
    }

    private GCHandle? gcHandle = null;
    private readonly DataLogCallback? callback = null;

    public delegate void DataLogCallback(ReadOnlySpan<byte> data);

    public DataLog(string? dir = null, string? filename = null, double period = 0.25, string? extraHeader = null)
    {
        NativeHandle = DataLogNative.DataLogCreate(dir, filename, period, extraHeader);
    }

    public DataLog(DataLogCallback callback, double period = 0.25, string? extraHeader = null)
    {
        gcHandle = GCHandle.Alloc(this);
        this.callback = callback;
        NativeHandle = DataLogNative.DataLogCreateFunc(&NativeDataLogCallback, (void*)GCHandle.ToIntPtr(gcHandle.Value), period, extraHeader);
    }

    public void Dispose()
    {
        DataLogNative.DataLogRelease(NativeHandle);
        if (gcHandle.HasValue)
        {
            gcHandle.Value.Free();
        }
    }

    public DataLogEntryHandle Start(string name, string type, string metadata = "", long timestamp = 0)
    {
        return DataLogNative.DataLogStart(NativeHandle, name, type, metadata, (ulong)timestamp);
    }

    public void SetMetadata(DataLogEntryHandle entry, string metadata, long timestamp = 0)
    {
        DataLogNative.DataLogSetMetadata(NativeHandle, entry, metadata, (ulong)timestamp);
    }

    public void Finish(DataLogEntryHandle entry, long timestamp = 0)
    {
        DataLogNative.DataLogFinish(NativeHandle, entry, (ulong)timestamp);
    }

    public void AppendRaw(DataLogEntryHandle entry, ReadOnlySpan<byte> data, long timestamp = 0)
    {
        DataLogNative.DataLogAppend(NativeHandle, entry, data, (ulong)timestamp);
    }

    public void AddSchema<T>(Struct<T> value, long timestamp = 0)
    {
        throw new NotImplementedException();
    }

    public unsafe OpaqueDataLog* NativeHandle { get; } = null;
}
