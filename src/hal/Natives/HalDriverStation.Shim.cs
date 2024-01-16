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
