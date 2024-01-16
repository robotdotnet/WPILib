using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalFPGAEncoderHandle>))]
public record struct HalFPGAEncoderHandle(int Handle) : IWPIIntHandle;
