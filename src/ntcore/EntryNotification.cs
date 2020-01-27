using NetworkTables.Natives;

namespace NetworkTables
{
    public readonly struct EntryNotification
    {
        public readonly NtEntryListener Listener;
        public readonly NtEntry EntryHandle;
        public readonly string Name;
        public readonly NetworkTableValue Value;
        public readonly NotifyFlags Flags;
        public NetworkTableEntry Entry => new NetworkTableEntry(m_instance, EntryHandle);
        private readonly NetworkTableInstance m_instance;

        internal unsafe EntryNotification(NetworkTableInstance inst, in RefEntryNotification entry)
        {
            Listener = entry.Listener;
            EntryHandle = entry.EntryHandle;
            Name = entry.Name.ToString();
            Value = new NetworkTableValue(entry.Value.Value);
            Flags = entry.Flags;
            m_instance = inst;
        }
    }
}
