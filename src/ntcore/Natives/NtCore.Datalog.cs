using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetworkTables.Handles;
using WPIUtil;
using WPIUtil.Natives;

namespace NetworkTables.Natives;

public static unsafe partial class NtCore
{
    [LibraryImport("ntcore", EntryPoint = "NT_StartEntryDataLog")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtDataLogger StartEntryDataLog(NtInst inst, OpaqueDataLog* log, WpiString prefix, WpiString logPrefix);

    [LibraryImport("ntcore", EntryPoint = "NT_StopEntryDataLog")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StopEntryDataLog(NtDataLogger logger);

    [LibraryImport("ntcore", EntryPoint = "NT_StartConnectionDataLog")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtConnectionDataLogger StartConnectionDataLog(NtInst inst, OpaqueDataLog* log, WpiString name);

    [LibraryImport("ntcore", EntryPoint = "NT_StopEntryDataLog")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StopConnectionDataLog(NtConnectionDataLogger logger);
}
