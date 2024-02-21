using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIUtil.Handles;
using WPIUtil.Natives;
using WPIUtil.Serialization.Protobuf;
using WPIUtil.Serialization.Struct;

namespace WPIUtil.Logging;

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

    public DataLog(string dir = "", string filename = "", double period = 0.25, string extraHeader = "")
    {
        NativeHandle = DataLogNative.Create(dir, filename, period, extraHeader);
    }

    public DataLog(DataLogCallback callback, double period = 0.25, string extraHeader = "")
    {
        gcHandle = GCHandle.Alloc(this);
        this.callback = callback;
        NativeHandle = DataLogNative.CreateFunc(&NativeDataLogCallback, (void*)GCHandle.ToIntPtr(gcHandle.Value), period, extraHeader);
    }

    public void Dispose()
    {
        DataLogNative.Release(NativeHandle);
        if (gcHandle.HasValue)
        {
            gcHandle.Value.Free();
        }
    }

    public void SetFilename(string filename)
    {
        DataLogNative.SetFilename(NativeHandle, filename);
    }

    public void Flush()
    {
        DataLogNative.Flush(NativeHandle);
    }

    public void Pause()
    {
        DataLogNative.Pause(NativeHandle);
    }

    public void Resume()
    {
        DataLogNative.Resume(NativeHandle);
    }

    public void Stop()
    {
        DataLogNative.Stop(NativeHandle);
    }

    public bool HasSchema(string name)
    {
        return m_schemaMap.ContainsKey(name);
    }

    private void AddSchemaNative(string name, WpiString type, WpiString schema, long timestamp = 0)
    {
        if (!m_schemaMap.TryAdd(name, 1))
        {
            return;
        }
        // TODO add schema functions
        // DataLogNative
    }

    public void AddSchema(string name, string type, string schema, long timestamp = 0)
    {
        if (!m_schemaMap.TryAdd(name, 1))
        {
            return;
        }
        // TODO add schema functions
        // DataLogNative
    }

    public void AddSchema(string name, string type, ReadOnlySpan<byte> schema, long timestamp = 0)
    {
        if (!m_schemaMap.TryAdd(name, 1))
        {
            return;
        }
        // TODO add schema functions
        // DataLogNative
    }

    public void AddSchema(IStructBase value, long timestamp = 0)
    {
        AddSchemaImpl(value, timestamp == 0 ? (long)TimestampNative.Now() : timestamp, []);
    }

    public void AddSchema(IProtobuf proto, long timestamp = 0)
    {
        long actualTimestamp = timestamp == 0 ? (long)TimestampNative.Now() : timestamp;
        proto.ForEachDescriptor(HasSchema, (typeString, schema) =>
        {
            AddSchemaNative(typeString, "proto:FileDescriptorProto"u8, schema, actualTimestamp);
        });
    }

    public DataLogEntryHandle Start(string name, string type, string metadata = "", long timestamp = 0)
    {
        return DataLogNative.Start(NativeHandle, name, type, metadata, (ulong)timestamp);
    }

    public DataLogEntryHandle Start(string name, ReadOnlySpan<byte> type, string metadata = "", long timestamp = 0)
    {
        return DataLogNative.Start(NativeHandle, name, type, metadata, (ulong)timestamp);
    }

    public void Finish(DataLogEntryHandle entry, long timestamp = 0)
    {
        DataLogNative.Finish(NativeHandle, entry, (ulong)timestamp);
    }

    public void SetMetadata(DataLogEntryHandle entry, string metadata, long timestamp = 0)
    {
        DataLogNative.SetMetadata(NativeHandle, entry, metadata, (ulong)timestamp);
    }

    public void AppendRaw(DataLogEntryHandle entry, ReadOnlySpan<byte> data, long timestamp = 0)
    {
        DataLogNative.AppendRaw(NativeHandle, entry, data, (ulong)timestamp);
    }

    public void AppendBoolean(DataLogEntryHandle entry, bool value, long timestamp = 0)
    {
        DataLogNative.AppendBoolean(NativeHandle, entry, value, (ulong)timestamp);
    }

    public void AppendInteger(DataLogEntryHandle entry, long value, long timestamp = 0)
    {
        DataLogNative.AppendInteger(NativeHandle, entry, value, (ulong)timestamp);
    }

    public void AppendFloat(DataLogEntryHandle entry, float value, long timestamp = 0)
    {
        DataLogNative.AppendFloat(NativeHandle, entry, value, (ulong)timestamp);
    }

    public void AppendDouble(DataLogEntryHandle entry, double value, long timestamp = 0)
    {
        DataLogNative.AppendDouble(NativeHandle, entry, value, (ulong)timestamp);
    }

    public void AppendString(DataLogEntryHandle entry, string value, long timestamp = 0)
    {
        DataLogNative.AppendString(NativeHandle, entry, value, (ulong)timestamp);
    }

    public void AppendString(DataLogEntryHandle entry, ReadOnlySpan<char> value, long timestamp = 0)
    {
        DataLogNative.AppendString(NativeHandle, entry, value, (ulong)timestamp);
    }

    public void AppendString(DataLogEntryHandle entry, ReadOnlySpan<byte> value, long timestamp = 0)
    {
        DataLogNative.AppendString(NativeHandle, entry, value, (ulong)timestamp);
    }

    public void AppendBooleanArray(DataLogEntryHandle entry, ReadOnlySpan<bool> value, long timestamp = 0)
    {
        DataLogNative.AppendBooleanArray(NativeHandle, entry, value, (ulong)timestamp);
    }

    public void AppendIntegerArray(DataLogEntryHandle entry, ReadOnlySpan<long> value, long timestamp = 0)
    {
        DataLogNative.AppendIntegerArray(NativeHandle, entry, value, (ulong)timestamp);
    }

    public void AppendFloatArray(DataLogEntryHandle entry, ReadOnlySpan<float> value, long timestamp = 0)
    {
        DataLogNative.AppendFloatArray(NativeHandle, entry, value, (ulong)timestamp);
    }

    public void AppendDoubleArray(DataLogEntryHandle entry, ReadOnlySpan<double> value, long timestamp = 0)
    {
        DataLogNative.AppendDoubleArray(NativeHandle, entry, value, (ulong)timestamp);
    }

    public void AppendStringArray(DataLogEntryHandle entry, ReadOnlySpan<string> value, long timestamp = 0)
    {
        DataLogNative.AppendStringArray(NativeHandle, entry, value, (ulong)timestamp);
    }

    private void AddSchemaImpl(IStructBase value, long timestamp, HashSet<string> seen)
    {
        string typeString = value.TypeString;
        if (HasSchema(typeString))
        {
            return;
        }
        if (!seen.Add(typeString))
        {
            throw new InvalidOperationException($"{typeString}: circular reference with {seen}");
        }
        AddSchemaNative(typeString, "structschema"u8, value.Schema, timestamp);
        foreach (var inner in value.Nested)
        {
            AddSchemaImpl(inner, timestamp, seen);
        }
        seen.Remove(typeString);
    }

    private readonly ConcurrentDictionary<string, int> m_schemaMap = [];

    public unsafe OpaqueDataLog* NativeHandle { get; } = null;
}
