
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(ICounter))]
    public unsafe static class Counter
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static ICounter lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public static int Get(int counterHandle)
        {
            return lowLevel.HAL_GetCounter(counterHandle);
        }

        public static void ClearDownSource(int counterHandle)
        {
            lowLevel.HAL_ClearCounterDownSource(counterHandle);
        }

        public static void ClearUpSource(int counterHandle)
        {
            lowLevel.HAL_ClearCounterUpSource(counterHandle);
        }

        public static void Free(int counterHandle)
        {
            lowLevel.HAL_FreeCounter(counterHandle);
        }

        public static int GetSamplesToAverage(int counterHandle)
        {
            return lowLevel.HAL_GetCounterSamplesToAverage(counterHandle);
        }

        public static int GetDirection(int counterHandle)
        {
            return lowLevel.HAL_GetCounterDirection(counterHandle);
        }

        public static double GetPeriod(int counterHandle)
        {
            return lowLevel.HAL_GetCounterPeriod(counterHandle);
        }

        public static int Initialize(CounterMode mode, int* index)
        {
            return lowLevel.HAL_InitializeCounter(mode, index);
        }

        public static void Reset(int counterHandle)
        {
            lowLevel.HAL_ResetCounter(counterHandle);
        }

        public static void SetAverageSize(int counterHandle, int size)
        {
            lowLevel.HAL_SetCounterAverageSize(counterHandle, size);
        }

        public static void SetDownSource(int counterHandle, int digitalSourceHandle, AnalogTriggerType analogTriggerType)
        {
            lowLevel.HAL_SetCounterDownSource(counterHandle, digitalSourceHandle, analogTriggerType);
        }

        public static void SetDownSourceEdge(int counterHandle, int risingEdge, int fallingEdge)
        {
            lowLevel.HAL_SetCounterDownSourceEdge(counterHandle, risingEdge, fallingEdge);
        }

        public static void SetExternalDirectionMode(int counterHandle)
        {
            lowLevel.HAL_SetCounterExternalDirectionMode(counterHandle);
        }

        public static void SetMaxPeriod(int counterHandle, double maxPeriod)
        {
            lowLevel.HAL_SetCounterMaxPeriod(counterHandle, maxPeriod);
        }

        public static void SetPulseLengthMode(int counterHandle, double threshold)
        {
            lowLevel.HAL_SetCounterPulseLengthMode(counterHandle, threshold);
        }

        public static void SetReverseDirection(int counterHandle, int reverseDirection)
        {
            lowLevel.HAL_SetCounterReverseDirection(counterHandle, reverseDirection);
        }

        public static void SetSamplesToAverage(int counterHandle, int samplesToAverage)
        {
            lowLevel.HAL_SetCounterSamplesToAverage(counterHandle, samplesToAverage);
        }

        public static void SetSemiPeriodMode(int counterHandle, int highSemiPeriod)
        {
            lowLevel.HAL_SetCounterSemiPeriodMode(counterHandle, highSemiPeriod);
        }

        public static void SetUpDownMode(int counterHandle)
        {
            lowLevel.HAL_SetCounterUpDownMode(counterHandle);
        }

        public static void SetUpSource(int counterHandle, int digitalSourceHandle, AnalogTriggerType analogTriggerType)
        {
            lowLevel.HAL_SetCounterUpSource(counterHandle, digitalSourceHandle, analogTriggerType);
        }

        public static void SetUpSourceEdge(int counterHandle, int risingEdge, int fallingEdge)
        {
            lowLevel.HAL_SetCounterUpSourceEdge(counterHandle, risingEdge, fallingEdge);
        }

        public static void SetUpdateWhenEmpty(int counterHandle, int enabled)
        {
            lowLevel.HAL_SetCounterUpdateWhenEmpty(counterHandle, enabled);
        }

        public static int GetStopped(int counterHandle)
        {
            return lowLevel.HAL_GetCounterStopped(counterHandle);
        }

    }
}
