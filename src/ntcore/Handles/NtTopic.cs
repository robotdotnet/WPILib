using System.Runtime.InteropServices.Marshalling;
using NetworkTables.Natives;
using WPIUtil.Marshal;

namespace NetworkTables.Handles;

[NativeMarshalling(typeof(NtHandleMarshaller<NtTopic>))]
public record struct NtTopic(int Handle) : INtHandle, INativeArrayFree<int>
{
    public static unsafe void FreeArray(int* ptr, int len)
    {
        NtCore.FreeCharArray((byte*)ptr);
    }
}
