using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using NetworkTables.Natives;

namespace NetworkTables;

[NativeMarshalling(typeof(ValueEventDataMarshaller))]
[StructLayout(LayoutKind.Auto)]
public readonly record struct ValueEventData(int TopicHandle, int Subentry, NetworkTableValue Value);

[CustomMarshaller(typeof(ValueEventData), MarshalMode.ManagedToUnmanagedOut, typeof(ValueEventDataMarshaller))]
public static unsafe class ValueEventDataMarshaller
{
    public static ValueEventData ConvertToManaged(in NativeValueEventData unmanaged)
    {
        return new ValueEventData
        {
            TopicHandle = unmanaged.topic,
            Subentry = unmanaged.subentry,
            Value = NetworkTableValueMarshaller.ReturnFrom.ConvertToManaged(unmanaged.value),
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeValueEventData
    {
        public int topic;
        public int subentry;
        public NetworkTableValueMarshaller.NativeNetworkTableValue value;
    }

}
