using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<NtPublisher>))]
public record struct NtPublisher(int Handle) : INtEntryHandle
{
}
