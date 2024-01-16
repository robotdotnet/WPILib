using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalGyroHandle>))]
public record struct HalGyroHandle(int Handle) : IWPIIntHandle;
