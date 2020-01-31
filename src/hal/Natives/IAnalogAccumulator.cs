using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    public unsafe interface IAnalogAccumulator
    {
        [StatusCheckLastParameter] long HAL_GetAccumulatorCount(int analogPortHandle);

        [StatusCheckLastParameter] void HAL_GetAccumulatorOutput(int analogPortHandle, long* value, long* count);

        [StatusCheckLastParameter] long HAL_GetAccumulatorValue(int analogPortHandle);

        [StatusCheckLastParameter] void HAL_InitAccumulator(int analogPortHandle);

        [StatusCheckLastParameter] int HAL_IsAccumulatorChannel(int analogPortHandle);

        [StatusCheckLastParameter] void HAL_ResetAccumulator(int analogPortHandle);

        [StatusCheckLastParameter] void HAL_SetAccumulatorCenter(int analogPortHandle, int center);

        [StatusCheckLastParameter] void HAL_SetAccumulatorDeadband(int analogPortHandle, int deadband);

    }
}
