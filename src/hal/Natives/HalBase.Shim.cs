using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalBase
{
    public static ulong ExpandFPGATime(uint unexpandedLower, out HalStatus status)
    {
        status = HalStatus.Ok;
        return ExpandFPGATimeRefShim(unexpandedLower, ref status);
    }
    public static int GetBrownedOut(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetBrownedOutRefShim(ref status);
    }
    public static int GetFPGAButton(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetFPGAButtonRefShim(ref status);
    }
    public static long GetFPGARevision(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetFPGARevisionRefShim(ref status);
    }
    public static int GetFPGAVersion(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetFPGAVersionRefShim(ref status);
    }
    public static int GetSystemActive(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetSystemActiveRefShim(ref status);
    }
}
