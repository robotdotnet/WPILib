
using Hal.Natives;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static unsafe class RelayLowLevel
    {
#pragma warning disable CS0649 // Field is never assigned to
        internal static RelayLowLevelNative lowLevel = null!;
#pragma warning restore CS0649 // Field is never assigned to

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
