using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalCounterHandle>))]
public record struct HalCounterHandle(int Handle) : IWPIIntHandle;
