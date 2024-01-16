using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalDutyCycleHandle>))]
public record struct HalDutyCycleHandle(int Handle) : IWPIIntHandle;
