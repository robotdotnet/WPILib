using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(NtHandleMarshaller<NtMultiSubscriber>))]
public record struct NtMultiSubscriber(int Handle) : INtHandle
{
}
