using System.Runtime.InteropServices;
using WPIUtil.Marshal;

namespace NetworkTables.Natives;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct NtString : INativeFree<NtString>, IStringLengthPair
{
    public byte* str;
    public nuint len;

    public byte* Ptr { readonly get => str; set => str = value; }
    public nuint Len { readonly get => len; set => len = value; }

    public static void Free(NtString* ptr)
    {
        NtCore.DisposeString(ptr);
    }
}
