using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalCANHandle>))]
public record struct HalCANHandle(int Handle) : IWPIIntHandle;
