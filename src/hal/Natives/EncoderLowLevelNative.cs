using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
public unsafe class EncoderLowLevelNative : IEncoder
{
[NativeFunctionPointer("HAL_FreeEncoder")]
private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_FreeEncoderFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_FreeEncoder(int encoderHandle)
{
int status = 0;
HAL_FreeEncoderFunc(encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_GetEncoder")]
private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetEncoderFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetEncoder(int encoderHandle)
{
int status = 0;
var retVal = HAL_GetEncoderFunc(encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetEncoderDecodingScaleFactor")]
private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetEncoderDecodingScaleFactorFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public double HAL_GetEncoderDecodingScaleFactor(int encoderHandle)
{
int status = 0;
var retVal = HAL_GetEncoderDecodingScaleFactorFunc(encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetEncoderDirection")]
private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetEncoderDirectionFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetEncoderDirection(int encoderHandle)
{
int status = 0;
var retVal = HAL_GetEncoderDirectionFunc(encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetEncoderDistance")]
private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetEncoderDistanceFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public double HAL_GetEncoderDistance(int encoderHandle)
{
int status = 0;
var retVal = HAL_GetEncoderDistanceFunc(encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetEncoderDistancePerPulse")]
private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetEncoderDistancePerPulseFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public double HAL_GetEncoderDistancePerPulse(int encoderHandle)
{
int status = 0;
var retVal = HAL_GetEncoderDistancePerPulseFunc(encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetEncoderEncodingScale")]
private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetEncoderEncodingScaleFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetEncoderEncodingScale(int encoderHandle)
{
int status = 0;
var retVal = HAL_GetEncoderEncodingScaleFunc(encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetEncoderEncodingType")]
private readonly delegate* unmanaged[Cdecl]<int, int*, EncoderEncodingType> HAL_GetEncoderEncodingTypeFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public EncoderEncodingType HAL_GetEncoderEncodingType(int encoderHandle)
{
int status = 0;
var retVal = HAL_GetEncoderEncodingTypeFunc(encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetEncoderFPGAIndex")]
private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetEncoderFPGAIndexFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetEncoderFPGAIndex(int encoderHandle)
{
int status = 0;
var retVal = HAL_GetEncoderFPGAIndexFunc(encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetEncoderPeriod")]
private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetEncoderPeriodFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public double HAL_GetEncoderPeriod(int encoderHandle)
{
int status = 0;
var retVal = HAL_GetEncoderPeriodFunc(encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetEncoderRate")]
private readonly delegate* unmanaged[Cdecl]<int, int*, double> HAL_GetEncoderRateFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public double HAL_GetEncoderRate(int encoderHandle)
{
int status = 0;
var retVal = HAL_GetEncoderRateFunc(encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetEncoderRaw")]
private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetEncoderRawFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetEncoderRaw(int encoderHandle)
{
int status = 0;
var retVal = HAL_GetEncoderRawFunc(encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetEncoderSamplesToAverage")]
private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetEncoderSamplesToAverageFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetEncoderSamplesToAverage(int encoderHandle)
{
int status = 0;
var retVal = HAL_GetEncoderSamplesToAverageFunc(encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_InitializeEncoder")]
private readonly delegate* unmanaged[Cdecl]<int, AnalogTriggerType, int, AnalogTriggerType, int, EncoderEncodingType, int*, int> HAL_InitializeEncoderFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_InitializeEncoder(int digitalSourceHandleA, AnalogTriggerType analogTriggerTypeA, int digitalSourceHandleB, AnalogTriggerType analogTriggerTypeB, int reverseDirection, EncoderEncodingType encodingType)
{
int status = 0;
var retVal = HAL_InitializeEncoderFunc(digitalSourceHandleA, analogTriggerTypeA, digitalSourceHandleB, analogTriggerTypeB, reverseDirection, encodingType, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_ResetEncoder")]
private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_ResetEncoderFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_ResetEncoder(int encoderHandle)
{
int status = 0;
HAL_ResetEncoderFunc(encoderHandle, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_SetEncoderIndexSource")]
private readonly delegate* unmanaged[Cdecl]<int, int, AnalogTriggerType, EncoderIndexingType, int*, void> HAL_SetEncoderIndexSourceFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetEncoderIndexSource(int encoderHandle, int digitalSourceHandle, AnalogTriggerType analogTriggerType, EncoderIndexingType type)
{
int status = 0;
HAL_SetEncoderIndexSourceFunc(encoderHandle, digitalSourceHandle, analogTriggerType, type, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_SetEncoderMaxPeriod")]
private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_SetEncoderMaxPeriodFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetEncoderMaxPeriod(int encoderHandle, double maxPeriod)
{
int status = 0;
HAL_SetEncoderMaxPeriodFunc(encoderHandle, maxPeriod, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_SetEncoderMinRate")]
private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_SetEncoderMinRateFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetEncoderMinRate(int encoderHandle, double minRate)
{
int status = 0;
HAL_SetEncoderMinRateFunc(encoderHandle, minRate, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_SetEncoderReverseDirection")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetEncoderReverseDirectionFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetEncoderReverseDirection(int encoderHandle, int reverseDirection)
{
int status = 0;
HAL_SetEncoderReverseDirectionFunc(encoderHandle, reverseDirection, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_SetEncoderSamplesToAverage")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetEncoderSamplesToAverageFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetEncoderSamplesToAverage(int encoderHandle, int samplesToAverage)
{
int status = 0;
HAL_SetEncoderSamplesToAverageFunc(encoderHandle, samplesToAverage, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_SetEncoderSimDevice")]
private readonly delegate* unmanaged[Cdecl]<int, int, void> HAL_SetEncoderSimDeviceFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetEncoderSimDevice(int handle, int device)
{
HAL_SetEncoderSimDeviceFunc(handle, device);
}



}
}
