using System.Runtime.InteropServices.Marshalling;

namespace CsCore.Handles;

[NativeMarshalling(typeof(CsHandleMarshaller<CsListener>))]
public record struct CsListener(int Handle) : ICsHandle;
