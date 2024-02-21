namespace CsCore;

[Flags]
public enum EventKind
{
    SourceCreated = 0x0001,
    SourceDestroyed = 0x0002,
    SourceConnected = 0x0004,
    SourceDisconnected = 0x0008,
    SourceVideoModesUpdated = 0x0010,
    SourceVideoModeChanged = 0x0020,
    SourcePropertyCreated = 0x0040,
    SourcePropertyValueUpdated = 0x0080,
    SourcePropertyChoicesUpdated = 0x0100,
    SinkSourceChanged = 0x0200,
    SinkCreated = 0x0400,
    SinkDestroyed = 0x0800,
    SinkEnabled = 0x1000,
    SinkDisabled = 0x2000,
    NetworkInterfacesChanged = 0x4000,
    TelemetryUpdated = 0x8000,
    SinkPropertyCreated = 0x10000,
    SinkPropertyValueUpdated = 0x20000,
    SinkPropertyChoicesUpdated = 0x40000,
    UsbCamerasChanged = 0x80000
}
