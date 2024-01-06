using System;
using NetworkTables.Natives;

namespace NetworkTables;

public abstract class EntryBase(int handle) : Subscriber, Publisher
{
    private readonly int m_handle = handle;

    public bool Exists => NtCore.GetTopicExists(m_handle);

    public long LastChange => (long)NtCore.GetEntryLastChange(m_handle);

    public bool IsValid => m_handle != 0;

    public int Handle => m_handle;

    public abstract Topic Topic { get; }

    public void Dispose()
    {
        NtCore.Release(m_handle);
        GC.SuppressFinalize(this);
    }
}
