
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(ISolenoid))]
    public unsafe static class Solenoid
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static ISolenoid lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

public static int CheckChannel(int channel)
{
return lowLevel.HAL_CheckSolenoidChannel(channel);
}

public static int CheckModule(int module)
{
return lowLevel.HAL_CheckSolenoidModule(module);
}

public static void ClearAllPCMStickyFaults(int module)
{
lowLevel.HAL_ClearAllPCMStickyFaults(module);
}

public static void FireOneShot(int solenoidPortHandle)
{
lowLevel.HAL_FireOneShot(solenoidPortHandle);
}

public static void FreePort(int solenoidPortHandle)
{
lowLevel.HAL_FreeSolenoidPort(solenoidPortHandle);
}

public static int GetAlls(int module)
{
return lowLevel.HAL_GetAllSolenoids(module);
}

public static int GetPCMBlackList(int module)
{
return lowLevel.HAL_GetPCMSolenoidBlackList(module);
}

public static int GetPCMVoltageFault(int module)
{
return lowLevel.HAL_GetPCMSolenoidVoltageFault(module);
}

public static int GetPCMVoltageStickyFault(int module)
{
return lowLevel.HAL_GetPCMSolenoidVoltageStickyFault(module);
}

public static int Get(int solenoidPortHandle)
{
return lowLevel.HAL_GetSolenoid(solenoidPortHandle);
}

public static int InitializePort(int portHandle)
{
return lowLevel.HAL_InitializeSolenoidPort(portHandle);
}

public static void SetAlls(int module, int state)
{
lowLevel.HAL_SetAllSolenoids(module, state);
}

public static void SetOneShotDuration(int solenoidPortHandle, int durMS)
{
lowLevel.HAL_SetOneShotDuration(solenoidPortHandle, durMS);
}

public static void Set(int solenoidPortHandle, int value)
{
lowLevel.HAL_SetSolenoid(solenoidPortHandle, value);
}

}
}
