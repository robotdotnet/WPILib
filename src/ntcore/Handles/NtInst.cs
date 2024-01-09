using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(NtHandleMarshaller<NtInst>))]
public record struct NtInst(int Handle) : INtHandle
{
}
