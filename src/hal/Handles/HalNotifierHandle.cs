using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalNotifierHandle>))]
public record struct HalNotifierHandle(int Handle) : IWPIIntHandle;
