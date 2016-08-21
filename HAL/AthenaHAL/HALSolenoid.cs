using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALSolenoid
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALSolenoid.HAL_InitializeSolenoidPort = (Base.HALSolenoid.HAL_InitializeSolenoidPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializeSolenoidPort"), typeof(Base.HALSolenoid.HAL_InitializeSolenoidPortDelegate));

Base.HALSolenoid.HAL_FreeSolenoidPort = (Base.HALSolenoid.HAL_FreeSolenoidPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_FreeSolenoidPort"), typeof(Base.HALSolenoid.HAL_FreeSolenoidPortDelegate));

Base.HALSolenoid.HAL_CheckSolenoidModule = (Base.HALSolenoid.HAL_CheckSolenoidModuleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_CheckSolenoidModule"), typeof(Base.HALSolenoid.HAL_CheckSolenoidModuleDelegate));

Base.HALSolenoid.HAL_CheckSolenoidPin = (Base.HALSolenoid.HAL_CheckSolenoidPinDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_CheckSolenoidPin"), typeof(Base.HALSolenoid.HAL_CheckSolenoidPinDelegate));

Base.HALSolenoid.HAL_GetSolenoid = (Base.HALSolenoid.HAL_GetSolenoidDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetSolenoid"), typeof(Base.HALSolenoid.HAL_GetSolenoidDelegate));

Base.HALSolenoid.HAL_GetAllSolenoids = (Base.HALSolenoid.HAL_GetAllSolenoidsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAllSolenoids"), typeof(Base.HALSolenoid.HAL_GetAllSolenoidsDelegate));

Base.HALSolenoid.HAL_SetSolenoid = (Base.HALSolenoid.HAL_SetSolenoidDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetSolenoid"), typeof(Base.HALSolenoid.HAL_SetSolenoidDelegate));

Base.HALSolenoid.HAL_GetPCMSolenoidBlackList = (Base.HALSolenoid.HAL_GetPCMSolenoidBlackListDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetPCMSolenoidBlackList"), typeof(Base.HALSolenoid.HAL_GetPCMSolenoidBlackListDelegate));

Base.HALSolenoid.HAL_GetPCMSolenoidVoltageStickyFault = (Base.HALSolenoid.HAL_GetPCMSolenoidVoltageStickyFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetPCMSolenoidVoltageStickyFault"), typeof(Base.HALSolenoid.HAL_GetPCMSolenoidVoltageStickyFaultDelegate));

Base.HALSolenoid.HAL_GetPCMSolenoidVoltageFault = (Base.HALSolenoid.HAL_GetPCMSolenoidVoltageFaultDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetPCMSolenoidVoltageFault"), typeof(Base.HALSolenoid.HAL_GetPCMSolenoidVoltageFaultDelegate));

Base.HALSolenoid.HAL_ClearAllPCMStickyFaults = (Base.HALSolenoid.HAL_ClearAllPCMStickyFaultsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ClearAllPCMStickyFaults"), typeof(Base.HALSolenoid.HAL_ClearAllPCMStickyFaultsDelegate));
}
}
}

