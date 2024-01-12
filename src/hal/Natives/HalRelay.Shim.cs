using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalRelay
{
    public static int GetRelay(HalRelayHandle relayPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetRelayRefShim(relayPortHandle, ref status);
    }
    public static HalRelayHandle InitializeRelayPort(HalPortHandle portHandle, int fwd, string allocationLocation, out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeRelayPortRefShim(portHandle, fwd, allocationLocation, ref status);
    }
    public static void SetRelay(HalRelayHandle relayPortHandle, int on, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetRelayRefShim(relayPortHandle, on, ref status);
    }
}
