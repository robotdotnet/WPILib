using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables;

[NativeMarshalling(typeof(TimeSyncEventDataMarshaller))]
[StructLayout(LayoutKind.Auto)]
public readonly record struct TimeSyncEventData(long ServerTimeOffset, long Rtt2, bool IsValid);

[CustomMarshaller(typeof(TimeSyncEventData), MarshalMode.ManagedToUnmanagedOut, typeof(TimeSyncEventDataMarshaller))]
public static unsafe class TimeSyncEventDataMarshaller
{
    public static TimeSyncEventData ConvertToManaged(in NativeTimeSyncEventData unmanaged)
    {
        return new TimeSyncEventData
        {
            ServerTimeOffset = unmanaged.serverTimeOffset,
            Rtt2 = unmanaged.rtt2,
            IsValid = unmanaged.valid != 0,
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeTimeSyncEventData
    {
        public long serverTimeOffset;
        public long rtt2;
        public int valid;
    }
}
