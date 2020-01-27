using NetworkTables.Natives;

namespace NetworkTables
{
    public readonly struct ConnectionNotification
    {
        public readonly NtConnectionListener Listener;
        public readonly bool Connected;
        public readonly ConnectionInfo Conn;
        public readonly NetworkTableInstance Instance;

        internal unsafe ConnectionNotification(NetworkTableInstance inst, in NtConnectionNotification notification)
        {
            this.Listener = notification.listener;
            this.Connected = notification.connected.Get();
            this.Conn = new ConnectionInfo(notification.conn);
            this.Instance = inst;
        }
    }
}
