using System;
using NetworkTables.Handles;

namespace NetworkTables;

public interface PubSub : IDisposable
{
    Topic Topic { get; }

    bool IsValid { get; }

    NtPubSubEntry Handle { get; }
}
