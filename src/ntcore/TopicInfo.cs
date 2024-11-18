using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using NetworkTables.Handles;
using NetworkTables.Natives;
using WPIUtil.Marshal;

namespace NetworkTables;

[NativeMarshalling(typeof(TopicInfoMarshaller))]
[StructLayout(LayoutKind.Auto)]
public record struct TopicInfo(NtTopic TopicHandle, string Name, NetworkTableType Type, string TypeStr, string Properties) : INativeArrayFree<TopicInfoMarshaller.NativeTopicInfo>
{
    public readonly Topic GetTopic(NetworkTableInstance inst)
    {
        return new Topic(inst, TopicHandle);
    }

    public static unsafe void FreeArray(TopicInfoMarshaller.NativeTopicInfo* array, int len)
    {
        NtCore.DisposeTopicInfoArray(array, (nuint)len);
    }
}

[CustomMarshaller(typeof(TopicInfo), MarshalMode.ManagedToUnmanagedOut, typeof(ReturnFrom))]
[CustomMarshaller(typeof(TopicInfo), MarshalMode.ElementOut, typeof(ReturnInArray))]
public static unsafe class TopicInfoMarshaller
{
    public static class ReturnFrom
    {
        public static TopicInfo ConvertToManaged(in NativeTopicInfo unmanaged)
        {
            return new TopicInfo
            {
                TopicHandle = new NtTopic(unmanaged.topic),
                Name = unmanaged.name.ConvertToString(),
                Type = unmanaged.type,
                TypeStr = unmanaged.typeStr.ConvertToString(),
                Properties = unmanaged.properties.ConvertToString(),

            };
        }

        public static void Free(NativeTopicInfo unmanaged)
        {
            NtCore.DisposeTopicInfo(&unmanaged);
        }
    }

    public static class ReturnInArray
    {
        public static TopicInfo ConvertToManaged(in NativeTopicInfo unmanaged)
        {
            return ReturnFrom.ConvertToManaged(unmanaged);
        }

        public static NativeTopicInfo ConvertToUnmanaged(in TopicInfo managed)
        {
            throw new NotSupportedException();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeTopicInfo
    {
        public int topic;
        public WpiStringMarshaller.WpiStringNative name;
        public NetworkTableType type;
        public WpiStringMarshaller.WpiStringNative typeStr;
        public WpiStringMarshaller.WpiStringNative properties;
    }

}
