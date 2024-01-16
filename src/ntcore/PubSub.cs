using System;
using NetworkTables.Handles;

namespace NetworkTables;

public interface PubSub<T> : IDisposable where T : struct, INtEntryHandle
{
    Topic Topic { get; }

    bool IsValid { get; }

    T Handle { get; }
}
