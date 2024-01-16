using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<NtSubscriber>))]
public record struct NtSubscriber(int Handle) : INtEntryHandle
{
}
