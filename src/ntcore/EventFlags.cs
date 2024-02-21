namespace NetworkTables;

[Flags]
public enum EventFlags : uint
{
    None = 0,
    Immediate = 0x1,
    Connected = 0x2,
    Disconnected = 0x4,
    Connection = Connected | Disconnected,
    Publish = 0x8,
    Unpublish = 0x10,
    Properties = 0x20,
    Topic = Publish | Unpublish | Properties,
    ValueRemote = 0x40,
    ValueLocal = 0x80,
    ValueAll = ValueRemote | ValueLocal,
    LogMessage = 0x100,
    TimeSync = 0x200,
}
