using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalAnalogGyro
{
    public static void CalibrateAnalogGyro(HalGyroHandle handle, out HalStatus status)
    {
        status = HalStatus.Ok;
        CalibrateAnalogGyroRefShim(handle, ref status);
    }
    public static double GetAnalogGyroAngle(HalGyroHandle handle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogGyroAngleRefShim(handle, ref status);
    }
    public static int GetAnalogGyroCenter(HalGyroHandle handle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogGyroCenterRefShim(handle, ref status);
    }
    public static double GetAnalogGyroOffset(HalGyroHandle handle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogGyroOffsetRefShim(handle, ref status);
    }
    public static double GetAnalogGyroRate(HalGyroHandle handle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogGyroRateRefShim(handle, ref status);
    }
    public static HalGyroHandle InitializeAnalogGyro(HalAnalogInputHandle handle, string allocationLocation, out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeAnalogGyroRefShim(handle, allocationLocation, ref status);
    }
    public static void ResetAnalogGyro(HalGyroHandle handle, out HalStatus status)
    {
        status = HalStatus.Ok;
        ResetAnalogGyroRefShim(handle, ref status);
    }
    public static void SetAnalogGyroDeadband(HalGyroHandle handle, double volts, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAnalogGyroDeadbandRefShim(handle, volts, ref status);
    }
    public static void SetAnalogGyroParameters(HalGyroHandle handle, double voltsPerDegreePerSecond, double offset, int center, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAnalogGyroParametersRefShim(handle, voltsPerDegreePerSecond, offset, center, ref status);
    }
    public static void SetAnalogGyroVoltsPerDegreePerSecond(HalGyroHandle handle, double voltsPerDegreePerSecond, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAnalogGyroVoltsPerDegreePerSecondRefShim(handle, voltsPerDegreePerSecond, ref status);
    }
    public static void SetupAnalogGyro(HalGyroHandle handle, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetupAnalogGyroRefShim(handle, ref status);
    }
}
