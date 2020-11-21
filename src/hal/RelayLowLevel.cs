
using Hal.Natives;
using WPIUtil.ILGeneration;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static unsafe class RelayLowLevel
    {
        internal static RelayLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

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
