using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalSolenoidHandle>))]
public record struct HalSolenoidHandle(int Handle) : IWPIIntHandle;
