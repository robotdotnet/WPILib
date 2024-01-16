using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalAddressableLEDHandle>))]
public record struct HalAddressableLEDHandle(int Handle) : IWPIIntHandle;
