using System;
using System.Runtime.InteropServices;
// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALRelay
    {
        static HALRelay()
        {
            HAL.Initialize();
        }

        public delegate int HAL_InitializeRelayPortDelegate(int port_handle, [MarshalAs(UnmanagedType.I4)]bool fwd, ref int status);
        public static HAL_InitializeRelayPortDelegate HAL_InitializeRelayPort;

        public delegate void HAL_FreeRelayPortDelegate(int relay_port_handle);
        public static HAL_FreeRelayPortDelegate HAL_FreeRelayPort;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_CheckRelayChannelDelegate(int pin);
        public static HAL_CheckRelayChannelDelegate HAL_CheckRelayChannel;

        public delegate void HAL_SetRelayDelegate(int relay_port_handle, [MarshalAs(UnmanagedType.I4)]bool on, ref int status);
        public static HAL_SetRelayDelegate HAL_SetRelay;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetRelayDelegate(int relay_port_handle, ref int status);
        public static HAL_GetRelayDelegate HAL_GetRelay;
    }
}

