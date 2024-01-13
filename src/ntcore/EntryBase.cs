﻿using System;
using NetworkTables.Handles;
using NetworkTables.Natives;
using WPIUtil.Handles;

namespace NetworkTables;

public abstract class EntryBase<T>(T handle) : Subscriber, Publisher where T : struct, INtEntryHandle
{
    private readonly T m_handle = handle;

    public bool Exists => NtCore.GetTopicExists(m_handle);

    public long LastChange => (long)NtCore.GetEntryLastChange(m_handle);

    public bool IsValid => m_handle.Handle != 0;

    public T Handle => m_handle;

    public abstract Topic Topic { get; }

    NtSubscriber PubSub<NtSubscriber>.Handle => new(m_handle.Handle);

    NtPublisher PubSub<NtPublisher>.Handle => new(m_handle.Handle);

    public void Dispose()
    {
        NtCore.Release(m_handle);
        GC.SuppressFinalize(this);
    }
}