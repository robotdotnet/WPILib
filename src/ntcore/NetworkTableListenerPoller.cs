using System;
using NetworkTables.Natives;

namespace NetworkTables;

public sealed class NetworkTableListenerPoller(NetworkTableInstance inst) : IDisposable
{
    public int Handle { get; private set; } = NtCore.CreateListenerPoller(inst.Handle);

    public NetworkTableInstance Instance { get; } = inst;

    public int AddListener(ReadOnlySpan<string> prefixes, EventFlags eventKinds)
    {
        return NtCore.AddListener(Handle, prefixes, eventKinds);
    }

    public int AddListener(Topic topic, EventFlags eventKinds)
    {
        return NtCore.AddListener(Handle, topic.Handle, eventKinds);
    }

    public int AddListener(Subscriber subscriber, EventFlags eventKinds)
    {
        return NtCore.AddListener(Handle, subscriber.Handle, eventKinds);
    }

    public int AddListener(MultiSubscriber subscriber, EventFlags eventKinds)
    {
        return NtCore.AddListener(Handle, subscriber.Handle, eventKinds);
    }

    public int AddListener(NetworkTableEntry entry, EventFlags eventKinds)
    {
        return NtCore.AddListener(Handle, entry.Handle, eventKinds);
    }

    public int AddConnectionListener(bool immediateNotify)
    {
        EventFlags flags = EventFlags.Connection;
        if (immediateNotify)
        {
            flags |= EventFlags.Immediate;
        }
        return NtCore.AddListener(Handle, Instance.Handle, flags);
    }

    public int AddTimeSyncListener(bool immediateNotify)
    {
        EventFlags flags = EventFlags.TimeSync;
        if (immediateNotify)
        {
            flags |= EventFlags.Immediate;
        }
        return NtCore.AddListener(Handle, Instance.Handle, flags);
    }

    public int AddLogger(int minLevel, int maxLevel)
    {
        return NtCore.AddLogger(Handle, (uint)minLevel, (uint)maxLevel);
    }

    public static void RemoveListener(int listener)
    {
        NtCore.RemoveListener(listener);
    }

    public NetworkTableEvent[] ReadQueue()
    {
        return NtCore.ReadListenerQueue(Handle);
    }

    public void Dispose()
    {
        lock (this)
        {
            if (Handle != 0)
            {
                NtCore.DestroyListenerPoller(Handle);
            }
            Handle = 0;
        }
    }

    public bool IsValid => Handle != 0;
}
