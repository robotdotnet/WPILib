using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Natives;

[NativeMarshalling(typeof(NtConnectionInfoMarshaller))]
[StructLayout(LayoutKind.Auto)]
public record struct ConnectionInfo(string RemoteId, string RemoteIp, uint RemotePort, ulong LastUpdate, uint ProtocolVersion);
