using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALPorts
    {
        static HALPorts()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALPorts>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        public delegate int HAL_GetNumAccumulatorsDelegate();
        [NativeDelegate] public static HAL_GetNumAccumulatorsDelegate HAL_GetNumAccumulators;

        public delegate int HAL_GetNumAnalogTriggersDelegate();
        [NativeDelegate] public static HAL_GetNumAnalogTriggersDelegate HAL_GetNumAnalogTriggers;

        public delegate int HAL_GetNumAnalogInputsDelegate();
        [NativeDelegate] public static HAL_GetNumAnalogInputsDelegate HAL_GetNumAnalogInputs;

        public delegate int HAL_GetNumAnalogOutputsDelegate();
        [NativeDelegate] public static HAL_GetNumAnalogOutputsDelegate HAL_GetNumAnalogOutputs;

        public delegate int HAL_GetNumCountersDelegate();
        [NativeDelegate] public static HAL_GetNumCountersDelegate HAL_GetNumCounters;

        public delegate int HAL_GetNumDigitalHeadersDelegate();
        [NativeDelegate] public static HAL_GetNumDigitalHeadersDelegate HAL_GetNumDigitalHeaders;

        public delegate int HAL_GetNumPWMHeadersDelegate();
        [NativeDelegate] public static HAL_GetNumPWMHeadersDelegate HAL_GetNumPWMHeaders;

        public delegate int HAL_GetNumDigitalPinsDelegate();
        [NativeDelegate] public static HAL_GetNumDigitalPinsDelegate HAL_GetNumDigitalPins;

        public delegate int HAL_GetNumPWMPinsDelegate();
        [NativeDelegate] public static HAL_GetNumPWMPinsDelegate HAL_GetNumPWMPins;

        public delegate int HAL_GetNumDigitalPWMOutputsDelegate();
        [NativeDelegate] public static HAL_GetNumDigitalPWMOutputsDelegate HAL_GetNumDigitalPWMOutputs;

        public delegate int HAL_GetNumEncodersDelegate();
        [NativeDelegate] public static HAL_GetNumEncodersDelegate HAL_GetNumEncoders;

        public delegate int HAL_GetNumInterruptsDelegate();
        [NativeDelegate] public static HAL_GetNumInterruptsDelegate HAL_GetNumInterrupts;

        public delegate int HAL_GetNumRelayPinsDelegate();
        [NativeDelegate] public static HAL_GetNumRelayPinsDelegate HAL_GetNumRelayPins;

        public delegate int HAL_GetNumRelayHeadersDelegate();
        [NativeDelegate] public static HAL_GetNumRelayHeadersDelegate HAL_GetNumRelayHeaders;

        public delegate int HAL_GetNumPCMModulesDelegate();
        [NativeDelegate] public static HAL_GetNumPCMModulesDelegate HAL_GetNumPCMModules;

        public delegate int HAL_GetNumSolenoidPinsDelegate();
        [NativeDelegate] public static HAL_GetNumSolenoidPinsDelegate HAL_GetNumSolenoidPins;

        public delegate int HAL_GetNumPDPModulesDelegate();
        [NativeDelegate] public static HAL_GetNumPDPModulesDelegate HAL_GetNumPDPModules;

        public delegate int HAL_GetNumPDPChannelsDelegate();
        [NativeDelegate] public static HAL_GetNumPDPChannelsDelegate HAL_GetNumPDPChannels;

        public delegate int HAL_GetNumCanTalonsDelegate();
        [NativeDelegate] public static HAL_GetNumCanTalonsDelegate HAL_GetNumCanTalons;
    }
}

