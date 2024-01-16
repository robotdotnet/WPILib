using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalCompressorHandle>))]
public record struct HalCompressorHandle(int Handle) : IWPIIntHandle;
