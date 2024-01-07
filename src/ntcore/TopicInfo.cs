using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Marshal;

namespace NetworkTables.Natives;

[NativeMarshalling(typeof(TopicInfoMarshaller))]
[StructLayout(LayoutKind.Auto)]
public record struct TopicInfo(int TopicHandle, string Name, NetworkTableType Type, string TypeStr, string Properties);

[CustomMarshaller(typeof(TopicInfo), MarshalMode.ManagedToUnmanagedOut, typeof(ReturnFrom))]
[CustomMarshaller(typeof(TopicInfo), MarshalMode.ElementOut, typeof(ReturnInArray))]
public static unsafe class TopicInfoMarshaller
{
    public static class ReturnFrom
    {
        public static TopicInfo ConvertToManaged(in NativeTopicInfo unmanaged)
        {
            return ReturnInArray.ConvertToManaged(unmanaged);
        }

        public static void Free(NativeTopicInfo unmanaged)
        {
            NativeTopicInfo.Free(&unmanaged);
        }
    }

    public static class ReturnInArray
    {
        public static TopicInfo ConvertToManaged(in NativeTopicInfo unmanaged)
        {
            return new TopicInfo
            {
                TopicHandle = unmanaged.topic,
                Name = NtStringMarshaller.ManagedConvert(unmanaged.name),
                Type = unmanaged.type,
                TypeStr = NtStringMarshaller.ManagedConvert(unmanaged.typeStr),
                Properties = NtStringMarshaller.ManagedConvert(unmanaged.properties),

            };
        }

        public static NativeTopicInfo ConvertToUnmanaged(in TopicInfo managed)
        {
            throw new NotSupportedException();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeTopicInfo : INativeArrayFree<NativeTopicInfo>, INativeFree<NativeTopicInfo>
    {
        public int topic;
        public NtString name;
        public NetworkTableType type;
        public NtString typeStr;
        public NtString properties;

        public static unsafe void Free(NativeTopicInfo* ptr)
        {
            NtCore.DisposeTopicInfo(ptr);
        }

        public static unsafe void FreeArray(NativeTopicInfo* ptr, int len)
        {
            NtCore.DisposeTopicInfoArray(ptr, (nuint)len);
        }
    }

}
