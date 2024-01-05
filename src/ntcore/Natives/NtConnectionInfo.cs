using System.Runtime.InteropServices;

namespace NetworkTables.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct NtConnectionInfo
{
    public NtString remoteId;
    public NtString remoteIp;
    public uint remotePort;
    public ulong lastUpdate;
    public uint protocolVersion;
}
