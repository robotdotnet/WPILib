using System.Runtime.InteropServices.Marshalling;

namespace CsCore.Handles;

[NativeMarshalling(typeof(CsHandleMarshaller<CsSink>))]
public record struct CsSink(int Handle) : ICsHandle;
