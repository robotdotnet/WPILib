using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalInterruptHandle>))]
public record struct HalInterruptHandle(int Handle) : IWPIIntHandle;
