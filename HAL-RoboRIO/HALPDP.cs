//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALPDP
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALPDP.InitializePDP = (HAL_Base.HALPDP.InitializePDPDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializePDP"), typeof(HAL_Base.HALPDP.InitializePDPDelegate));

            HAL_Base.HALPDP.GetPDPTemperature = (HAL_Base.HALPDP.GetPDPTemperatureDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPTemperature"), typeof(HAL_Base.HALPDP.GetPDPTemperatureDelegate));

            HAL_Base.HALPDP.GetPDPVoltage = (HAL_Base.HALPDP.GetPDPVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPVoltage"), typeof(HAL_Base.HALPDP.GetPDPVoltageDelegate));

            HAL_Base.HALPDP.GetPDPChannelCurrent = (HAL_Base.HALPDP.GetPDPChannelCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPChannelCurrent"), typeof(HAL_Base.HALPDP.GetPDPChannelCurrentDelegate));

            HAL_Base.HALPDP.GetPDPTotalCurrent = (HAL_Base.HALPDP.GetPDPTotalCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPTotalCurrent"), typeof(HAL_Base.HALPDP.GetPDPTotalCurrentDelegate));

            HAL_Base.HALPDP.GetPDPTotalPower = (HAL_Base.HALPDP.GetPDPTotalPowerDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPTotalPower"), typeof(HAL_Base.HALPDP.GetPDPTotalPowerDelegate));

            HAL_Base.HALPDP.GetPDPTotalEnergy = (HAL_Base.HALPDP.GetPDPTotalEnergyDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPTotalEnergy"), typeof(HAL_Base.HALPDP.GetPDPTotalEnergyDelegate));

            HAL_Base.HALPDP.ResetPDPTotalEnergy = (HAL_Base.HALPDP.ResetPDPTotalEnergyDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "resetPDPTotalEnergy"), typeof(HAL_Base.HALPDP.ResetPDPTotalEnergyDelegate));

            HAL_Base.HALPDP.ClearPDPStickyFaults = (HAL_Base.HALPDP.ClearPDPStickyFaultsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "clearPDPStickyFaults"), typeof(HAL_Base.HALPDP.ClearPDPStickyFaultsDelegate));

        }
    }
}
