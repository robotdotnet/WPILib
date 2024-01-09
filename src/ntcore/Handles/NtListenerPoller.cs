using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(NtHandleMarshaller<NtListenerPoller>))]
public record struct NtListenerPoller(int Handle) : INtHandle
{
}
