
using Hal.Natives;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIUtil.ILGeneration;

namespace Hal
{

    public static unsafe class InterruptsLowLevel
    {
        internal static InterruptsLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

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
