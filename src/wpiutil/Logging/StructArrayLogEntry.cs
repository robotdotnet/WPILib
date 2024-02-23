using WPIUtil.Serialization.Struct;

namespace WPIUtil.Logging;

public sealed class StructArrayLogEntry<T> : DataLogEntry where T : IStructSerializable<T>
{
    private StructBuffer<T> m_storage;
    private readonly object m_lockObject = new();

    public StructArrayLogEntry(DataLog log, string name, string metadata = "", long timestamp = 0) : base(log, name, $"{T.Struct.TypeString}[]", metadata, timestamp)
    {
        m_storage = new();
        log.AddSchema(T.Struct, timestamp);
    }

    public void Reserve(int nelem)
    {
        lock (m_lockObject)
        {
            m_storage.Reserve(nelem);
        }
    }

    public void Append(ReadOnlySpan<T> value, long timestamp = 0)
    {
        lock (m_lockObject)
        {
            Log.AppendRaw(Entry, m_storage.WriteArray(value), timestamp);
        }
    }
}
