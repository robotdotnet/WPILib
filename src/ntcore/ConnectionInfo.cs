using NetworkTables.Natives;
using WPIUtil;

namespace NetworkTables
{
    /// <summary>
    /// This class contains all info needed for a given connection.
    /// </summary>
    public readonly struct ConnectionInfo
    {
        /// <summary>Gets the Remote Id of the Connection.</summary>
        public readonly string RemoteId;
        /// <summary>Gets the Remote IP Address of the Connection.</summary>
        public readonly string RemoteIp;
        /// <summary>Gets the Remote Port of the Connection.</summary>
        public readonly int RemotePort;
        /// <summary>Gets the last update time of the Connection.</summary>
        public readonly long LastUpdate;
        /// <summary>Gets the Protocol Version of the Connection.</summary>
        public readonly int ProtocolVersion;

        internal unsafe ConnectionInfo(NtConnectionInfo* info)
        {
            RemoteId = UTF8String.ReadUTF8String(info->remote_id.str, info->remote_id.len);
            RemoteIp = UTF8String.ReadUTF8String(info->remote_ip.str, info->remote_ip.len);
            RemotePort = (int)info->remote_port;
            LastUpdate = (long)info->last_update;
            ProtocolVersion = (int)info->protocol_version;
        }

        internal unsafe ConnectionInfo(in NtConnectionInfo info)
        {
            RemoteId = UTF8String.ReadUTF8String(info.remote_id.str, info.remote_id.len);
            RemoteIp = UTF8String.ReadUTF8String(info.remote_ip.str, info.remote_ip.len);
            RemotePort = (int)info.remote_port;
            LastUpdate = (long)info.last_update;
            ProtocolVersion = (int)info.protocol_version;
        }
    }
}
