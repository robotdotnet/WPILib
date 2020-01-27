
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IRelay))]
    public unsafe static class Relay
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IRelay lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

public static int CheckChannel(int channel)
{
return lowLevel.HAL_CheckRelayChannel(channel);
}

public static void FreePort(int relayPortHandle)
{
lowLevel.HAL_FreeRelayPort(relayPortHandle);
}

public static int Get(int relayPortHandle)
{
return lowLevel.HAL_GetRelay(relayPortHandle);
}

public static int InitializePort(int portHandle, int fwd)
{
return lowLevel.HAL_InitializeRelayPort(portHandle, fwd);
}

public static void Set(int relayPortHandle, int on)
{
lowLevel.HAL_SetRelay(relayPortHandle, on);
}

}
}
