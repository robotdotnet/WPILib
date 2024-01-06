using System;

namespace NetworkTables;

public interface PubSub : IDisposable
{
    Topic Topic { get; }

    bool IsValid { get; }

    int Handle { get; }
}
