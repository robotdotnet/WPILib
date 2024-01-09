using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(NtHandleMarshaller<NtEntry>))]
public record struct NtEntry(int Handle) : INtHandle
{
}
