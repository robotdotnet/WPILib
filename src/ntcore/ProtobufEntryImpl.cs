using System;
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
        return FromRaw(m_defaultValue).Value;
    }

    public T Get(T defaultValue)
    {
        return FromRaw(defaultValue).Value;
    }

    public bool GetInto(ref T output)
    {
        NetworkTableValue value = NtCore.GetEntryValue(Handle);
        if (!value.IsRaw)
        {
            return false;
        }
        byte[] raw = value.GetRaw();
        if (raw.Length == 0)
        {
            return false;
        }
        try
        {
            lock (m_lockObject)
            {
                m_buf.ReadInto(ref output, raw);
                return true;
            }
        }
        catch (Exception)
        {

        }
        return false;
    }

    public TimestampedObject<T> GetAtomic()
    {
        return FromRaw(m_defaultValue);
    }

    public TimestampedObject<T> GetAtomic(T defaultValue)
    {
        return FromRaw(defaultValue);
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

    private TimestampedObject<T> FromRaw(T defaultValue)
    {
        NetworkTableValue value = NtCore.GetEntryValue(Handle);
        if (!value.IsRaw)
        {
            return new TimestampedObject<T>(value.Time, value.ServerTime, defaultValue);
        }
        byte[] raw = value.GetRaw();
        if (raw.Length == 0)
        {
            return new TimestampedObject<T>(value.Time, value.ServerTime, defaultValue);
        }
        try
        {
            lock (m_lockObject)
            {
                return new TimestampedObject<T>(value.Time, value.ServerTime, m_buf.Read(raw));
            }
        }
        catch
        {
            return new TimestampedObject<T>(0, 0, defaultValue);
        }
    }

    private static readonly byte[] m_emptyRaw = Array.Empty<byte>();
}
