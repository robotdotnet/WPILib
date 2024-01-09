using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(NtHandleMarshaller<NtSubscriber>))]
public record struct NtSubscriber(int Handle) : INtHandle
{
}
