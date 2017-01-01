using System.Runtime.InteropServices;
using NativeLibraryUtilities;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALAnalogInput
    {
        public static void Ping() { }

        static HALAnalogInput()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALAnalogInput>(LibraryLoaderHolder.NativeLoader);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_InitializeAnalogInputPortDelegate(int port_handle, ref int status);
        [NativeDelegate] public static HAL_InitializeAnalogInputPortDelegate HAL_InitializeAnalogInputPort;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_FreeAnalogInputPortDelegate(int analog_port_handle);
        [NativeDelegate] public static HAL_FreeAnalogInputPortDelegate HAL_FreeAnalogInputPort;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_CheckAnalogModuleDelegate(int module);
        [NativeDelegate] public static HAL_CheckAnalogModuleDelegate HAL_CheckAnalogModule;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_CheckAnalogInputChannelDelegate(int pin);
        [NativeDelegate] public static HAL_CheckAnalogInputChannelDelegate HAL_CheckAnalogInputChannel;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetAnalogSampleRateDelegate(double samplesPerSecond, ref int status);
        [NativeDelegate] public static HAL_SetAnalogSampleRateDelegate HAL_SetAnalogSampleRate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetAnalogSampleRateDelegate(ref int status);
        [NativeDelegate] public static HAL_GetAnalogSampleRateDelegate HAL_GetAnalogSampleRate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetAnalogAverageBitsDelegate(int analog_port_handle, int bits, ref int status);
        [NativeDelegate] public static HAL_SetAnalogAverageBitsDelegate HAL_SetAnalogAverageBits;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetAnalogAverageBitsDelegate(int analog_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogAverageBitsDelegate HAL_GetAnalogAverageBits;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetAnalogOversampleBitsDelegate(int analog_port_handle, int bits, ref int status);
        [NativeDelegate] public static HAL_SetAnalogOversampleBitsDelegate HAL_SetAnalogOversampleBits;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetAnalogOversampleBitsDelegate(int analog_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogOversampleBitsDelegate HAL_GetAnalogOversampleBits;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetAnalogValueDelegate(int analog_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogValueDelegate HAL_GetAnalogValue;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetAnalogAverageValueDelegate(int analog_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogAverageValueDelegate HAL_GetAnalogAverageValue;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetAnalogVoltsToValueDelegate(int analog_port_handle, double voltage, ref int status);
        [NativeDelegate] public static HAL_GetAnalogVoltsToValueDelegate HAL_GetAnalogVoltsToValue;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetAnalogVoltageDelegate(int analog_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogVoltageDelegate HAL_GetAnalogVoltage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetAnalogAverageVoltageDelegate(int analog_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogAverageVoltageDelegate HAL_GetAnalogAverageVoltage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetAnalogLSBWeightDelegate(int analog_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogLSBWeightDelegate HAL_GetAnalogLSBWeight;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetAnalogOffsetDelegate(int analog_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogOffsetDelegate HAL_GetAnalogOffset;
    }
}

