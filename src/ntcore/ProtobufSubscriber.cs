using Google.Protobuf;
using WPIUtil.Serialization.Protobuf;

namespace NetworkTables;

public interface IProtobufSubscriber<T> : Subscriber where T : IProtobufSerializable<T>
{
    new ProtobufTopic<T> Topic { get; }

    T Get();

    T Get(T defaultValue);

    bool GetInto(ref T output);

    TimestampedObject<T> GetAtomic();

    TimestampedObject<T> GetAtomic(T defaultValue);

    TimestampedObject<T>[] ReadQueue();

    T[] ReadQueueValues();
}
