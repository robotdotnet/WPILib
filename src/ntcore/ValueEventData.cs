using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using NetworkTables.Handles;
using NetworkTables.Natives;

namespace NetworkTables;

[NativeMarshalling(typeof(ValueEventDataMarshaller))]
[StructLayout(LayoutKind.Auto)]
public record struct ValueEventData(NtTopic TopicHandle, int Subentry, NetworkTableValue Value)
{
    private Topic? m_topicObject;
    public Topic GetTopic(NetworkTableInstance instance)
    {
        m_topicObject ??= new Topic(instance, TopicHandle);
        return m_topicObject;
    }

    public readonly string GetTopicName()
    {
        NtCore.GetTopicName(TopicHandle, out var name);
        return name;
    }
}

[CustomMarshaller(typeof(ValueEventData), MarshalMode.ManagedToUnmanagedOut, typeof(ValueEventDataMarshaller))]
public static unsafe class ValueEventDataMarshaller
{
    public static ValueEventData ConvertToManaged(in NativeValueEventData unmanaged)
    {
        return new ValueEventData
        {
            TopicHandle = new(unmanaged.topic),
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
