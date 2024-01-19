using WPIUtil.Serialization.Struct;

namespace NetworkTables;

public interface IStructPublisher<T> : IPublisher where T : IStructSerializable<T>
{
    new StructTopic<T> Topic { get; }

    void Set(T value);

    void Set(T value, long time);

    void SetDefault(T value);
}
