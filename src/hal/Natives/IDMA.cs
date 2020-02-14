using WPIUtil.ILGeneration;

namespace Hal.Natives
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IDMA
    {
        [StatusCheckLastParameter] void HAL_AddDMAAnalogAccumulator(int handle, int aInHandle);

        [StatusCheckLastParameter] void HAL_AddDMAAnalogInput(int handle, int aInHandle);

        [StatusCheckLastParameter] void HAL_AddDMAAveragedAnalogInput(int handle, int aInHandle);

        [StatusCheckLastParameter] void HAL_AddDMACounter(int handle, int counterHandle);

        [StatusCheckLastParameter] void HAL_AddDMACounterPeriod(int handle, int counterHandle);

        [StatusCheckLastParameter] void HAL_AddDMADigitalSource(int handle, int digitalSourceHandle);

        [StatusCheckLastParameter] void HAL_AddDMADutyCycle(int handle, int dutyCycleHandle);

        [StatusCheckLastParameter] void HAL_AddDMAEncoder(int handle, int encoderHandle);

        [StatusCheckLastParameter] void HAL_AddDMAEncoderPeriod(int handle, int encoderHandle);

        void HAL_FreeDMA(int handle);

        void* HAL_GetDMADirectPointer(int handle);

        [StatusCheckLastParameter] void HAL_GetDMASampleAnalogAccumulator(DMASample* dmaSample, int aInHandle, long* count, long* value);

        [StatusCheckLastParameter] int HAL_GetDMASampleAnalogInputRaw(DMASample* dmaSample, int aInHandle);

        [StatusCheckLastParameter] int HAL_GetDMASampleAveragedAnalogInputRaw(DMASample* dmaSample, int aInHandle);

        [StatusCheckLastParameter] int HAL_GetDMASampleCounter(DMASample* dmaSample, int counterHandle);

        [StatusCheckLastParameter] int HAL_GetDMASampleCounterPeriod(DMASample* dmaSample, int counterHandle);

        [StatusCheckLastParameter] int HAL_GetDMASampleDigitalSource(DMASample* dmaSample, int dSourceHandle);

        [StatusCheckLastParameter] int HAL_GetDMASampleDutyCycleOutputRaw(DMASample* dmaSample, int dutyCycleHandle);

        [StatusCheckLastParameter] int HAL_GetDMASampleEncoderPeriodRaw(DMASample* dmaSample, int encoderHandle);

        [StatusCheckLastParameter] int HAL_GetDMASampleEncoderRaw(DMASample* dmaSample, int encoderHandle);

        [StatusCheckLastParameter] ulong HAL_GetDMASampleTime(DMASample* dmaSample);

        [StatusCheckLastParameter] int HAL_InitializeDMA();

        [StatusCheckLastParameter] DMAReadStatus HAL_ReadDMADirect(void* dmaPointer, DMASample* dmaSample, int timeoutMs, int* remainingOut);

        [StatusCheckLastParameter] void HAL_SetDMAExternalTrigger(int handle, int digitalSourceHandle, AnalogTriggerType analogTriggerType, int rising, int falling);

        [StatusCheckLastParameter] void HAL_SetDMAPause(int handle, int pause);

        [StatusCheckLastParameter] void HAL_SetDMARate(int handle, int cycles);

        [StatusCheckLastParameter] void HAL_StartDMA(int handle, int queueDepth);

        [StatusCheckLastParameter] void HAL_StopDMA(int handle);

    }
}
