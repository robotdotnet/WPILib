
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IDIO))]
    public unsafe static class DIO
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IDIO lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
