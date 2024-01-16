using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<NtListenerPoller>))]
public record struct NtListenerPoller(int Handle) : IWPIIntHandle
{
}
