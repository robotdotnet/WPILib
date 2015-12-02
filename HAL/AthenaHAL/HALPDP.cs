//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;

namespace HAL.Athena
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALPDP
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALPDP.InitializePDP = (Base.HALPDP.InitializePDPDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializePDP"), typeof(Base.HALPDP.InitializePDPDelegate));

            Base.HALPDP.GetPDPTemperature = (Base.HALPDP.GetPDPTemperatureDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPTemperature"), typeof(Base.HALPDP.GetPDPTemperatureDelegate));

            Base.HALPDP.GetPDPVoltage = (Base.HALPDP.GetPDPVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPVoltage"), typeof(Base.HALPDP.GetPDPVoltageDelegate));

            Base.HALPDP.GetPDPChannelCurrent = (Base.HALPDP.GetPDPChannelCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPChannelCurrent"), typeof(Base.HALPDP.GetPDPChannelCurrentDelegate));

            Base.HALPDP.GetPDPTotalCurrent = (Base.HALPDP.GetPDPTotalCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPTotalCurrent"), typeof(Base.HALPDP.GetPDPTotalCurrentDelegate));

            Base.HALPDP.GetPDPTotalPower = (Base.HALPDP.GetPDPTotalPowerDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPTotalPower"), typeof(Base.HALPDP.GetPDPTotalPowerDelegate));

            Base.HALPDP.GetPDPTotalEnergy = (Base.HALPDP.GetPDPTotalEnergyDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPDPTotalEnergy"), typeof(Base.HALPDP.GetPDPTotalEnergyDelegate));

            Base.HALPDP.ResetPDPTotalEnergy = (Base.HALPDP.ResetPDPTotalEnergyDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "resetPDPTotalEnergy"), typeof(Base.HALPDP.ResetPDPTotalEnergyDelegate));

            Base.HALPDP.ClearPDPStickyFaults = (Base.HALPDP.ClearPDPStickyFaultsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "clearPDPStickyFaults"), typeof(Base.HALPDP.ClearPDPStickyFaultsDelegate));

        }
    }
}
