using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalCTREPCMHandle>))]
public record struct HalCTREPCMHandle(int Handle) : IWPIIntHandle;
