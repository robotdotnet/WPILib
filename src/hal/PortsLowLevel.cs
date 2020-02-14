
using Hal.Natives;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IPorts))]
    public static unsafe class PortsLowLevel
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IPorts lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public static int GetNumAccumulators()
        {
            return lowLevel.HAL_GetNumAccumulators();
        }

        public static int GetNumAddressableLEDs()
        {
            return lowLevel.HAL_GetNumAddressableLEDs();
        }

        public static int GetNumAnalogInputs()
        {
            return lowLevel.HAL_GetNumAnalogInputs();
        }

        public static int GetNumAnalogOutputs()
        {
            return lowLevel.HAL_GetNumAnalogOutputs();
        }

        public static int GetNumAnalogTriggers()
        {
            return lowLevel.HAL_GetNumAnalogTriggers();
        }

        public static int GetNumCounters()
        {
            return lowLevel.HAL_GetNumCounters();
        }

        public static int GetNumDigitalChannels()
        {
            return lowLevel.HAL_GetNumDigitalChannels();
        }

        public static int GetNumDigitalHeaders()
        {
            return lowLevel.HAL_GetNumDigitalHeaders();
        }

        public static int GetNumDigitalPWMOutputs()
        {
            return lowLevel.HAL_GetNumDigitalPWMOutputs();
        }

        public static int GetNumDutyCycles()
        {
            return lowLevel.HAL_GetNumDutyCycles();
        }

        public static int GetNumEncoders()
        {
            return lowLevel.HAL_GetNumEncoders();
        }

        public static int GetNumInterrupts()
        {
            return lowLevel.HAL_GetNumInterrupts();
        }

        public static int GetNumPCMModules()
        {
            return lowLevel.HAL_GetNumPCMModules();
        }

        public static int GetNumPDPChannels()
        {
            return lowLevel.HAL_GetNumPDPChannels();
        }

        public static int GetNumPDPModules()
        {
            return lowLevel.HAL_GetNumPDPModules();
        }

        public static int GetNumPWMChannels()
        {
            return lowLevel.HAL_GetNumPWMChannels();
        }

        public static int GetNumPWMHeaders()
        {
            return lowLevel.HAL_GetNumPWMHeaders();
        }

        public static int GetNumRelayChannels()
        {
            return lowLevel.HAL_GetNumRelayChannels();
        }

        public static int GetNumRelayHeaders()
        {
            return lowLevel.HAL_GetNumRelayHeaders();
        }

        public static int GetNumSolenoidChannels()
        {
            return lowLevel.HAL_GetNumSolenoidChannels();
        }

    }
}
