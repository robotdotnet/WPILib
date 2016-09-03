using System;
using System.Runtime.InteropServices;
using HAL.Base;
/*
// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALCounter
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALCounter.HAL_InitializeCounter = HAL_InitializeCounter;
            Base.HALCounter.HAL_FreeCounter = HAL_FreeCounter;
            Base.HALCounter.HAL_SetCounterAverageSize = HAL_SetCounterAverageSize;
            Base.HALCounter.HAL_SetCounterUpSource = HAL_SetCounterUpSource;
            Base.HALCounter.HAL_SetCounterUpSourceEdge = HAL_SetCounterUpSourceEdge;
            Base.HALCounter.HAL_ClearCounterUpSource = HAL_ClearCounterUpSource;
            Base.HALCounter.HAL_SetCounterDownSource = HAL_SetCounterDownSource;
            Base.HALCounter.HAL_SetCounterDownSourceEdge = HAL_SetCounterDownSourceEdge;
            Base.HALCounter.HAL_ClearCounterDownSource = HAL_ClearCounterDownSource;
            Base.HALCounter.HAL_SetCounterUpDownMode = HAL_SetCounterUpDownMode;
            Base.HALCounter.HAL_SetCounterExternalDirectionMode = HAL_SetCounterExternalDirectionMode;
            Base.HALCounter.HAL_SetCounterSemiPeriodMode = HAL_SetCounterSemiPeriodMode;
            Base.HALCounter.HAL_SetCounterPulseLengthMode = HAL_SetCounterPulseLengthMode;
            Base.HALCounter.HAL_GetCounterSamplesToAverage = HAL_GetCounterSamplesToAverage;
            Base.HALCounter.HAL_SetCounterSamplesToAverage = HAL_SetCounterSamplesToAverage;
            Base.HALCounter.HAL_ResetCounter = HAL_ResetCounter;
            Base.HALCounter.HAL_GetCounter = HAL_GetCounter;
            Base.HALCounter.HAL_GetCounterPeriod = HAL_GetCounterPeriod;
            Base.HALCounter.HAL_SetCounterMaxPeriod = HAL_SetCounterMaxPeriod;
            Base.HALCounter.HAL_SetCounterUpdateWhenEmpty = HAL_SetCounterUpdateWhenEmpty;
            Base.HALCounter.HAL_GetCounterStopped = HAL_GetCounterStopped;
            Base.HALCounter.HAL_GetCounterDirection = HAL_GetCounterDirection;
            Base.HALCounter.HAL_SetCounterReverseDirection = HAL_SetCounterReverseDirection;
        }

        public static int HAL_InitializeCounter(HALCounterMode mode, ref int index, ref int status)
        {
        }

        public static void HAL_FreeCounter(int counter_handle, ref int status)
        {
        }

        public static void HAL_SetCounterAverageSize(int counter_handle, int size, ref int status)
        {
        }

        public static void HAL_SetCounterUpSource(int counter_handle, int digitalSourceHandle, HALAnalogTriggerType analogTriggerType, ref int status)
        {
        }

        public static void HAL_SetCounterUpSourceEdge(int counter_handle, bool risingEdge, bool fallingEdge, ref int status)
        {
        }

        public static void HAL_ClearCounterUpSource(int counter_handle, ref int status)
        {
        }

        public static void HAL_SetCounterDownSource(int counter_handle, int digitalSourceHandle, HALAnalogTriggerType analogTriggerType, ref int status)
        {
        }

        public static void HAL_SetCounterDownSourceEdge(int counter_handle, bool risingEdge, bool fallingEdge, ref int status)
        {
        }

        public static void HAL_ClearCounterDownSource(int counter_handle, ref int status)
        {
        }

        public static void HAL_SetCounterUpDownMode(int counter_handle, ref int status)
        {
        }

        public static void HAL_SetCounterExternalDirectionMode(int counter_handle, ref int status)
        {
        }

        public static void HAL_SetCounterSemiPeriodMode(int counter_handle, bool highSemiPeriod, ref int status)
        {
        }

        public static void HAL_SetCounterPulseLengthMode(int counter_handle, double threshold, ref int status)
        {
        }

        public static int HAL_GetCounterSamplesToAverage(int counter_handle, ref int status)
        {
        }

        public static void HAL_SetCounterSamplesToAverage(int counter_handle, int samplesToAverage, ref int status)
        {
        }

        public static void HAL_ResetCounter(int counter_handle, ref int status)
        {
        }

        public static int HAL_GetCounter(int counter_handle, ref int status)
        {
        }

        public static double HAL_GetCounterPeriod(int counter_handle, ref int status)
        {
        }

        public static void HAL_SetCounterMaxPeriod(int counter_handle, double maxPeriod, ref int status)
        {
        }

        public static void HAL_SetCounterUpdateWhenEmpty(int counter_handle, bool enabled, ref int status)
        {
        }

        public static bool HAL_GetCounterStopped(int counter_handle, ref int status)
        {
        }

        public static bool HAL_GetCounterDirection(int counter_handle, ref int status)
        {
        }

        public static void HAL_SetCounterReverseDirection(int counter_handle, bool reverseDirection, ref int status)
        {
        }
    }
}

    */