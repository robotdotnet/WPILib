
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IAnalogGyro))]
    public unsafe static class AnalogGyro
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IAnalogGyro lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

public static void CalibrateAnalogGyro(int handle)
{
lowLevel.HAL_CalibrateAnalogGyro(handle);
}

public static void FreeAnalogGyro(int handle)
{
lowLevel.HAL_FreeAnalogGyro(handle);
}

public static double GetAnalogGyroAngle(int handle)
{
return lowLevel.HAL_GetAnalogGyroAngle(handle);
}

public static int GetAnalogGyroCenter(int handle)
{
return lowLevel.HAL_GetAnalogGyroCenter(handle);
}

public static double GetAnalogGyroOffset(int handle)
{
return lowLevel.HAL_GetAnalogGyroOffset(handle);
}

public static double GetAnalogGyroRate(int handle)
{
return lowLevel.HAL_GetAnalogGyroRate(handle);
}

public static int InitializeAnalogGyro(int handle)
{
return lowLevel.HAL_InitializeAnalogGyro(handle);
}

public static void ResetAnalogGyro(int handle)
{
lowLevel.HAL_ResetAnalogGyro(handle);
}

public static void SetAnalogGyroDeadband(int handle, double volts)
{
lowLevel.HAL_SetAnalogGyroDeadband(handle, volts);
}

public static void SetAnalogGyroParameters(int handle, double voltsPerDegreePerSecond, double offset, int center)
{
lowLevel.HAL_SetAnalogGyroParameters(handle, voltsPerDegreePerSecond, offset, center);
}

public static void SetAnalogGyroVoltsPerDegreePerSecond(int handle, double voltsPerDegreePerSecond)
{
lowLevel.HAL_SetAnalogGyroVoltsPerDegreePerSecond(handle, voltsPerDegreePerSecond);
}

public static void SetupAnalogGyro(int handle)
{
lowLevel.HAL_SetupAnalogGyro(handle);
}

}
}
