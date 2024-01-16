using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalPortHandle>))]
public record struct HalPortHandle(int Handle) : IWPIIntHandle;
