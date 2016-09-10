using System.Runtime.InteropServices;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALPDP
    {
        static HALPDP()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALPDP>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        public delegate void HAL_InitializePDPDelegate(int module, ref int status);
        [NativeDelegate] public static HAL_InitializePDPDelegate HAL_InitializePDP;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_CheckPDPModuleDelegate(int module);
        [NativeDelegate] public static HAL_CheckPDPModuleDelegate HAL_CheckPDPModule;

        public delegate double HAL_GetPDPTemperatureDelegate(int module, ref int status);
        [NativeDelegate] public static HAL_GetPDPTemperatureDelegate HAL_GetPDPTemperature;

        public delegate double HAL_GetPDPVoltageDelegate(int module, ref int status);
        [NativeDelegate] public static HAL_GetPDPVoltageDelegate HAL_GetPDPVoltage;

        public delegate double HAL_GetPDPChannelCurrentDelegate(int module, int channel, ref int status);
        [NativeDelegate] public static HAL_GetPDPChannelCurrentDelegate HAL_GetPDPChannelCurrent;

        public delegate double HAL_GetPDPTotalCurrentDelegate(int module, ref int status);
        [NativeDelegate] public static HAL_GetPDPTotalCurrentDelegate HAL_GetPDPTotalCurrent;

        public delegate double HAL_GetPDPTotalPowerDelegate(int module, ref int status);
        [NativeDelegate] public static HAL_GetPDPTotalPowerDelegate HAL_GetPDPTotalPower;

        public delegate double HAL_GetPDPTotalEnergyDelegate(int module, ref int status);
        [NativeDelegate] public static HAL_GetPDPTotalEnergyDelegate HAL_GetPDPTotalEnergy;

        public delegate void HAL_ResetPDPTotalEnergyDelegate(int module, ref int status);
        [NativeDelegate] public static HAL_ResetPDPTotalEnergyDelegate HAL_ResetPDPTotalEnergy;

        public delegate void HAL_ClearPDPStickyFaultsDelegate(int module, ref int status);
        [NativeDelegate] public static HAL_ClearPDPStickyFaultsDelegate HAL_ClearPDPStickyFaults;
    }
}

