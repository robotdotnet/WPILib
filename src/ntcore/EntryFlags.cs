namespace NetworkTables;

[Flags]
public enum EntryFlags
{
    None = 0x0,
    Persistent = 0x1,
    Retained = 0x2,
    Uncached = 0x4
}
