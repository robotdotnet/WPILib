
using Hal.Natives;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IInterrupts))]
    public static unsafe class InterruptsLowLevel
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IInterrupts lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static void InterruptHandler(uint mask, void* context)
        {
            var gcHandle = GCHandle.FromIntPtr((IntPtr)context);
            var callback = gcHandle.Target as Action<uint>;
            callback?.Invoke(mask);
        }

        public static void AttachInterruptHandler(int interruptHandle, Action<uint> handler)
        {
            var handle = GCHandle.Alloc(handler);
            lowLevel.HAL_AttachInterruptHandler(interruptHandle, &InterruptHandler, GCHandle.ToIntPtr(handle).ToPointer());
        }

        public static void AttachInterruptHandlerThreaded(int interruptHandle, Action<uint> handler)
        {
            var handle = GCHandle.Alloc(handler);
            lowLevel.HAL_AttachInterruptHandlerThreaded(interruptHandle, &InterruptHandler, GCHandle.ToIntPtr(handle).ToPointer());
        }

        public static void Clean(int interruptHandle)
        {
            void* handle = lowLevel.HAL_CleanInterrupts(interruptHandle);
            var gcHandle = GCHandle.FromIntPtr((IntPtr)handle);
            gcHandle.Free();
        }

        public static void Disable(int interruptHandle)
        {
            lowLevel.HAL_DisableInterrupts(interruptHandle);
        }

        public static void Enable(int interruptHandle)
        {
            lowLevel.HAL_EnableInterrupts(interruptHandle);
        }

        public static int Initialize(bool watcher)
        {
            return lowLevel.HAL_InitializeInterrupts(watcher ? 1 : 0);
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

        public static void SetInterruptUpSourceEdge(int interruptHandle, bool risingEdge, bool fallingEdge)
        {
            lowLevel.HAL_SetInterruptUpSourceEdge(interruptHandle, risingEdge ? 1 : 0, fallingEdge ? 1 : 0);
        }

        public static long WaitForInterrupt(int interruptHandle, double timeout, bool ignorePrevious)
        {
            return lowLevel.HAL_WaitForInterrupt(interruptHandle, timeout, ignorePrevious ? 1 : 0);
        }

        public static void ReleaseWaitingInterrupt(int interruptHandle)
        {
            lowLevel.HAL_ReleaseWaitingInterrupt(interruptHandle);
        }
    }
}
