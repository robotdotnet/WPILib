using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IAnalogTrigger
    {
        [StatusCheckLastParameter] void HAL_CleanAnalogTrigger(int analogTriggerHandle);

        [StatusCheckLastParameter] int HAL_GetAnalogTriggerFPGAIndex(int analogTriggerHandle);

        [StatusCheckLastParameter] int HAL_GetAnalogTriggerInWindow(int analogTriggerHandle);

        [StatusCheckLastParameter] int HAL_GetAnalogTriggerOutput(int analogTriggerHandle, AnalogTriggerType type);

        [StatusCheckLastParameter] int HAL_GetAnalogTriggerTriggerState(int analogTriggerHandle);

        [StatusCheckRange(0, typeof(StatusHandling), "AnalogTriggerStatusCheck")] int HAL_InitializeAnalogTrigger(int portHandle);

        [StatusCheckRange(0, typeof(StatusHandling), "AnalogTriggerDutyCycleStatusCheck")] int HAL_InitializeAnalogTriggerDutyCycle(int dutyCycleHandle);

        [StatusCheckLastParameter] void HAL_SetAnalogTriggerAveraged(int analogTriggerHandle, int useAveragedValue);

        [StatusCheckLastParameter] void HAL_SetAnalogTriggerFiltered(int analogTriggerHandle, int useFilteredValue);

        [StatusCheckLastParameter] void HAL_SetAnalogTriggerLimitsRaw(int analogTriggerHandle, int lower, int upper);

        [StatusCheckLastParameter] void HAL_SetAnalogTriggerLimitsVoltage(int analogTriggerHandle, double lower, double upper);

        [StatusCheckLastParameter] void HAL_SetAnalogTriggerLimitsDutyCycle(int analogTriggerHandle, double lower, double upper);

    }
}
