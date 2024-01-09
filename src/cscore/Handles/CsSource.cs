using System.Runtime.InteropServices.Marshalling;

namespace CsCore.Handles;

[NativeMarshalling(typeof(CsHandleMarshaller<CsSource>))]
public record struct CsSource(int Handle) : ICsHandle;
