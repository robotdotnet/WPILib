using System;
using HAL.Base;
using HAL.NativeLoader;
using HAL.SimulatorHAL.Handles;
using static HAL.Base.HALErrors;
using static HAL.Base.HAL;
using static HAL.SimulatorHAL.HALPorts;
using static HAL.SimulatorHAL.Handles.Handle;
using static HAL.Simulator.SimData;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALAnalogOutput
    {
        class AnalogOutput
        {
            public byte Pin { get; set; }
        }

        private static IndexedHandleResource<AnalogOutput> analogOutputHandles = new IndexedHandleResource<AnalogOutput>(kNumAnalogTriggers, HALHandleEnum.AnalogOutput);

        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALAnalogOutput.HAL_InitializeAnalogOutputPort = HAL_InitializeAnalogOutputPort;
            Base.HALAnalogOutput.HAL_FreeAnalogOutputPort = HAL_FreeAnalogOutputPort;
            Base.HALAnalogOutput.HAL_SetAnalogOutput = HAL_SetAnalogOutput;
            Base.HALAnalogOutput.HAL_GetAnalogOutput = HAL_GetAnalogOutput;
            Base.HALAnalogOutput.HAL_CheckAnalogOutputChannel = HAL_CheckAnalogOutputChannel;
        }

        public static int HAL_InitializeAnalogOutputPort(int port_handle, ref int status)
        {
            status = 0;

            short pin = GetPortHandlePin(port_handle);

            if (pin == InvalidHandleIndex)
            {
                status = PARAMETER_OUT_OF_RANGE;
                return HALInvalidHandle;
            }

            int handle = analogOutputHandles.Allocate(pin, ref status);
            
            if (status != 0) return HALInvalidHandle;

            var port = analogOutputHandles.Get(handle);

            if (port == null)
            {
                status = HAL_HANDLE_ERROR;
                return HALInvalidHandle;
            }

            port.Pin = (byte) pin;
            return handle;
        }

        public static void HAL_FreeAnalogOutputPort(int analog_output_handle)
        {
            analogOutputHandles.Free(analog_output_handle);
        }

        public static void HAL_SetAnalogOutput(int analog_output_handle, double voltage, ref int status)
        {
            var port = analogOutputHandles.Get(analog_output_handle);
            if (port == null)
            {
                status = HAL_HANDLE_ERROR;
                return;
            }

            AnalogOut[port.Pin].Voltage = voltage;
        }

        public static double HAL_GetAnalogOutput(int analog_output_handle, ref int status)
        {
            status = 0;

            var port = analogOutputHandles.Get(analog_output_handle);
            if (port == null)
            {
                status = HAL_HANDLE_ERROR;
                return 0;
            }

            return AnalogOut[port.Pin].Voltage;
        }

        public static bool HAL_CheckAnalogOutputChannel(int pin)
        {
            return pin < kNumAnalogOutputs && pin >= 0;
        }
    }
}

