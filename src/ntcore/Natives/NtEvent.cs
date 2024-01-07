using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Marshal;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(NetworkTableEvent), MarshalMode.ManagedToUnmanagedOut, typeof(ReturnFrom))]
[CustomMarshaller(typeof(NetworkTableEvent), MarshalMode.ElementOut, typeof(ReturnInArray))]
public static unsafe class NtEventMarshaller
{
public static class ReturnFrom
    {
        public static NetworkTableEvent ConvertToManaged(in NtEvent unmanaged)
        {
            return ReturnInArray.ConvertToManaged(unmanaged);
        }

        public static void Free(NtEvent unmanaged)
        {
            NtEvent.Free(&unmanaged);
        }
    }

    public static class ReturnInArray
    {
        public static NetworkTableEvent ConvertToManaged(in NtEvent unmanaged)
        {
            return default;
        }

        public static NtEvent ConvertToUnmanaged(in NetworkTableEvent managed)
        {
            throw new NotSupportedException();
        }
    }
}

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
