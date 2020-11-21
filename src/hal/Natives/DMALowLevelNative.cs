using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
public unsafe class DMALowLevelNative : IDMA
{
[NativeFunctionPointer("HAL_AddDMAAnalogAccumulator")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMAAnalogAccumulatorFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_AddDMAAnalogAccumulator(int handle, int aInHandle)
{
int status = 0;
HAL_AddDMAAnalogAccumulatorFunc(handle, aInHandle, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_AddDMAAnalogInput")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMAAnalogInputFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_AddDMAAnalogInput(int handle, int aInHandle)
{
int status = 0;
HAL_AddDMAAnalogInputFunc(handle, aInHandle, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_AddDMAAveragedAnalogInput")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMAAveragedAnalogInputFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_AddDMAAveragedAnalogInput(int handle, int aInHandle)
{
int status = 0;
HAL_AddDMAAveragedAnalogInputFunc(handle, aInHandle, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_AddDMACounter")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMACounterFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_AddDMACounter(int handle, int counterHandle)
{
int status = 0;
HAL_AddDMACounterFunc(handle, counterHandle, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_AddDMACounterPeriod")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMACounterPeriodFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_AddDMACounterPeriod(int handle, int counterHandle)
{
int status = 0;
HAL_AddDMACounterPeriodFunc(handle, counterHandle, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_AddDMADigitalSource")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMADigitalSourceFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_AddDMADigitalSource(int handle, int digitalSourceHandle)
{
int status = 0;
HAL_AddDMADigitalSourceFunc(handle, digitalSourceHandle, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_AddDMADutyCycle")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMADutyCycleFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_AddDMADutyCycle(int handle, int dutyCycleHandle)
{
int status = 0;
HAL_AddDMADutyCycleFunc(handle, dutyCycleHandle, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_AddDMAEncoder")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMAEncoderFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_AddDMAEncoder(int handle, int encoderHandle)
{
int status = 0;
HAL_AddDMAEncoderFunc(handle, encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_AddDMAEncoderPeriod")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_AddDMAEncoderPeriodFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_AddDMAEncoderPeriod(int handle, int encoderHandle)
{
int status = 0;
HAL_AddDMAEncoderPeriodFunc(handle, encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_FreeDMA")]
private readonly delegate* unmanaged[Cdecl]<int, void> HAL_FreeDMAFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_FreeDMA(int handle)
{
HAL_FreeDMAFunc(handle);
}


[NativeFunctionPointer("HAL_GetDMADirectPointer")]
private readonly delegate* unmanaged[Cdecl]<int, void*> HAL_GetDMADirectPointerFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void* HAL_GetDMADirectPointer(int handle)
{
return HAL_GetDMADirectPointerFunc(handle);
}


[NativeFunctionPointer("HAL_GetDMASampleAnalogAccumulator")]
private readonly delegate* unmanaged[Cdecl]<DMASample*, int, long*, long*, int*, void> HAL_GetDMASampleAnalogAccumulatorFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_GetDMASampleAnalogAccumulator(DMASample* dmaSample, int aInHandle, long* count, long* value)
{
int status = 0;
HAL_GetDMASampleAnalogAccumulatorFunc(dmaSample, aInHandle, count, value, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_GetDMASampleAnalogInputRaw")]
private readonly delegate* unmanaged[Cdecl]<DMASample*, int, int*, int> HAL_GetDMASampleAnalogInputRawFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetDMASampleAnalogInputRaw(DMASample* dmaSample, int aInHandle)
{
int status = 0;
var retVal = HAL_GetDMASampleAnalogInputRawFunc(dmaSample, aInHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetDMASampleAveragedAnalogInputRaw")]
private readonly delegate* unmanaged[Cdecl]<DMASample*, int, int*, int> HAL_GetDMASampleAveragedAnalogInputRawFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetDMASampleAveragedAnalogInputRaw(DMASample* dmaSample, int aInHandle)
{
int status = 0;
var retVal = HAL_GetDMASampleAveragedAnalogInputRawFunc(dmaSample, aInHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetDMASampleCounter")]
private readonly delegate* unmanaged[Cdecl]<DMASample*, int, int*, int> HAL_GetDMASampleCounterFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetDMASampleCounter(DMASample* dmaSample, int counterHandle)
{
int status = 0;
var retVal = HAL_GetDMASampleCounterFunc(dmaSample, counterHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetDMASampleCounterPeriod")]
private readonly delegate* unmanaged[Cdecl]<DMASample*, int, int*, int> HAL_GetDMASampleCounterPeriodFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetDMASampleCounterPeriod(DMASample* dmaSample, int counterHandle)
{
int status = 0;
var retVal = HAL_GetDMASampleCounterPeriodFunc(dmaSample, counterHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetDMASampleDigitalSource")]
private readonly delegate* unmanaged[Cdecl]<DMASample*, int, int*, int> HAL_GetDMASampleDigitalSourceFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetDMASampleDigitalSource(DMASample* dmaSample, int dSourceHandle)
{
int status = 0;
var retVal = HAL_GetDMASampleDigitalSourceFunc(dmaSample, dSourceHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetDMASampleDutyCycleOutputRaw")]
private readonly delegate* unmanaged[Cdecl]<DMASample*, int, int*, int> HAL_GetDMASampleDutyCycleOutputRawFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetDMASampleDutyCycleOutputRaw(DMASample* dmaSample, int dutyCycleHandle)
{
int status = 0;
var retVal = HAL_GetDMASampleDutyCycleOutputRawFunc(dmaSample, dutyCycleHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetDMASampleEncoderPeriodRaw")]
private readonly delegate* unmanaged[Cdecl]<DMASample*, int, int*, int> HAL_GetDMASampleEncoderPeriodRawFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetDMASampleEncoderPeriodRaw(DMASample* dmaSample, int encoderHandle)
{
int status = 0;
var retVal = HAL_GetDMASampleEncoderPeriodRawFunc(dmaSample, encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetDMASampleEncoderRaw")]
private readonly delegate* unmanaged[Cdecl]<DMASample*, int, int*, int> HAL_GetDMASampleEncoderRawFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetDMASampleEncoderRaw(DMASample* dmaSample, int encoderHandle)
{
int status = 0;
var retVal = HAL_GetDMASampleEncoderRawFunc(dmaSample, encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetDMASampleTime")]
private readonly delegate* unmanaged[Cdecl]<DMASample*, int*, ulong> HAL_GetDMASampleTimeFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public ulong HAL_GetDMASampleTime(DMASample* dmaSample)
{
int status = 0;
var retVal = HAL_GetDMASampleTimeFunc(dmaSample, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_InitializeDMA")]
private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_InitializeDMAFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_InitializeDMA()
{
int status = 0;
var retVal = HAL_InitializeDMAFunc(&status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_ReadDMADirect")]
private readonly delegate* unmanaged[Cdecl]<void*, DMASample*, int, int*, int*, DMAReadStatus> HAL_ReadDMADirectFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public DMAReadStatus HAL_ReadDMADirect(void* dmaPointer, DMASample* dmaSample, int timeoutMs, int* remainingOut)
{
int status = 0;
var retVal = HAL_ReadDMADirectFunc(dmaPointer, dmaSample, timeoutMs, remainingOut, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_SetDMAExternalTrigger")]
private readonly delegate* unmanaged[Cdecl]<int, int, AnalogTriggerType, int, int, int*, void> HAL_SetDMAExternalTriggerFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetDMAExternalTrigger(int handle, int digitalSourceHandle, AnalogTriggerType analogTriggerType, int rising, int falling)
{
int status = 0;
HAL_SetDMAExternalTriggerFunc(handle, digitalSourceHandle, analogTriggerType, rising, falling, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_SetDMAPause")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetDMAPauseFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetDMAPause(int handle, int pause)
{
int status = 0;
HAL_SetDMAPauseFunc(handle, pause, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_SetDMARate")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetDMARateFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetDMARate(int handle, int cycles)
{
int status = 0;
HAL_SetDMARateFunc(handle, cycles, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_StartDMA")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_StartDMAFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_StartDMA(int handle, int queueDepth)
{
int status = 0;
HAL_StartDMAFunc(handle, queueDepth, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_StopDMA")]
private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_StopDMAFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_StopDMA(int handle)
{
int status = 0;
HAL_StopDMAFunc(handle, &status);
Hal.StatusHandling.StatusCheck(status);
}



}
}
