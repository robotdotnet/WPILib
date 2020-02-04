using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WPIUtil.ILGeneration;

namespace Hal
{
    public static class StatusHandling
    {
        public const int HalHandleErrorCode = -1098;
        public const int NoAvailableResourceCode = -1004;
        public const int ResourceIsAllocatedCode = -1029;
        public const int ResourceOutOfRangeCode = -1030;

        [StatusCheckFunction]
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
                var message = HalBase.GetErrorMessage(status);
                var stackTrace = Environment.StackTrace;
                DriverStation.SendError(true, status, false, message.AsSpan(), "".AsSpan(), stackTrace.AsSpan(), true);
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
            ThrowRangeCheck(status, GetPortHandleChannel(portHandle), 0, Ports.GetNumAddressableLEDs());
        }

        public static void AccumulatorStatusCheck(int status, int portHandle)
        {
            ThrowRangeCheck(status, GetPortHandleChannel(portHandle), 0, Ports.GetNumAccumulators());
        }

        public static void AnalogGyroStatusCheck(int status, int portHandle)
        {
            StatusCheckForce(status);
        }

        public static void AnalogInputStatusCheck(int status, int portHandle)
        {
            ThrowRangeCheck(status, GetPortHandleChannel(portHandle), 0, Ports.GetNumAnalogInputs());
        }

        public static void AnalogOutputStatusCheck(int status, int portHandle)
        {
            ThrowRangeCheck(status, GetPortHandleChannel(portHandle), 0, Ports.GetNumAnalogOutputs());
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
            ThrowRangeCheck(status, GetPortHandleChannel(portHandle), 0, Ports.GetNumDigitalChannels());
        }

        public static void PWMStatusCheck(int status, int portHandle)
        {
            ThrowRangeCheck(status, GetPortHandleChannel(portHandle), 0, Ports.GetNumPWMChannels());
        }

        public static void CompressorStatusCheck(int status, int module)
        {
            ThrowRangeCheck(status, module, 0, Ports.GetNumPCMModules());
        }

        public static void SolenoidStatusCheck(int status, int portHandle)
        {
            ThrowRangeCheck(status, GetPortHandleChannel(portHandle), 0, Ports.GetNumSolenoidChannels());
        }

        public static void I2CStatusCheck(int status, I2CPort handle)
        {
            StatusCheckForce(status);
        }

        public static void PDPStatusCheck(int status, int module)
        {
            ThrowRangeCheck(status, module, 0, Ports.GetNumPDPModules());
        }

        public static void RelayStatusCheck(int status, int portHandle)
        {
            ThrowRangeCheck(status, GetPortHandleChannel(portHandle), 0, Ports.GetNumRelayChannels());
        }

        public static void SerialPortStatusCheck(int status, SerialPortLocation port)
        {
            StatusCheckForce(status);
        }

        public static void SPIStatusCheck(int status, SPIPort port)
        {
            StatusCheckForce(status);
        }
    }
}
