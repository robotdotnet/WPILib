
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

        public static bool GetDirection(int counterHandle)
        {
            return lowLevel.HAL_GetCounterDirection(counterHandle) != 0;
        }

        public static double GetPeriod(int counterHandle)
        {
            return lowLevel.HAL_GetCounterPeriod(counterHandle);
        }

        public static int Initialize(CounterMode mode, out int index)
        {
            int idx = 0;
            var ret = lowLevel.HAL_InitializeCounter(mode, &idx);
            index = idx;
            return ret;
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

        public static void SetDownSourceEdge(int counterHandle, bool risingEdge, bool fallingEdge)
        {
            lowLevel.HAL_SetCounterDownSourceEdge(counterHandle, risingEdge ? 1 : 0, fallingEdge ? 1 : 0);
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

        public static void SetReverseDirection(int counterHandle, bool reverseDirection)
        {
            lowLevel.HAL_SetCounterReverseDirection(counterHandle, reverseDirection ? 1 : 0);
        }

        public static void SetSamplesToAverage(int counterHandle, int samplesToAverage)
        {
            lowLevel.HAL_SetCounterSamplesToAverage(counterHandle, samplesToAverage);
        }

        public static void SetSemiPeriodMode(int counterHandle, bool highSemiPeriod)
        {
            lowLevel.HAL_SetCounterSemiPeriodMode(counterHandle, highSemiPeriod ? 1 : 0);
        }

        public static void SetUpDownMode(int counterHandle)
        {
            lowLevel.HAL_SetCounterUpDownMode(counterHandle);
        }

        public static void SetUpSource(int counterHandle, int digitalSourceHandle, AnalogTriggerType analogTriggerType)
        {
            lowLevel.HAL_SetCounterUpSource(counterHandle, digitalSourceHandle, analogTriggerType);
        }

        public static void SetUpSourceEdge(int counterHandle, bool risingEdge, bool fallingEdge)
        {
            lowLevel.HAL_SetCounterUpSourceEdge(counterHandle, risingEdge ? 1 : 0, fallingEdge ? 1 : 0);
        }

        public static void SetUpdateWhenEmpty(int counterHandle, bool enabled)
        {
            lowLevel.HAL_SetCounterUpdateWhenEmpty(counterHandle, enabled ? 1 : 0);
        }

        public static bool GetStopped(int counterHandle)
        {
            return lowLevel.HAL_GetCounterStopped(counterHandle) != 0;
        }

    }
}
