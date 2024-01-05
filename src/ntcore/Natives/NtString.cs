using System.Runtime.InteropServices;

namespace NetworkTables.Natives;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct NtString
{
    public byte* str;
    public nuint len;
}
