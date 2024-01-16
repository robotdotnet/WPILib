using NetworkTables.Handles;

namespace NetworkTables;

public interface ISubscriber : IPubSub<NtSubscriber>
{
    bool Exists { get; }
    long LastChange { get; }
}
