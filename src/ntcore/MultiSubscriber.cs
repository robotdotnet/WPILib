using System;
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
            if (Handle != 0)
            {
                NtCore.UnsubscribeMultiple(Handle);
            }
        }
    }

    public int Handle { get; }

    public NetworkTableInstance Inst { get; }

    public bool IsValid => Handle != 0;
}
