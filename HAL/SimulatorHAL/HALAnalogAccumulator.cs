using System;
using System.Runtime.InteropServices;
using HAL.Base;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALAnalogAccumulator
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALAnalogAccumulator.HAL_IsAccumulatorChannel = HAL_IsAccumulatorChannel;
            Base.HALAnalogAccumulator.HAL_InitAccumulator = HAL_InitAccumulator;
            Base.HALAnalogAccumulator.HAL_ResetAccumulator = HAL_ResetAccumulator;
            Base.HALAnalogAccumulator.HAL_SetAccumulatorCenter = HAL_SetAccumulatorCenter;
            Base.HALAnalogAccumulator.HAL_SetAccumulatorDeadband = HAL_SetAccumulatorDeadband;
            Base.HALAnalogAccumulator.HAL_GetAccumulatorValue = HAL_GetAccumulatorValue;
            Base.HALAnalogAccumulator.HAL_GetAccumulatorCount = HAL_GetAccumulatorCount;
            Base.HALAnalogAccumulator.HAL_GetAccumulatorOutput = HAL_GetAccumulatorOutput;
        }

        public static bool HAL_IsAccumulatorChannel(int analog_port_handle, ref int status)
        {
        }

        public static void HAL_InitAccumulator(int analog_port_handle, ref int status)
        {
        }

        public static void HAL_ResetAccumulator(int analog_port_handle, ref int status)
        {
        }

        public static void HAL_SetAccumulatorCenter(int analog_port_handle, int center, ref int status)
        {
        }

        public static void HAL_SetAccumulatorDeadband(int analog_port_handle, int deadband, ref int status)
        {
        }

        public static long HAL_GetAccumulatorValue(int analog_port_handle, ref int status)
        {
        }

        public static long HAL_GetAccumulatorCount(int analog_port_handle, ref int status)
        {
        }

        public static void HAL_GetAccumulatorOutput(int analog_port_handle, ref long value, ref long count, ref int status)
        {
        }
    }
}

