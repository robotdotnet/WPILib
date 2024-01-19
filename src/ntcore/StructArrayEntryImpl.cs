using System;
using NetworkTables.Handles;
using NetworkTables.Natives;
using WPIUtil.Serialization.Struct;

namespace NetworkTables;

internal sealed class StructArrayEntryImpl<T, THandle> : EntryBase<THandle>, IStructArrayEntry<T> where THandle : struct, INtEntryHandle where T : IStructSerializable<T>
{
    internal StructArrayEntryImpl(StructArrayTopic<T> topic, THandle handle, T[] defaultValue, bool schemaPublished) : base(handle)
    {
        Topic = topic;
        m_defaultValue = defaultValue;
        m_buf = new StructBuffer<T>();
        m_schemaPublished = schemaPublished;
    }

    private readonly T[] m_defaultValue;
    private readonly StructBuffer<T> m_buf;
    private bool m_schemaPublished;
    private readonly object m_lockObject = new();

    public override StructArrayTopic<T> Topic { get; }

    public T[] Get()
    {
        return FromRaw(m_defaultValue).Value;
    }

    public T[] Get(T[] defaultValue)
    {
        return FromRaw(defaultValue).Value;
    }

    public ReadOnlySpan<T> GetInto(Span<T> output, out bool copiedAll)
    {
        NetworkTableValue value = NtCore.GetEntryValue(Handle);
        if (!value.IsRaw)
        {
            copiedAll = false;
            return new();
        }
        byte[] raw = value.GetRaw();
        if (raw.Length == 0)
        {
            copiedAll = false;
            return new();
        }
        try
        {
            lock (m_lockObject)
            {
                return m_buf.ReadInto(output, raw, out copiedAll);
            }
        }
        catch (Exception)
        {

        }
        copiedAll = false;
        return new();
    }

    public TimestampedObject<T[]> GetAtomic()
    {
        return FromRaw(m_defaultValue);
    }

    public TimestampedObject<T[]> GetAtomic(T[] defaultValue)
    {
        return FromRaw(defaultValue);
    }

    public TimestampedObject<T[]>[] ReadQueue()
    {

        throw new System.NotImplementedException();
    }

    public T[][] ReadQueueValues()
    {
        throw new System.NotImplementedException();
    }

    public void Set(ReadOnlySpan<T> value)
    {
        Set(value, 0);
    }

    public void Set(ReadOnlySpan<T> value, long time)
    {
        try
        {
            lock (m_lockObject)
            {
                if (!m_schemaPublished)
                {
                    m_schemaPublished = true;
                    Topic.Instance.AddSchema(m_buf.Struct);
                }
                NtCore.SetEntryValue(Handle, RefNetworkTableValue.MakeRaw(m_buf.WriteArray(value), time));
            }
        }
        catch
        {

        }
    }

    public void SetDefault(ReadOnlySpan<T> value)
    {
        try
        {
            lock (m_lockObject)
            {
                if (!m_schemaPublished)
                {
                    m_schemaPublished = true;
                    Topic.Instance.AddSchema(m_buf.Struct);
                }
                NtCore.SetDefaultEntryValue(Handle, RefNetworkTableValue.MakeRaw(m_buf.WriteArray(value)));
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

    private TimestampedObject<T[]> FromRaw(T[] defaultValue)
    {
        NetworkTableValue value = NtCore.GetEntryValue(Handle);
        if (!value.IsRaw)
        {
            return new TimestampedObject<T[]>(value.Time, value.ServerTime, defaultValue);
        }
        byte[] raw = value.GetRaw();
        if (raw.Length == 0)
        {
            return new TimestampedObject<T[]>(value.Time, value.ServerTime, defaultValue);
        }
        try
        {
            lock (m_lockObject)
            {
                return new TimestampedObject<T[]>(value.Time, value.ServerTime, m_buf.ReadArray(raw));
            }
        }
        catch
        {
            return new TimestampedObject<T[]>(0, 0, defaultValue);
        }
    }
}
