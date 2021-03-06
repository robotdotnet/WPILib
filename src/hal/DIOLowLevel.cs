﻿
using Hal.Natives;
using WPIUtil.ILGeneration;

namespace Hal
{

    public static unsafe class DIOLowLevel
    {
        internal static DIOLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

        public static int AllocateDigitalPWM()
        {
            return lowLevel.HAL_AllocateDigitalPWM();
        }

        public static int CheckChannel(int channel)
        {
            return lowLevel.HAL_CheckDIOChannel(channel);
        }

        public static void FreePort(int dioPortHandle)
        {
            lowLevel.HAL_FreeDIOPort(dioPortHandle);
        }

        public static void Set(int dioPortHandle, bool value)
        {
            lowLevel.HAL_SetDIO(dioPortHandle, value ? 1 : 0);
        }

        public static void FreeDigitalPWM(int pwmGenerator)
        {
            lowLevel.HAL_FreeDigitalPWM(pwmGenerator);
        }

        public static bool Get(int dioPortHandle)
        {
            return lowLevel.HAL_GetDIO(dioPortHandle) != 0;
        }

        public static bool GetDirection(int dioPortHandle)
        {
            return lowLevel.HAL_GetDIODirection(dioPortHandle) != 0;
        }

        public static long GetFilterPeriod(int filterIndex)
        {
            return lowLevel.HAL_GetFilterPeriod(filterIndex);
        }

        public static int GetFilterSelect(int dioPortHandle)
        {
            return lowLevel.HAL_GetFilterSelect(dioPortHandle);
        }

        public static int InitializePort(int portHandle, bool input)
        {
            return lowLevel.HAL_InitializeDIOPort(portHandle, input ? 1 : 0);
        }

        public static int IsAnyPulsing()
        {
            return lowLevel.HAL_IsAnyPulsing();
        }

        public static int IsPulsing(int dioPortHandle)
        {
            return lowLevel.HAL_IsPulsing(dioPortHandle);
        }

        public static void Pulse(int dioPortHandle, double pulseLength)
        {
            lowLevel.HAL_Pulse(dioPortHandle, pulseLength);
        }

        public static void SetSimDevice(int handle, int device)
        {
            lowLevel.HAL_SetDIOSimDevice(handle, device);
        }

        public static void SetDirection(int dioPortHandle, int input)
        {
            lowLevel.HAL_SetDIODirection(dioPortHandle, input);
        }

        public static void SetDigitalPWMDutyCycle(int pwmGenerator, double dutyCycle)
        {
            lowLevel.HAL_SetDigitalPWMDutyCycle(pwmGenerator, dutyCycle);
        }

        public static void SetDigitalPWMOutputChannel(int pwmGenerator, int channel)
        {
            lowLevel.HAL_SetDigitalPWMOutputChannel(pwmGenerator, channel);
        }

        public static void SetDigitalPWMRate(double rate)
        {
            lowLevel.HAL_SetDigitalPWMRate(rate);
        }

        public static void SetFilterPeriod(int filterIndex, long value)
        {
            lowLevel.HAL_SetFilterPeriod(filterIndex, value);
        }

        public static void SetFilterSelect(int dioPortHandle, int filterIndex)
        {
            lowLevel.HAL_SetFilterSelect(dioPortHandle, filterIndex);
        }

    }
}
