using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetworkTables.Natives;

public static unsafe partial class NtCore
{
    [LibraryImport("ntcore", EntryPoint = "NT_DisposeValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DisposeValue(NetworkTableValueMarshaller.NativeNetworkTableValue* nativePtr);

    [LibraryImport("ntcore", EntryPoint = "NT_DisposeValueArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DisposeValueArray(NetworkTableValueMarshaller.NativeNetworkTableValue* nativePtr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_DisposeConnectionInfoArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DisposeConnectionInfoArray(ConnectionInfoMarshaller.NativeConnectionInfo* nativePtr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_DisposeTopicInfoArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DisposeTopicInfoArray(TopicInfoMarshaller.NativeTopicInfo* nativePtr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_DisposeTopicInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DisposeTopicInfo(TopicInfoMarshaller.NativeTopicInfo* nativePtr);

    [LibraryImport("ntcore", EntryPoint = "NT_DisposeEventArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DisposeEventArray(NetworkTableEventMarshaller.NativeNetworkTableEvent* nativePtr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_DisposeEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DisposeEvent(NetworkTableEventMarshaller.NativeNetworkTableEvent* nativePtr);

    [LibraryImport("ntcore", EntryPoint = "NT_AllocateCharArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* AllocateCharArray(nuint len);

    [LibraryImport("ntcore", EntryPoint = "NT_FreeCharArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeCharArray(byte* nativePtr);

    [LibraryImport("ntcore", EntryPoint = "NT_Meta_FreeTopicPublishers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MetaFreeTopicPublishers(void* nativePtr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_Meta_FreeTopicSubscribers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MetaFreeTopicSubscribers(void* nativePtr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_Meta_FreeClientPublishers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MetaFreeClientPublishers(void* nativePtr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_Meta_FreeClientSubscribers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MetaFreeClientSubscribers(void* nativePtr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_Meta_FreeClients")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MetaFreeClients(void* nativePtr, nuint count);
}
