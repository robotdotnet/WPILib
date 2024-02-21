using NetworkTables.Handles;

namespace NetworkTables;

public interface IPubSub<T> : IDisposable where T : struct, INtEntryHandle
{
    Topic Topic { get; }

    bool IsValid { get; }

    T Handle { get; }
}
