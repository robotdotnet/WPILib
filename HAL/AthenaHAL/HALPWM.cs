using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALPWM
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALPWM.HAL_InitializePWMPort = (Base.HALPWM.HAL_InitializePWMPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializePWMPort"), typeof(Base.HALPWM.HAL_InitializePWMPortDelegate));

Base.HALPWM.HAL_FreePWMPort = (Base.HALPWM.HAL_FreePWMPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_FreePWMPort"), typeof(Base.HALPWM.HAL_FreePWMPortDelegate));

Base.HALPWM.HAL_CheckPWMChannel = (Base.HALPWM.HAL_CheckPWMChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_CheckPWMChannel"), typeof(Base.HALPWM.HAL_CheckPWMChannelDelegate));

Base.HALPWM.HAL_SetPWMConfig = (Base.HALPWM.HAL_SetPWMConfigDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetPWMConfig"), typeof(Base.HALPWM.HAL_SetPWMConfigDelegate));

Base.HALPWM.HAL_SetPWMConfigRaw = (Base.HALPWM.HAL_SetPWMConfigRawDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetPWMConfigRaw"), typeof(Base.HALPWM.HAL_SetPWMConfigRawDelegate));

Base.HALPWM.HAL_GetPWMConfigRaw = (Base.HALPWM.HAL_GetPWMConfigRawDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetPWMConfigRaw"), typeof(Base.HALPWM.HAL_GetPWMConfigRawDelegate));

Base.HALPWM.HAL_SetPWMEliminateDeadband = (Base.HALPWM.HAL_SetPWMEliminateDeadbandDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetPWMEliminateDeadband"), typeof(Base.HALPWM.HAL_SetPWMEliminateDeadbandDelegate));

Base.HALPWM.HAL_GetPWMEliminateDeadband = (Base.HALPWM.HAL_GetPWMEliminateDeadbandDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetPWMEliminateDeadband"), typeof(Base.HALPWM.HAL_GetPWMEliminateDeadbandDelegate));

Base.HALPWM.HAL_SetPWMRaw = (Base.HALPWM.HAL_SetPWMRawDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetPWMRaw"), typeof(Base.HALPWM.HAL_SetPWMRawDelegate));

Base.HALPWM.HAL_SetPWMSpeed = (Base.HALPWM.HAL_SetPWMSpeedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetPWMSpeed"), typeof(Base.HALPWM.HAL_SetPWMSpeedDelegate));

Base.HALPWM.HAL_SetPWMPosition = (Base.HALPWM.HAL_SetPWMPositionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetPWMPosition"), typeof(Base.HALPWM.HAL_SetPWMPositionDelegate));

Base.HALPWM.HAL_SetPWMDisabled = (Base.HALPWM.HAL_SetPWMDisabledDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetPWMDisabled"), typeof(Base.HALPWM.HAL_SetPWMDisabledDelegate));

Base.HALPWM.HAL_GetPWMRaw = (Base.HALPWM.HAL_GetPWMRawDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetPWMRaw"), typeof(Base.HALPWM.HAL_GetPWMRawDelegate));

Base.HALPWM.HAL_GetPWMSpeed = (Base.HALPWM.HAL_GetPWMSpeedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetPWMSpeed"), typeof(Base.HALPWM.HAL_GetPWMSpeedDelegate));

Base.HALPWM.HAL_GetPWMPosition = (Base.HALPWM.HAL_GetPWMPositionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetPWMPosition"), typeof(Base.HALPWM.HAL_GetPWMPositionDelegate));

Base.HALPWM.HAL_LatchPWMZero = (Base.HALPWM.HAL_LatchPWMZeroDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_LatchPWMZero"), typeof(Base.HALPWM.HAL_LatchPWMZeroDelegate));

Base.HALPWM.HAL_SetPWMPeriodScale = (Base.HALPWM.HAL_SetPWMPeriodScaleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetPWMPeriodScale"), typeof(Base.HALPWM.HAL_SetPWMPeriodScaleDelegate));

Base.HALPWM.HAL_GetLoopTiming = (Base.HALPWM.HAL_GetLoopTimingDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetLoopTiming"), typeof(Base.HALPWM.HAL_GetLoopTimingDelegate));
}
}
}

