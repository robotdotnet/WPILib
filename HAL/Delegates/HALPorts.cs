using System;
using System.Runtime.InteropServices;
// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALPorts
    {
        static HALPorts()
        {
            HAL.Initialize();
        }

        public delegate int HAL_GetNumAccumulatorsDelegate();
        public static HAL_GetNumAccumulatorsDelegate HAL_GetNumAccumulators;

        public delegate int HAL_GetNumAnalogTriggersDelegate();
        public static HAL_GetNumAnalogTriggersDelegate HAL_GetNumAnalogTriggers;

        public delegate int HAL_GetNumAnalogInputsDelegate();
        public static HAL_GetNumAnalogInputsDelegate HAL_GetNumAnalogInputs;

        public delegate int HAL_GetNumAnalogOutputsDelegate();
        public static HAL_GetNumAnalogOutputsDelegate HAL_GetNumAnalogOutputs;

        public delegate int HAL_GetNumCountersDelegate();
        public static HAL_GetNumCountersDelegate HAL_GetNumCounters;

        public delegate int HAL_GetNumDigitalHeadersDelegate();
        public static HAL_GetNumDigitalHeadersDelegate HAL_GetNumDigitalHeaders;

        public delegate int HAL_GetNumPWMHeadersDelegate();
        public static HAL_GetNumPWMHeadersDelegate HAL_GetNumPWMHeaders;

        public delegate int HAL_GetNumDigitalPinsDelegate();
        public static HAL_GetNumDigitalPinsDelegate HAL_GetNumDigitalPins;

        public delegate int HAL_GetNumPWMPinsDelegate();
        public static HAL_GetNumPWMPinsDelegate HAL_GetNumPWMPins;

        public delegate int HAL_GetNumDigitalPWMOutputsDelegate();
        public static HAL_GetNumDigitalPWMOutputsDelegate HAL_GetNumDigitalPWMOutputs;

        public delegate int HAL_GetNumEncodersDelegate();
        public static HAL_GetNumEncodersDelegate HAL_GetNumEncoders;

        public delegate int HAL_GetNumInterruptsDelegate();
        public static HAL_GetNumInterruptsDelegate HAL_GetNumInterrupts;

        public delegate int HAL_GetNumRelayPinsDelegate();
        public static HAL_GetNumRelayPinsDelegate HAL_GetNumRelayPins;

        public delegate int HAL_GetNumRelayHeadersDelegate();
        public static HAL_GetNumRelayHeadersDelegate HAL_GetNumRelayHeaders;

        public delegate int HAL_GetNumPCMModulesDelegate();
        public static HAL_GetNumPCMModulesDelegate HAL_GetNumPCMModules;

        public delegate int HAL_GetNumSolenoidPinsDelegate();
        public static HAL_GetNumSolenoidPinsDelegate HAL_GetNumSolenoidPins;

        public delegate int HAL_GetNumPDPModulesDelegate();
        public static HAL_GetNumPDPModulesDelegate HAL_GetNumPDPModules;

        public delegate int HAL_GetNumPDPChannelsDelegate();
        public static HAL_GetNumPDPChannelsDelegate HAL_GetNumPDPChannels;

        public delegate int HAL_GetNumCanTalonsDelegate();
        public static HAL_GetNumCanTalonsDelegate HAL_GetNumCanTalons;
    }
}

