using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using WPIUtil.Marshal;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(StringWrapper), MarshalMode.ManagedToUnmanagedIn, typeof(StringUtf8WrapperMarshaller))]
public static unsafe class StringUtf8WrapperMarshaller
{
    public static ref readonly byte GetPinnableReference(StringWrapper managed)
    {
        return ref managed.Str.AsSpan().GetPinnableReference();
    }

    public static byte* ConvertToUnmanaged(StringWrapper managed)
    {
        throw new NotSupportedException("Have to have to satify the marshaller");
    }
}

[NativeMarshalling(typeof(StringUtf8WrapperMarshaller))]
public readonly ref struct StringWrapper(string value)
{
    public readonly byte[] Str = Encoding.UTF8.GetBytes(value);

    public nuint Len => (nuint)Str.Length;

    public static implicit operator StringWrapper(string value)
    {
        return new StringWrapper(value);
    }
}

public static partial class NtCore
{
    [LibraryImport("ntcore", EntryPoint = "NT_GetDefaultInstance")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDefaultInstance();

    [LibraryImport("ntcore", EntryPoint = "NT_CreateInstance")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int CreateInstance();

    [LibraryImport("ntcore", EntryPoint = "NT_DestroyInstance")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyInstance(int inst);

    [LibraryImport("ntcore", EntryPoint = "NT_GetInstanceFromHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetInstanceFromHandle(int handle);

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static unsafe partial int GetEntry(int inst, StringWrapper name, nuint nameLen);

    public static unsafe int GetEntry(int inst, string name)
    {
        StringWrapper wrapper = name;
        return GetEntry(inst, wrapper, wrapper.Len);
    }

    // Cannot build source generator due to not being able to get the len for a string source generator
    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(StringUtf8ReturnMarshaller<>), CountElementName = nameof(nameLen))]
    internal static unsafe partial string GetEntryName(int entry, out nuint nameLen);

    public static unsafe string GetEntryName(int entry)
    {
        return GetEntryName(entry, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NetworkTableType GetEntryType(int entry);

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryLastChange")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GetEntryLastChange(int entry);

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetEntryValue(int entry, out NetworkTableValue value);

    [LibraryImport("ntcore", EntryPoint = "NT_SetDefaultEntryValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetDefaultEntryValue(int entry, in NetworkTableValue defaultValue);

    [LibraryImport("ntcore", EntryPoint = "NT_SetEntryValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetEntryValue(int entry, in NetworkTableValue value);

    [LibraryImport("ntcore", EntryPoint = "NT_SetEntryFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEntryFlags(int entry, EntryFlags flags);

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial EntryFlags GetEntryFlags(int entry);

    [LibraryImport("ntcore", EntryPoint = "NT_ReadQueueValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    internal static unsafe partial NetworkTableValue[] ReadQueueValue(int subentry, out nuint count);

    public static unsafe NetworkTableValue[] ReadQueueValue(int subentry)
    {
        return ReadQueueValue(subentry, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopics")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(TopicArrayMarshaller<int, int>), CountElementName = "count")]
    internal static unsafe partial int[] GetTopics(int inst, StringWrapper prefix, nuint prefixLen, NetworkTableType types, out nuint count);

    public static unsafe int[] GetTopics(int inst, string prefix, NetworkTableType types)
    {
        StringWrapper wrapper = prefix;
        return GetTopics(inst, wrapper, wrapper.Len, types, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicsStr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(TopicArrayMarshaller<int, int>), CountElementName = "count")]
    internal static unsafe partial int[] GetTopics(int inst, StringWrapper prefix, nuint prefixLen, [MarshalUsing(typeof(Utf8StringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> types, nuint typesLen, out nuint count);

    public static unsafe int[] GetTopics(int inst, string prefix, ReadOnlySpan<string> types)
    {
        StringWrapper wrapper = prefix;
        return GetTopics(inst, wrapper, wrapper.Len, types, (nuint)types.Length, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicInfos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    internal static unsafe partial TopicInfo[] GetTopicInfos(int inst, StringWrapper prefix, nuint prefixLen, NetworkTableType types, out nuint count);

    public static TopicInfo[] GetTopicInfos(int inst, string prefix, NetworkTableType types)
    {
        StringWrapper wrapper = prefix;
        return GetTopicInfos(inst, wrapper, wrapper.Len, types, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicInfosStr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    internal static unsafe partial TopicInfo[] GetTopicInfos(int inst, StringWrapper prefix, nuint prefixLen, [MarshalUsing(typeof(Utf8StringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> types, nuint typesLen, out nuint count);

    public static TopicInfo[] GetTopicInfos(int inst, string prefix, ReadOnlySpan<string> types)
    {
        StringWrapper wrapper = prefix;
        return GetTopicInfos(inst, wrapper, wrapper.Len, types, (nuint)types.Length, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static unsafe partial bool GetTopicInfo(int topic, TopicInfoMarshaller.NativeTopicInfo* info);

    public static unsafe TopicInfo GetTopicInfo(int topic)
    {
        TopicInfoMarshaller.NativeTopicInfo tmp = default;
        bool isValid = GetTopicInfo(topic, &tmp);
        return isValid ? TopicInfoMarshaller.ReturnFrom.ConvertToManaged(tmp) : TopicInfo.Empty;
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static unsafe partial int GetTopic(int inst, StringWrapper name, nuint nameLen);

    public static int GetTopic(int inst, string name)
    {
        StringWrapper wrapper = name;
        return GetTopic(inst, wrapper, wrapper.Len);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(StringUtf8ReturnMarshaller<>), CountElementName = nameof(nameLen))]
    internal static unsafe partial string GetTopicName(int topic, out nuint nameLen);

    public static unsafe string GetTopicName(int topic)
    {
        return GetTopicName(topic, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NetworkTableType GetTopicType(int topic);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicTypeString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(StringUtf8ReturnMarshaller<>), CountElementName = nameof(typeLen))]
    internal static unsafe partial string GetTopicTypeString(int topic, out nuint typeLen);

    public static unsafe string GetTopicTypeString(int topic)
    {
        return GetTopicTypeString(topic, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_SetTopicPersistent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTopicPersistent(int topic, [MarshalAs(UnmanagedType.I4)] bool value);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicPersistent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetTopicPersistent(int topic);

    [LibraryImport("ntcore", EntryPoint = "NT_SetTopicRetained")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTopicRetained(int topic, [MarshalAs(UnmanagedType.I4)] bool value);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicRetained")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetTopicRetained(int topic);

    [LibraryImport("ntcore", EntryPoint = "NT_SetTopicCached")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTopicCached(int topic, [MarshalAs(UnmanagedType.I4)] bool value);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicCached")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetTopicCached(int topic);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicExists")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetTopicExists(int handle);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(StringUtf8ReturnMarshaller<>), CountElementName = nameof(len))]
    internal static unsafe partial string GetTopicProperty(int topic, string name, out nuint len);

    public static unsafe string GetTopicProperty(int topic, string name)
    {
        return GetTopicProperty(topic, name, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_SetTopicProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetTopicProperty(int topic, string name, string value);

    [LibraryImport("ntcore", EntryPoint = "NT_DeleteTopicProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DeleteTopicProperty(int topic, string name);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(StringUtf8ReturnMarshaller<>), CountElementName = nameof(len))]
    internal static unsafe partial string GetTopicProperties(int topic, out nuint len);

    public static string GetTopicProperties(int topic)
    {
        return GetTopicProperties(topic, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_SetTopicProperties", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTopicProperties(int topic, string properties);

    [LibraryImport("ntcore", EntryPoint = "NT_Subscribe", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Subscribe(int topic, NetworkTableType type, string typeStr, in PubSubOptions options);

    [LibraryImport("ntcore", EntryPoint = "NT_Unsubscribe")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Unsubscribe(int sub);

    [LibraryImport("ntcore", EntryPoint = "NT_Publish", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Publish(int topic, NetworkTableType type, string typeStr, in PubSubOptions options);

    [LibraryImport("ntcore", EntryPoint = "NT_PublishEx", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int PublishEx(int topic, NetworkTableType type, string typeStr, string properties, in PubSubOptions options);

    [LibraryImport("ntcore", EntryPoint = "NT_Unpublish")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Unpublish(int pubentry);

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryEx", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int GetEntry(int topic, NetworkTableType type, string typeStr, in PubSubOptions options);

    [LibraryImport("ntcore", EntryPoint = "NT_ReleaseEntry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReleaseEntry(int entry);

    [LibraryImport("ntcore", EntryPoint = "NT_Release")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Release(int pubsubentry);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicFromHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetTopicFromHandle(int pubsubentry);

    [LibraryImport("ntcore", EntryPoint = "NT_Now")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long Now();

    [LibraryImport("ntcore", EntryPoint = "NT_SubscribeMultiple")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SubscribeMultiple(int inst, [MarshalUsing(typeof(NtStringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> prefixes, nuint prefixesLen, in PubSubOptions options);

    [LibraryImport("ntcore", EntryPoint = "NT_UnsubscribeMultiple")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UnsubscribeMultiple(int sub);
}
