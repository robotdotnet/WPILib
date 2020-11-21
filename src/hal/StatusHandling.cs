using System;
using WPIUtil.ILGeneration;

namespace Hal
{
#pragma warning disable CA1801 // unused parameters
#pragma warning disable IDE0060 // Remove unused parameter
    public static class StatusHandling
    {
        public const int HalHandleErrorCode = -1098;
        public const int NoAvailableResourceCode = -1004;
        public const int ResourceIsAllocatedCode = -1029;
        public const int ResourceOutOfRangeCode = -1030;

        public static void StatusCheck(int status)
        {
            if (status < 0)
            {
                if (status == HalHandleErrorCode)
                {
                    throw new HalHandleException(status);
                }
                throw new UncleanStatusException(status);
            }
            else if (status > 0)
            {
                var message = HALLowLevel.GetErrorMessage(status);
                var stackTrace = Environment.StackTrace;
                DriverStationLowLevel.SendError(true, status, false, message.AsSpan(), "".AsSpan(), stackTrace.AsSpan(), true);
            }
        }

        public static void StatusCheckForce(int status)
        {
            if (status != 0)
            {
                if (status == HalHandleErrorCode)
                {
                    throw new HalHandleException(status);
                }
                throw new UncleanStatusException(status);
            }
        }


        private static void ThrowRangeCheck(int status, int requested, int min, int max)
        {
            if (status == 0) return;
            if (status == NoAvailableResourceCode || status == ResourceIsAllocatedCode || status == ResourceOutOfRangeCode)
            {
                throw new AllocationException(status, requested, min, max);
            }
            else if (status == HalHandleErrorCode)
            {
                throw new HalHandleException(status);
            }
            throw new UncleanStatusException(status);
        }

        private static int GetPortHandleChannel(int handle)
        {
            return handle & 0xFF;
        }

        public static void AddressableLEDStatusCheck(int status, int portHandle)
        {
            ThrowRangeCheck(status, GetPortHandleChannel(portHandle), 0, PortsLowLevel.GetNumAddressableLEDs());
        }

        public static void AccumulatorStatusCheck(int status, int portHandle)
        {
            ThrowRangeCheck(status, GetPortHandleChannel(portHandle), 0, PortsLowLevel.GetNumAccumulators());
        }
        public static void AnalogGyroStatusCheck(int status, int portHandle)
        {
            StatusCheckForce(status);
        }

        public static void AnalogInputStatusCheck(int status, int portHandle)
        {
            ThrowRangeCheck(status, GetPortHandleChannel(portHandle), 0, PortsLowLevel.GetNumAnalogInputs());
        }

        public static void AnalogOutputStatusCheck(int status, int portHandle)
        {
            ThrowRangeCheck(status, GetPortHandleChannel(portHandle), 0, PortsLowLevel.GetNumAnalogOutputs());
        }

        public static void AnalogTriggerStatusCheck(int status, int handle)
        {
            StatusCheck(status);
        }

        public static void AnalogTriggerDutyCycleStatusCheck(int status, int handle)
        {
            StatusCheck(status);
        }

        public static void DIOStatusCheck(int status, int portHandle)
        {
            ThrowRangeCheck(status, GetPortHandleChannel(portHandle), 0, PortsLowLevel.GetNumDigitalChannels());
        }

        public static void PWMStatusCheck(int status, int portHandle)
        {
            ThrowRangeCheck(status, GetPortHandleChannel(portHandle), 0, PortsLowLevel.GetNumPWMChannels());
        }

        public static void CompressorStatusCheck(int status, int module)
        {
            ThrowRangeCheck(status, module, 0, PortsLowLevel.GetNumPCMModules());
        }

        public static void SolenoidStatusCheck(int status, int portHandle)
        {
            ThrowRangeCheck(status, GetPortHandleChannel(portHandle), 0, PortsLowLevel.GetNumSolenoidChannels());
        }

        public static void I2CStatusCheck(int status, I2CPort handle)
        {
            StatusCheckForce(status);
        }

        public static void PDPStatusCheck(int status, int module)
        {
            ThrowRangeCheck(status, module, 0, PortsLowLevel.GetNumPDPModules());
        }

        public static void RelayStatusCheck(int status, int portHandle)
        {
            ThrowRangeCheck(status, GetPortHandleChannel(portHandle), 0, PortsLowLevel.GetNumRelayChannels());
        }

        public static void SerialPortStatusCheck(int status, SerialPortLocation port)
        {
            StatusCheckForce(status);
        }

        public static void SPIStatusCheck(int status, SPIPort port)
        {
            StatusCheckForce(status);
        }
#pragma warning restore CA1801 // unused parameters
#pragma warning restore IDE0060 // Remove unused parameter
    }
}
