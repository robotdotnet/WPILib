namespace NetworkTables;

[Flags]
public enum NetworkMode : uint
{
    None = 0x0,
    Server = 0x1,
    Client = 0x4,
    Client4 = Client,
    Starting = 0x8,
    Local = 0x10,
    MdnsAnnouncing = 0x20,
}
