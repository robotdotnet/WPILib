
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IAnalogAccumulator))]
    public unsafe static class AnalogAccumulator
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IAnalogAccumulator lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public static long GetAccumulatorCount(int analogPortHandle)
        {
            return lowLevel.HAL_GetAccumulatorCount(analogPortHandle);
        }

        public static void GetAccumulatorOutput(int analogPortHandle, long* value, long* count)
        {
            lowLevel.HAL_GetAccumulatorOutput(analogPortHandle, value, count);
        }

        public static long GetAccumulatorValue(int analogPortHandle)
        {
            return lowLevel.HAL_GetAccumulatorValue(analogPortHandle);
        }

        public static void InitAccumulator(int analogPortHandle)
        {
            lowLevel.HAL_InitAccumulator(analogPortHandle);
        }

        public static int IsAccumulatorChannel(int analogPortHandle)
        {
            return lowLevel.HAL_IsAccumulatorChannel(analogPortHandle);
        }

        public static void ResetAccumulator(int analogPortHandle)
        {
            lowLevel.HAL_ResetAccumulator(analogPortHandle);
        }

        public static void SetAccumulatorCenter(int analogPortHandle, int center)
        {
            lowLevel.HAL_SetAccumulatorCenter(analogPortHandle, center);
        }

        public static void SetAccumulatorDeadband(int analogPortHandle, int deadband)
        {
            lowLevel.HAL_SetAccumulatorDeadband(analogPortHandle, deadband);
        }

    }
}
