using System.Runtime.InteropServices.Marshalling;
using NetworkTables.Natives;
using WPIUtil.Handles;
using WPIUtil.Marshal;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<NtTopic>))]
public record struct NtTopic(int Handle) : IWPIIntHandle, INativeArrayFree<int>
{
    public static unsafe void FreeArray(int* array, int len)
    {
        NtCore.FreeCharArray((byte*)array);
    }
}
