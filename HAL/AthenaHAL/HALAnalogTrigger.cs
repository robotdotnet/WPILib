using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALAnalogTrigger
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALAnalogTrigger.HAL_InitializeAnalogTrigger = (Base.HALAnalogTrigger.HAL_InitializeAnalogTriggerDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializeAnalogTrigger"), typeof(Base.HALAnalogTrigger.HAL_InitializeAnalogTriggerDelegate));

Base.HALAnalogTrigger.HAL_CleanAnalogTrigger = (Base.HALAnalogTrigger.HAL_CleanAnalogTriggerDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_CleanAnalogTrigger"), typeof(Base.HALAnalogTrigger.HAL_CleanAnalogTriggerDelegate));

Base.HALAnalogTrigger.HAL_SetAnalogTriggerLimitsRaw = (Base.HALAnalogTrigger.HAL_SetAnalogTriggerLimitsRawDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetAnalogTriggerLimitsRaw"), typeof(Base.HALAnalogTrigger.HAL_SetAnalogTriggerLimitsRawDelegate));

Base.HALAnalogTrigger.HAL_SetAnalogTriggerLimitsVoltage = (Base.HALAnalogTrigger.HAL_SetAnalogTriggerLimitsVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetAnalogTriggerLimitsVoltage"), typeof(Base.HALAnalogTrigger.HAL_SetAnalogTriggerLimitsVoltageDelegate));

Base.HALAnalogTrigger.HAL_SetAnalogTriggerAveraged = (Base.HALAnalogTrigger.HAL_SetAnalogTriggerAveragedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetAnalogTriggerAveraged"), typeof(Base.HALAnalogTrigger.HAL_SetAnalogTriggerAveragedDelegate));

Base.HALAnalogTrigger.HAL_SetAnalogTriggerFiltered = (Base.HALAnalogTrigger.HAL_SetAnalogTriggerFilteredDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetAnalogTriggerFiltered"), typeof(Base.HALAnalogTrigger.HAL_SetAnalogTriggerFilteredDelegate));

Base.HALAnalogTrigger.HAL_GetAnalogTriggerInWindow = (Base.HALAnalogTrigger.HAL_GetAnalogTriggerInWindowDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogTriggerInWindow"), typeof(Base.HALAnalogTrigger.HAL_GetAnalogTriggerInWindowDelegate));

Base.HALAnalogTrigger.HAL_GetAnalogTriggerTriggerState = (Base.HALAnalogTrigger.HAL_GetAnalogTriggerTriggerStateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogTriggerTriggerState"), typeof(Base.HALAnalogTrigger.HAL_GetAnalogTriggerTriggerStateDelegate));

Base.HALAnalogTrigger.HAL_GetAnalogTriggerOutput = (Base.HALAnalogTrigger.HAL_GetAnalogTriggerOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogTriggerOutput"), typeof(Base.HALAnalogTrigger.HAL_GetAnalogTriggerOutputDelegate));
}
}
}

