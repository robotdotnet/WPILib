using WPIUtil.Serialization.Protobuf;

namespace NetworkTables;

public interface IProtobufPublisher<T> : IPublisher where T : IProtobufSerializable<T>
{
    new ProtobufTopic<T> Topic { get; }

    void Set(T value);

    void Set(long time, T value);

    void SetDefault(T value);
}
