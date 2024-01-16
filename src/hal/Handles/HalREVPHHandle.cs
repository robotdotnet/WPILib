using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalREVPHHandle>))]
public record struct HalREVPHHandle(int Handle) : IWPIIntHandle;
