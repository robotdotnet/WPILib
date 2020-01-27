using NetworkTables.Natives;
using System;
using WPIUtil;

namespace NetworkTables
{
    /// <summary>
    /// This class contains all info for a given entry.
    /// </summary>
    public readonly struct EntryInfo
    {
        public readonly NtEntry EntryHandle;
        /// <summary>Gets the Name of the entry.</summary>
        public readonly string Name;
        /// <summary>Gets the Type of the entry.</summary>
        public readonly NtType Type;
        /// <summary>Gets the Flags attached to the entry.</summary>
        public readonly EntryFlags Flags;
        /// <summary>Gets the last change time of the entry.</summary>
        public readonly long LastChange;
        /// <summary>Gets the entry object for this entry.</summary>
        public NetworkTableEntry Entry => new NetworkTableEntry(m_instance, EntryHandle);
        private readonly NetworkTableInstance m_instance;

        internal unsafe EntryInfo(NetworkTableInstance instance, NtEntryInfo* entryInfo)
        {
            EntryHandle = entryInfo->entry;
            Type = (NtType)entryInfo->type;
            Flags = (EntryFlags)entryInfo->flags;
            LastChange = (long)entryInfo->last_change;
            Name = UTF8String.ReadUTF8String(entryInfo->name.str, (int)entryInfo->name.len);
            m_instance = instance;
        }
    }
}
