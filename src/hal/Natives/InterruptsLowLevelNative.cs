using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class InterruptsLowLevelNative
    {
        public InterruptsLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_AttachInterruptHandlerFunc = (delegate* unmanaged[Cdecl] < System.Int32, delegate* unmanaged[Cdecl] < uint, void *, void >, void *, int *, void >)loader.GetProcAddress("HAL_AttachInterruptHandler");
            HAL_AttachInterruptHandlerThreadedFunc = (delegate* unmanaged[Cdecl] < System.Int32, delegate* unmanaged[Cdecl] < uint, void *, void >, void *, int *, void >)loader.GetProcAddress("HAL_AttachInterruptHandlerThreaded");
            HAL_CleanInterruptsFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void *>)loader.GetProcAddress("HAL_CleanInterrupts");
            HAL_DisableInterruptsFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_DisableInterrupts");
            HAL_DisableInterruptsFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32 *, void >)loader.GetProcAddress("HAL_DisableInterrupts");
            HAL_EnableInterruptsFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_EnableInterrupts");
            HAL_EnableInterruptsFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32 *, void >)loader.GetProcAddress("HAL_EnableInterrupts");
            HAL_InitializeInterruptsFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int32 >)loader.GetProcAddress("HAL_InitializeInterrupts");
            HAL_ReadInterruptFallingTimestampFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int64 >)loader.GetProcAddress("HAL_ReadInterruptFallingTimestamp");
            HAL_ReadInterruptRisingTimestampFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, System.Int64 >)loader.GetProcAddress("HAL_ReadInterruptRisingTimestamp");
            HAL_RequestInterruptsFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, Hal.AnalogTriggerType, int *, void >)loader.GetProcAddress("HAL_RequestInterrupts");
            HAL_SetInterruptUpSourceEdgeFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Int32, System.Int32, int *, void >)loader.GetProcAddress("HAL_SetInterruptUpSourceEdge");
            HAL_WaitForInterruptFunc = (delegate* unmanaged[Cdecl] < System.Int32, System.Double, System.Int32, int *, System.Int64 >)loader.GetProcAddress("HAL_WaitForInterrupt");
            HAL_ReleaseWaitingInterruptFunc = (delegate* unmanaged[Cdecl] < System.Int32, int *, void >)loader.GetProcAddress("HAL_ReleaseWaitingInterrupt");
        }

        private readonly delegate* unmanaged[Cdecl]<int, delegate* unmanaged[Cdecl]<uint, void*, void>, void*, int*, void> HAL_AttachInterruptHandlerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_AttachInterruptHandler(int interruptHandle, delegate* unmanaged[Cdecl]<uint, void*, void> handler, void* param)
        {
            int status = 0;
        HAL_AttachInterruptHandlerFunc(interruptHandle, handler, param, &status);
        Hal.StatusHandling.StatusCheck(status);
        }



    private readonly delegate* unmanaged[Cdecl]<int, delegate* unmanaged[Cdecl]<uint, void*, void>, void*, int*, void> HAL_AttachInterruptHandlerThreadedFunc;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void HAL_AttachInterruptHandlerThreaded(int interruptHandle, delegate* unmanaged[Cdecl]<uint, void*, void> handler, void* param)
        {
            int status = 0;
    HAL_AttachInterruptHandlerThreadedFunc(interruptHandle, handler, param, &status);
    Hal.StatusHandling.StatusCheck(status);
        }



private readonly delegate* unmanaged[Cdecl]<int, int*, void*> HAL_CleanInterruptsFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void* HAL_CleanInterrupts(int interruptHandle)
{
    int status = 0;
    var retVal = HAL_CleanInterruptsFunc(interruptHandle, &status);
    Hal.StatusHandling.StatusCheck(status);
    return retVal;
}



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



private readonly delegate* unmanaged[Cdecl]<int, int*, int> HAL_InitializeInterruptsFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_InitializeInterrupts(int watcher)
{
    int status = 0;
    var retVal = HAL_InitializeInterruptsFunc(watcher, &status);
    Hal.StatusHandling.StatusCheck(status);
    return retVal;
}



private readonly delegate* unmanaged[Cdecl]<int, int*, long> HAL_ReadInterruptFallingTimestampFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public long HAL_ReadInterruptFallingTimestamp(int interruptHandle)
{
    int status = 0;
    var retVal = HAL_ReadInterruptFallingTimestampFunc(interruptHandle, &status);
    Hal.StatusHandling.StatusCheck(status);
    return retVal;
}



private readonly delegate* unmanaged[Cdecl]<int, int*, long> HAL_ReadInterruptRisingTimestampFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public long HAL_ReadInterruptRisingTimestamp(int interruptHandle)
{
    int status = 0;
    var retVal = HAL_ReadInterruptRisingTimestampFunc(interruptHandle, &status);
    Hal.StatusHandling.StatusCheck(status);
    return retVal;
}



private readonly delegate* unmanaged[Cdecl]<int, int, AnalogTriggerType, int*, void> HAL_RequestInterruptsFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_RequestInterrupts(int interruptHandle, int digitalSourceHandle, AnalogTriggerType analogTriggerType)
{
    int status = 0;
    HAL_RequestInterruptsFunc(interruptHandle, digitalSourceHandle, analogTriggerType, &status);
    Hal.StatusHandling.StatusCheck(status);
}



private readonly delegate* unmanaged[Cdecl]<int, int, int, int*, void> HAL_SetInterruptUpSourceEdgeFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_SetInterruptUpSourceEdge(int interruptHandle, int risingEdge, int fallingEdge)
{
    int status = 0;
    HAL_SetInterruptUpSourceEdgeFunc(interruptHandle, risingEdge, fallingEdge, &status);
    Hal.StatusHandling.StatusCheck(status);
}



private readonly delegate* unmanaged[Cdecl]<int, double, int, int*, long> HAL_WaitForInterruptFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public long HAL_WaitForInterrupt(int interruptHandle, double timeout, int ignorePrevious)
{
    int status = 0;
    var retVal = HAL_WaitForInterruptFunc(interruptHandle, timeout, ignorePrevious, &status);
    Hal.StatusHandling.StatusCheck(status);
    return retVal;
}



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
