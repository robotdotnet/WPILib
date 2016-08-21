using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALAnalogInput
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALAnalogInput.HAL_InitializeAnalogInputPort = (Base.HALAnalogInput.HAL_InitializeAnalogInputPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializeAnalogInputPort"), typeof(Base.HALAnalogInput.HAL_InitializeAnalogInputPortDelegate));

Base.HALAnalogInput.HAL_FreeAnalogInputPort = (Base.HALAnalogInput.HAL_FreeAnalogInputPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_FreeAnalogInputPort"), typeof(Base.HALAnalogInput.HAL_FreeAnalogInputPortDelegate));

Base.HALAnalogInput.HAL_CheckAnalogModule = (Base.HALAnalogInput.HAL_CheckAnalogModuleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_CheckAnalogModule"), typeof(Base.HALAnalogInput.HAL_CheckAnalogModuleDelegate));

Base.HALAnalogInput.HAL_CheckAnalogInputChannel = (Base.HALAnalogInput.HAL_CheckAnalogInputChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_CheckAnalogInputChannel"), typeof(Base.HALAnalogInput.HAL_CheckAnalogInputChannelDelegate));

Base.HALAnalogInput.HAL_SetAnalogSampleRate = (Base.HALAnalogInput.HAL_SetAnalogSampleRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetAnalogSampleRate"), typeof(Base.HALAnalogInput.HAL_SetAnalogSampleRateDelegate));

Base.HALAnalogInput.HAL_GetAnalogSampleRate = (Base.HALAnalogInput.HAL_GetAnalogSampleRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogSampleRate"), typeof(Base.HALAnalogInput.HAL_GetAnalogSampleRateDelegate));

Base.HALAnalogInput.HAL_SetAnalogAverageBits = (Base.HALAnalogInput.HAL_SetAnalogAverageBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetAnalogAverageBits"), typeof(Base.HALAnalogInput.HAL_SetAnalogAverageBitsDelegate));

Base.HALAnalogInput.HAL_GetAnalogAverageBits = (Base.HALAnalogInput.HAL_GetAnalogAverageBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogAverageBits"), typeof(Base.HALAnalogInput.HAL_GetAnalogAverageBitsDelegate));

Base.HALAnalogInput.HAL_SetAnalogOversampleBits = (Base.HALAnalogInput.HAL_SetAnalogOversampleBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetAnalogOversampleBits"), typeof(Base.HALAnalogInput.HAL_SetAnalogOversampleBitsDelegate));

Base.HALAnalogInput.HAL_GetAnalogOversampleBits = (Base.HALAnalogInput.HAL_GetAnalogOversampleBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogOversampleBits"), typeof(Base.HALAnalogInput.HAL_GetAnalogOversampleBitsDelegate));

Base.HALAnalogInput.HAL_GetAnalogValue = (Base.HALAnalogInput.HAL_GetAnalogValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogValue"), typeof(Base.HALAnalogInput.HAL_GetAnalogValueDelegate));

Base.HALAnalogInput.HAL_GetAnalogAverageValue = (Base.HALAnalogInput.HAL_GetAnalogAverageValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogAverageValue"), typeof(Base.HALAnalogInput.HAL_GetAnalogAverageValueDelegate));

Base.HALAnalogInput.HAL_GetAnalogVoltsToValue = (Base.HALAnalogInput.HAL_GetAnalogVoltsToValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogVoltsToValue"), typeof(Base.HALAnalogInput.HAL_GetAnalogVoltsToValueDelegate));

Base.HALAnalogInput.HAL_GetAnalogVoltage = (Base.HALAnalogInput.HAL_GetAnalogVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogVoltage"), typeof(Base.HALAnalogInput.HAL_GetAnalogVoltageDelegate));

Base.HALAnalogInput.HAL_GetAnalogAverageVoltage = (Base.HALAnalogInput.HAL_GetAnalogAverageVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogAverageVoltage"), typeof(Base.HALAnalogInput.HAL_GetAnalogAverageVoltageDelegate));

Base.HALAnalogInput.HAL_GetAnalogLSBWeight = (Base.HALAnalogInput.HAL_GetAnalogLSBWeightDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogLSBWeight"), typeof(Base.HALAnalogInput.HAL_GetAnalogLSBWeightDelegate));

Base.HALAnalogInput.HAL_GetAnalogOffset = (Base.HALAnalogInput.HAL_GetAnalogOffsetDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetAnalogOffset"), typeof(Base.HALAnalogInput.HAL_GetAnalogOffsetDelegate));
}
}
}

