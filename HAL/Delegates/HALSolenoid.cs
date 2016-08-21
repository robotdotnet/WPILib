using System;
using System.Runtime.InteropServices;
// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALSolenoid
    {
        static HALSolenoid()
        {
            HAL.Initialize();
        }

        public delegate int HAL_InitializeSolenoidPortDelegate(int port_handle, ref int status);
        public static HAL_InitializeSolenoidPortDelegate HAL_InitializeSolenoidPort;

        public delegate void HAL_FreeSolenoidPortDelegate(int solenoid_port_handle);
        public static HAL_FreeSolenoidPortDelegate HAL_FreeSolenoidPort;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_CheckSolenoidModuleDelegate(int module);
        public static HAL_CheckSolenoidModuleDelegate HAL_CheckSolenoidModule;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_CheckSolenoidPinDelegate(int pin);
        public static HAL_CheckSolenoidPinDelegate HAL_CheckSolenoidPin;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetSolenoidDelegate(int solenoid_port_handle, ref int status);
        public static HAL_GetSolenoidDelegate HAL_GetSolenoid;

        public delegate int HAL_GetAllSolenoidsDelegate(int module, ref int status);
        public static HAL_GetAllSolenoidsDelegate HAL_GetAllSolenoids;

        public delegate void HAL_SetSolenoidDelegate(int solenoid_port_handle, [MarshalAs(UnmanagedType.I4)]bool value, ref int status);
        public static HAL_SetSolenoidDelegate HAL_SetSolenoid;

        public delegate int HAL_GetPCMSolenoidBlackListDelegate(int module, ref int status);
        public static HAL_GetPCMSolenoidBlackListDelegate HAL_GetPCMSolenoidBlackList;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetPCMSolenoidVoltageStickyFaultDelegate(int module, ref int status);
        public static HAL_GetPCMSolenoidVoltageStickyFaultDelegate HAL_GetPCMSolenoidVoltageStickyFault;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetPCMSolenoidVoltageFaultDelegate(int module, ref int status);
        public static HAL_GetPCMSolenoidVoltageFaultDelegate HAL_GetPCMSolenoidVoltageFault;

        public delegate void HAL_ClearAllPCMStickyFaultsDelegate(int module, ref int status);
        public static HAL_ClearAllPCMStickyFaultsDelegate HAL_ClearAllPCMStickyFaults;
    }
}

