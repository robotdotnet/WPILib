using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalSimValueHandle>))]
public record struct HalSimValueHandle(int Handle) : IWPIIntHandle;
