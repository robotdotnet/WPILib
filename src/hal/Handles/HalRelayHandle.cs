using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalRelayHandle>))]
public record struct HalRelayHandle(int Handle) : IWPIIntHandle;
