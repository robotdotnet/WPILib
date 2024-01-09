using System;
using NetworkTables.Handles;
using NetworkTables.Natives;

namespace NetworkTables;

public sealed class MultiSubscriber : IDisposable
{
    public MultiSubscriber(NetworkTableInstance inst, string[] prefixes, in PubSubOptions options)
    {
        Inst = inst;
        Handle = NtCore.SubscribeMultiple(inst.Handle, prefixes, (nuint)prefixes.Length, options);
    }

    public void Dispose()
    {
        lock (this)
        {
            if (Handle.Handle != 0)
            {
                NtCore.UnsubscribeMultiple(Handle);
                Handle = default;
            }
        }
    }

    public NtMultiSubscriber Handle { get; private set; }

    public NetworkTableInstance Inst { get; }

    public bool IsValid => Handle.Handle != 0;
}
