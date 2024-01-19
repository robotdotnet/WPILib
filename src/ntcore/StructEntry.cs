using WPIUtil.Serialization.Struct;

namespace NetworkTables;

public interface IStructEntry<T> : IStructSubscriber<T>, IStructPublisher<T> where T : IStructSerializable<T>
{
    void Unpublish();
}
