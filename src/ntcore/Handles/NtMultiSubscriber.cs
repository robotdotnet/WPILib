using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<NtMultiSubscriber>))]
public record struct NtMultiSubscriber(int Handle) : IWPIIntHandle
{
}
