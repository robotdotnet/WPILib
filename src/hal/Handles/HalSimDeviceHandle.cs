using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalSimDeviceHandle>))]
public record struct HalSimDeviceHandle(int Handle) : IWPIIntHandle;
