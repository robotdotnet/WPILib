using System.Runtime.InteropServices.Marshalling;

namespace CsCore.Handles;

[NativeMarshalling(typeof(CsHandleMarshaller<CsListenerPoller>))]
public record struct CsListenerPoller(int Handle) : ICsHandle;
