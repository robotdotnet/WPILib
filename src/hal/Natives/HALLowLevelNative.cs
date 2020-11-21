﻿using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
public unsafe class HALLowLevelNative : IHAL
{
[NativeFunctionPointer("HAL_Initialize")]
private readonly delegate* unmanaged[Cdecl]<int, int, int> HAL_InitializeFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_Initialize(int timeout, int mode)
{
return HAL_InitializeFunc(timeout, mode);
}


[NativeFunctionPointer("HAL_ExpandFPGATime")]
private readonly delegate* unmanaged[Cdecl]<uint, int*, ulong> HAL_ExpandFPGATimeFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public ulong HAL_ExpandFPGATime(uint unexpanded_lower)
{
int status = 0;
var retVal = HAL_ExpandFPGATimeFunc(unexpanded_lower, &status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetBrownedOut")]
private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetBrownedOutFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetBrownedOut()
{
int status = 0;
var retVal = HAL_GetBrownedOutFunc(&status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetErrorMessage")]
private readonly delegate* unmanaged[Cdecl]<int, byte*> HAL_GetErrorMessageFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public byte* HAL_GetErrorMessage(int code)
{
return HAL_GetErrorMessageFunc(code);
}


[NativeFunctionPointer("HAL_GetFPGAButton")]
private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetFPGAButtonFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetFPGAButton()
{
int status = 0;
var retVal = HAL_GetFPGAButtonFunc(&status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetFPGARevision")]
private readonly delegate* unmanaged[Cdecl]<int*, long> HAL_GetFPGARevisionFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public long HAL_GetFPGARevision()
{
int status = 0;
var retVal = HAL_GetFPGARevisionFunc(&status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetFPGATime")]
private readonly delegate* unmanaged[Cdecl]<int*, ulong> HAL_GetFPGATimeFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public ulong HAL_GetFPGATime()
{
int status = 0;
var retVal = HAL_GetFPGATimeFunc(&status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetFPGAVersion")]
private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetFPGAVersionFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetFPGAVersion()
{
int status = 0;
var retVal = HAL_GetFPGAVersionFunc(&status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}


[NativeFunctionPointer("HAL_GetPort")]
private readonly delegate* unmanaged[Cdecl]<int, int> HAL_GetPortFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetPort(int channel)
{
return HAL_GetPortFunc(channel);
}


[NativeFunctionPointer("HAL_GetPortWithModule")]
private readonly delegate* unmanaged[Cdecl]<int, int, int> HAL_GetPortWithModuleFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetPortWithModule(int module, int channel)
{
return HAL_GetPortWithModuleFunc(module, channel);
}


[NativeFunctionPointer("HAL_GetRuntimeType")]
private readonly delegate* unmanaged[Cdecl]<RuntimeType> HAL_GetRuntimeTypeFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public RuntimeType HAL_GetRuntimeType()
{
return HAL_GetRuntimeTypeFunc();
}


[NativeFunctionPointer("HAL_GetSystemActive")]
private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetSystemActiveFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_GetSystemActive()
{
int status = 0;
var retVal = HAL_GetSystemActiveFunc(&status);
Hal.StatusHandling.StatusCheck(status);
return retVal;
}



}
}
