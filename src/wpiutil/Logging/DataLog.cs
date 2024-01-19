using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

    public void AddSchema(IStructBase value, long timestamp = 0)
    {
        AddSchemaImpl(value, timestamp == 0 ? (long)TimestampNative.Now() : timestamp, []);
    }

    public void AddSchema(IProtobufBase proto, long timestamp = 0)
    {
        long actualTimestamp = timestamp == 0 ? (long)TimestampNative.Now() : timestamp;
        proto.ForEachDescriptor(HasSchema, (typeString, schema) =>
        {
            AddSchema(typeString, "proto:FileDescriptorProto", schema, actualTimestamp);
        });
    }

    public bool HasSchema(string name)
    {
        return m_schemaMap.ContainsKey(name);
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
        AddSchema(typeString, "structschema", value.Schema, timestamp);
        foreach (var inner in value.Nested)
        {
            AddSchemaImpl(inner, timestamp, seen);
        }
        seen.Remove(typeString);
    }

    private readonly ConcurrentDictionary<string, int> m_schemaMap = [];

    public unsafe OpaqueDataLog* NativeHandle { get; } = null;
}
