﻿using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace Hal.Natives;

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
    public static partial int GetRelay(HalRelayHandle relayPortHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeRelayPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalRelayHandle InitializeRelayPort(HalPortHandle portHandle, int fwd, string allocationLocation, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetRelay")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRelay(HalRelayHandle relayPortHandle, int on, out HalStatus status);


}
