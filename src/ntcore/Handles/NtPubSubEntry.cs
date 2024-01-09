using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(NtHandleMarshaller<NtPubSubEntry>))]
public record struct NtPubSubEntry(int Handle) : INtHandle
{
    public static implicit operator NtPubSubEntry(NtEntry logger)
    {
        return new NtPubSubEntry(logger.Handle);
    }

    public static implicit operator NtPubSubEntry(NtSubscriber logger)
    {
        return new NtPubSubEntry(logger.Handle);
    }

    public static implicit operator NtPubSubEntry(NtPublisher logger)
    {
        return new NtPubSubEntry(logger.Handle);
    }
}
