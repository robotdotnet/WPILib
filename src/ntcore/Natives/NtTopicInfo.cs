using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(TopicInfo), MarshalMode.ManagedToUnmanagedOut, typeof(ReturnFrom))]
[CustomMarshaller(typeof(TopicInfo), MarshalMode.ElementOut, typeof(ReturnInArray))]
public static unsafe class NtTopicInfoMarshaller
{
    public static class ReturnFrom
    {
        public static TopicInfo ConvertToManaged(in NtTopicInfo unmanaged)
        {
            return ReturnInArray.ConvertToManaged(unmanaged);
        }

        public static void Free(NtTopicInfo* unmanaged)
        {
        }
    }

    public static class ReturnInArray
    {
        public static TopicInfo ConvertToManaged(in NtTopicInfo unmanaged)
        {
            return new TopicInfo
            {
                TopicHandle = unmanaged.topic,
                Name = NtStringMarshaller.ConvertToManaged(unmanaged.name),
                Type = unmanaged.type,
                TypeStr = NtStringMarshaller.ConvertToManaged(unmanaged.typeStr),
                Properties = NtStringMarshaller.ConvertToManaged(unmanaged.properties),

            };
        }

        public static NtTopicInfo ConvertToUnmanaged(in TopicInfo managed)
        {
            throw new NotImplementedException();
        }
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct NtTopicInfo
{
    public int topic;
    public NtString name;
    public NetworkTableType type;
    public NtString typeStr;
    public NtString properties;
}
