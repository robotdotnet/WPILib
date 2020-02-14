using System;
using System.Collections.Generic;
using NetworkTables.Natives;
using WPIUtil;

namespace NetworkTables
{
    /// <summary>
    /// This class contains all info needed for a given connection.
    /// </summary>
    public readonly struct ConnectionInfo : IEquatable<ConnectionInfo>
    {
        /// <summary>Gets the Remote Id of the Connection.</summary>
        public string RemoteId { get; }
        /// <summary>Gets the Remote IP Address of the Connection.</summary>
        public string RemoteIp { get; }
        /// <summary>Gets the Remote Port of the Connection.</summary>
        public int RemotePort { get; }
        /// <summary>Gets the last update time of the Connection.</summary>
        public long LastUpdate { get; }
        /// <summary>Gets the Protocol Version of the Connection.</summary>
        public int ProtocolVersion { get; }

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

        public override bool Equals(object? obj)
        {
            return obj is ConnectionInfo info && Equals(info);
        }

        public bool Equals(ConnectionInfo other)
        {
            return RemoteId == other.RemoteId &&
                   RemoteIp == other.RemoteIp &&
                   RemotePort == other.RemotePort &&
                   LastUpdate == other.LastUpdate &&
                   ProtocolVersion == other.ProtocolVersion;
        }

        public override int GetHashCode()
        {
            var hashCode = 1891253395;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RemoteId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RemoteIp);
            hashCode = hashCode * -1521134295 + RemotePort.GetHashCode();
            hashCode = hashCode * -1521134295 + LastUpdate.GetHashCode();
            hashCode = hashCode * -1521134295 + ProtocolVersion.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(ConnectionInfo left, ConnectionInfo right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ConnectionInfo left, ConnectionInfo right)
        {
            return !(left == right);
        }
    }
}
