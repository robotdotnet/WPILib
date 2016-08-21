using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALDIO
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALDIO.HAL_InitializeDIOPort = (Base.HALDIO.HAL_InitializeDIOPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializeDIOPort"), typeof(Base.HALDIO.HAL_InitializeDIOPortDelegate));

Base.HALDIO.HAL_FreeDIOPort = (Base.HALDIO.HAL_FreeDIOPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_FreeDIOPort"), typeof(Base.HALDIO.HAL_FreeDIOPortDelegate));

Base.HALDIO.HAL_AllocateDigitalPWM = (Base.HALDIO.HAL_AllocateDigitalPWMDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_AllocateDigitalPWM"), typeof(Base.HALDIO.HAL_AllocateDigitalPWMDelegate));

Base.HALDIO.HAL_FreeDigitalPWM = (Base.HALDIO.HAL_FreeDigitalPWMDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_FreeDigitalPWM"), typeof(Base.HALDIO.HAL_FreeDigitalPWMDelegate));

Base.HALDIO.HAL_SetDigitalPWMRate = (Base.HALDIO.HAL_SetDigitalPWMRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetDigitalPWMRate"), typeof(Base.HALDIO.HAL_SetDigitalPWMRateDelegate));

Base.HALDIO.HAL_SetDigitalPWMDutyCycle = (Base.HALDIO.HAL_SetDigitalPWMDutyCycleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetDigitalPWMDutyCycle"), typeof(Base.HALDIO.HAL_SetDigitalPWMDutyCycleDelegate));

Base.HALDIO.HAL_SetDigitalPWMOutputChannel = (Base.HALDIO.HAL_SetDigitalPWMOutputChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetDigitalPWMOutputChannel"), typeof(Base.HALDIO.HAL_SetDigitalPWMOutputChannelDelegate));

Base.HALDIO.HAL_SetDIO = (Base.HALDIO.HAL_SetDIODelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetDIO"), typeof(Base.HALDIO.HAL_SetDIODelegate));

Base.HALDIO.HAL_GetDIO = (Base.HALDIO.HAL_GetDIODelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetDIO"), typeof(Base.HALDIO.HAL_GetDIODelegate));

Base.HALDIO.HAL_GetDIODirection = (Base.HALDIO.HAL_GetDIODirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetDIODirection"), typeof(Base.HALDIO.HAL_GetDIODirectionDelegate));

Base.HALDIO.HAL_Pulse = (Base.HALDIO.HAL_PulseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_Pulse"), typeof(Base.HALDIO.HAL_PulseDelegate));

Base.HALDIO.HAL_IsPulsing = (Base.HALDIO.HAL_IsPulsingDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_IsPulsing"), typeof(Base.HALDIO.HAL_IsPulsingDelegate));

Base.HALDIO.HAL_IsAnyPulsing = (Base.HALDIO.HAL_IsAnyPulsingDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_IsAnyPulsing"), typeof(Base.HALDIO.HAL_IsAnyPulsingDelegate));

Base.HALDIO.HAL_SetFilterSelect = (Base.HALDIO.HAL_SetFilterSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetFilterSelect"), typeof(Base.HALDIO.HAL_SetFilterSelectDelegate));

Base.HALDIO.HAL_GetFilterSelect = (Base.HALDIO.HAL_GetFilterSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetFilterSelect"), typeof(Base.HALDIO.HAL_GetFilterSelectDelegate));

Base.HALDIO.HAL_SetFilterPeriod = (Base.HALDIO.HAL_SetFilterPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetFilterPeriod"), typeof(Base.HALDIO.HAL_SetFilterPeriodDelegate));

Base.HALDIO.HAL_GetFilterPeriod = (Base.HALDIO.HAL_GetFilterPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetFilterPeriod"), typeof(Base.HALDIO.HAL_GetFilterPeriodDelegate));
}
}
}

