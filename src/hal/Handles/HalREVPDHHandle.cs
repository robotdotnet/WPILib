using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalREVPDHHandle>))]
public record struct HalREVPDHHandle(int Handle) : IWPIIntHandle;
