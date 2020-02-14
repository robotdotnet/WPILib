using System;

namespace NetworkTables
{
    public delegate void TableListener(NetworkTable parent, ReadOnlySpan<char> name, NotifyFlags flags);
}
