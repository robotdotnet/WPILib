using NetworkTables.Handles;
using NetworkTables.Natives;
using WPIUtil.Serialization.Protobuf;

namespace NetworkTables;

public sealed class ProtobufTopic<T> : Topic where T : IProtobufSerializable<T>
{
    private IProtobufBase m_proto = T.Proto;

    private ProtobufTopic(Topic topic) : base(topic.Instance, topic.Handle)
    {
    }

    private ProtobufTopic(NetworkTableInstance inst, NtTopic handle) : base(inst, handle)
    {
    }

    public IProtobufSubscriber<T> Subscribe(T defaultValue, PubSubOptions options)
    {
        return new ProtobufEntryImpl<T, NtSubscriber>(this, NtCore.Subscribe(Handle, NetworkTableType.Raw, m_proto.TypeString, options), defaultValue, false);
    }
}
