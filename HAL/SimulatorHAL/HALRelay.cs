using System;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALRelay
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALRelay.HAL_InitializeRelayPort = HAL_InitializeRelayPort;
            Base.HALRelay.HAL_FreeRelayPort = HAL_FreeRelayPort;
            Base.HALRelay.HAL_CheckRelayChannel = HAL_CheckRelayChannel;
            Base.HALRelay.HAL_SetRelay = HAL_SetRelay;
            Base.HALRelay.HAL_GetRelay = HAL_GetRelay;
        }

        public static int HAL_InitializeRelayPort(int port_handle, bool fwd, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_FreeRelayPort(int relay_port_handle)
        {
        }

        public static bool HAL_CheckRelayChannel(int pin)
        {
            throw new NotImplementedException();
        }

        public static void HAL_SetRelay(int relay_port_handle, bool on, ref int status)
        {
        }

        public static bool HAL_GetRelay(int relay_port_handle, ref int status)
        {
            throw new NotImplementedException();
        }
    }
}

