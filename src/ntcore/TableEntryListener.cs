using System;

namespace NetworkTables
{
    public delegate void TableEntryListener(NetworkTable table, ReadOnlySpan<char> key, in NetworkTableEntry entry, in RefNetworkTableValue value, NotifyFlags flags);
}
