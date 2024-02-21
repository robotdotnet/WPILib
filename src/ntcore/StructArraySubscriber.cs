using WPIUtil.Serialization.Struct;

namespace NetworkTables;

public interface IStructArraySubscriber<T> : ISubscriber where T : IStructSerializable<T>
{
    new StructArrayTopic<T> Topic { get; }

    T[] Get();

    T[] Get(T[] defaultValue);

    ReadOnlySpan<T> GetInto(Span<T> output, out bool copiedAll);

    TimestampedObject<T[]> GetAtomic();

    TimestampedObject<T[]> GetAtomic(T[] defaultValue);

    TimestampedObject<T[]>[] ReadQueue();

    T[][] ReadQueueValues();
}
