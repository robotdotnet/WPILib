using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Marshal;

namespace NetworkTables.Natives;

public static unsafe partial class NtCore
{
    [LibraryImport("ntcore", EntryPoint = "NT_ReadListenerQueue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = nameof(len))]
    internal static partial NetworkTableEvent[] ReadListenerQueue(int poller, out nuint len);

    public static NetworkTableEvent[] ReadListenerQueue(int poller)
    {
        return ReadListenerQueue(poller, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_CreateListenerPoller")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int CreateListenerPoller(int inst);

    [LibraryImport("ntcore", EntryPoint = "NT_DestroyListenerPoller")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyListenerPoller(int poller);

    [LibraryImport("ntcore", EntryPoint = "NT_AddPolledListenerMultiple")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int AddListener(int poller, [MarshalUsing(typeof(NtStringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> prefixes, nuint prefixesLen, EventFlags flags);

    public static int AddListener(int poller, ReadOnlySpan<string> prefixes, EventFlags flags)
    {
        return AddListener(poller, prefixes, (nuint)prefixes.Length, flags);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_AddPolledListener")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int AddListener(int poller, int handle, EventFlags flags);

    [LibraryImport("ntcore", EntryPoint = "NT_AddPolledLogger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int AddLogger(int poller, uint minLevel, uint maxLevel);

    [LibraryImport("ntcore", EntryPoint = "NT_RemoveListener")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RemoveListener(int listener);
}
