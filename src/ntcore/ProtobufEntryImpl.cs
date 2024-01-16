using System;
using System.Buffers;
using System.Reflection.Metadata;
using Google.Protobuf;
using NetworkTables.Handles;
using NetworkTables.Natives;
using WPIUtil.Serialization.Protobuf;

namespace NetworkTables;

internal sealed class ProtobufEntryImpl<T, THandle> : EntryBase<THandle>, IProtobufEntry<T> where THandle : struct, INtEntryHandle where T : IProtobufSerializable<T>
{
    internal ProtobufEntryImpl(ProtobufTopic<T> topic, THandle handle, T defaultValue, bool schemaPublished) : base(handle)
    {
        Topic = topic;
        m_defaultValue = defaultValue;
        m_buf = new ProtobufBuffer<T>();
        m_schemaPublished = schemaPublished;
    }

    private readonly T m_defaultValue;
    private readonly ProtobufBuffer<T> m_buf;
    private bool m_schemaPublished;
    private readonly object m_lockObject = new();

    public override ProtobufTopic<T> Topic { get; }

    public T Get()
    {
        throw new System.NotImplementedException();
    }

    public T Get(T defaultValue)
    {
        throw new System.NotImplementedException();
    }

    public TimestampedObject<T> GetAtomic()
    {
        throw new System.NotImplementedException();
    }

    public TimestampedObject<T> GetAtomic(T defaultValue)
    {
        throw new System.NotImplementedException();
    }

    public bool GetInto(ref T output)
    {
        throw new System.NotImplementedException();
    }

    public TimestampedObject<T>[] ReadQueue()
    {
        throw new System.NotImplementedException();
    }

    public T[] ReadQueueValues()
    {
        throw new System.NotImplementedException();
    }

    public void Set(T value)
    {
        throw new System.NotImplementedException();
    }

    public void Set(long time, T value)
    {
        throw new System.NotImplementedException();
    }

    public void SetDefault(T value)
    {
        try
        {
            lock (m_lockObject)
            {
                if (!m_schemaPublished)
                {
                    m_schemaPublished = true;
                    Topic.Instance.AddSchema(m_buf.Proto);
                }
                ReadOnlySpan<byte> buf = m_buf.Write(value);
                NtCore.SetDefaultEntryValue(Handle, RefNetworkTableValue.MakeRaw(buf));
            }
        }
        catch
        {

        }
    }

    public void Unpublish()
    {
        NtCore.Unpublish(Handle);
    }

    private T FromRaw(ReadOnlySpan<byte> raw, T defaultValue)
    {
        if (raw.Length == 0)
        {
            return defaultValue;
        }

        try
        {
            lock (m_lockObject)
            {
                return m_buf.Read(raw);
            }
        }
        catch
        {
            return defaultValue;
        }
    }

    private TimestampedObject<T> FromRaw(TimestampedRaw raw, T defaultValue)
    {
        if (raw.Value.Length == 0)
        {
            return new TimestampedObject<T>(0, 0, defaultValue);
        }
        try
        {
            lock (m_lockObject)
            {
                return new TimestampedObject<T>(raw.Timestamp, raw.ServerTime, m_buf.Read(raw.Value));
            }
        }
        catch
        {
            return new TimestampedObject<T>(0, 0, defaultValue);
        }
    }

    private static readonly ReadOnlyMemory<byte> m_emptyRaw = Array.Empty<byte>();
}
