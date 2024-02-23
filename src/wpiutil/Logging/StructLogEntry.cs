using WPIUtil.Serialization.Struct;

namespace WPIUtil.Logging;

public sealed class StructLogEntry<T> : DataLogEntry where T : IStructSerializable<T>
{
    private readonly StructBuffer<T> m_storage = new();
    private readonly object m_lockObject = new();

    public StructLogEntry(DataLog log, string name, string metadata = "", long timestamp = 0) : base(log, name, T.Struct.TypeString, metadata, timestamp)
    {
        log.AddSchema(T.Struct, timestamp);
    }

    public void Append(T value, long timestamp = 0)
    {
        lock (m_lockObject)
        {
            Log.AppendRaw(Entry, m_storage.Write(value), timestamp);
        }
    }
}
