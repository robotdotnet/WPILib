using System.Runtime.InteropServices;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALAnalogOutput
    {
        static HALAnalogOutput()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALAnalogOutput>(LibraryLoaderHolder.NativeLoader);
        }

        public delegate int HAL_InitializeAnalogOutputPortDelegate(int port_handle, ref int status);
        [NativeDelegate] public static HAL_InitializeAnalogOutputPortDelegate HAL_InitializeAnalogOutputPort;

        public delegate void HAL_FreeAnalogOutputPortDelegate(int analog_output_handle);
        [NativeDelegate] public static HAL_FreeAnalogOutputPortDelegate HAL_FreeAnalogOutputPort;

        public delegate void HAL_SetAnalogOutputDelegate(int analog_output_handle, double voltage, ref int status);
        [NativeDelegate] public static HAL_SetAnalogOutputDelegate HAL_SetAnalogOutput;

        public delegate double HAL_GetAnalogOutputDelegate(int analog_output_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogOutputDelegate HAL_GetAnalogOutput;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_CheckAnalogOutputChannelDelegate(int pin);
        [NativeDelegate] public static HAL_CheckAnalogOutputChannelDelegate HAL_CheckAnalogOutputChannel;
    }
}

