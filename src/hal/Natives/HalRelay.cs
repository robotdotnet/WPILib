using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalRelay
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CheckRelayChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int CheckRelayChannel(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeRelayPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeRelayPort(HalRelayHandle relayPortHandle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetRelay")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetRelayRefShim(HalRelayHandle relayPortHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeRelayPort", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalRelayHandle InitializeRelayPortRefShim(HalPortHandle portHandle, int fwd, string allocationLocation, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetRelay")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetRelayRefShim(HalRelayHandle relayPortHandle, int on, ref HalStatus status);


}
