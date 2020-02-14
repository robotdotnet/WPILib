
using Hal.Natives;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IAnalogAccumulator))]
    public static unsafe class AnalogAccumulatorLowLevel
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

        public static void GetAccumulatorOutput(int analogPortHandle, out long value, out long count)
        {
            long tmpValue = 0;
            long tmpCount = 0;
            lowLevel.HAL_GetAccumulatorOutput(analogPortHandle, &tmpValue, &tmpCount);
            value = tmpValue;
            count = tmpCount;
        }

        public static long GetAccumulatorValue(int analogPortHandle)
        {
            return lowLevel.HAL_GetAccumulatorValue(analogPortHandle);
        }

        public static void InitAccumulator(int analogPortHandle)
        {
            lowLevel.HAL_InitAccumulator(analogPortHandle);
        }

        public static bool IsAccumulatorChannel(int analogPortHandle)
        {
            return lowLevel.HAL_IsAccumulatorChannel(analogPortHandle) != 0;
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
