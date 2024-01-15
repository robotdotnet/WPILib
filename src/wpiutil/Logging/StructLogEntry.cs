using System;
using WPIUtil.Serialization;

namespace WPIUtil.Logging;

public sealed class StructLogEntry<T> : DataLogEntry
{
    private readonly Struct<T> m_struct;
    private readonly byte[] m_storage;

    private StructLogEntry(DataLog log, string name, Struct<T> value, string metadata, long timestamp) : base(log, name, value.TypeString, metadata, timestamp)
    {
        m_struct = value;
        m_storage = new byte[value.Size];
        log.AddSchema(value, timestamp);
    }

    public StructLogEntry<T> Create(DataLog log, string name, Struct<T> value, string metadata = "", long timestamp = 0)
    {
        return new StructLogEntry<T>(log, name, value, metadata, timestamp);
    }

    public void Append(T value, long timestamp = 0)
    {
        ReadOnlySpan<byte> toWrite;
        lock (m_storage)
        {
            toWrite = m_struct.Pack(m_storage, value);

        }
        m_log.AppendRaw(m_entry, toWrite, timestamp);
    }
}
