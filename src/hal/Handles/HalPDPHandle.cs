using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalPDPHandle>))]
public record struct HalPDPHandle(int Handle) : IWPIIntHandle;
