using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(NtHandleMarshaller<NtPublisher>))]
public record struct NtPublisher(int Handle) : INtHandle
{
}
