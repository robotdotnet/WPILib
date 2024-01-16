using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalDigitalHandle>))]
public record struct HalDigitalHandle(int Handle) : IWPIIntHandle;
