using WPIUtil.Serialization.Struct;

namespace NetworkTables;

public interface IStructArrayPublisher<T> : IPublisher where T : IStructSerializable<T>
{
    new StructArrayTopic<T> Topic { get; }

    void Set(ReadOnlySpan<T> value);

    void Set(ReadOnlySpan<T> value, long time);

    void SetDefault(ReadOnlySpan<T> value);
}
