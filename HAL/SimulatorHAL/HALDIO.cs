using System;
using System.Runtime.InteropServices;
using HAL.Base;
/*
// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALDIO
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALDIO.HAL_InitializeDIOPort = HAL_InitializeDIOPort;
            Base.HALDIO.HAL_FreeDIOPort = HAL_FreeDIOPort;
            Base.HALDIO.HAL_AllocateDigitalPWM = HAL_AllocateDigitalPWM;
            Base.HALDIO.HAL_FreeDigitalPWM = HAL_FreeDigitalPWM;
            Base.HALDIO.HAL_SetDigitalPWMRate = HAL_SetDigitalPWMRate;
            Base.HALDIO.HAL_SetDigitalPWMDutyCycle = HAL_SetDigitalPWMDutyCycle;
            Base.HALDIO.HAL_SetDigitalPWMOutputChannel = HAL_SetDigitalPWMOutputChannel;
            Base.HALDIO.HAL_SetDIO = HAL_SetDIO;
            Base.HALDIO.HAL_GetDIO = HAL_GetDIO;
            Base.HALDIO.HAL_GetDIODirection = HAL_GetDIODirection;
            Base.HALDIO.HAL_Pulse = HAL_Pulse;
            Base.HALDIO.HAL_IsPulsing = HAL_IsPulsing;
            Base.HALDIO.HAL_IsAnyPulsing = HAL_IsAnyPulsing;
            Base.HALDIO.HAL_SetFilterSelect = HAL_SetFilterSelect;
            Base.HALDIO.HAL_GetFilterSelect = HAL_GetFilterSelect;
            Base.HALDIO.HAL_SetFilterPeriod = HAL_SetFilterPeriod;
            Base.HALDIO.HAL_GetFilterPeriod = HAL_GetFilterPeriod;
        }

        public static int HAL_InitializeDIOPort(int port_handle, bool input, ref int status)
        {
        }

        public static void HAL_FreeDIOPort(int dio_port_handle)
        {
        }

        public static int HAL_AllocateDigitalPWM(ref int status)
        {
        }

        public static void HAL_FreeDigitalPWM(int pwmGenerator, ref int status)
        {
        }

        public static void HAL_SetDigitalPWMRate(double rate, ref int status)
        {
        }

        public static void HAL_SetDigitalPWMDutyCycle(int pwmGenerator, double dutyCycle, ref int status)
        {
        }

        public static void HAL_SetDigitalPWMOutputChannel(int pwmGenerator, int pin, ref int status)
        {
        }

        public static void HAL_SetDIO(int dio_port_handle, bool value, ref int status)
        {
        }

        public static bool HAL_GetDIO(int dio_port_handle, ref int status)
        {
        }

        public static bool HAL_GetDIODirection(int dio_port_handle, ref int status)
        {
        }

        public static void HAL_Pulse(int dio_port_handle, double pulseLength, ref int status)
        {
        }

        public static bool HAL_IsPulsing(int dio_port_handle, ref int status)
        {
        }

        public static bool HAL_IsAnyPulsing(ref int status)
        {
        }

        public static void HAL_SetFilterSelect(int dio_port_handle, int filter_index, ref int status)
        {
        }

        public static int HAL_GetFilterSelect(int dio_port_handle, ref int status)
        {
        }

        public static void HAL_SetFilterPeriod(int filter_index, long value, ref int status)
        {
        }

        public static long HAL_GetFilterPeriod(int filter_index, ref int status)
        {
        }
    }
}

    */