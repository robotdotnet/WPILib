using System;
using System.Collections.Generic;
using NetworkTables.Natives;

namespace NetworkTables
{
    public readonly struct EntryNotification : IEquatable<EntryNotification>
    {
        public NtEntryListener Listener { get; }
        public NtEntry EntryHandle { get; }
        public string Name { get; }
        public NetworkTableValue Value { get; }
        public NotifyFlags Flags { get; }
        public NetworkTableEntry Entry => new NetworkTableEntry(m_instance, EntryHandle);
        private readonly NetworkTableInstance m_instance;

        internal unsafe EntryNotification(NetworkTableInstance inst, in RefEntryNotification entry)
        {
            Listener = entry.Listener;
            EntryHandle = entry.EntryHandle;
            Name = entry.Name;
            Value = new NetworkTableValue(entry.Value.Value);
            Flags = entry.Flags;
            m_instance = inst;
        }


        public static bool operator ==(EntryNotification left, EntryNotification right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(EntryNotification left, EntryNotification right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            return obj is EntryNotification notification && Equals(notification);
        }

        public bool Equals(EntryNotification other)
        {
            return Listener.Equals(other.Listener) &&
                   EntryHandle.Equals(other.EntryHandle) &&
                   Name == other.Name &&
                   Value.Equals(other.Value) &&
                   Flags == other.Flags &&
                   EqualityComparer<NetworkTableEntry>.Default.Equals(Entry, other.Entry) &&
                   EqualityComparer<NetworkTableInstance>.Default.Equals(m_instance, other.m_instance);
        }

        public override int GetHashCode()
        {
            var hashCode = 2065220519;
            hashCode = hashCode * -1521134295 + Listener.GetHashCode();
            hashCode = hashCode * -1521134295 + EntryHandle.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + Flags.GetHashCode();
            hashCode = hashCode * -1521134295 + Entry.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<NetworkTableInstance>.Default.GetHashCode(m_instance);
            return hashCode;
        }
    }
}
