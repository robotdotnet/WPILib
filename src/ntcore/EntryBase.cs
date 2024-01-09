using System;
using NetworkTables.Handles;
using NetworkTables.Natives;

namespace NetworkTables;

public abstract class EntryBase(NtPubSubEntry handle) : Subscriber, Publisher
{
    private readonly NtPubSubEntry m_handle = handle;

    public bool Exists => NtCore.GetTopicExists(m_handle);

    public long LastChange => (long)NtCore.GetEntryLastChange(m_handle);

    public bool IsValid => m_handle.Handle != 0;

    public NtPubSubEntry Handle => m_handle;

    public abstract Topic Topic { get; }

    public void Dispose()
    {
        NtCore.Release(m_handle);
        GC.SuppressFinalize(this);
    }
}
