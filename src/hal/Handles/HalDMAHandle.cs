using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalDMAHandle>))]
public record struct HalDMAHandle(int Handle) : IWPIIntHandle;
