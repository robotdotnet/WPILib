using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(NtHandleMarshaller<NtDataLogger>))]
public record struct NtDataLogger(int Handle) : INtHandle
{
}
