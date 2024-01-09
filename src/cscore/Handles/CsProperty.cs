using System.Runtime.InteropServices.Marshalling;

namespace CsCore.Handles;

[NativeMarshalling(typeof(CsHandleMarshaller<CsProperty>))]
public record struct CsProperty(int Handle) : ICsHandle;
