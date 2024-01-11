using System;
using System.Runtime.InteropServices.Marshalling;
using CsCore.Natives;
using WPIUtil.Marshal;

namespace CsCore.Handles;

[NativeMarshalling(typeof(CsHandleMarshaller<CsSink>))]
public record struct CsSink(int Handle) : ICsHandle, INativeArrayFree<int>
{
    public static unsafe void FreeArray(int* ptr, int len)
    {
        CsNatives.ReleaseEnumeratedSinks(ptr, len);
    }
}
