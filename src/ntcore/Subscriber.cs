using NetworkTables.Handles;

namespace NetworkTables;

public interface Subscriber : PubSub<NtSubscriber>
{
    bool Exists { get; }
    long LastChange { get; }
}
