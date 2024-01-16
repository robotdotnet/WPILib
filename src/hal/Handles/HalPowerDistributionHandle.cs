using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalPowerDistributionHandle>))]
public record struct HalPowerDistributionHandle(int Handle) : IWPIIntHandle;
