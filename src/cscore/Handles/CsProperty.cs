using System.Runtime.InteropServices.Marshalling;
using CsCore.Natives;
using WPIUtil.Handles;
using WPIUtil.Marshal;

namespace CsCore.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<CsProperty>))]
public record struct CsProperty(int Handle) : IWPIIntHandle, INativeArrayFree<int>
{
    public static unsafe void FreeArray(int* array, int len)
    {
        CsNative.FreeEnumeratedProperties(array, len);
    }
}
