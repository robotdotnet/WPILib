using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalAnalogOutputHandle>))]
public record struct HalAnalogOutputHandle(int Handle) : IWPIIntHandle;
