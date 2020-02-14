using NetworkTables.Natives;
using System;
using WPIUtil;

namespace NetworkTables
{
    /// <summary>
    /// This class contains all info for a given entry.
    /// </summary>
    public readonly struct EntryInfo : IEquatable<EntryInfo>
    {
        public NtEntry EntryHandle { get; }
        /// <summary>Gets the Name of the entry.</summary>
        public string Name { get; }
        /// <summary>Gets the Type of the entry.</summary>
        public NtType Type { get; }
        /// <summary>Gets the Flags attached to the entry.</summary>
        public EntryFlags Flags { get; }
        /// <summary>Gets the last change time of the entry.</summary>
        public long LastChange { get; }
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

        public override bool Equals(object? obj)
        {
            return obj is EntryInfo info && Equals(info);
        }

        public bool Equals(EntryInfo other)
        {
            return EntryHandle.Equals(other.EntryHandle);
        }

        public override int GetHashCode()
        {
            return -2013023459 + EntryHandle.GetHashCode();
        }

        public static bool operator ==(EntryInfo left, EntryInfo right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(EntryInfo left, EntryInfo right)
        {
            return !(left == right);
        }
    }
}
