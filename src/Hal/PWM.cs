
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IPWM))]
    public unsafe static class PWM
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IPWM lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

public static int CheckPWMChannel(int channel)
{
return lowLevel.HAL_CheckPWMChannel(channel);
}

public static void FreePWMPort(int pwmPortHandle)
{
lowLevel.HAL_FreePWMPort(pwmPortHandle);
}

public static ulong GetPWMCycleStartTime()
{
return lowLevel.HAL_GetPWMCycleStartTime();
}

public static int GetPWMEliminateDeadband(int pwmPortHandle)
{
return lowLevel.HAL_GetPWMEliminateDeadband(pwmPortHandle);
}

public static int GetPWMLoopTiming()
{
return lowLevel.HAL_GetPWMLoopTiming();
}

public static double GetPWMPosition(int pwmPortHandle)
{
return lowLevel.HAL_GetPWMPosition(pwmPortHandle);
}

public static int GetPWMRaw(int pwmPortHandle)
{
return lowLevel.HAL_GetPWMRaw(pwmPortHandle);
}

public static double GetPWMSpeed(int pwmPortHandle)
{
return lowLevel.HAL_GetPWMSpeed(pwmPortHandle);
}

public static int InitializePWMPort(int portHandle)
{
return lowLevel.HAL_InitializePWMPort(portHandle);
}

public static void LatchPWMZero(int pwmPortHandle)
{
lowLevel.HAL_LatchPWMZero(pwmPortHandle);
}

public static void SetPWMConfig(int pwmPortHandle, double maxPwm, double deadbandMaxPwm, double centerPwm, double deadbandMinPwm, double minPwm)
{
lowLevel.HAL_SetPWMConfig(pwmPortHandle, maxPwm, deadbandMaxPwm, centerPwm, deadbandMinPwm, minPwm);
}

public static void SetPWMConfigRaw(int pwmPortHandle, int maxPwm, int deadbandMaxPwm, int centerPwm, int deadbandMinPwm, int minPwm)
{
lowLevel.HAL_SetPWMConfigRaw(pwmPortHandle, maxPwm, deadbandMaxPwm, centerPwm, deadbandMinPwm, minPwm);
}

public static void SetPWMDisabled(int pwmPortHandle)
{
lowLevel.HAL_SetPWMDisabled(pwmPortHandle);
}

public static void SetPWMEliminateDeadband(int pwmPortHandle, int eliminateDeadband)
{
lowLevel.HAL_SetPWMEliminateDeadband(pwmPortHandle, eliminateDeadband);
}

public static void SetPWMPeriodScale(int pwmPortHandle, int squelchMask)
{
lowLevel.HAL_SetPWMPeriodScale(pwmPortHandle, squelchMask);
}

public static void SetPWMPosition(int pwmPortHandle, double position)
{
lowLevel.HAL_SetPWMPosition(pwmPortHandle, position);
}

public static void SetPWMRaw(int pwmPortHandle, int value)
{
lowLevel.HAL_SetPWMRaw(pwmPortHandle, value);
}

public static void SetPWMSpeed(int pwmPortHandle, double speed)
{
lowLevel.HAL_SetPWMSpeed(pwmPortHandle, speed);
}

}
}
