using System.Runtime.InteropServices;
using FRC.NativeLibraryUtilities;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALDIO
    {
        static HALDIO()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALDIO>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_InitializeDIOPortDelegate(int port_handle, [MarshalAs(UnmanagedType.Bool)]bool input, ref int status);
        [NativeDelegate] public static HAL_InitializeDIOPortDelegate HAL_InitializeDIOPort;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool HAL_CheckDIOChannelChannel(int channel);
        [NativeDelegate]
        public static HAL_CheckDIOChannelChannel HAL_CheckDIOChannel;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_FreeDIOPortDelegate(int dio_port_handle);
        [NativeDelegate] public static HAL_FreeDIOPortDelegate HAL_FreeDIOPort;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_AllocateDigitalPWMDelegate(ref int status);
        [NativeDelegate] public static HAL_AllocateDigitalPWMDelegate HAL_AllocateDigitalPWM;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_FreeDigitalPWMDelegate(int pwmGenerator, ref int status);
        [NativeDelegate] public static HAL_FreeDigitalPWMDelegate HAL_FreeDigitalPWM;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetDigitalPWMRateDelegate(double rate, ref int status);
        [NativeDelegate] public static HAL_SetDigitalPWMRateDelegate HAL_SetDigitalPWMRate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetDigitalPWMDutyCycleDelegate(int pwmGenerator, double dutyCycle, ref int status);
        [NativeDelegate] public static HAL_SetDigitalPWMDutyCycleDelegate HAL_SetDigitalPWMDutyCycle;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetDigitalPWMOutputChannelDelegate(int pwmGenerator, int pin, ref int status);
        [NativeDelegate] public static HAL_SetDigitalPWMOutputChannelDelegate HAL_SetDigitalPWMOutputChannel;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetDIODelegate(int dio_port_handle, [MarshalAs(UnmanagedType.Bool)]bool value, ref int status);
        [NativeDelegate] public static HAL_SetDIODelegate HAL_SetDIO;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetDIODelegate(int dio_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetDIODelegate HAL_GetDIO;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetDIODirectionDelegate(int dio_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetDIODirectionDelegate HAL_GetDIODirection;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_PulseDelegate(int dio_port_handle, double pulseLength, ref int status);
        [NativeDelegate] public static HAL_PulseDelegate HAL_Pulse;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_IsPulsingDelegate(int dio_port_handle, ref int status);
        [NativeDelegate] public static HAL_IsPulsingDelegate HAL_IsPulsing;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_IsAnyPulsingDelegate(ref int status);
        [NativeDelegate] public static HAL_IsAnyPulsingDelegate HAL_IsAnyPulsing;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetFilterSelectDelegate(int dio_port_handle, int filter_index, ref int status);
        [NativeDelegate] public static HAL_SetFilterSelectDelegate HAL_SetFilterSelect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetFilterSelectDelegate(int dio_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetFilterSelectDelegate HAL_GetFilterSelect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetFilterPeriodDelegate(int filter_index, long value, ref int status);
        [NativeDelegate] public static HAL_SetFilterPeriodDelegate HAL_SetFilterPeriod;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate long HAL_GetFilterPeriodDelegate(int filter_index, ref int status);
        [NativeDelegate] public static HAL_GetFilterPeriodDelegate HAL_GetFilterPeriod;
    }
}

