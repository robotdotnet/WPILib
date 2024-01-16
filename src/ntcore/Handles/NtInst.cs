using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<NtInst>))]
public record struct NtInst(int Handle) : IWPIIntHandle
{
}
