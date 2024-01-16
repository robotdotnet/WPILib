using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<NtEntry>))]
public record struct NtEntry(int Handle) : INtEntryHandle
{
}
