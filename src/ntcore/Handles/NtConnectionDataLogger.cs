using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(NtHandleMarshaller<NtConnectionDataLogger>))]
public record struct NtConnectionDataLogger(int Handle) : INtHandle
{
}
