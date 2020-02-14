using NetworkTables.Natives;
using WPIUtil;

namespace NetworkTables
{
#pragma warning disable CA1815 // Override equals and operator equals on value types
    public readonly ref struct RefEntryNotification
#pragma warning restore CA1815 // Override equals and operator equals on value types
    {
        public readonly NtEntryListener Listener { get; }
        public readonly NtEntry EntryHandle { get; }
        public readonly string Name { get; }
        public readonly RefNetworkTableValue Value { get; }
        public readonly NotifyFlags Flags { get; }
        public NetworkTableEntry Entry => new NetworkTableEntry(m_instance, EntryHandle);
        private readonly NetworkTableInstance m_instance;

        public EntryNotification CopyNotification => new EntryNotification(m_instance, this);

        internal unsafe RefEntryNotification(NetworkTableInstance inst, in NtEntryNotification entry)
        {
            Listener = entry.listener;
            EntryHandle = entry.entry;
            Name = UTF8String.ReadUTF8String(entry.name.str, entry.name.len);
            Value = new RefNetworkTableValue(new RefManagedValue(entry.value));
            Flags = (NotifyFlags)entry.flags;
            m_instance = inst;
        }
    }
}
