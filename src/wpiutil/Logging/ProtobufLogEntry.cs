using Google.Protobuf;
using WPIUtil.Serialization.Protobuf;

namespace WPIUtil.Logging;

public sealed class ProtobufLogEntry<T, MessageType> : DataLogEntry where MessageType : IMessage
                                                   where T : IProtobufSerializable<T, MessageType>
{
    private readonly ProtobufBuffer<T, MessageType> m_storage = new();
    private readonly object m_lockObject = new();

    private ProtobufLogEntry(DataLog log, string name, IProtobufBase proto, string metadata = "", long timestamp = 0) : base(log, name, proto.TypeString, metadata, timestamp)
    {
        log.AddSchema(proto, timestamp);
    }

    public ProtobufLogEntry<T, MessageType> Create(DataLog log, string name, string metadata = "", long timestamp = 0)
    {
        return new ProtobufLogEntry<T, MessageType>(log, name, T.Proto, metadata, timestamp);
    }

    public void Append(T value, long timestamp = 0)
    {
        try
        {
            lock (m_lockObject)
            {
                m_log.AppendRaw(m_entry, m_storage.Write(value), timestamp);
            }
        }
        catch
        {
            // ignore
        }
    }
}
