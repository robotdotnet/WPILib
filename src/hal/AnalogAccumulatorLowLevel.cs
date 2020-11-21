
using Hal.Natives;
using WPIUtil.ILGeneration;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static unsafe class AnalogAccumulatorLowLevel
    {
        internal static AnalogAccumulatorLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

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
