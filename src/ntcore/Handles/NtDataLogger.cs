using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<NtDataLogger>))]
public record struct NtDataLogger(int Handle) : IWPIIntHandle
{
}
