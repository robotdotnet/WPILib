using System.Runtime.InteropServices;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALAnalogOutput
    {
        public static void Ping() { }

        static HALAnalogOutput()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALAnalogOutput>(LibraryLoaderHolder.NativeLoader);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_InitializeAnalogOutputPortDelegate(int port_handle, ref int status);
        [NativeDelegate] public static HAL_InitializeAnalogOutputPortDelegate HAL_InitializeAnalogOutputPort;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_FreeAnalogOutputPortDelegate(int analog_output_handle);
        [NativeDelegate] public static HAL_FreeAnalogOutputPortDelegate HAL_FreeAnalogOutputPort;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetAnalogOutputDelegate(int analog_output_handle, double voltage, ref int status);
        [NativeDelegate] public static HAL_SetAnalogOutputDelegate HAL_SetAnalogOutput;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetAnalogOutputDelegate(int analog_output_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogOutputDelegate HAL_GetAnalogOutput;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_CheckAnalogOutputChannelDelegate(int pin);
        [NativeDelegate] public static HAL_CheckAnalogOutputChannelDelegate HAL_CheckAnalogOutputChannel;
    }
}

