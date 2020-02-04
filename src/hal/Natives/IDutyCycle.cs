using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IDutyCycle
    {
        void HAL_FreeDutyCycle(int dutyCycleHandle);

        [StatusCheckLastParameter] int HAL_GetDutyCycleFPGAIndex(int dutyCycleHandle);

        [StatusCheckLastParameter] int HAL_GetDutyCycleFrequency(int dutyCycleHandle);

        [StatusCheckLastParameter] double HAL_GetDutyCycleOutput(int dutyCycleHandle);

        [StatusCheckLastParameter] int HAL_GetDutyCycleOutputRaw(int dutyCycleHandle);

        [StatusCheckLastParameter] int HAL_GetDutyCycleOutputScaleFactor(int dutyCycleHandle);

        [StatusCheckLastParameter] int HAL_InitializeDutyCycle(int digitalSourceHandle, AnalogTriggerType triggerType);

        void HAL_SetDutyCycleSimDevice(int handle, int device);

    }
}
