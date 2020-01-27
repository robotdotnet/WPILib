
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IInterrupts))]
    public unsafe static class Interrupts
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IInterrupts lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

public static void AttachInterruptHandler(int interruptHandle, IntPtr handler, void* param)
{
lowLevel.HAL_AttachInterruptHandler(interruptHandle, handler, param);
}

public static void AttachInterruptHandlerThreaded(int interruptHandle, IntPtr handler, void* param)
{
lowLevel.HAL_AttachInterruptHandlerThreaded(interruptHandle, handler, param);
}

public static void* Clean(int interruptHandle)
{
return lowLevel.HAL_CleanInterrupts(interruptHandle);
}

public static void Disable(int interruptHandle)
{
lowLevel.HAL_DisableInterrupts(interruptHandle);
}

public static void Enable(int interruptHandle)
{
lowLevel.HAL_EnableInterrupts(interruptHandle);
}

public static int Initialize(int watcher)
{
return lowLevel.HAL_InitializeInterrupts(watcher);
}

public static long ReadInterruptFallingTimestamp(int interruptHandle)
{
return lowLevel.HAL_ReadInterruptFallingTimestamp(interruptHandle);
}

public static long ReadInterruptRisingTimestamp(int interruptHandle)
{
return lowLevel.HAL_ReadInterruptRisingTimestamp(interruptHandle);
}

public static void Request(int interruptHandle, int digitalSourceHandle, AnalogTriggerType analogTriggerType)
{
lowLevel.HAL_RequestInterrupts(interruptHandle, digitalSourceHandle, analogTriggerType);
}

public static void SetInterruptUpSourceEdge(int interruptHandle, int risingEdge, int fallingEdge)
{
lowLevel.HAL_SetInterruptUpSourceEdge(interruptHandle, risingEdge, fallingEdge);
}

public static long WaitForInterrupt(int interruptHandle, double timeout, int ignorePrevious)
{
return lowLevel.HAL_WaitForInterrupt(interruptHandle, timeout, ignorePrevious);
}

}
}
