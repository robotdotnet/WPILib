
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

public static int CheckSolenoidChannel(int channel)
{
return lowLevel.HAL_CheckSolenoidChannel(channel);
}

public static int CheckSolenoidModule(int module)
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

public static void FreeSolenoidPort(int solenoidPortHandle)
{
lowLevel.HAL_FreeSolenoidPort(solenoidPortHandle);
}

public static int GetAllSolenoids(int module)
{
return lowLevel.HAL_GetAllSolenoids(module);
}

public static int GetPCMSolenoidBlackList(int module)
{
return lowLevel.HAL_GetPCMSolenoidBlackList(module);
}

public static int GetPCMSolenoidVoltageFault(int module)
{
return lowLevel.HAL_GetPCMSolenoidVoltageFault(module);
}

public static int GetPCMSolenoidVoltageStickyFault(int module)
{
return lowLevel.HAL_GetPCMSolenoidVoltageStickyFault(module);
}

public static int GetSolenoid(int solenoidPortHandle)
{
return lowLevel.HAL_GetSolenoid(solenoidPortHandle);
}

public static int InitializeSolenoidPort(int portHandle)
{
return lowLevel.HAL_InitializeSolenoidPort(portHandle);
}

public static void SetAllSolenoids(int module, int state)
{
lowLevel.HAL_SetAllSolenoids(module, state);
}

public static void SetOneShotDuration(int solenoidPortHandle, int durMS)
{
lowLevel.HAL_SetOneShotDuration(solenoidPortHandle, durMS);
}

public static void SetSolenoid(int solenoidPortHandle, int value)
{
lowLevel.HAL_SetSolenoid(solenoidPortHandle, value);
}

}
}
