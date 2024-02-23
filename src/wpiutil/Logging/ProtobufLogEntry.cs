using WPIUtil.Serialization.Protobuf;

namespace WPIUtil.Logging;

public sealed class ProtobufLogEntry<T> : DataLogEntry where T : IProtobufSerializable<T>
{
    private readonly ProtobufBuffer<T> m_storage = new();
    private readonly object m_lockObject = new();

    public ProtobufLogEntry(DataLog log, string name, string metadata = "", long timestamp = 0) : base(log, name, T.Proto.TypeString, metadata, timestamp)
    {
        log.AddSchema(T.Proto, timestamp);
    }

    public void Append(T value, long timestamp = 0)
    {
        try
        {
            lock (m_lockObject)
            {
                Log.AppendRaw(Entry, m_storage.Write(value), timestamp);
            }
        }
        catch
        {
            // ignore
        }
    }
}
