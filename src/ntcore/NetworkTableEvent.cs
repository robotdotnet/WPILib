using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using NetworkTables.Natives;
using WPIUtil.Marshal;

namespace NetworkTables;

[NativeMarshalling(typeof(NetworkTableEventMarshaller))]
public readonly struct NetworkTableEvent
{
    public int ListenerHandle { get; }
    public EventFlags Flags { get; }
    public ConnectionInfo? ConnectionInfo { get; }
    public TopicInfo? TopicInfo { get; }
    public ValueEventData? ValueData { get; }
    public LogMessage? LogMessage { get; }
    public TimeSyncEventData? TimeSyncData { get; }
}

[CustomMarshaller(typeof(NetworkTableEvent), MarshalMode.ElementOut, typeof(ReturnInArray))]
public static unsafe class NetworkTableEventMarshaller
{
    public static class ReturnInArray
    {
        public static NetworkTableEvent ConvertToManaged(in NativeNetworkTableEvent unmanaged)
        {
            throw new NotImplementedException();
        }

        public static NativeNetworkTableEvent ConvertToUnmanaged(in NetworkTableEvent managed)
        {
            throw new NotSupportedException();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeNetworkTableEvent : INativeArrayFree<NativeNetworkTableEvent>, INativeFree<NativeNetworkTableEvent>
    {
        public int listener;
        public uint flags;
        public NtEventUnion data;

        public static unsafe void Free(NativeNetworkTableEvent* ptr)
        {
            NtCore.DisposeEvent(ptr);
        }

        public static unsafe void FreeArray(NativeNetworkTableEvent* ptr, int len)
        {
            NtCore.DisposeEventArray(ptr, (nuint)len);
        }

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
