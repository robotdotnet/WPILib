//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALPDP
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALPDP.InitializePDP = (global::HAL.HALPDP.InitializePDPDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializePDP"), typeof(global::HAL.HALPDP.InitializePDPDelegate));

            global::HAL.HALPDP.GetPDPTemperature = (global::HAL.HALPDP.GetPDPTemperatureDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPTemperature"), typeof(global::HAL.HALPDP.GetPDPTemperatureDelegate));

            global::HAL.HALPDP.GetPDPVoltage = (global::HAL.HALPDP.GetPDPVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPVoltage"), typeof(global::HAL.HALPDP.GetPDPVoltageDelegate));

            global::HAL.HALPDP.GetPDPChannelCurrent = (global::HAL.HALPDP.GetPDPChannelCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPChannelCurrent"), typeof(global::HAL.HALPDP.GetPDPChannelCurrentDelegate));

            global::HAL.HALPDP.GetPDPTotalCurrent = (global::HAL.HALPDP.GetPDPTotalCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPTotalCurrent"), typeof(global::HAL.HALPDP.GetPDPTotalCurrentDelegate));

            global::HAL.HALPDP.GetPDPTotalPower = (global::HAL.HALPDP.GetPDPTotalPowerDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPTotalPower"), typeof(global::HAL.HALPDP.GetPDPTotalPowerDelegate));

            global::HAL.HALPDP.GetPDPTotalEnergy = (global::HAL.HALPDP.GetPDPTotalEnergyDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPTotalEnergy"), typeof(global::HAL.HALPDP.GetPDPTotalEnergyDelegate));

            global::HAL.HALPDP.ResetPDPTotalEnergy = (global::HAL.HALPDP.ResetPDPTotalEnergyDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "resetPDPTotalEnergy"), typeof(global::HAL.HALPDP.ResetPDPTotalEnergyDelegate));

            global::HAL.HALPDP.ClearPDPStickyFaults = (global::HAL.HALPDP.ClearPDPStickyFaultsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "clearPDPStickyFaults"), typeof(global::HAL.HALPDP.ClearPDPStickyFaultsDelegate));

        }
    }
}
