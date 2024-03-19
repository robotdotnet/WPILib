using NetworkTables.Handles;
using NetworkTables.Natives;

namespace NetworkTables;

public sealed class MultiSubscriber(NetworkTableInstance inst, string[] prefixes, PubSubOptions options = default) : IDisposable
{
    public void Dispose()
    {
        if (Handle.Handle != 0)
        {
            NtCore.UnsubscribeMultiple(Handle);
            Handle = default;
        }
    }

    public NtMultiSubscriber Handle { get; private set; } = NtCore.SubscribeMultiple(inst.Handle, prefixes, (nuint)prefixes.Length, options);

    public NetworkTableInstance Instance { get; } = inst;

    public bool IsValid => Handle.Handle != 0;
}
