using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using NetworkTables.Handles;
using NetworkTables.Natives;
using WPIUtil.Marshal;

namespace NetworkTables;

[NativeMarshalling(typeof(NetworkTableEventMarshaller))]
public readonly struct NetworkTableEvent : INativeArrayFree<NetworkTableEventMarshaller.NativeNetworkTableEvent>
{
    public bool Is(EventFlags kind) => (Flags & kind) != 0;
    public required NtListener ListenerHandle { get; init; }
    public required EventFlags Flags { get; init; }
    public ConnectionInfo? ConnectionInfo { get; init; }
    public TopicInfo? TopicInfo { get; init; }
    public ValueEventData? ValueData { get; init; }
    public LogMessage? LogMessage { get; init; }
    public TimeSyncEventData? TimeSyncData { get; init; }

    public static unsafe void FreeArray(NetworkTableEventMarshaller.NativeNetworkTableEvent* array, int len)
    {
        NtCore.DisposeEventArray(array, (nuint)len);
    }
}

[CustomMarshaller(typeof(NetworkTableEvent), MarshalMode.ElementOut, typeof(ReturnInArray))]
public static unsafe class NetworkTableEventMarshaller
{
    public static class ReturnInArray
    {
        public static NetworkTableEvent ConvertToManaged(in NativeNetworkTableEvent unmanaged)
        {
            if ((unmanaged.flags & EventFlags.Connection) != 0)
            {
                return new NetworkTableEvent
                {
                    ListenerHandle = new NtListener(unmanaged.listener),
                    Flags = unmanaged.flags,
                    ConnectionInfo = ConnectionInfoMarshaller.ReturnInArray.ConvertToManaged(unmanaged.data.connInfo),
                };
            }
            else if ((unmanaged.flags & EventFlags.LogMessage) != 0)
            {
                return new NetworkTableEvent
                {
                    ListenerHandle = new NtListener(unmanaged.listener),
                    Flags = unmanaged.flags,
                    LogMessage = LogMessageMarshaller.ConvertToManaged(unmanaged.data.logMessage),
                };
            }
            else if ((unmanaged.flags & EventFlags.Topic) != 0)
            {
                return new NetworkTableEvent
                {
                    ListenerHandle = new NtListener(unmanaged.listener),
                    Flags = unmanaged.flags,
                    TopicInfo = TopicInfoMarshaller.ReturnInArray.ConvertToManaged(unmanaged.data.topicInfo),
                };
            }
            else if ((unmanaged.flags & EventFlags.ValueAll) != 0)
            {
                return new NetworkTableEvent
                {
                    ListenerHandle = new NtListener(unmanaged.listener),
                    Flags = unmanaged.flags,
                    ValueData = ValueEventDataMarshaller.ConvertToManaged(unmanaged.data.valueData),
                };
            }
            else if ((unmanaged.flags & EventFlags.TimeSync) != 0)
            {
                return new NetworkTableEvent
                {
                    ListenerHandle = new NtListener(unmanaged.listener),
                    Flags = unmanaged.flags,
                    TimeSyncData = TimeSyncEventDataMarshaller.ConvertToManaged(unmanaged.data.timeSyncData),
                };
            }
            else
            {
                return new NetworkTableEvent
                {
                    ListenerHandle = new NtListener(unmanaged.listener),
                    Flags = unmanaged.flags,
                };
            }
        }

        public static NativeNetworkTableEvent ConvertToUnmanaged(in NetworkTableEvent managed)
        {
            throw new NotSupportedException();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeNetworkTableEvent
    {
        public int listener;
        public EventFlags flags;
        public NtEventUnion data;

        [StructLayout(LayoutKind.Explicit)]
        public struct NtEventUnion
        {
            [FieldOffset(0)]
            public ConnectionInfoMarshaller.NativeConnectionInfo connInfo;

            [FieldOffset(0)]
            public TopicInfoMarshaller.NativeTopicInfo topicInfo;

            [FieldOffset(0)]
            public ValueEventDataMarshaller.NativeValueEventData valueData;

            [FieldOffset(0)]
            public LogMessageMarshaller.NativeLogMessage logMessage;

            [FieldOffset(0)]
            public TimeSyncEventDataMarshaller.NativeTimeSyncEventData timeSyncData;
        }
    }

}
