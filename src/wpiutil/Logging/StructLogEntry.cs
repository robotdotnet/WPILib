using System;
using WPIUtil.Serialization.Struct;

namespace WPIUtil.Logging;

public sealed class StructLogEntry<T> : DataLogEntry where T : IStructSerializable<T>
{
    private readonly StructBuffer<T> m_storage = new();
    private readonly object m_lockObject = new();

    private StructLogEntry(DataLog log, string name, IStruct<T> value, string metadata, long timestamp) : base(log, name, value.TypeString, metadata, timestamp)
    {
        log.AddSchema(value, timestamp);
    }

    public StructLogEntry<T> Create(DataLog log, string name, string metadata = "", long timestamp = 0)
    {
        return new StructLogEntry<T>(log, name, T.Struct, metadata, timestamp);
    }

    public void Append(T value, long timestamp = 0)
    {
        lock (m_lockObject)
        {
            m_log.AppendRaw(m_entry, m_storage.Write(value), timestamp);
        }
    }
}
