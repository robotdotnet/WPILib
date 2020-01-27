
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

public static int GetCounter(int counterHandle)
{
return lowLevel.HAL_GetCounter(counterHandle);
}

public static void ClearCounterDownSource(int counterHandle)
{
lowLevel.HAL_ClearCounterDownSource(counterHandle);
}

public static void ClearCounterUpSource(int counterHandle)
{
lowLevel.HAL_ClearCounterUpSource(counterHandle);
}

public static void FreeCounter(int counterHandle)
{
lowLevel.HAL_FreeCounter(counterHandle);
}

public static int GetCounterSamplesToAverage(int counterHandle)
{
return lowLevel.HAL_GetCounterSamplesToAverage(counterHandle);
}

public static int GetCounterDirection(int counterHandle)
{
return lowLevel.HAL_GetCounterDirection(counterHandle);
}

public static double GetCounterPeriod(int counterHandle)
{
return lowLevel.HAL_GetCounterPeriod(counterHandle);
}

public static int InitializeCounter(CounterMode mode, int* index)
{
return lowLevel.HAL_InitializeCounter(mode, index);
}

public static void ResetCounter(int counterHandle)
{
lowLevel.HAL_ResetCounter(counterHandle);
}

public static void SetCounterAverageSize(int counterHandle, int size)
{
lowLevel.HAL_SetCounterAverageSize(counterHandle, size);
}

public static void SetCounterDownSource(int counterHandle, int digitalSourceHandle, AnalogTriggerType analogTriggerType)
{
lowLevel.HAL_SetCounterDownSource(counterHandle, digitalSourceHandle, analogTriggerType);
}

public static void SetCounterDownSourceEdge(int counterHandle, int risingEdge, int fallingEdge)
{
lowLevel.HAL_SetCounterDownSourceEdge(counterHandle, risingEdge, fallingEdge);
}

public static void SetCounterExternalDirectionMode(int counterHandle)
{
lowLevel.HAL_SetCounterExternalDirectionMode(counterHandle);
}

public static void SetCounterMaxPeriod(int counterHandle, double maxPeriod)
{
lowLevel.HAL_SetCounterMaxPeriod(counterHandle, maxPeriod);
}

public static void SetCounterPulseLengthMode(int counterHandle, double threshold)
{
lowLevel.HAL_SetCounterPulseLengthMode(counterHandle, threshold);
}

public static void SetCounterReverseDirection(int counterHandle, int reverseDirection)
{
lowLevel.HAL_SetCounterReverseDirection(counterHandle, reverseDirection);
}

public static void SetCounterSamplesToAverage(int counterHandle, int samplesToAverage)
{
lowLevel.HAL_SetCounterSamplesToAverage(counterHandle, samplesToAverage);
}

public static void SetCounterSemiPeriodMode(int counterHandle, int highSemiPeriod)
{
lowLevel.HAL_SetCounterSemiPeriodMode(counterHandle, highSemiPeriod);
}

public static void SetCounterUpDownMode(int counterHandle)
{
lowLevel.HAL_SetCounterUpDownMode(counterHandle);
}

public static void SetCounterUpSource(int counterHandle, int digitalSourceHandle, AnalogTriggerType analogTriggerType)
{
lowLevel.HAL_SetCounterUpSource(counterHandle, digitalSourceHandle, analogTriggerType);
}

public static void SetCounterUpSourceEdge(int counterHandle, int risingEdge, int fallingEdge)
{
lowLevel.HAL_SetCounterUpSourceEdge(counterHandle, risingEdge, fallingEdge);
}

public static void SetCounterUpdateWhenEmpty(int counterHandle, int enabled)
{
lowLevel.HAL_SetCounterUpdateWhenEmpty(counterHandle, enabled);
}

public static int GetCounterStopped(int counterHandle)
{
return lowLevel.HAL_GetCounterStopped(counterHandle);
}

}
}
