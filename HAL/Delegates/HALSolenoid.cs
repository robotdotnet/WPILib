using System.Runtime.InteropServices;
 using NativeLibraryUtilities;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALSolenoid
    {
        static HALSolenoid()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALSolenoid>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_InitializeSolenoidPortDelegate(int port_handle, ref int status);
        [NativeDelegate] public static HAL_InitializeSolenoidPortDelegate HAL_InitializeSolenoidPort;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_FreeSolenoidPortDelegate(int solenoid_port_handle);
        [NativeDelegate] public static HAL_FreeSolenoidPortDelegate HAL_FreeSolenoidPort;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_CheckSolenoidModuleDelegate(int module);
        [NativeDelegate] public static HAL_CheckSolenoidModuleDelegate HAL_CheckSolenoidModule;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_CheckSolenoidChannelDelegate(int channel);
        [NativeDelegate] public static HAL_CheckSolenoidChannelDelegate HAL_CheckSolenoidChannel;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetSolenoidDelegate(int solenoid_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetSolenoidDelegate HAL_GetSolenoid;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetAllSolenoidsDelegate(int module, ref int status);
        [NativeDelegate] public static HAL_GetAllSolenoidsDelegate HAL_GetAllSolenoids;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetSolenoidDelegate(int solenoid_port_handle, [MarshalAs(UnmanagedType.Bool)]bool value, ref int status);
        [NativeDelegate] public static HAL_SetSolenoidDelegate HAL_SetSolenoid;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetPCMSolenoidBlackListDelegate(int module, ref int status);
        [NativeDelegate] public static HAL_GetPCMSolenoidBlackListDelegate HAL_GetPCMSolenoidBlackList;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetPCMSolenoidVoltageStickyFaultDelegate(int module, ref int status);
        [NativeDelegate] public static HAL_GetPCMSolenoidVoltageStickyFaultDelegate HAL_GetPCMSolenoidVoltageStickyFault;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetPCMSolenoidVoltageFaultDelegate(int module, ref int status);
        [NativeDelegate] public static HAL_GetPCMSolenoidVoltageFaultDelegate HAL_GetPCMSolenoidVoltageFault;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_ClearAllPCMStickyFaultsDelegate(int module, ref int status);
        [NativeDelegate] public static HAL_ClearAllPCMStickyFaultsDelegate HAL_ClearAllPCMStickyFaults;
    }
}

