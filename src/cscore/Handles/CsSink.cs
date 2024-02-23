using System.Runtime.InteropServices.Marshalling;
using CsCore.Natives;
using WPIUtil.Handles;
using WPIUtil.Marshal;

namespace CsCore.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<CsSink>))]
public record struct CsSink(int Handle) : IWPIIntHandle, INativeArrayFree<int>
{
    public static unsafe void FreeArray(int* array, int len)
    {
        CsNative.ReleaseEnumeratedSinks(array, len);
    }
}
