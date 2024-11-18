using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using NetworkTables.Natives;
using WPIUtil.Marshal;

namespace NetworkTables;

[NativeMarshalling(typeof(ConnectionInfoMarshaller))]
[StructLayout(LayoutKind.Auto)]
public record struct ConnectionInfo(string RemoteId, string RemoteIp, uint RemotePort, ulong LastUpdate, uint ProtocolVersion) : INativeArrayFree<ConnectionInfoMarshaller.NativeConnectionInfo>
{
    public static unsafe void FreeArray(ConnectionInfoMarshaller.NativeConnectionInfo* array, int len)
    {
        NtCore.DisposeConnectionInfoArray(array, (nuint)len);
    }
}

[CustomMarshaller(typeof(ConnectionInfo), MarshalMode.ElementOut, typeof(ReturnInArray))]
public static unsafe class ConnectionInfoMarshaller
{
    public static class ReturnInArray
    {
        public static ConnectionInfo ConvertToManaged(in NativeConnectionInfo unmanaged)
        {
            return new ConnectionInfo
            {
                RemoteId = unmanaged.remoteId.ConvertToString(),
                RemoteIp = unmanaged.remoteIp.ConvertToString(),
                RemotePort = unmanaged.remotePort,
                LastUpdate = unmanaged.lastUpdate,
                ProtocolVersion = unmanaged.protocolVersion,
            };
        }

        public static NativeConnectionInfo ConvertToUnmanaged(ConnectionInfo managed)
        {
            throw new NotSupportedException();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeConnectionInfo
    {
        public WpiStringMarshaller.WpiStringNative remoteId;
        public WpiStringMarshaller.WpiStringNative remoteIp;
        public uint remotePort;
        public ulong lastUpdate;
        public uint protocolVersion;
    }
}
