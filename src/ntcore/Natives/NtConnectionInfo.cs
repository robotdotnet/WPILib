using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(ConnectionInfo), MarshalMode.UnmanagedToManagedIn, typeof(NtConnectionInfoMarshaller))]
public static unsafe class NtConnectionInfoMarshaller
{
    public static ConnectionInfo ConvertToManaged(NtConnectionInfo unmanaged)
    {
        return new ConnectionInfo
        {
            RemoteId = NtStringMarshaller.ConvertToManaged(unmanaged.remoteId),
            RemoteIp = NtStringMarshaller.ConvertToManaged(unmanaged.remoteIp),
            RemotePort = unmanaged.remotePort,
            LastUpdate = unmanaged.lastUpdate,
            ProtocolVersion = unmanaged.protocolVersion,
        };
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct NtConnectionInfo
{
    public NtString remoteId;
    public NtString remoteIp;
    public uint remotePort;
    public ulong lastUpdate;
    public uint protocolVersion;
}
