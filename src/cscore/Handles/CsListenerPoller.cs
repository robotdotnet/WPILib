using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace CsCore.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<CsListenerPoller>))]
public record struct CsListenerPoller(int Handle) : IWPIIntHandle;
