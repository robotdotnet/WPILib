using System.Runtime.InteropServices.Marshalling;
using CsCore.Natives;
using WPIUtil.Marshal;

namespace CsCore.Handles;

[NativeMarshalling(typeof(CsHandleMarshaller<CsProperty>))]
public record struct CsProperty(int Handle) : ICsHandle, INativeArrayFree<int>
{
    public static unsafe void FreeArray(int* ptr, int len)
    {
        CsNative.FreeEnumeratedProperties(ptr, len);
    }
}
