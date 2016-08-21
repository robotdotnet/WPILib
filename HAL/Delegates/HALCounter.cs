using System;
using System.Runtime.InteropServices;
// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALCounter
    {
        static HALCounter()
        {
            HAL.Initialize();
        }

        public delegate int HAL_InitializeCounterDelegate(HALCounterMode mode, ref int index, ref int status);
        public static HAL_InitializeCounterDelegate HAL_InitializeCounter;

        public delegate void HAL_FreeCounterDelegate(int counter_handle, ref int status);
        public static HAL_FreeCounterDelegate HAL_FreeCounter;

        public delegate void HAL_SetCounterAverageSizeDelegate(int counter_handle, int size, ref int status);
        public static HAL_SetCounterAverageSizeDelegate HAL_SetCounterAverageSize;

        public delegate void HAL_SetCounterUpSourceDelegate(int counter_handle, int digitalSourceHandle, HALAnalogTriggerType analogTriggerType, ref int status);
        public static HAL_SetCounterUpSourceDelegate HAL_SetCounterUpSource;

        public delegate void HAL_SetCounterUpSourceEdgeDelegate(int counter_handle, [MarshalAs(UnmanagedType.I4)]bool risingEdge, [MarshalAs(UnmanagedType.I4)]bool fallingEdge, ref int status);
        public static HAL_SetCounterUpSourceEdgeDelegate HAL_SetCounterUpSourceEdge;

        public delegate void HAL_ClearCounterUpSourceDelegate(int counter_handle, ref int status);
        public static HAL_ClearCounterUpSourceDelegate HAL_ClearCounterUpSource;

        public delegate void HAL_SetCounterDownSourceDelegate(int counter_handle, int digitalSourceHandle, HALAnalogTriggerType analogTriggerType, ref int status);
        public static HAL_SetCounterDownSourceDelegate HAL_SetCounterDownSource;

        public delegate void HAL_SetCounterDownSourceEdgeDelegate(int counter_handle, [MarshalAs(UnmanagedType.I4)]bool risingEdge, [MarshalAs(UnmanagedType.I4)]bool fallingEdge, ref int status);
        public static HAL_SetCounterDownSourceEdgeDelegate HAL_SetCounterDownSourceEdge;

        public delegate void HAL_ClearCounterDownSourceDelegate(int counter_handle, ref int status);
        public static HAL_ClearCounterDownSourceDelegate HAL_ClearCounterDownSource;

        public delegate void HAL_SetCounterUpDownModeDelegate(int counter_handle, ref int status);
        public static HAL_SetCounterUpDownModeDelegate HAL_SetCounterUpDownMode;

        public delegate void HAL_SetCounterExternalDirectionModeDelegate(int counter_handle, ref int status);
        public static HAL_SetCounterExternalDirectionModeDelegate HAL_SetCounterExternalDirectionMode;

        public delegate void HAL_SetCounterSemiPeriodModeDelegate(int counter_handle, [MarshalAs(UnmanagedType.I4)]bool highSemiPeriod, ref int status);
        public static HAL_SetCounterSemiPeriodModeDelegate HAL_SetCounterSemiPeriodMode;

        public delegate void HAL_SetCounterPulseLengthModeDelegate(int counter_handle, double threshold, ref int status);
        public static HAL_SetCounterPulseLengthModeDelegate HAL_SetCounterPulseLengthMode;

        public delegate int HAL_GetCounterSamplesToAverageDelegate(int counter_handle, ref int status);
        public static HAL_GetCounterSamplesToAverageDelegate HAL_GetCounterSamplesToAverage;

        public delegate void HAL_SetCounterSamplesToAverageDelegate(int counter_handle, int samplesToAverage, ref int status);
        public static HAL_SetCounterSamplesToAverageDelegate HAL_SetCounterSamplesToAverage;

        public delegate void HAL_ResetCounterDelegate(int counter_handle, ref int status);
        public static HAL_ResetCounterDelegate HAL_ResetCounter;

        public delegate int HAL_GetCounterDelegate(int counter_handle, ref int status);
        public static HAL_GetCounterDelegate HAL_GetCounter;

        public delegate double HAL_GetCounterPeriodDelegate(int counter_handle, ref int status);
        public static HAL_GetCounterPeriodDelegate HAL_GetCounterPeriod;

        public delegate void HAL_SetCounterMaxPeriodDelegate(int counter_handle, double maxPeriod, ref int status);
        public static HAL_SetCounterMaxPeriodDelegate HAL_SetCounterMaxPeriod;

        public delegate void HAL_SetCounterUpdateWhenEmptyDelegate(int counter_handle, [MarshalAs(UnmanagedType.I4)]bool enabled, ref int status);
        public static HAL_SetCounterUpdateWhenEmptyDelegate HAL_SetCounterUpdateWhenEmpty;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetCounterStoppedDelegate(int counter_handle, ref int status);
        public static HAL_GetCounterStoppedDelegate HAL_GetCounterStopped;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetCounterDirectionDelegate(int counter_handle, ref int status);
        public static HAL_GetCounterDirectionDelegate HAL_GetCounterDirection;

        public delegate void HAL_SetCounterReverseDirectionDelegate(int counter_handle, [MarshalAs(UnmanagedType.I4)]bool reverseDirection, ref int status);
        public static HAL_SetCounterReverseDirectionDelegate HAL_SetCounterReverseDirection;
    }
}

