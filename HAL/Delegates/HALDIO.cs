using System;
using System.Runtime.InteropServices;
// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALDIO
    {
        static HALDIO()
        {
            HAL.Initialize();
        }

        public delegate int HAL_InitializeDIOPortDelegate(int port_handle, [MarshalAs(UnmanagedType.I4)]bool input, ref int status);
        public static HAL_InitializeDIOPortDelegate HAL_InitializeDIOPort;

        public delegate void HAL_FreeDIOPortDelegate(int dio_port_handle);
        public static HAL_FreeDIOPortDelegate HAL_FreeDIOPort;

        public delegate int HAL_AllocateDigitalPWMDelegate(ref int status);
        public static HAL_AllocateDigitalPWMDelegate HAL_AllocateDigitalPWM;

        public delegate void HAL_FreeDigitalPWMDelegate(int pwmGenerator, ref int status);
        public static HAL_FreeDigitalPWMDelegate HAL_FreeDigitalPWM;

        public delegate void HAL_SetDigitalPWMRateDelegate(double rate, ref int status);
        public static HAL_SetDigitalPWMRateDelegate HAL_SetDigitalPWMRate;

        public delegate void HAL_SetDigitalPWMDutyCycleDelegate(int pwmGenerator, double dutyCycle, ref int status);
        public static HAL_SetDigitalPWMDutyCycleDelegate HAL_SetDigitalPWMDutyCycle;

        public delegate void HAL_SetDigitalPWMOutputChannelDelegate(int pwmGenerator, int pin, ref int status);
        public static HAL_SetDigitalPWMOutputChannelDelegate HAL_SetDigitalPWMOutputChannel;

        public delegate void HAL_SetDIODelegate(int dio_port_handle, [MarshalAs(UnmanagedType.I4)]bool value, ref int status);
        public static HAL_SetDIODelegate HAL_SetDIO;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetDIODelegate(int dio_port_handle, ref int status);
        public static HAL_GetDIODelegate HAL_GetDIO;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetDIODirectionDelegate(int dio_port_handle, ref int status);
        public static HAL_GetDIODirectionDelegate HAL_GetDIODirection;

        public delegate void HAL_PulseDelegate(int dio_port_handle, double pulseLength, ref int status);
        public static HAL_PulseDelegate HAL_Pulse;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_IsPulsingDelegate(int dio_port_handle, ref int status);
        public static HAL_IsPulsingDelegate HAL_IsPulsing;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_IsAnyPulsingDelegate(ref int status);
        public static HAL_IsAnyPulsingDelegate HAL_IsAnyPulsing;

        public delegate void HAL_SetFilterSelectDelegate(int dio_port_handle, int filter_index, ref int status);
        public static HAL_SetFilterSelectDelegate HAL_SetFilterSelect;

        public delegate int HAL_GetFilterSelectDelegate(int dio_port_handle, ref int status);
        public static HAL_GetFilterSelectDelegate HAL_GetFilterSelect;

        public delegate void HAL_SetFilterPeriodDelegate(int filter_index, long value, ref int status);
        public static HAL_SetFilterPeriodDelegate HAL_SetFilterPeriod;

        public delegate long HAL_GetFilterPeriodDelegate(int filter_index, ref int status);
        public static HAL_GetFilterPeriodDelegate HAL_GetFilterPeriod;
    }
}

