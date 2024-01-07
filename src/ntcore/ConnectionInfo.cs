using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Marshal;

namespace NetworkTables.Natives;

[NativeMarshalling(typeof(ConnectionInfoMarshaller))]
[StructLayout(LayoutKind.Auto)]
public record struct ConnectionInfo(string RemoteId, string RemoteIp, uint RemotePort, ulong LastUpdate, uint ProtocolVersion);

[CustomMarshaller(typeof(ConnectionInfo), MarshalMode.ManagedToUnmanagedOut, typeof(ConnectionInfoMarshaller))]
public static unsafe class ConnectionInfoMarshaller
{
    public static ConnectionInfo ConvertToManaged(in NativeConnectionInfo unmanaged)
    {
        return new ConnectionInfo
        {
            RemoteId = NtStringMarshaller.ManagedConvert(unmanaged.remoteId),
            RemoteIp = NtStringMarshaller.ManagedConvert(unmanaged.remoteIp),
            RemotePort = unmanaged.remotePort,
            LastUpdate = unmanaged.lastUpdate,
            ProtocolVersion = unmanaged.protocolVersion,
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeConnectionInfo : INativeArrayFree<NativeConnectionInfo>
    {
        public NtString remoteId;
        public NtString remoteIp;
        public uint remotePort;
        public ulong lastUpdate;
        public uint protocolVersion;

        public static unsafe void FreeArray(NativeConnectionInfo* ptr, int len)
        {
            NtCore.DisposeConnectionInfoArray(ptr, (nuint)len);
        }
    }
}
