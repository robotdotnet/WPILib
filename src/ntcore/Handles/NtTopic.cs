using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(NtHandleMarshaller<NtTopic>))]
public record struct NtTopic(int Handle) : INtHandle
{
}
