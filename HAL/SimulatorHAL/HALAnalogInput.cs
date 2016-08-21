using System;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.Simulator;
using HAL.SimulatorHAL.Handles;
using static HAL.Base.HALErrors;
using static HAL.Base.HAL;
using static HAL.SimulatorHAL.HALPorts;
using static HAL.SimulatorHAL.Handles.Handle;
using static HAL.Simulator.SimData;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALAnalogInput
    {
        internal const uint Timebase = 40000000;
        internal const int DefaultOversampleBits = 0;
        internal const double DefaultSampleRate = 50000.0;
        internal const int AnalogInputPins = 8;
        internal const int AnalogOutputPins = 2;
        internal const int AccumulatorNumChannels = 2;
        internal const int DefaultAverageBits = 7;
        internal const int DefaultLSBWeight = 1220703;
        internal const int DefaultOffset = 0;


        internal class AnalogInput
        {
            public byte Pin { get; set; }
        }

        internal static IndexedHandleResource<AnalogInput> analogInputHandles = new IndexedHandleResource<AnalogInput>(kNumAnalogInputs, HALHandleEnum.AnalogInput);

        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALAnalogInput.HAL_InitializeAnalogInputPort = HAL_InitializeAnalogInputPort;
            Base.HALAnalogInput.HAL_FreeAnalogInputPort = HAL_FreeAnalogInputPort;
            Base.HALAnalogInput.HAL_CheckAnalogModule = HAL_CheckAnalogModule;
            Base.HALAnalogInput.HAL_CheckAnalogInputChannel = HAL_CheckAnalogInputChannel;
            Base.HALAnalogInput.HAL_SetAnalogSampleRate = HAL_SetAnalogSampleRate;
            Base.HALAnalogInput.HAL_GetAnalogSampleRate = HAL_GetAnalogSampleRate;
            Base.HALAnalogInput.HAL_SetAnalogAverageBits = HAL_SetAnalogAverageBits;
            Base.HALAnalogInput.HAL_GetAnalogAverageBits = HAL_GetAnalogAverageBits;
            Base.HALAnalogInput.HAL_SetAnalogOversampleBits = HAL_SetAnalogOversampleBits;
            Base.HALAnalogInput.HAL_GetAnalogOversampleBits = HAL_GetAnalogOversampleBits;
            Base.HALAnalogInput.HAL_GetAnalogValue = HAL_GetAnalogValue;
            Base.HALAnalogInput.HAL_GetAnalogAverageValue = HAL_GetAnalogAverageValue;
            Base.HALAnalogInput.HAL_GetAnalogVoltsToValue = HAL_GetAnalogVoltsToValue;
            Base.HALAnalogInput.HAL_GetAnalogVoltage = HAL_GetAnalogVoltage;
            Base.HALAnalogInput.HAL_GetAnalogAverageVoltage = HAL_GetAnalogAverageVoltage;
            Base.HALAnalogInput.HAL_GetAnalogLSBWeight = HAL_GetAnalogLSBWeight;
            Base.HALAnalogInput.HAL_GetAnalogOffset = HAL_GetAnalogOffset;
        }

        public static int HAL_InitializeAnalogInputPort(int port_handle, ref int status)
        {
            status = 0;

            short pin = GetPortHandlePin(port_handle);

            if (pin == InvalidHandleIndex)
            {
                status = PARAMETER_OUT_OF_RANGE;
                return HALInvalidHandle;
            }

            int handle = analogInputHandles.Allocate(pin, ref status);

            if (status != 0) return HALInvalidHandle;

            var port = analogInputHandles.Get(handle);

            if (port == null)
            {
                status = HAL_HANDLE_ERROR;
                return HALInvalidHandle;
            }

            port.Pin = (byte)pin;
            return handle;
        }

        public static void HAL_FreeAnalogInputPort(int analog_port_handle)
        {
            analogInputHandles.Free(analog_port_handle);
        }

        public static bool HAL_CheckAnalogModule(int module)
        {
            return module == 1;
        }

        public static bool HAL_CheckAnalogInputChannel(int pin)
        {
            return pin < kNumAnalogInputs && pin >= 0;
        }

        public static void HAL_SetAnalogSampleRate(double samplesPerSecond, ref int status)
        {
            status = 0;
            SimData.GlobalData.AnalogSampleRate = samplesPerSecond;
        }

        public static double HAL_GetAnalogSampleRate(ref int status)
        {
            status = 0;
            return SimData.GlobalData.AnalogSampleRate;
        }

        public static void HAL_SetAnalogAverageBits(int analog_port_handle, int bits, ref int status)
        {
            status = 0;

            var port = analogInputHandles.Get(analog_port_handle);
            if (port == null)
            {
                status = HAL_HANDLE_ERROR;
                return;
            }

            AnalogIn[port.Pin].AverageBits = bits;
        }

        public static int HAL_GetAnalogAverageBits(int analog_port_handle, ref int status)
        {
            status = 0;

            var port = analogInputHandles.Get(analog_port_handle);
            if (port == null)
            {
                status = HAL_HANDLE_ERROR;
                return 0;
            }

            return AnalogIn[port.Pin].AverageBits;
        }

        public static void HAL_SetAnalogOversampleBits(int analog_port_handle, int bits, ref int status)
        {
            status = 0;

            var port = analogInputHandles.Get(analog_port_handle);
            if (port == null)
            {
                status = HAL_HANDLE_ERROR;
                return;
            }

            AnalogIn[port.Pin].OversampleBits = bits;
        }

        public static int HAL_GetAnalogOversampleBits(int analog_port_handle, ref int status)
        {
            status = 0;

            var port = analogInputHandles.Get(analog_port_handle);
            if (port == null)
            {
                status = HAL_HANDLE_ERROR;
                return 0;
            }

            return AnalogIn[port.Pin].OversampleBits;
        }

        public static int HAL_GetAnalogValue(int analog_port_handle, ref int status)
        {
            status = 0;

            var port = analogInputHandles.Get(analog_port_handle);
            if (port == null)
            {
                status = HAL_HANDLE_ERROR;
                return 0;
            }

            return HAL_GetAnalogVoltsToValue(analog_port_handle, AnalogIn[port.Pin].Voltage, ref status);
        }

        public static int HAL_GetAnalogAverageValue(int analog_port_handle, ref int status)
        {
            return HAL_GetAnalogValue(analog_port_handle, ref status);
        }

        public static int HAL_GetAnalogVoltsToValue(int analog_port_handle, double voltage, ref int status)
        {
            status = 0;

            var port = analogInputHandles.Get(analog_port_handle);
            if (port == null)
            {
                status = HAL_HANDLE_ERROR;
                return 0;
            }

            if (voltage > 5.0)
            {
                voltage = 5.0;
                status = VOLTAGE_OUT_OF_RANGE;
            }
            else if (voltage < 0.0)
            {
                voltage = 0.0;
                status = VOLTAGE_OUT_OF_RANGE;
            }

            var LSBWeight = HAL_GetAnalogLSBWeight(analog_port_handle, ref status);
            var offset = HAL_GetAnalogOffset(analog_port_handle, ref status);
            return (int)((voltage + offset * 1.0e-9) / (LSBWeight * 1.0e-9));
        }

        public static double HAL_GetAnalogVoltage(int analog_port_handle, ref int status)
        {
            status = 0;

            var port = analogInputHandles.Get(analog_port_handle);
            if (port == null)
            {
                status = HAL_HANDLE_ERROR;
                return 0;
            }

            return AnalogIn[port.Pin].Voltage;
        }

        public static double HAL_GetAnalogAverageVoltage(int analog_port_handle, ref int status)
        {
            return HAL_GetAnalogVoltage(analog_port_handle, ref status);
        }

        public static int HAL_GetAnalogLSBWeight(int analog_port_handle, ref int status)
        {
            return DefaultLSBWeight;
        }

        public static int HAL_GetAnalogOffset(int analog_port_handle, ref int status)
        {
            return DefaultOffset;
        }
    }
}

