using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
public unsafe class DIOLowLevelNative : IDIO
{
[NativeFunctionPointer("HAL_AllocateDigitalPWM")]
private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_AllocateDigitalPWMFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_AllocateDigitalPWM()
{
int status = 0;
var retVal = HAL_AllocateDigitalPWMFunc(&status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_CheckDIOChannel")]
private readonly delegate* unmanaged[Cdecl]<int, int> HAL_CheckDIOChannelFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_CheckDIOChannel(int channel)
{
return HAL_CheckDIOChannelFunc(channel);
}


[NativeFunctionPointer("HAL_FreeDIOPort")]
private readonly delegate* unmanaged[Cdecl]<int, void> HAL_FreeDIOPortFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_FreeDIOPort(int dioPortHandle)
{
HAL_FreeDIOPortFunc(dioPortHandle);
}


[NativeFunctionPointer("HAL_SetDIO")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetDIOFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetDIO(int dioPortHandle, int value)
{
int status = 0;
HAL_SetDIOFunc(dioPortHandle, value, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_FreeDigitalPWM")]
private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_FreeDigitalPWMFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_FreeDigitalPWM(int pwmGenerator)
{
int status = 0;
HAL_FreeDigitalPWMFunc(pwmGenerator, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_GetDIO")]
private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetDIOFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetDIO(int dioPortHandle)
{
int status = 0;
var retVal = HAL_GetDIOFunc(dioPortHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetDIODirection")]
private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetDIODirectionFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetDIODirection(int dioPortHandle)
{
int status = 0;
var retVal = HAL_GetDIODirectionFunc(dioPortHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetFilterPeriod")]
private readonly delegate* unmanaged[Cdecl]<int, int*, long> HAL_GetFilterPeriodFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public long HAL_GetFilterPeriod(int filterIndex)
{
int status = 0;
var retVal = HAL_GetFilterPeriodFunc(filterIndex, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetFilterSelect")]
private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_GetFilterSelectFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetFilterSelect(int dioPortHandle)
{
int status = 0;
var retVal = HAL_GetFilterSelectFunc(dioPortHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_InitializeDIOPort")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, int> HAL_InitializeDIOPortFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_InitializeDIOPort(int portHandle, int input)
{
int status = 0;
var retVal = HAL_InitializeDIOPortFunc(portHandle, input, &status);
            Hal.StatusHandling.DIOStatusCheck(status, portHandle);
return retVal;
}


[NativeFunctionPointer("HAL_IsAnyPulsing")]
private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_IsAnyPulsingFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_IsAnyPulsing()
{
int status = 0;
var retVal = HAL_IsAnyPulsingFunc(&status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_IsPulsing")]
private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_IsPulsingFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_IsPulsing(int dioPortHandle)
{
int status = 0;
var retVal = HAL_IsPulsingFunc(dioPortHandle, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_Pulse")]
private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_PulseFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_Pulse(int dioPortHandle, double pulseLength)
{
int status = 0;
HAL_PulseFunc(dioPortHandle, pulseLength, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_SetDIOSimDevice")]
private readonly delegate* unmanaged[Cdecl]<int, int, void> HAL_SetDIOSimDeviceFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetDIOSimDevice(int handle, int device)
{
HAL_SetDIOSimDeviceFunc(handle, device);
}


[NativeFunctionPointer("HAL_SetDIODirection")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetDIODirectionFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetDIODirection(int dioPortHandle, int input)
{
int status = 0;
HAL_SetDIODirectionFunc(dioPortHandle, input, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_SetDigitalPWMDutyCycle")]
private readonly delegate* unmanaged[Cdecl]<int, double, int*, void> HAL_SetDigitalPWMDutyCycleFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetDigitalPWMDutyCycle(int pwmGenerator, double dutyCycle)
{
int status = 0;
HAL_SetDigitalPWMDutyCycleFunc(pwmGenerator, dutyCycle, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_SetDigitalPWMOutputChannel")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetDigitalPWMOutputChannelFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetDigitalPWMOutputChannel(int pwmGenerator, int channel)
{
int status = 0;
HAL_SetDigitalPWMOutputChannelFunc(pwmGenerator, channel, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_SetDigitalPWMRate")]
private readonly delegate* unmanaged[Cdecl]<double, int*, void> HAL_SetDigitalPWMRateFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetDigitalPWMRate(double rate)
{
int status = 0;
HAL_SetDigitalPWMRateFunc(rate, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_SetFilterPeriod")]
private readonly delegate* unmanaged[Cdecl]<int, long, int*, void> HAL_SetFilterPeriodFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetFilterPeriod(int filterIndex, long value)
{
int status = 0;
HAL_SetFilterPeriodFunc(filterIndex, value, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_SetFilterSelect")]
private readonly delegate* unmanaged[Cdecl]<int, int, int*, void> HAL_SetFilterSelectFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetFilterSelect(int dioPortHandle, int filterIndex)
{
int status = 0;
HAL_SetFilterSelectFunc(dioPortHandle, filterIndex, &status);
Hal.StatusHandling.StatusCheck(status);
}



}
}
