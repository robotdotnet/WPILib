using System.Runtime.InteropServices;

namespace NetworkTables.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct NtValueEventData
{
    public int topic;
    public int subentry;
    public NtValue value;
}
