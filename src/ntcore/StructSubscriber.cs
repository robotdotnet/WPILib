using WPIUtil.Serialization.Struct;

namespace NetworkTables;

public interface IStructSubscriber<T> : ISubscriber where T : IStructSerializable<T>
{
    new StructTopic<T> Topic { get; }

    T Get();

    T Get(T defaultValue);

    bool GetInto(ref T output);

    TimestampedObject<T> GetAtomic();

    TimestampedObject<T> GetAtomic(T defaultValue);

    TimestampedObject<T>[] ReadQueue();

    T[] ReadQueueValues();
}
