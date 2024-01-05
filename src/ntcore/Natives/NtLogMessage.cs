using System.Runtime.InteropServices;

namespace NetworkTables.Natives;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct NtLogMessage
{
    public uint level;
    public byte* filename;
    public uint line;
    public byte* message;
}
