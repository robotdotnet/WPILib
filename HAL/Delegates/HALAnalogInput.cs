using System.Runtime.InteropServices;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALAnalogInput
    {
        static HALAnalogInput()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALAnalogInput>(LibraryLoaderHolder.NativeLoader);
        }

        public delegate int HAL_InitializeAnalogInputPortDelegate(int port_handle, ref int status);
        [NativeDelegate] public static HAL_InitializeAnalogInputPortDelegate HAL_InitializeAnalogInputPort;

        public delegate void HAL_FreeAnalogInputPortDelegate(int analog_port_handle);
        [NativeDelegate] public static HAL_FreeAnalogInputPortDelegate HAL_FreeAnalogInputPort;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_CheckAnalogModuleDelegate(int module);
        [NativeDelegate] public static HAL_CheckAnalogModuleDelegate HAL_CheckAnalogModule;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_CheckAnalogInputChannelDelegate(int pin);
        [NativeDelegate] public static HAL_CheckAnalogInputChannelDelegate HAL_CheckAnalogInputChannel;

        public delegate void HAL_SetAnalogSampleRateDelegate(double samplesPerSecond, ref int status);
        [NativeDelegate] public static HAL_SetAnalogSampleRateDelegate HAL_SetAnalogSampleRate;

        public delegate double HAL_GetAnalogSampleRateDelegate(ref int status);
        [NativeDelegate] public static HAL_GetAnalogSampleRateDelegate HAL_GetAnalogSampleRate;

        public delegate void HAL_SetAnalogAverageBitsDelegate(int analog_port_handle, int bits, ref int status);
        [NativeDelegate] public static HAL_SetAnalogAverageBitsDelegate HAL_SetAnalogAverageBits;

        public delegate int HAL_GetAnalogAverageBitsDelegate(int analog_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogAverageBitsDelegate HAL_GetAnalogAverageBits;

        public delegate void HAL_SetAnalogOversampleBitsDelegate(int analog_port_handle, int bits, ref int status);
        [NativeDelegate] public static HAL_SetAnalogOversampleBitsDelegate HAL_SetAnalogOversampleBits;

        public delegate int HAL_GetAnalogOversampleBitsDelegate(int analog_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogOversampleBitsDelegate HAL_GetAnalogOversampleBits;

        public delegate int HAL_GetAnalogValueDelegate(int analog_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogValueDelegate HAL_GetAnalogValue;

        public delegate int HAL_GetAnalogAverageValueDelegate(int analog_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogAverageValueDelegate HAL_GetAnalogAverageValue;

        public delegate int HAL_GetAnalogVoltsToValueDelegate(int analog_port_handle, double voltage, ref int status);
        [NativeDelegate] public static HAL_GetAnalogVoltsToValueDelegate HAL_GetAnalogVoltsToValue;

        public delegate double HAL_GetAnalogVoltageDelegate(int analog_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogVoltageDelegate HAL_GetAnalogVoltage;

        public delegate double HAL_GetAnalogAverageVoltageDelegate(int analog_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogAverageVoltageDelegate HAL_GetAnalogAverageVoltage;

        public delegate int HAL_GetAnalogLSBWeightDelegate(int analog_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogLSBWeightDelegate HAL_GetAnalogLSBWeight;

        public delegate int HAL_GetAnalogOffsetDelegate(int analog_port_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogOffsetDelegate HAL_GetAnalogOffset;
    }
}

