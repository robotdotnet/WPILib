using WPIUtil.Serialization.Protobuf;

namespace NetworkTables;

public interface IProtobufEntry<T> : IProtobufSubscriber<T>, IProtobufPublisher<T> where T : IProtobufSerializable<T>
{
    void Unpublish();
}
