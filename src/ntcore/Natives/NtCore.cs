using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using NetworkTables.Handles;
using WPIUtil;
using WPIUtil.Handles;
using WPIUtil.Marshal;

namespace NetworkTables.Natives;

public static partial class NtCore
{
    [LibraryImport("ntcore", EntryPoint = "NT_GetDefaultInstance")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtInst GetDefaultInstance();

    [LibraryImport("ntcore", EntryPoint = "NT_CreateInstance")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtInst CreateInstance();

    [LibraryImport("ntcore", EntryPoint = "NT_DestroyInstance")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyInstance(NtInst inst);

    [LibraryImport("ntcore", EntryPoint = "NT_GetInstanceFromHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial NtInst GetInstanceFromHandle(int handle);

    public static NtInst GetInstanceFromHandle<T>(T handle) where T : IWPIIntHandle
    {
        return GetInstanceFromHandle(handle.Handle);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial NtEntry GetEntry(NtInst inst, WpiString name);

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void GetEntryName(int entry, [MarshalUsing(typeof(WpiStringMarshaller))] out string name);

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial NetworkTableType GetEntryType(int entry);

    public static unsafe NetworkTableType GetEntryType<T>(T entry) where T : struct, INtEntryHandle
    {
        return GetEntryType(entry.Handle);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryLastChange")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial ulong GetEntryLastChange(int entry);

    public static ulong GetEntryLastChange<T>(T entry) where T : struct, INtEntryHandle
    {
        return GetEntryLastChange(entry.Handle);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetEntryValue(int entry, out NetworkTableValue value);

    public static NetworkTableValue GetEntryValue<T>(T entry) where T : struct, INtEntryHandle
    {
        GetEntryValue(entry.Handle, out var value);
        return value;
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryValueType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetEntryValue(int entry, NetworkTableType types, out NetworkTableValue value);

    public static NetworkTableValue GetEntryValue<T>(T entry, NetworkTableType types) where T : struct, INtEntryHandle
    {
        GetEntryValue(entry.Handle, types, out var value);
        return value;
    }

    [LibraryImport("ntcore", EntryPoint = "NT_SetDefaultEntryValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static unsafe partial bool SetDefaultEntryValue(int entry, in RefNetworkTableValue defaultValue);

    public static unsafe bool SetDefaultEntryValue<T>(T entry, in RefNetworkTableValue defaultValue) where T : struct, INtEntryHandle
    {
        return SetDefaultEntryValue(entry.Handle, defaultValue);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_SetEntryValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static unsafe partial bool SetEntryValue(int entry, in RefNetworkTableValue value);

    public static unsafe bool SetEntryValue<T>(T entry, in RefNetworkTableValue value) where T : struct, INtEntryHandle
    {
        return SetEntryValue(entry.Handle, value);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_SetEntryFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetEntryFlags(int entry, EntryFlags flags);

    public static void SetEntryFlags<T>(T entry, EntryFlags flags) where T : struct, INtEntryHandle
    {
        SetEntryFlags(entry.Handle, flags);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial EntryFlags GetEntryFlags(int entry);

    public static EntryFlags GetEntryFlags<T>(T entry) where T : struct, INtEntryHandle
    {
        return GetEntryFlags(entry.Handle);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_ReadQueueValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    internal static unsafe partial NetworkTableValue[] ReadQueueValue(int subentry, out nuint count);

    public static unsafe NetworkTableValue[] ReadQueueValue<T>(T subentry) where T : struct, INtEntryHandle
    {
        return ReadQueueValue(subentry.Handle, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_ReadQueueValueType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    internal static unsafe partial NetworkTableValue[] ReadQueueValue(int subentry, NetworkTableType types, out nuint count);

    public static unsafe NetworkTableValue[] ReadQueueValue<T>(T subentry, NetworkTableType types) where T : struct, INtEntryHandle
    {
        return ReadQueueValue(subentry.Handle, types, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopics")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = "count")]
    internal static unsafe partial NtTopic[] GetTopics(NtInst inst, WpiString prefix, NetworkTableType types, out nuint count);

    public static unsafe NtTopic[] GetTopics(NtInst inst, WpiString prefix, NetworkTableType types)
    {
        return GetTopics(inst, prefix, types, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicsStr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = "count")]
    internal static unsafe partial NtTopic[] GetTopics(NtInst inst, WpiString prefix, [MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> types, nuint typesLen, out nuint count);

    public static unsafe NtTopic[] GetTopics(NtInst inst, WpiString prefix, ReadOnlySpan<string> types)
    {
        return GetTopics(inst, prefix, types, (nuint)types.Length, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicInfos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    internal static unsafe partial TopicInfo[] GetTopicInfos(NtInst inst, WpiString prefix, NetworkTableType types, out nuint count);

    public static TopicInfo[] GetTopicInfos(NtInst inst, WpiString prefix, NetworkTableType types)
    {
        return GetTopicInfos(inst, prefix, types, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicInfosStr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    internal static unsafe partial TopicInfo[] GetTopicInfos(NtInst inst, WpiString prefix, [MarshalUsing(typeof(Utf8StringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> types, nuint typesLen, out nuint count);

    public static TopicInfo[] GetTopicInfos(NtInst inst, WpiString prefix, ReadOnlySpan<string> types)
    {
        return GetTopicInfos(inst, prefix, types, (nuint)types.Length, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static unsafe partial bool GetTopicInfo(NtTopic topic, TopicInfoMarshaller.NativeTopicInfo* info);

    public static unsafe TopicInfo? GetTopicInfo(NtTopic topic)
    {
        TopicInfoMarshaller.NativeTopicInfo tmp = default;
        bool isValid = GetTopicInfo(topic, &tmp);
        return isValid ? TopicInfoMarshaller.ReturnFrom.ConvertToManaged(tmp) : null;
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial NtTopic GetTopic(NtInst inst, WpiString name);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void GetTopicName(NtTopic topic, [MarshalUsing(typeof(WpiStringMarshaller))] out string name);


    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NetworkTableType GetTopicType(NtTopic topic);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicTypeString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static unsafe partial void GetTopicTypeString(NtTopic topic, [MarshalUsing(typeof(WpiStringMarshaller))] out string name);

    [LibraryImport("ntcore", EntryPoint = "NT_SetTopicPersistent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTopicPersistent(NtTopic topic, [MarshalAs(UnmanagedType.I4)] bool value);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicPersistent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetTopicPersistent(NtTopic topic);

    [LibraryImport("ntcore", EntryPoint = "NT_SetTopicRetained")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTopicRetained(NtTopic topic, [MarshalAs(UnmanagedType.I4)] bool value);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicRetained")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetTopicRetained(NtTopic topic);

    [LibraryImport("ntcore", EntryPoint = "NT_SetTopicCached")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTopicCached(NtTopic topic, [MarshalAs(UnmanagedType.I4)] bool value);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicCached")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetTopicCached(NtTopic topic);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicExists")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetTopicExists(NtTopic handle);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicExists")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static partial bool GetTopicExists(int handle);

    public static bool GetTopicExists<T>(T handle) where T : struct, INtEntryHandle
    {
        return GetTopicExists(handle.Handle);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void GetTopicProperty(NtTopic topic, WpiString name, [MarshalUsing(typeof(WpiStringMarshaller))] out string property);

    [LibraryImport("ntcore", EntryPoint = "NT_SetTopicProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetTopicProperty(NtTopic topic, WpiString name, WpiString value);

    [LibraryImport("ntcore", EntryPoint = "NT_DeleteTopicProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DeleteTopicProperty(NtTopic topic, WpiString name);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void GetTopicProperties(NtTopic topic, [MarshalUsing(typeof(WpiStringMarshaller))] out string properties);

    [LibraryImport("ntcore", EntryPoint = "NT_SetTopicProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTopicProperties(NtTopic topic, WpiString properties);

    [LibraryImport("ntcore", EntryPoint = "NT_Subscribe")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtSubscriber Subscribe(NtTopic topic, NetworkTableType type, WpiString typeStr, in PubSubOptions options);

    [LibraryImport("ntcore", EntryPoint = "NT_Unsubscribe")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Unsubscribe(NtSubscriber sub);

    [LibraryImport("ntcore", EntryPoint = "NT_Publish")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtPublisher Publish(NtTopic topic, NetworkTableType type, WpiString typeStr, in PubSubOptions options);

    [LibraryImport("ntcore", EntryPoint = "NT_PublishEx")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtPublisher PublishEx(NtTopic topic, NetworkTableType type, WpiString typeStr, WpiString properties, in PubSubOptions options);

    [LibraryImport("ntcore", EntryPoint = "NT_Unpublish")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void Unpublish(int pubentry);

    public static void Unpublish<T>(T pubsubentry) where T : struct, INtEntryHandle
    {
        Unpublish(pubsubentry.Handle);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryEx")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial NtEntry GetEntry(NtTopic topic, NetworkTableType type, WpiString typeStr, in PubSubOptions options);

    [LibraryImport("ntcore", EntryPoint = "NT_ReleaseEntry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReleaseEntry(NtEntry entry);

    [LibraryImport("ntcore", EntryPoint = "NT_Release")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void Release(int pubsubentry);

    public static void Release<T>(T pubsubentry) where T : struct, INtEntryHandle
    {
        Release(pubsubentry.Handle);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicFromHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial NtTopic GetTopicFromHandle(int pubsubentry);

    public static NtTopic GetTopicFromHandle<T>(T pubsubentry) where T : struct, INtEntryHandle
    {
        return GetTopicFromHandle(pubsubentry.Handle);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_Now")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long Now();

    [LibraryImport("ntcore", EntryPoint = "NT_SetNow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetNow(long timestamp);

    [LibraryImport("ntcore", EntryPoint = "NT_SubscribeMultiple")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial NtMultiSubscriber SubscribeMultiple(NtInst inst, [MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> prefixes, nuint prefixesLen, in PubSubOptions options);

    public static NtMultiSubscriber SubscribeMultiple(NtInst inst, [MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> prefixes, in PubSubOptions options)
    {
        return SubscribeMultiple(inst, prefixes, (nuint)prefixes.Length, options);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_UnsubscribeMultiple")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UnsubscribeMultiple(NtMultiSubscriber sub);
}
