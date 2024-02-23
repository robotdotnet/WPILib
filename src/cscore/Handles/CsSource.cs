using System.Runtime.InteropServices.Marshalling;
using CsCore.Natives;
using WPIUtil.Handles;
using WPIUtil.Marshal;

namespace CsCore.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<CsSource>))]
public record struct CsSource(int Handle) : IWPIIntHandle, INativeArrayFree<int>
{
    public static unsafe void FreeArray(int* array, int len)
    {
        CsNative.ReleaseEnumeratedSources(array, len);
    }
}
