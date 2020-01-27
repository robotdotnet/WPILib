using NetworkTables.Natives;
using System;
using System.Collections.Generic;
using System.Text;
using WPIUtil;

namespace NetworkTables
{
    public readonly ref struct RefEntryNotification
    {
        public readonly NtEntryListener Listener;
        public readonly NtEntry EntryHandle;
        public readonly string Name;
        public readonly RefNetworkTableValue Value;
        public readonly NotifyFlags Flags;
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
