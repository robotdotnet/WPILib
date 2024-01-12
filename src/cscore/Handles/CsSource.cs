using System.Runtime.InteropServices.Marshalling;
using CsCore.Natives;
using WPIUtil.Marshal;

namespace CsCore.Handles;

[NativeMarshalling(typeof(CsHandleMarshaller<CsSource>))]
public record struct CsSource(int Handle) : ICsHandle, INativeArrayFree<int>
{
    public static unsafe void FreeArray(int* ptr, int len)
    {
        CsNative.ReleaseEnumeratedSources(ptr, len);
    }
}
