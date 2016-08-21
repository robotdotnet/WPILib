using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALPDP
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALPDP.HAL_InitializePDP = (Base.HALPDP.HAL_InitializePDPDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializePDP"), typeof(Base.HALPDP.HAL_InitializePDPDelegate));

Base.HALPDP.HAL_CheckPDPModule = (Base.HALPDP.HAL_CheckPDPModuleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_CheckPDPModule"), typeof(Base.HALPDP.HAL_CheckPDPModuleDelegate));

Base.HALPDP.HAL_GetPDPTemperature = (Base.HALPDP.HAL_GetPDPTemperatureDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetPDPTemperature"), typeof(Base.HALPDP.HAL_GetPDPTemperatureDelegate));

Base.HALPDP.HAL_GetPDPVoltage = (Base.HALPDP.HAL_GetPDPVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetPDPVoltage"), typeof(Base.HALPDP.HAL_GetPDPVoltageDelegate));

Base.HALPDP.HAL_GetPDPChannelCurrent = (Base.HALPDP.HAL_GetPDPChannelCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetPDPChannelCurrent"), typeof(Base.HALPDP.HAL_GetPDPChannelCurrentDelegate));

Base.HALPDP.HAL_GetPDPTotalCurrent = (Base.HALPDP.HAL_GetPDPTotalCurrentDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetPDPTotalCurrent"), typeof(Base.HALPDP.HAL_GetPDPTotalCurrentDelegate));

Base.HALPDP.HAL_GetPDPTotalPower = (Base.HALPDP.HAL_GetPDPTotalPowerDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetPDPTotalPower"), typeof(Base.HALPDP.HAL_GetPDPTotalPowerDelegate));

Base.HALPDP.HAL_GetPDPTotalEnergy = (Base.HALPDP.HAL_GetPDPTotalEnergyDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetPDPTotalEnergy"), typeof(Base.HALPDP.HAL_GetPDPTotalEnergyDelegate));

Base.HALPDP.HAL_ResetPDPTotalEnergy = (Base.HALPDP.HAL_ResetPDPTotalEnergyDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ResetPDPTotalEnergy"), typeof(Base.HALPDP.HAL_ResetPDPTotalEnergyDelegate));

Base.HALPDP.HAL_ClearPDPStickyFaults = (Base.HALPDP.HAL_ClearPDPStickyFaultsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ClearPDPStickyFaults"), typeof(Base.HALPDP.HAL_ClearPDPStickyFaultsDelegate));
}
}
}

