using System.Runtime.InteropServices;

namespace NetworkTables.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct NtEvent
{
    public int listener;
    public uint flags;
    public NtEventUnion data;

    [StructLayout(LayoutKind.Explicit)]
    public struct NtEventUnion
    {
        [FieldOffset(0)]
        public NtConnectionInfo connInfo;

        [FieldOffset(0)]
        public NtTopicInfo topicInfo;

        [FieldOffset(0)]
        public NtValueEventData valueData;

        [FieldOffset(0)]
        public NtLogMessage logMessage;

        [FieldOffset(0)]
        public NtTimeSyncEventData timeSyncData;
    }
}
