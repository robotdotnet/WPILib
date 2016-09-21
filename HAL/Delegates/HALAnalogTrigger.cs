using System.Runtime.InteropServices;
using HAL.NativeLoader;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALAnalogTrigger
    {
        public static void Ping() { }

        static HALAnalogTrigger()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALAnalogTrigger>(LibraryLoaderHolder.NativeLoader);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_InitializeAnalogTriggerDelegate(int port_handle, ref int index, ref int status);
        [NativeDelegate] public static HAL_InitializeAnalogTriggerDelegate HAL_InitializeAnalogTrigger;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_CleanAnalogTriggerDelegate(int analog_trigger_handle, ref int status);
        [NativeDelegate] public static HAL_CleanAnalogTriggerDelegate HAL_CleanAnalogTrigger;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetAnalogTriggerLimitsRawDelegate(int analog_trigger_handle, int lower, int upper, ref int status);
        [NativeDelegate] public static HAL_SetAnalogTriggerLimitsRawDelegate HAL_SetAnalogTriggerLimitsRaw;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetAnalogTriggerLimitsVoltageDelegate(int analog_trigger_handle, double lower, double upper, ref int status);
        [NativeDelegate] public static HAL_SetAnalogTriggerLimitsVoltageDelegate HAL_SetAnalogTriggerLimitsVoltage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetAnalogTriggerAveragedDelegate(int analog_trigger_handle, [MarshalAs(UnmanagedType.Bool)]bool useAveragedValue, ref int status);
        [NativeDelegate] public static HAL_SetAnalogTriggerAveragedDelegate HAL_SetAnalogTriggerAveraged;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetAnalogTriggerFilteredDelegate(int analog_trigger_handle, [MarshalAs(UnmanagedType.Bool)]bool useFilteredValue, ref int status);
        [NativeDelegate] public static HAL_SetAnalogTriggerFilteredDelegate HAL_SetAnalogTriggerFiltered;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetAnalogTriggerInWindowDelegate(int analog_trigger_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogTriggerInWindowDelegate HAL_GetAnalogTriggerInWindow;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetAnalogTriggerTriggerStateDelegate(int analog_trigger_handle, ref int status);
        [NativeDelegate] public static HAL_GetAnalogTriggerTriggerStateDelegate HAL_GetAnalogTriggerTriggerState;

        [return: MarshalAs(UnmanagedType.Bool)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate bool HAL_GetAnalogTriggerOutputDelegate(int analog_trigger_handle, HALAnalogTriggerType type, ref int status);
        [NativeDelegate] public static HAL_GetAnalogTriggerOutputDelegate HAL_GetAnalogTriggerOutput;
    }
}

