using CsCore.Natives;
using WPIUtil.Marshal;

namespace CsCore;

public class CsStringFree : INullTerminatedStringFree<byte>
{
    public static unsafe void FreeString(byte* ptr)
    {
        CsNatives.FreeString(ptr);
    }
}
