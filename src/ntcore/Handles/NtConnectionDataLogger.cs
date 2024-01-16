using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<NtConnectionDataLogger>))]
public record struct NtConnectionDataLogger(int Handle) : IWPIIntHandle
{
}
