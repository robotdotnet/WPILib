using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalAnalogTriggerHandle>))]
public record struct HalAnalogTriggerHandle(int Handle) : IWPIIntHandle;
