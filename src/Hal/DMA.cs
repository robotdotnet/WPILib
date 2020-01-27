
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IDMA))]
    public unsafe static class DMA
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IDMA lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

public static void AddDMAAnalogAccumulator(int handle, int aInHandle)
{
lowLevel.HAL_AddDMAAnalogAccumulator(handle, aInHandle);
}

public static void AddDMAAnalogInput(int handle, int aInHandle)
{
lowLevel.HAL_AddDMAAnalogInput(handle, aInHandle);
}

public static void AddDMAAveragedAnalogInput(int handle, int aInHandle)
{
lowLevel.HAL_AddDMAAveragedAnalogInput(handle, aInHandle);
}

public static void AddDMACounter(int handle, int counterHandle)
{
lowLevel.HAL_AddDMACounter(handle, counterHandle);
}

public static void AddDMACounterPeriod(int handle, int counterHandle)
{
lowLevel.HAL_AddDMACounterPeriod(handle, counterHandle);
}

public static void AddDMADigitalSource(int handle, int digitalSourceHandle)
{
lowLevel.HAL_AddDMADigitalSource(handle, digitalSourceHandle);
}

public static void AddDMADutyCycle(int handle, int dutyCycleHandle)
{
lowLevel.HAL_AddDMADutyCycle(handle, dutyCycleHandle);
}

public static void AddDMAEncoder(int handle, int encoderHandle)
{
lowLevel.HAL_AddDMAEncoder(handle, encoderHandle);
}

public static void AddDMAEncoderPeriod(int handle, int encoderHandle)
{
lowLevel.HAL_AddDMAEncoderPeriod(handle, encoderHandle);
}

public static void FreeDMA(int handle)
{
lowLevel.HAL_FreeDMA(handle);
}

public static void* GetDMADirectPointer(int handle)
{
return lowLevel.HAL_GetDMADirectPointer(handle);
}

public static void GetDMASampleAnalogAccumulator(DMASample* dmaSample, int aInHandle, long* count, long* value)
{
lowLevel.HAL_GetDMASampleAnalogAccumulator(dmaSample, aInHandle, count, value);
}

public static int GetDMASampleAnalogInputRaw(DMASample* dmaSample, int aInHandle)
{
return lowLevel.HAL_GetDMASampleAnalogInputRaw(dmaSample, aInHandle);
}

public static int GetDMASampleAveragedAnalogInputRaw(DMASample* dmaSample, int aInHandle)
{
return lowLevel.HAL_GetDMASampleAveragedAnalogInputRaw(dmaSample, aInHandle);
}

public static int GetDMASampleCounter(DMASample* dmaSample, int counterHandle)
{
return lowLevel.HAL_GetDMASampleCounter(dmaSample, counterHandle);
}

public static int GetDMASampleCounterPeriod(DMASample* dmaSample, int counterHandle)
{
return lowLevel.HAL_GetDMASampleCounterPeriod(dmaSample, counterHandle);
}

public static int GetDMASampleDigitalSource(DMASample* dmaSample, int dSourceHandle)
{
return lowLevel.HAL_GetDMASampleDigitalSource(dmaSample, dSourceHandle);
}

public static int GetDMASampleDutyCycleOutputRaw(DMASample* dmaSample, int dutyCycleHandle)
{
return lowLevel.HAL_GetDMASampleDutyCycleOutputRaw(dmaSample, dutyCycleHandle);
}

public static int GetDMASampleEncoderPeriodRaw(DMASample* dmaSample, int encoderHandle)
{
return lowLevel.HAL_GetDMASampleEncoderPeriodRaw(dmaSample, encoderHandle);
}

public static int GetDMASampleEncoderRaw(DMASample* dmaSample, int encoderHandle)
{
return lowLevel.HAL_GetDMASampleEncoderRaw(dmaSample, encoderHandle);
}

public static ulong GetDMASampleTime(DMASample* dmaSample)
{
return lowLevel.HAL_GetDMASampleTime(dmaSample);
}

public static int InitializeDMA()
{
return lowLevel.HAL_InitializeDMA();
}

public static DMAReadStatus ReadDMADirect(void* dmaPointer, DMASample* dmaSample, int timeoutMs, int* remainingOut)
{
return lowLevel.HAL_ReadDMADirect(dmaPointer, dmaSample, timeoutMs, remainingOut);
}

public static void SetDMAExternalTrigger(int handle, int digitalSourceHandle, AnalogTriggerType analogTriggerType, int rising, int falling)
{
lowLevel.HAL_SetDMAExternalTrigger(handle, digitalSourceHandle, analogTriggerType, rising, falling);
}

public static void SetDMAPause(int handle, int pause)
{
lowLevel.HAL_SetDMAPause(handle, pause);
}

public static void SetDMARate(int handle, int cycles)
{
lowLevel.HAL_SetDMARate(handle, cycles);
}

public static void StartDMA(int handle, int queueDepth)
{
lowLevel.HAL_StartDMA(handle, queueDepth);
}

public static void StopDMA(int handle)
{
lowLevel.HAL_StopDMA(handle);
}

}
}
