using System.Runtime.InteropServices;
using NativeLibraryUtilities;

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

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_InitializeRelayPortDelegate(int port_handle, [MarshalAs(UnmanagedType.Bool)]bool fwd, ref int status);
        [NativeDelegate] public static HAL_InitializeRelayPortDelegate HAL_InitializeRelayPort;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_FreeRelayPortDelegate(int relay_port_handle);
        [NativeDelegate] public static HAL_FreeRelayPortDelegate HAL_FreeRelayPort;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_CheckRelayChannelDelegate(int pin);
        [NativeDelegate] public static HAL_CheckRelayChannelDelegate HAL_CheckRelayChannel;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetRelayDelegate(int relay_port_handle, [MarshalAs(UnmanagedType.Bool)]bool on, ref int status);
        [NativeDelegate] public static HAL_SetRelayDelegate HAL_SetRelay;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetRelayDelegate(int relay_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetRelayDelegate HAL_GetRelay;
    }
}

