using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalEncoderHandle>))]
public record struct HalEncoderHandle(int Handle) : IWPIIntHandle;
