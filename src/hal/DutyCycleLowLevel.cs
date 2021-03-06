﻿
using Hal.Natives;
using WPIUtil.ILGeneration;

namespace Hal
{

    public static unsafe class DutyCycleLowLevel
    {
        internal static DutyCycleLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

        public static void Free(int dutyCycleHandle)
        {
            lowLevel.HAL_FreeDutyCycle(dutyCycleHandle);
        }

        public static int GetFPGAIndex(int dutyCycleHandle)
        {
            return lowLevel.HAL_GetDutyCycleFPGAIndex(dutyCycleHandle);
        }

        public static int GetFrequency(int dutyCycleHandle)
        {
            return lowLevel.HAL_GetDutyCycleFrequency(dutyCycleHandle);
        }

        public static double GetOutput(int dutyCycleHandle)
        {
            return lowLevel.HAL_GetDutyCycleOutput(dutyCycleHandle);
        }

        public static int GetOutputRaw(int dutyCycleHandle)
        {
            return lowLevel.HAL_GetDutyCycleOutputRaw(dutyCycleHandle);
        }

        public static int GetOutputScaleFactor(int dutyCycleHandle)
        {
            return lowLevel.HAL_GetDutyCycleOutputScaleFactor(dutyCycleHandle);
        }

        public static int Initialize(int digitalSourceHandle, AnalogTriggerType triggerType)
        {
            return lowLevel.HAL_InitializeDutyCycle(digitalSourceHandle, triggerType);
        }

        public static void SetSimDevice(int handle, int device)
        {
            lowLevel.HAL_SetDutyCycleSimDevice(handle, device);
        }

    }
}
