using System.Runtime.InteropServices;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALRelay
    {
        static HALRelay()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALRelay>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        public delegate int HAL_InitializeRelayPortDelegate(int port_handle, [MarshalAs(UnmanagedType.I4)]bool fwd, ref int status);
        [NativeDelegate] public static HAL_InitializeRelayPortDelegate HAL_InitializeRelayPort;

        public delegate void HAL_FreeRelayPortDelegate(int relay_port_handle);
        [NativeDelegate] public static HAL_FreeRelayPortDelegate HAL_FreeRelayPort;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_CheckRelayChannelDelegate(int pin);
        [NativeDelegate] public static HAL_CheckRelayChannelDelegate HAL_CheckRelayChannel;

        public delegate void HAL_SetRelayDelegate(int relay_port_handle, [MarshalAs(UnmanagedType.I4)]bool on, ref int status);
        [NativeDelegate] public static HAL_SetRelayDelegate HAL_SetRelay;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetRelayDelegate(int relay_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetRelayDelegate HAL_GetRelay;
    }
}

