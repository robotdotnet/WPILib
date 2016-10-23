using System.Runtime.InteropServices;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALCounter
    {
        static HALCounter()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALCounter>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_InitializeCounterDelegate(HALCounterMode mode, ref int index, ref int status);
        [NativeDelegate] public static HAL_InitializeCounterDelegate HAL_InitializeCounter;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_FreeCounterDelegate(int counter_handle, ref int status);
        [NativeDelegate] public static HAL_FreeCounterDelegate HAL_FreeCounter;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetCounterAverageSizeDelegate(int counter_handle, int size, ref int status);
        [NativeDelegate] public static HAL_SetCounterAverageSizeDelegate HAL_SetCounterAverageSize;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetCounterUpSourceDelegate(int counter_handle, int digitalSourceHandle, HALAnalogTriggerType analogTriggerType, ref int status);
        [NativeDelegate] public static HAL_SetCounterUpSourceDelegate HAL_SetCounterUpSource;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetCounterUpSourceEdgeDelegate(int counter_handle, [MarshalAs(UnmanagedType.Bool)]bool risingEdge, [MarshalAs(UnmanagedType.Bool)]bool fallingEdge, ref int status);
        [NativeDelegate] public static HAL_SetCounterUpSourceEdgeDelegate HAL_SetCounterUpSourceEdge;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_ClearCounterUpSourceDelegate(int counter_handle, ref int status);
        [NativeDelegate] public static HAL_ClearCounterUpSourceDelegate HAL_ClearCounterUpSource;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetCounterDownSourceDelegate(int counter_handle, int digitalSourceHandle, HALAnalogTriggerType analogTriggerType, ref int status);
        [NativeDelegate] public static HAL_SetCounterDownSourceDelegate HAL_SetCounterDownSource;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetCounterDownSourceEdgeDelegate(int counter_handle, [MarshalAs(UnmanagedType.Bool)]bool risingEdge, [MarshalAs(UnmanagedType.Bool)]bool fallingEdge, ref int status);
        [NativeDelegate] public static HAL_SetCounterDownSourceEdgeDelegate HAL_SetCounterDownSourceEdge;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_ClearCounterDownSourceDelegate(int counter_handle, ref int status);
        [NativeDelegate] public static HAL_ClearCounterDownSourceDelegate HAL_ClearCounterDownSource;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetCounterUpDownModeDelegate(int counter_handle, ref int status);
        [NativeDelegate] public static HAL_SetCounterUpDownModeDelegate HAL_SetCounterUpDownMode;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetCounterExternalDirectionModeDelegate(int counter_handle, ref int status);
        [NativeDelegate] public static HAL_SetCounterExternalDirectionModeDelegate HAL_SetCounterExternalDirectionMode;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetCounterSemiPeriodModeDelegate(int counter_handle, [MarshalAs(UnmanagedType.Bool)]bool highSemiPeriod, ref int status);
        [NativeDelegate] public static HAL_SetCounterSemiPeriodModeDelegate HAL_SetCounterSemiPeriodMode;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetCounterPulseLengthModeDelegate(int counter_handle, double threshold, ref int status);
        [NativeDelegate] public static HAL_SetCounterPulseLengthModeDelegate HAL_SetCounterPulseLengthMode;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetCounterSamplesToAverageDelegate(int counter_handle, ref int status);
        [NativeDelegate] public static HAL_GetCounterSamplesToAverageDelegate HAL_GetCounterSamplesToAverage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetCounterSamplesToAverageDelegate(int counter_handle, int samplesToAverage, ref int status);
        [NativeDelegate] public static HAL_SetCounterSamplesToAverageDelegate HAL_SetCounterSamplesToAverage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_ResetCounterDelegate(int counter_handle, ref int status);
        [NativeDelegate] public static HAL_ResetCounterDelegate HAL_ResetCounter;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetCounterDelegate(int counter_handle, ref int status);
        [NativeDelegate] public static HAL_GetCounterDelegate HAL_GetCounter;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetCounterPeriodDelegate(int counter_handle, ref int status);
        [NativeDelegate] public static HAL_GetCounterPeriodDelegate HAL_GetCounterPeriod;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetCounterMaxPeriodDelegate(int counter_handle, double maxPeriod, ref int status);
        [NativeDelegate] public static HAL_SetCounterMaxPeriodDelegate HAL_SetCounterMaxPeriod;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetCounterUpdateWhenEmptyDelegate(int counter_handle, [MarshalAs(UnmanagedType.Bool)]bool enabled, ref int status);
        [NativeDelegate] public static HAL_SetCounterUpdateWhenEmptyDelegate HAL_SetCounterUpdateWhenEmpty;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetCounterStoppedDelegate(int counter_handle, ref int status);
        [NativeDelegate] public static HAL_GetCounterStoppedDelegate HAL_GetCounterStopped;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetCounterDirectionDelegate(int counter_handle, ref int status);
        [NativeDelegate] public static HAL_GetCounterDirectionDelegate HAL_GetCounterDirection;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetCounterReverseDirectionDelegate(int counter_handle, [MarshalAs(UnmanagedType.Bool)]bool reverseDirection, ref int status);
        [NativeDelegate] public static HAL_SetCounterReverseDirectionDelegate HAL_SetCounterReverseDirection;
    }
}

