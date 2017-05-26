using System.Runtime.InteropServices;
using FRC.NativeLibraryUtilities;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALAnalogAccumulator
    {
        public static void Ping() { }

        static HALAnalogAccumulator()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALAnalogAccumulator>(LibraryLoaderHolder.NativeLoader);
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_IsAccumulatorChannelDelegate(int analog_port_handle, ref int status);
        [NativeDelegate]
        public static HAL_IsAccumulatorChannelDelegate HAL_IsAccumulatorChannel;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_InitAccumulatorDelegate(int analog_port_handle, ref int status);
        [NativeDelegate]
        public static HAL_InitAccumulatorDelegate HAL_InitAccumulator;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_ResetAccumulatorDelegate(int analog_port_handle, ref int status);
        [NativeDelegate]
        public static HAL_ResetAccumulatorDelegate HAL_ResetAccumulator;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetAccumulatorCenterDelegate(int analog_port_handle, int center, ref int status);
        [NativeDelegate]
        public static HAL_SetAccumulatorCenterDelegate HAL_SetAccumulatorCenter;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetAccumulatorDeadbandDelegate(int analog_port_handle, int deadband, ref int status);
        [NativeDelegate]
        public static HAL_SetAccumulatorDeadbandDelegate HAL_SetAccumulatorDeadband;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate long HAL_GetAccumulatorValueDelegate(int analog_port_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetAccumulatorValueDelegate HAL_GetAccumulatorValue;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate long HAL_GetAccumulatorCountDelegate(int analog_port_handle, ref int status);
        [NativeDelegate]
        public static HAL_GetAccumulatorCountDelegate HAL_GetAccumulatorCount;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_GetAccumulatorOutputDelegate(int analog_port_handle, ref long value, ref long count, ref int status);
        [NativeDelegate]
        public static HAL_GetAccumulatorOutputDelegate HAL_GetAccumulatorOutput;
    }
}

