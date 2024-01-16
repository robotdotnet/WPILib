namespace WPIHal.Natives;

public static unsafe partial class HalPower
{
    public static int GetUserActive3V3(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetUserActive3V3RefShim(ref status);
    }
    public static int GetUserActive5V(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetUserActive5VRefShim(ref status);
    }
    public static int GetUserActive6V(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetUserActive6VRefShim(ref status);
    }
    public static double GetUserCurrent3V3(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetUserCurrent3V3RefShim(ref status);
    }
    public static double GetUserCurrent5V(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetUserCurrent5VRefShim(ref status);
    }
    public static double GetUserCurrent6V(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetUserCurrent6VRefShim(ref status);
    }
    public static int GetUserCurrentFaults3V3(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetUserCurrentFaults3V3RefShim(ref status);
    }
    public static int GetUserCurrentFaults5V(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetUserCurrentFaults5VRefShim(ref status);
    }
    public static int GetUserCurrentFaults6V(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetUserCurrentFaults6VRefShim(ref status);
    }
    public static double GetUserVoltage3V3(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetUserVoltage3V3RefShim(ref status);
    }
    public static double GetUserVoltage5V(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetUserVoltage5VRefShim(ref status);
    }
    public static double GetUserVoltage6V(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetUserVoltage6VRefShim(ref status);
    }
    public static double GetVinCurrent(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetVinCurrentRefShim(ref status);
    }
    public static double GetVinVoltage(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetVinVoltageRefShim(ref status);
    }
}
