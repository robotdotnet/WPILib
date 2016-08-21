using System;
using System.Runtime.InteropServices;
// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALAnalogTrigger
    {
        static HALAnalogTrigger()
        {
            HAL.Initialize();
        }

        public delegate int HAL_InitializeAnalogTriggerDelegate(int port_handle, ref int index, ref int status);
        public static HAL_InitializeAnalogTriggerDelegate HAL_InitializeAnalogTrigger;

        public delegate void HAL_CleanAnalogTriggerDelegate(int analog_trigger_handle, ref int status);
        public static HAL_CleanAnalogTriggerDelegate HAL_CleanAnalogTrigger;

        public delegate void HAL_SetAnalogTriggerLimitsRawDelegate(int analog_trigger_handle, int lower, int upper, ref int status);
        public static HAL_SetAnalogTriggerLimitsRawDelegate HAL_SetAnalogTriggerLimitsRaw;

        public delegate void HAL_SetAnalogTriggerLimitsVoltageDelegate(int analog_trigger_handle, double lower, double upper, ref int status);
        public static HAL_SetAnalogTriggerLimitsVoltageDelegate HAL_SetAnalogTriggerLimitsVoltage;

        public delegate void HAL_SetAnalogTriggerAveragedDelegate(int analog_trigger_handle, [MarshalAs(UnmanagedType.I4)]bool useAveragedValue, ref int status);
        public static HAL_SetAnalogTriggerAveragedDelegate HAL_SetAnalogTriggerAveraged;

        public delegate void HAL_SetAnalogTriggerFilteredDelegate(int analog_trigger_handle, [MarshalAs(UnmanagedType.I4)]bool useFilteredValue, ref int status);
        public static HAL_SetAnalogTriggerFilteredDelegate HAL_SetAnalogTriggerFiltered;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetAnalogTriggerInWindowDelegate(int analog_trigger_handle, ref int status);
        public static HAL_GetAnalogTriggerInWindowDelegate HAL_GetAnalogTriggerInWindow;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetAnalogTriggerTriggerStateDelegate(int analog_trigger_handle, ref int status);
        public static HAL_GetAnalogTriggerTriggerStateDelegate HAL_GetAnalogTriggerTriggerState;

        [return: MarshalAs(UnmanagedType.I4)]
        public delegate bool HAL_GetAnalogTriggerOutputDelegate(int analog_trigger_handle, HALAnalogTriggerType type, ref int status);
        public static HAL_GetAnalogTriggerOutputDelegate HAL_GetAnalogTriggerOutput;
    }
}

