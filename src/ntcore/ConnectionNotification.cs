using System;
using System.Collections.Generic;
using NetworkTables.Natives;

namespace NetworkTables
{
    public readonly struct ConnectionNotification : IEquatable<ConnectionNotification>
    {
        public NtConnectionListener Listener { get; }
        public bool Connected { get; }
        public ConnectionInfo Conn { get; }
        public NetworkTableInstance Instance { get; }

        internal unsafe ConnectionNotification(NetworkTableInstance inst, in NtConnectionNotification notification)
        {
            this.Listener = notification.listener;
            this.Connected = notification.connected.Get();
            this.Conn = new ConnectionInfo(notification.conn);
            this.Instance = inst;
        }

        public override bool Equals(object? obj)
        {
            return obj is ConnectionNotification notification && Equals(notification);
        }

        public bool Equals(ConnectionNotification other)
        {
            return Listener.Equals(other.Listener) &&
                   Connected == other.Connected &&
                   Conn.Equals(other.Conn) &&
                   EqualityComparer<NetworkTableInstance>.Default.Equals(Instance, other.Instance);
        }

        public override int GetHashCode()
        {
            var hashCode = -384210942;
            hashCode = hashCode * -1521134295 + Listener.GetHashCode();
            hashCode = hashCode * -1521134295 + Connected.GetHashCode();
            hashCode = hashCode * -1521134295 + Conn.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<NetworkTableInstance>.Default.GetHashCode(Instance);
            return hashCode;
        }

        public static bool operator ==(ConnectionNotification left, ConnectionNotification right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ConnectionNotification left, ConnectionNotification right)
        {
            return !(left == right);
        }
    }
}
