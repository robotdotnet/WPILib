using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALPorts
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALPorts.HAL_GetNumAccumulators = (Base.HALPorts.HAL_GetNumAccumulatorsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumAccumulators"), typeof(Base.HALPorts.HAL_GetNumAccumulatorsDelegate));

Base.HALPorts.HAL_GetNumAnalogTriggers = (Base.HALPorts.HAL_GetNumAnalogTriggersDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumAnalogTriggers"), typeof(Base.HALPorts.HAL_GetNumAnalogTriggersDelegate));

Base.HALPorts.HAL_GetNumAnalogInputs = (Base.HALPorts.HAL_GetNumAnalogInputsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumAnalogInputs"), typeof(Base.HALPorts.HAL_GetNumAnalogInputsDelegate));

Base.HALPorts.HAL_GetNumAnalogOutputs = (Base.HALPorts.HAL_GetNumAnalogOutputsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumAnalogOutputs"), typeof(Base.HALPorts.HAL_GetNumAnalogOutputsDelegate));

Base.HALPorts.HAL_GetNumCounters = (Base.HALPorts.HAL_GetNumCountersDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumCounters"), typeof(Base.HALPorts.HAL_GetNumCountersDelegate));

Base.HALPorts.HAL_GetNumDigitalHeaders = (Base.HALPorts.HAL_GetNumDigitalHeadersDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumDigitalHeaders"), typeof(Base.HALPorts.HAL_GetNumDigitalHeadersDelegate));

Base.HALPorts.HAL_GetNumPWMHeaders = (Base.HALPorts.HAL_GetNumPWMHeadersDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumPWMHeaders"), typeof(Base.HALPorts.HAL_GetNumPWMHeadersDelegate));

Base.HALPorts.HAL_GetNumDigitalPins = (Base.HALPorts.HAL_GetNumDigitalPinsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumDigitalPins"), typeof(Base.HALPorts.HAL_GetNumDigitalPinsDelegate));

Base.HALPorts.HAL_GetNumPWMPins = (Base.HALPorts.HAL_GetNumPWMPinsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumPWMPins"), typeof(Base.HALPorts.HAL_GetNumPWMPinsDelegate));

Base.HALPorts.HAL_GetNumDigitalPWMOutputs = (Base.HALPorts.HAL_GetNumDigitalPWMOutputsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumDigitalPWMOutputs"), typeof(Base.HALPorts.HAL_GetNumDigitalPWMOutputsDelegate));

Base.HALPorts.HAL_GetNumEncoders = (Base.HALPorts.HAL_GetNumEncodersDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumEncoders"), typeof(Base.HALPorts.HAL_GetNumEncodersDelegate));

Base.HALPorts.HAL_GetNumInterrupts = (Base.HALPorts.HAL_GetNumInterruptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumInterrupts"), typeof(Base.HALPorts.HAL_GetNumInterruptsDelegate));

Base.HALPorts.HAL_GetNumRelayPins = (Base.HALPorts.HAL_GetNumRelayPinsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumRelayPins"), typeof(Base.HALPorts.HAL_GetNumRelayPinsDelegate));

Base.HALPorts.HAL_GetNumRelayHeaders = (Base.HALPorts.HAL_GetNumRelayHeadersDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumRelayHeaders"), typeof(Base.HALPorts.HAL_GetNumRelayHeadersDelegate));

Base.HALPorts.HAL_GetNumPCMModules = (Base.HALPorts.HAL_GetNumPCMModulesDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumPCMModules"), typeof(Base.HALPorts.HAL_GetNumPCMModulesDelegate));

Base.HALPorts.HAL_GetNumSolenoidPins = (Base.HALPorts.HAL_GetNumSolenoidPinsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumSolenoidPins"), typeof(Base.HALPorts.HAL_GetNumSolenoidPinsDelegate));

Base.HALPorts.HAL_GetNumPDPModules = (Base.HALPorts.HAL_GetNumPDPModulesDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumPDPModules"), typeof(Base.HALPorts.HAL_GetNumPDPModulesDelegate));

Base.HALPorts.HAL_GetNumPDPChannels = (Base.HALPorts.HAL_GetNumPDPChannelsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumPDPChannels"), typeof(Base.HALPorts.HAL_GetNumPDPChannelsDelegate));

Base.HALPorts.HAL_GetNumCanTalons = (Base.HALPorts.HAL_GetNumCanTalonsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetNumCanTalons"), typeof(Base.HALPorts.HAL_GetNumCanTalonsDelegate));
}
}
}

