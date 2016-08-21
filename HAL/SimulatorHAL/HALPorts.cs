using System;
using System.Runtime.InteropServices;
using HAL.Base;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALPorts
    {
        internal const int kNumAccumulators = 8;
        internal const int kNumAnalogTriggers = 8;
        internal const int kNumAnalogInputs = 8;
        internal const int kNumAnalogOutputs = 2;
        internal const int kNumCounters = 8;
        internal const int kNumDigitalHeaders = 10;
        internal const int kNumPWMHeaders = 10;
        internal const int kNumDigitalPins = 26;
        internal const int kNumPWMPins = 20;
        internal const int kNumDigitalPWMOutputs = 6;
        internal const int kNumEncoders = 8;
        internal const int kNumInterrupts = 8;
        internal const int kNumRelayPins = 8;
        internal const int kNumRelayHeaders = kNumRelayPins / 2;
        internal const int kNumPCMModules = 63;
        internal const int kNumSolenoidPins = 8;
        internal const int kNumPDPModules = 63;
        internal const int kNumPDPChannels = 16;
        internal const int kNumCanTalons = 63;


        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALPorts.HAL_GetNumAccumulators = HAL_GetNumAccumulators;
            Base.HALPorts.HAL_GetNumAnalogTriggers = HAL_GetNumAnalogTriggers;
            Base.HALPorts.HAL_GetNumAnalogInputs = HAL_GetNumAnalogInputs;
            Base.HALPorts.HAL_GetNumAnalogOutputs = HAL_GetNumAnalogOutputs;
            Base.HALPorts.HAL_GetNumCounters = HAL_GetNumCounters;
            Base.HALPorts.HAL_GetNumDigitalHeaders = HAL_GetNumDigitalHeaders;
            Base.HALPorts.HAL_GetNumPWMHeaders = HAL_GetNumPWMHeaders;
            Base.HALPorts.HAL_GetNumDigitalPins = HAL_GetNumDigitalPins;
            Base.HALPorts.HAL_GetNumPWMPins = HAL_GetNumPWMPins;
            Base.HALPorts.HAL_GetNumDigitalPWMOutputs = HAL_GetNumDigitalPWMOutputs;
            Base.HALPorts.HAL_GetNumEncoders = HAL_GetNumEncoders;
            Base.HALPorts.HAL_GetNumInterrupts = HAL_GetNumInterrupts;
            Base.HALPorts.HAL_GetNumRelayPins = HAL_GetNumRelayPins;
            Base.HALPorts.HAL_GetNumRelayHeaders = HAL_GetNumRelayHeaders;
            Base.HALPorts.HAL_GetNumPCMModules = HAL_GetNumPCMModules;
            Base.HALPorts.HAL_GetNumSolenoidPins = HAL_GetNumSolenoidPins;
            Base.HALPorts.HAL_GetNumPDPModules = HAL_GetNumPDPModules;
            Base.HALPorts.HAL_GetNumPDPChannels = HAL_GetNumPDPChannels;
            Base.HALPorts.HAL_GetNumCanTalons = HAL_GetNumCanTalons;
        }

        private static int HAL_GetNumAccumulators() { return kNumAccumulators; }
        private static int HAL_GetNumAnalogTriggers() { return kNumAnalogTriggers; }
        private static int HAL_GetNumAnalogInputs() { return kNumAnalogInputs; }
        private static int HAL_GetNumAnalogOutputs() { return kNumAnalogOutputs; }
        private static int HAL_GetNumCounters() { return kNumCounters; }
        private static int HAL_GetNumDigitalHeaders() { return kNumDigitalHeaders; }
        private static int HAL_GetNumPWMHeaders() { return kNumPWMHeaders; }
        private static int HAL_GetNumDigitalPins() { return kNumDigitalPins; }
        private static int HAL_GetNumPWMPins() { return kNumPWMPins; }
        private static int HAL_GetNumDigitalPWMOutputs() { return kNumDigitalPWMOutputs; }
        private static int HAL_GetNumEncoders() { return kNumEncoders; }
        private static int HAL_GetNumInterrupts() { return kNumInterrupts; }
        private static int HAL_GetNumRelayPins() { return kNumRelayPins; }
        private static int HAL_GetNumRelayHeaders() { return kNumRelayHeaders; }
        private static int HAL_GetNumPCMModules() { return kNumPCMModules; }
        private static int HAL_GetNumSolenoidPins() { return kNumSolenoidPins; }
        private static int HAL_GetNumPDPModules() { return kNumPDPModules; }
        private static int HAL_GetNumPDPChannels() { return kNumPDPChannels; }
        private static int HAL_GetNumCanTalons() { return kNumCanTalons; }
    }
}

