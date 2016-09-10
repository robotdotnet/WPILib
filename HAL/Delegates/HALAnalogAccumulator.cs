using System.Runtime.InteropServices;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALAnalogAccumulator
    {
        static HALAnalogAccumulator()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALAnalogAccumulator>(LibraryLoaderHolder.NativeLoader);
        }

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_IsAccumulatorChannelDelegate(int analog_port_handle, ref int status);
        [NativeDelegate]
        public static HAL_IsAccumulatorChannelDelegate HAL_IsAccumulatorChannel;

        public delegate void HAL_InitAccumulatorDelegate(int analog_port_handle, ref int status);
        [NativeDelegate]
        public static HAL_InitAccumulatorDelegate HAL_InitAccumulator;

        public delegate void HAL_ResetAccumulatorDelegate(int analog_port_handle, ref int status);
        [NativeDelegate]
        public static HAL_ResetAccumulatorDelegate HAL_ResetAccumulator;

        public delegate void HAL_SetAccumulatorCenterDelegate(int analog_port_handle, int center, ref int status);
        [NativeDelegate]
        public static HAL_SetAccumulatorCenterDelegate HAL_SetAccumulatorCenter;

        public delegate void HAL_SetAccumulatorDeadbandDelegate(int analog_port_handle, int deadband, ref int status);
        [NativeDelegate]
        public static HAL_SetAccumulatorDeadbandDelegate HAL_SetAccumulatorDeadband;

        public delegate long HAL_GetAccumulatorValueDelegate(int analog_port_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetAccumulatorValueDelegate HAL_GetAccumulatorValue;

        public delegate long HAL_GetAccumulatorCountDelegate(int analog_port_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetAccumulatorCountDelegate HAL_GetAccumulatorCount;

        public delegate void HAL_GetAccumulatorOutputDelegate(int analog_port_handle, ref long value, ref long count, ref int status);
        [NativeDelegate]
        public static HAL_GetAccumulatorOutputDelegate HAL_GetAccumulatorOutput;
    }
}

