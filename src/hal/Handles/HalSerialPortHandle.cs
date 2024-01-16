using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalSerialPortHandle>))]
public record struct HalSerialPortHandle(int Handle) : IWPIIntHandle;
