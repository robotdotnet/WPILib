using System.Runtime.InteropServices;

namespace NetworkTables.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct NtTimeSyncEventData
{
    public long serverTimeOffset;
    public long rtt2;
    public int valid;
}
