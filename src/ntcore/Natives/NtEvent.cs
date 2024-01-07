using System.Runtime.InteropServices;
using WPIUtil.Marshal;

namespace NetworkTables.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct NtEvent : INativeArrayFree<NtEvent>, INativeFree<NtEvent>
{
    public int listener;
    public uint flags;
    public NtEventUnion data;

    public static unsafe void Free(NtEvent* ptr)
    {
        NtCore.DisposeEvent(ptr);
    }

    public static unsafe void FreeArray(NtEvent* ptr, int len)
    {
        NtCore.DisposeEventArray(ptr, (nuint)len);
    }

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
