using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class InterruptsLowLevelNative
    {
        [NativeFunctionPointer("HAL_AttachInterruptHandler")]
        private readonly delegate* unmanaged[Cdecl]<int, delegate* unmanaged[Cdecl]<uint, void*, void>, void*, int*, void> HAL_AttachInterruptHandlerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_AttachInterruptHandler(int interruptHandle, delegate* unmanaged[Cdecl]<uint, void*, void> handler, void* param)
    {
int status = 0;
        HAL_AttachInterruptHandlerFunc(interruptHandle, handler, param, &status);
        Hal.StatusHandling.StatusCheck(status);
}


    [NativeFunctionPointer("HAL_AttachInterruptHandlerThreaded")]
    private readonly delegate* unmanaged[Cdecl]<int, delegate* unmanaged[Cdecl]<uint, void*, void>, void*, int*, void> HAL_AttachInterruptHandlerThreadedFunc;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void HAL_AttachInterruptHandlerThreaded(int interruptHandle, delegate* unmanaged[Cdecl]<uint, void*, void> handler, void* param)
{
int status = 0;
    HAL_AttachInterruptHandlerThreadedFunc(interruptHandle, handler, param, &status);
    Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_CleanInterrupts")]
private readonly delegate* unmanaged[Cdecl]<int, int*, void*> HAL_CleanInterruptsFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void* HAL_CleanInterrupts(int interruptHandle)
{
    int status = 0;
    var retVal = HAL_CleanInterruptsFunc(interruptHandle, &status);
    Hal.StatusHandling.StatusCheck(status);
    return retVal;
}


[NativeFunctionPointer("HAL_DisableInterrupts")]
private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_DisableInterruptsFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_DisableInterrupts(int interruptHandle)
{
    int status = 0;
    HAL_DisableInterruptsFunc(interruptHandle, &status);
    Hal.StatusHandling.StatusCheck(status);
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_DisableInterrupts(int interruptHandle, int* status)
{
    HAL_DisableInterruptsFunc(interruptHandle, status);
}


[NativeFunctionPointer("HAL_EnableInterrupts")]
private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_EnableInterruptsFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_EnableInterrupts(int interruptHandle)
{
    int status = 0;
    HAL_EnableInterruptsFunc(interruptHandle, &status);
    Hal.StatusHandling.StatusCheck(status);
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_EnableInterrupts(int interruptHandle, int* status)
{
    HAL_EnableInterruptsFunc(interruptHandle, status);
}


[NativeFunctionPointer("HAL_InitializeInterrupts")]
private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_InitializeInterruptsFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_InitializeInterrupts(int watcher)
{
    int status = 0;
    var retVal = HAL_InitializeInterruptsFunc(watcher, &status);
    Hal.StatusHandling.StatusCheck(status);
    return retVal;
}


[NativeFunctionPointer("HAL_ReadInterruptFallingTimestamp")]
private readonly delegate* unmanaged[Cdecl]<int, int*, long> HAL_ReadInterruptFallingTimestampFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public long HAL_ReadInterruptFallingTimestamp(int interruptHandle)
{
    int status = 0;
    var retVal = HAL_ReadInterruptFallingTimestampFunc(interruptHandle, &status);
    Hal.StatusHandling.StatusCheck(status);
    return retVal;
}


[NativeFunctionPointer("HAL_ReadInterruptRisingTimestamp")]
private readonly delegate* unmanaged[Cdecl]<int, int*, long> HAL_ReadInterruptRisingTimestampFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public long HAL_ReadInterruptRisingTimestamp(int interruptHandle)
{
    int status = 0;
    var retVal = HAL_ReadInterruptRisingTimestampFunc(interruptHandle, &status);
    Hal.StatusHandling.StatusCheck(status);
    return retVal;
}


[NativeFunctionPointer("HAL_RequestInterrupts")]
private readonly delegate* unmanaged[Cdecl]<int, int, AnalogTriggerType, int*, void> HAL_RequestInterruptsFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_RequestInterrupts(int interruptHandle, int digitalSourceHandle, AnalogTriggerType analogTriggerType)
{
    int status = 0;
    HAL_RequestInterruptsFunc(interruptHandle, digitalSourceHandle, analogTriggerType, &status);
    Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_SetInterruptUpSourceEdge")]
private readonly delegate* unmanaged[Cdecl]<int, int, int, int*, void> HAL_SetInterruptUpSourceEdgeFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetInterruptUpSourceEdge(int interruptHandle, int risingEdge, int fallingEdge)
{
    int status = 0;
    HAL_SetInterruptUpSourceEdgeFunc(interruptHandle, risingEdge, fallingEdge, &status);
    Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_WaitForInterrupt")]
private readonly delegate* unmanaged[Cdecl]<int, double, int, int*, long> HAL_WaitForInterruptFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public long HAL_WaitForInterrupt(int interruptHandle, double timeout, int ignorePrevious)
{
    int status = 0;
    var retVal = HAL_WaitForInterruptFunc(interruptHandle, timeout, ignorePrevious, &status);
    Hal.StatusHandling.StatusCheck(status);
    return retVal;
}


[NativeFunctionPointer("HAL_ReleaseWaitingInterrupt")]
private readonly delegate* unmanaged[Cdecl]<int, int*, void> HAL_ReleaseWaitingInterruptFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_ReleaseWaitingInterrupt(int interruptHandle)
{
    int status = 0;
    HAL_ReleaseWaitingInterruptFunc(interruptHandle, &status);
    Hal.StatusHandling.StatusCheck(status);
}



}
}
