using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalDriverStation
{
    public static AllianceStationID GetAllianceStation(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAllianceStationRefShim(ref status);
    }
    public static double GetMatchTime(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetMatchTimeRefShim(ref status);
    }
}
