using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using NetworkTables.Handles;
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

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static unsafe partial NtEntry GetEntry(NtInst inst, StringWrapper name, nuint nameLen);

    public static unsafe NtEntry GetEntry(NtInst inst, string name)
    {
        StringWrapper wrapper = name;
        return GetEntry(inst, wrapper, wrapper.Len);
    }

    // Cannot build source generator due to not being able to get the len for a string source generator
    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(StringUtf8ReturnMarshaller<>), CountElementName = nameof(nameLen))]
    internal static unsafe partial string GetEntryName(NtPubSubEntry entry, out nuint nameLen);

    public static unsafe string GetEntryName(NtPubSubEntry entry)
    {
        return GetEntryName(entry, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NetworkTableType GetEntryType(NtPubSubEntry entry);

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryLastChange")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GetEntryLastChange(NtPubSubEntry entry);

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetEntryValue(NtPubSubEntry entry, out NetworkTableValue value);

    [LibraryImport("ntcore", EntryPoint = "NT_SetDefaultEntryValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetDefaultEntryValue(NtPubSubEntry entry, in NetworkTableValue defaultValue);

    [LibraryImport("ntcore", EntryPoint = "NT_SetEntryValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetEntryValue(NtPubSubEntry entry, in NetworkTableValue value);

    [LibraryImport("ntcore", EntryPoint = "NT_SetEntryFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEntryFlags(NtPubSubEntry entry, EntryFlags flags);

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial EntryFlags GetEntryFlags(NtPubSubEntry entry);

    [LibraryImport("ntcore", EntryPoint = "NT_ReadQueueValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    internal static unsafe partial NetworkTableValue[] ReadQueueValue(NtPubSubEntry subentry, out nuint count);

    public static unsafe NetworkTableValue[] ReadQueueValue(NtPubSubEntry subentry)
    {
        return ReadQueueValue(subentry, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopics")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(TopicArrayMarshaller<NtTopic, int>), CountElementName = "count")]
    internal static unsafe partial NtTopic[] GetTopics(NtInst inst, StringWrapper prefix, nuint prefixLen, NetworkTableType types, out nuint count);

    public static unsafe NtTopic[] GetTopics(NtInst inst, string prefix, NetworkTableType types)
    {
        StringWrapper wrapper = prefix;
        return GetTopics(inst, wrapper, wrapper.Len, types, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicsStr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(TopicArrayMarshaller<int, int>), CountElementName = "count")]
    internal static unsafe partial NtTopic[] GetTopics(NtInst inst, StringWrapper prefix, nuint prefixLen, [MarshalUsing(typeof(Utf8StringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> types, nuint typesLen, out nuint count);

    public static unsafe NtTopic[] GetTopics(NtInst inst, string prefix, ReadOnlySpan<string> types)
    {
        StringWrapper wrapper = prefix;
        return GetTopics(inst, wrapper, wrapper.Len, types, (nuint)types.Length, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicInfos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    internal static unsafe partial TopicInfo[] GetTopicInfos(NtInst inst, StringWrapper prefix, nuint prefixLen, NetworkTableType types, out nuint count);

    public static TopicInfo[] GetTopicInfos(NtInst inst, string prefix, NetworkTableType types)
    {
        StringWrapper wrapper = prefix;
        return GetTopicInfos(inst, wrapper, wrapper.Len, types, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicInfosStr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    internal static unsafe partial TopicInfo[] GetTopicInfos(NtInst inst, StringWrapper prefix, nuint prefixLen, [MarshalUsing(typeof(Utf8StringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> types, nuint typesLen, out nuint count);

    public static TopicInfo[] GetTopicInfos(NtInst inst, string prefix, ReadOnlySpan<string> types)
    {
        StringWrapper wrapper = prefix;
        return GetTopicInfos(inst, wrapper, wrapper.Len, types, (nuint)types.Length, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static unsafe partial bool GetTopicInfo(NtTopic topic, TopicInfoMarshaller.NativeTopicInfo* info);

    public static unsafe TopicInfo GetTopicInfo(NtTopic topic)
    {
        TopicInfoMarshaller.NativeTopicInfo tmp = default;
        bool isValid = GetTopicInfo(topic, &tmp);
        return isValid ? TopicInfoMarshaller.ReturnFrom.ConvertToManaged(tmp) : TopicInfo.Empty;
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static unsafe partial NtTopic GetTopic(NtInst inst, StringWrapper name, nuint nameLen);

    public static NtTopic GetTopic(NtInst inst, string name)
    {
        StringWrapper wrapper = name;
        return GetTopic(inst, wrapper, wrapper.Len);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(StringUtf8ReturnMarshaller<>), CountElementName = nameof(nameLen))]
    internal static unsafe partial string GetTopicName(NtTopic topic, out nuint nameLen);

    public static unsafe string GetTopicName(NtTopic topic)
    {
        return GetTopicName(topic, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NetworkTableType GetTopicType(NtTopic topic);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicTypeString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(StringUtf8ReturnMarshaller<>), CountElementName = nameof(typeLen))]
    internal static unsafe partial string GetTopicTypeString(NtTopic topic, out nuint typeLen);

    public static unsafe string GetTopicTypeString(NtTopic topic)
    {
        return GetTopicTypeString(topic, out var _);
    }

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
    public static partial bool GetTopicExists(NtPubSubEntry handle);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(StringUtf8ReturnMarshaller<>), CountElementName = nameof(len))]
    internal static unsafe partial string GetTopicProperty(NtTopic topic, string name, out nuint len);

    public static unsafe string GetTopicProperty(NtTopic topic, string name)
    {
        return GetTopicProperty(topic, name, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_SetTopicProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetTopicProperty(NtTopic topic, string name, string value);

    [LibraryImport("ntcore", EntryPoint = "NT_DeleteTopicProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DeleteTopicProperty(NtTopic topic, string name);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(StringUtf8ReturnMarshaller<>), CountElementName = nameof(len))]
    internal static unsafe partial string GetTopicProperties(NtTopic topic, out nuint len);

    public static string GetTopicProperties(NtTopic topic)
    {
        return GetTopicProperties(topic, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_SetTopicProperties", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTopicProperties(NtTopic topic, string properties);

    [LibraryImport("ntcore", EntryPoint = "NT_Subscribe", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtSubscriber Subscribe(NtTopic topic, NetworkTableType type, string typeStr, in PubSubOptions options);

    [LibraryImport("ntcore", EntryPoint = "NT_Unsubscribe")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Unsubscribe(NtSubscriber sub);

    [LibraryImport("ntcore", EntryPoint = "NT_Publish", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtPublisher Publish(NtTopic topic, NetworkTableType type, string typeStr, in PubSubOptions options);

    [LibraryImport("ntcore", EntryPoint = "NT_PublishEx", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtPublisher PublishEx(NtTopic topic, NetworkTableType type, string typeStr, string properties, in PubSubOptions options);

    [LibraryImport("ntcore", EntryPoint = "NT_Unpublish")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Unpublish(NtPubSubEntry pubentry);

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryEx", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial NtEntry GetEntry(NtTopic topic, NetworkTableType type, string typeStr, in PubSubOptions options);

    [LibraryImport("ntcore", EntryPoint = "NT_ReleaseEntry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReleaseEntry(NtEntry entry);

    [LibraryImport("ntcore", EntryPoint = "NT_Release")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Release(NtPubSubEntry pubsubentry);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicFromHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtTopic GetTopicFromHandle(NtPubSubEntry pubsubentry);

    [LibraryImport("ntcore", EntryPoint = "NT_Now")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long Now();

    [LibraryImport("ntcore", EntryPoint = "NT_SetNow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetNow(long timestamp);

    [LibraryImport("ntcore", EntryPoint = "NT_SubscribeMultiple")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtMultiSubscriber SubscribeMultiple(NtInst inst, [MarshalUsing(typeof(NtStringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> prefixes, nuint prefixesLen, in PubSubOptions options);

    [LibraryImport("ntcore", EntryPoint = "NT_UnsubscribeMultiple")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UnsubscribeMultiple(NtMultiSubscriber sub);
}
