using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalAnalogInputHandle>))]
public record struct HalAnalogInputHandle(int Handle) : IWPIIntHandle;
