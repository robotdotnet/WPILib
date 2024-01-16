using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalPWM
{
    public static void FreePWMPort(HalDigitalHandle pwmPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        FreePWMPortRefShim(pwmPortHandle, ref status);
    }
    public static ulong GetPWMCycleStartTime(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetPWMCycleStartTimeRefShim(ref status);
    }
    public static int GetPWMEliminateDeadband(HalDigitalHandle pwmPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetPWMEliminateDeadbandRefShim(pwmPortHandle, ref status);
    }
    public static int GetPWMLoopTiming(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetPWMLoopTimingRefShim(ref status);
    }
    public static double GetPWMPosition(HalDigitalHandle pwmPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetPWMPositionRefShim(pwmPortHandle, ref status);
    }
    public static double GetPWMSpeed(HalDigitalHandle pwmPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetPWMSpeedRefShim(pwmPortHandle, ref status);
    }
    public static HalDigitalHandle InitializePWMPort(HalPortHandle portHandle, string allocationLocation, out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializePWMPortRefShim(portHandle, allocationLocation, ref status);
    }
    public static void LatchPWMZero(HalDigitalHandle pwmPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        LatchPWMZeroRefShim(pwmPortHandle, ref status);
    }
    public static void SetPWMConfigMicroseconds(HalDigitalHandle pwmPortHandle, int maxPwm, int deadbandMaxPwm, int centerPwm, int deadbandMinPwm, int minPwm, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetPWMConfigMicrosecondsRefShim(pwmPortHandle, maxPwm, deadbandMaxPwm, centerPwm, deadbandMinPwm, minPwm, ref status);
    }
    public static void SetPWMDisabled(HalDigitalHandle pwmPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetPWMDisabledRefShim(pwmPortHandle, ref status);
    }
    public static void SetPWMEliminateDeadband(HalDigitalHandle pwmPortHandle, int eliminateDeadband, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetPWMEliminateDeadbandRefShim(pwmPortHandle, eliminateDeadband, ref status);
    }
    public static void SetPWMPeriodScale(HalDigitalHandle pwmPortHandle, int squelchMask, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetPWMPeriodScaleRefShim(pwmPortHandle, squelchMask, ref status);
    }
    public static void SetPWMPosition(HalDigitalHandle pwmPortHandle, double position, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetPWMPositionRefShim(pwmPortHandle, position, ref status);
    }
    public static void SetPWMSpeed(HalDigitalHandle pwmPortHandle, double speed, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetPWMSpeedRefShim(pwmPortHandle, speed, ref status);
    }
}
