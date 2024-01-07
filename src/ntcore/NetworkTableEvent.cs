using System.Runtime.InteropServices.Marshalling;
using NetworkTables.Natives;

namespace NetworkTables;

[NativeMarshalling(typeof(NtEventMarshaller))]
public struct NetworkTableEvent {
    public int ListenerHandle {get;}
    public EventFlags Flags{get;}
}
