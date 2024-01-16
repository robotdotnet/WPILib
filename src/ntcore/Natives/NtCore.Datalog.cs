using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetworkTables.Handles;
using WPIUtil.Natives;

namespace NetworkTables.Natives;

public static unsafe partial class NtCore
{
    [LibraryImport("ntcore", EntryPoint = "NT_StartEntryDataLog", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtDataLogger StartEntryDataLog(NtInst inst, OpaqueDataLog* log, string prefix, string logPrefix);

    [LibraryImport("ntcore", EntryPoint = "NT_StopEntryDataLog")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StopEntryDataLog(NtDataLogger logger);

    [LibraryImport("ntcore", EntryPoint = "NT_StartConnectionDataLog", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NtConnectionDataLogger StartConnectionDataLog(NtInst inst, OpaqueDataLog* log, string name);

    [LibraryImport("ntcore", EntryPoint = "NT_StopEntryDataLog")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StopConnectionDataLog(NtConnectionDataLogger logger);
}
