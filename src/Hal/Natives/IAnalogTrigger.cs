using WPIUtil.ILGeneration;

namespace Hal.Natives
{
   public unsafe interface IAnalogTrigger
    {
        [StatusCheckLastParameter]  void HAL_CleanAnalogTrigger(int analogTriggerHandle);

        [StatusCheckLastParameter]  int HAL_GetAnalogTriggerFPGAIndex( int analogTriggerHandle);

        [StatusCheckLastParameter]  int HAL_GetAnalogTriggerInWindow( int analogTriggerHandle);

        [StatusCheckLastParameter]  int HAL_GetAnalogTriggerOutput(int analogTriggerHandle, AnalogTriggerType type);

        [StatusCheckLastParameter]  int HAL_GetAnalogTriggerTriggerState( int analogTriggerHandle);

        [StatusCheckLastParameter]  int HAL_InitializeAnalogTrigger( int portHandle);

        [StatusCheckLastParameter]  int HAL_InitializeAnalogTriggerDutyCycle( int dutyCycleHandle);

        [StatusCheckLastParameter]  void HAL_SetAnalogTriggerAveraged(int analogTriggerHandle, int useAveragedValue);

        [StatusCheckLastParameter]  void HAL_SetAnalogTriggerFiltered(int analogTriggerHandle, int useFilteredValue);

        [StatusCheckLastParameter]  void HAL_SetAnalogTriggerLimitsRaw(int analogTriggerHandle, int lower, int upper);

    }
}
