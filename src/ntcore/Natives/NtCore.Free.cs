using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetworkTables.Natives;

public static unsafe partial class NtCore
{
    [LibraryImport("ntcore", EntryPoint = "NT_DisposeValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DisposeValue(NtValue* ptr);

    [LibraryImport("ntcore", EntryPoint = "NT_DisposeString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DisposeString(NtString* ptr);

    [LibraryImport("ntcore", EntryPoint = "NT_DisposeValueArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DisposeValueArray(NtValue* ptr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_DisposeConnectionInfoArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DisposeConnectionInfoArray(NtConnectionInfo* ptr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_DisposeTopicInfoArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DisposeTopicInfoArray(NtTopicInfo* ptr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_DisposeTopicInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DisposeTopicInfo(NtTopicInfo* ptr);

    [LibraryImport("ntcore", EntryPoint = "NT_DisposeEventArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DisposeEventArray(NtEvent* ptr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_DisposeEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DisposeEvent(NtEvent* ptr);

    [LibraryImport("ntcore", EntryPoint = "NT_FreeCharArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeCharArray(byte* ptr);

    [LibraryImport("ntcore", EntryPoint = "NT_Meta_FreeTopicPublishers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MetaFreeTopicPublishers(void* ptr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_Meta_FreeTopicSubscribers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MetaFreeTopicSubscribers(void* ptr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_Meta_FreeClientPublishers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MetaFreeClientPublishers(void* ptr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_Meta_FreeClientSubscribers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MetaFreeClientSubscribers(void* ptr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_Meta_FreeClients")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MetaFreeClients(void* ptr, nuint count);
}
