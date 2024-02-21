using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using NetworkTables.Handles;
using WPIUtil.Marshal;

namespace NetworkTables.Natives;

public static unsafe partial class NtCore
{
    [LibraryImport("ntcore", EntryPoint = "NT_ReadListenerQueue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ManagedFreeArrayMarshaller<,>), CountElementName = nameof(len))]
    internal static partial NetworkTableEvent[] ReadListenerQueue(NtListenerPoller poller, out nuint len);

    public static NetworkTableEvent[] ReadListenerQueue(NtListenerPoller poller)
    {
        return ReadListenerQueue(poller, out var _);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_CreateListenerPoller")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtListenerPoller CreateListenerPoller(NtInst inst);

    [LibraryImport("ntcore", EntryPoint = "NT_DestroyListenerPoller")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyListenerPoller(NtListenerPoller poller);

    [LibraryImport("ntcore", EntryPoint = "NT_AddPolledListenerMultiple")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial NtListener AddListener(NtListenerPoller poller, [MarshalUsing(typeof(WpiStringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> prefixes, nuint prefixesLen, EventFlags flags);

    public static NtListener AddListener(NtListenerPoller poller, ReadOnlySpan<string> prefixes, EventFlags flags)
    {
        return AddListener(poller, prefixes, (nuint)prefixes.Length, flags);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_AddPolledListener")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtListener AddListener(NtListenerPoller poller, NtTopic handle, EventFlags flags);

    [LibraryImport("ntcore", EntryPoint = "NT_AddPolledListener")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial NtListener AddListener(NtListenerPoller poller, int handle, EventFlags flags);

    public static NtListener AddListener<T>(NtListenerPoller poller, T handle, EventFlags flags) where T : struct, INtEntryHandle
    {
        return AddListener(poller, handle.Handle, flags);
    }

    [LibraryImport("ntcore", EntryPoint = "NT_AddPolledListener")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtListener AddListener(NtListenerPoller poller, NtMultiSubscriber handle, EventFlags flags);

    [LibraryImport("ntcore", EntryPoint = "NT_AddPolledListener")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtListener AddListener(NtListenerPoller poller, NtInst handle, EventFlags flags);

    [LibraryImport("ntcore", EntryPoint = "NT_AddPolledLogger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtListener AddLogger(NtListenerPoller poller, uint minLevel, uint maxLevel);

    [LibraryImport("ntcore", EntryPoint = "NT_RemoveListener")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RemoveListener(NtListener listener);
}
