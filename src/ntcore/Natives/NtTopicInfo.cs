using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Natives;

public static unsafe partial class TopicInfoFree
{
    [LibraryImport("ntcore", EntryPoint = "NT_DisposeTopicInfoArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DisposeTopicInfoArray(NtTopicInfo* arr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_DisposeTopicInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DisposeTopicInfo(NtTopicInfo* arr);
}


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

        public static void Free(in NtTopicInfo unmanaged)
        {
            fixed (NtTopicInfo* ptr = &unmanaged)
            {
                TopicInfoFree.DisposeTopicInfo(ptr);
            }
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

[CustomMarshaller(typeof(TopicInfo[]), MarshalMode.ManagedToUnmanagedOut, typeof(TopicInfoArrayMarshaller<,>))]
[ContiguousCollectionMarshaller]
public unsafe struct TopicInfoArrayMarshaller<GenericPlaceholder, TUnmanagedElement> where TUnmanagedElement : unmanaged
{
    private TUnmanagedElement* unmanagedStorage;
    private int? length;
    private TopicInfo[]? managedStorage;

    public TopicInfoArrayMarshaller()
    {
        if (typeof(TUnmanagedElement) != typeof(NtTopicInfo))
        {
            throw new InvalidOperationException("Unmanaged type must be topic");
        }
    }

    public ReadOnlySpan<TUnmanagedElement> GetUnmanagedValuesSource(int numElements)
    {
        length = numElements;
        return new ReadOnlySpan<TUnmanagedElement>(unmanagedStorage, numElements);
    }

    public Span<TopicInfo> GetManagedValuesDestination(int numElements)
    {
        length = numElements;
        managedStorage = new TopicInfo[numElements];
        return managedStorage;
    }

    public readonly void Free()
    {
        if (unmanagedStorage != null && length.HasValue)
        {
            TopicInfoFree.DisposeTopicInfoArray((NtTopicInfo*)unmanagedStorage, (nuint)length.Value);
        }
    }

    public void FromUnmanaged(TUnmanagedElement* unmanaged)
    {
        unmanagedStorage = unmanaged;
    }

    public readonly TopicInfo[] ToManaged()
    {
        return managedStorage!;
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
