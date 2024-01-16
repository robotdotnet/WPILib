using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<NtListener>))]
public record struct NtListener(int Handle) : IWPIIntHandle
{
}
