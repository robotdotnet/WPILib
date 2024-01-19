using WPIUtil.Serialization.Struct;

namespace NetworkTables;

public interface IStructArrayEntry<T> : IStructArraySubscriber<T>, IStructArrayPublisher<T> where T : IStructSerializable<T>
{
    void Unpublish();
}
