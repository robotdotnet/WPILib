using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetworkTables.Handles;

namespace NetworkTables.Natives;

public static partial class NtCore
{
    [LibraryImport("ntcore", EntryPoint = "NT_GetNetworkMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial NetworkMode GetNetworkMode(NtInst inst);
}
