using NetworkTables.Handles;
using WPIUtil.Handles;

namespace NetworkTables;

public interface Subscriber : PubSub<NtSubscriber>
{
    bool Exists { get; }
    long LastChange { get; }
}
