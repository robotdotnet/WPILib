using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(NtHandleMarshaller<NtListener>))]
public record struct NtListener(int Handle) : INtHandle
{
}
