using NetworkTables.Handles;
using NetworkTables.Natives;

namespace NetworkTables;

public sealed class NetworkTableListenerPoller(NetworkTableInstance inst) : IDisposable
{
    public NtListenerPoller Handle { get; private set; } = NtCore.CreateListenerPoller(inst.Handle);

    public NetworkTableInstance Instance { get; } = inst;

    public NtListener AddListener(ReadOnlySpan<string> prefixes, EventFlags eventKinds)
    {
        return NtCore.AddListener(Handle, prefixes, eventKinds);
    }

    public NtListener AddListener(Topic topic, EventFlags eventKinds)
    {
        return NtCore.AddListener(Handle, topic.Handle, eventKinds);
    }

    public NtListener AddListener(ISubscriber subscriber, EventFlags eventKinds)
    {
        return NtCore.AddListener(Handle, subscriber.Handle, eventKinds);
    }

    public NtListener AddListener(MultiSubscriber subscriber, EventFlags eventKinds)
    {
        return NtCore.AddListener(Handle, subscriber.Handle, eventKinds);
    }

    public NtListener AddConnectionListener(bool immediateNotify)
    {
        EventFlags flags = EventFlags.Connection;
        if (immediateNotify)
        {
            flags |= EventFlags.Immediate;
        }
        return NtCore.AddListener(Handle, Instance.Handle, flags);
    }

    public NtListener AddTimeSyncListener(bool immediateNotify)
    {
        EventFlags flags = EventFlags.TimeSync;
        if (immediateNotify)
        {
            flags |= EventFlags.Immediate;
        }
        return NtCore.AddListener(Handle, Instance.Handle, flags);
    }

    public NtListener AddLogger(int minLevel, int maxLevel)
    {
        return NtCore.AddLogger(Handle, (uint)minLevel, (uint)maxLevel);
    }

    public static void RemoveListener(NtListener listener)
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
            if (Handle.Handle != 0)
            {
                NtCore.DestroyListenerPoller(Handle);
            }
            Handle = default;
        }
    }

    public bool IsValid => Handle.Handle != 0;
}
