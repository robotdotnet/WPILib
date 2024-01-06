using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using WPIUtil.Marshal;
using WPIUtil.Natives;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(StringWrapper), MarshalMode.ManagedToUnmanagedIn, typeof(StringUtf8WrapperMarshaller))]
public static unsafe class StringUtf8WrapperMarshaller
{
    public static ref byte GetPinnableReference(StringWrapper managed)
    {
        return ref managed.str[0];
    }

    public static byte* ConvertToUnmanaged(StringWrapper managed)
    {
        throw new InvalidOperationException("Have to have to satify the marshaller");
    }
}

[NativeMarshalling(typeof(StringUtf8WrapperMarshaller))]
public readonly ref struct StringWrapper
{
    public readonly byte[] str;

    public StringWrapper(string value)
    {
        str = Encoding.UTF8.GetBytes(value);
    }

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
    public static unsafe partial int GetEntry(int inst, StringWrapper name, nuint nameLen);

    // Cannot build source generator due to not being able to get the len for a string source generator
    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(StringUtf8ReturnMarshaller<>), CountElementName = nameof(nameLen))]
    public static unsafe partial string GetEntryName(int entry, out nuint nameLen);

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
    public static partial void SetEntryFlags(int entry, uint flags);

    [LibraryImport("ntcore", EntryPoint = "NT_GetEntryFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetEntryFlags(int entry);

    [LibraryImport("ntcore", EntryPoint = "NT_ReadQueueValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    public static unsafe partial NetworkTableValue[] ReadQueueValue(int subentry, out nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopics")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(TopicArrayMarshaller<int, int>), CountElementName = "count")]
    public static unsafe partial int[] GetTopics(int inst, byte* prefix, nuint prefixLen, uint types, out nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicsStr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(TopicArrayMarshaller<int, int>), CountElementName = "count")]
    public static unsafe partial int[] GetTopics(int inst, byte* prefix, nuint prefixLen, [MarshalUsing(typeof(Utf8StringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> types, nuint typesLen, out nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicInfos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    public static unsafe partial TopicInfo[] GetTopicInfos(int inst, byte* prefix, nuint prefixLen, uint types, out nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicInfosStr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = nameof(count))]
    public static unsafe partial TopicInfo[] GetTopicInfos(int inst, byte* prefix, nuint prefixLen, byte** types, nuint typesLen, out nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static unsafe partial bool GetTopicInfo(int topic, out TopicInfo info);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int GetTopic(int inst, byte* name, nuint nameLen);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* GetTopicName(int topic, out nuint nameLen);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NetworkTableType GetTopicType(int topic);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicTypeString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* GetTopicTypeString(int topic, out nuint typeLen);

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
    public static unsafe partial byte* GetTopicProperty(int topic, string name, out nuint len);

    [LibraryImport("ntcore", EntryPoint = "NT_SetTopicProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool SetTopicProperty(int topic, string name, string value);

    [LibraryImport("ntcore", EntryPoint = "NT_DeleteTopicProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DeleteTopicProperty(int topic, string name);

    [LibraryImport("ntcore", EntryPoint = "NT_GetTopicProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* GetTopicProperties(int topic, out nuint len);

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
