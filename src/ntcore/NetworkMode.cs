namespace NetworkTables;

[Flags]
public enum NetworkMode : uint
{
    None = 0,
    Server = 0x1,
    Client3 = 0x2,
    Client4 = 0x4,
    Starting = 0x8,
    Local = 0x10,
}
