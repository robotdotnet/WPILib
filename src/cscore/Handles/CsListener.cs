using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace CsCore.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<CsListener>))]
public record struct CsListener(int Handle) : IWPIIntHandle;
