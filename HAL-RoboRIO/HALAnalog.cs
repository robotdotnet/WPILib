//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using HAL_Base;

namespace HAL_RoboRIO
{
    internal class HALAnalog
    {

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeAnalogOutputPort")]
        internal static extern IntPtr initializeAnalogOutputPort(IntPtr port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogOutput")]
        internal static extern void setAnalogOutput(IntPtr analog_port_pointer, double voltage, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogOutput")]
        internal static extern double getAnalogOutput(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "checkAnalogOutputChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool checkAnalogOutputChannel(uint pin);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeAnalogInputPort")]
        internal static extern IntPtr initializeAnalogInputPort(IntPtr port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "checkAnalogModule")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool checkAnalogModule(byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "checkAnalogInputChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool checkAnalogInputChannel(uint pin);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogSampleRate")]
        internal static extern void setAnalogSampleRate(double samplesPerSecond, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogSampleRate")]
        internal static extern float getAnalogSampleRate(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogAverageBits")]
        internal static extern void setAnalogAverageBits(IntPtr analog_port_pointer, uint bits, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogAverageBits")]
        internal static extern uint getAnalogAverageBits(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogOversampleBits")]
        internal static extern void setAnalogOversampleBits(IntPtr analog_port_pointer, uint bits, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogOversampleBits")]
        internal static extern uint getAnalogOversampleBits(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogValue")]
        internal static extern short getAnalogValue(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogAverageValue")]
        internal static extern int getAnalogAverageValue(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogVoltsToValue")]
        internal static extern int getAnalogVoltsToValue(IntPtr analog_port_pointer, double voltage, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogVoltage")]
        internal static extern float getAnalogVoltage(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogAverageVoltage")]
        internal static extern float getAnalogAverageVoltage(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogLSBWeight")]
        internal static extern uint getAnalogLSBWeight(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogOffset")]
        internal static extern int getAnalogOffset(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "isAccumulatorChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool isAccumulatorChannel(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initAccumulator")]
        internal static extern void initAccumulator(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "resetAccumulator")]
        internal static extern void resetAccumulator(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAccumulatorCenter")]
        internal static extern void setAccumulatorCenter(IntPtr analog_port_pointer, int center, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAccumulatorDeadband")]
        internal static extern void setAccumulatorDeadband(IntPtr analog_port_pointer, int deadband, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAccumulatorValue")]
        internal static extern long getAccumulatorValue(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAccumulatorCount")]
        internal static extern uint getAccumulatorCount(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAccumulatorOutput")]
        internal static extern void getAccumulatorOutput(IntPtr analog_port_pointer, ref long value, ref uint count, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeAnalogTrigger")]
        internal static extern IntPtr initializeAnalogTrigger(IntPtr port_pointer, ref uint index, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "cleanAnalogTrigger")]
        internal static extern void cleanAnalogTrigger(IntPtr analog_trigger_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogTriggerLimitsRaw")]
        internal static extern void setAnalogTriggerLimitsRaw(IntPtr analog_trigger_pointer, int lower, int upper, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogTriggerLimitsVoltage")]
        internal static extern void setAnalogTriggerLimitsVoltage(IntPtr analog_trigger_pointer, double lower, double upper, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogTriggerAveraged")]
        internal static extern void setAnalogTriggerAveraged(IntPtr analog_trigger_pointer, [MarshalAs(UnmanagedType.I1)] bool useAveragedValue, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogTriggerFiltered")]
        internal static extern void setAnalogTriggerFiltered(IntPtr analog_trigger_pointer, [MarshalAs(UnmanagedType.I1)] bool useFilteredValue, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogTriggerInWindow")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getAnalogTriggerInWindow(IntPtr analog_trigger_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogTriggerTriggerState")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getAnalogTriggerTriggerState(IntPtr analog_trigger_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogTriggerOutput")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getAnalogTriggerOutput(IntPtr analog_trigger_pointer, AnalogTriggerType type, ref int status);
    }
}
