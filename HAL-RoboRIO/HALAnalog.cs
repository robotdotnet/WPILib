//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using HAL_Base;

namespace HAL_RoboRIO
{
    internal class HALAnalog
    {

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeAnalogOutputPort")]
        public static extern IntPtr initializeAnalogOutputPort(IntPtr port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogOutput")]
        public static extern void setAnalogOutput(IntPtr analog_port_pointer, double voltage, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogOutput")]
        public static extern double getAnalogOutput(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "checkAnalogOutputChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkAnalogOutputChannel(uint pin);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeAnalogInputPort")]
        public static extern IntPtr initializeAnalogInputPort(IntPtr port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "checkAnalogModule")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkAnalogModule(byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "checkAnalogInputChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkAnalogInputChannel(uint pin);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogSampleRate")]
        public static extern void setAnalogSampleRate(double samplesPerSecond, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogSampleRate")]
        public static extern float getAnalogSampleRate(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogAverageBits")]
        public static extern void setAnalogAverageBits(IntPtr analog_port_pointer, uint bits, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogAverageBits")]
        public static extern uint getAnalogAverageBits(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogOversampleBits")]
        public static extern void setAnalogOversampleBits(IntPtr analog_port_pointer, uint bits, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogOversampleBits")]
        public static extern uint getAnalogOversampleBits(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogValue")]
        public static extern short getAnalogValue(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogAverageValue")]
        public static extern int getAnalogAverageValue(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogVoltsToValue")]
        public static extern int getAnalogVoltsToValue(IntPtr analog_port_pointer, double voltage, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogVoltage")]
        public static extern float getAnalogVoltage(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogAverageVoltage")]
        public static extern float getAnalogAverageVoltage(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogLSBWeight")]
        public static extern uint getAnalogLSBWeight(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogOffset")]
        public static extern int getAnalogOffset(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "isAccumulatorChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool isAccumulatorChannel(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initAccumulator")]
        public static extern void initAccumulator(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "resetAccumulator")]
        public static extern void resetAccumulator(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAccumulatorCenter")]
        public static extern void setAccumulatorCenter(IntPtr analog_port_pointer, int center, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAccumulatorDeadband")]
        public static extern void setAccumulatorDeadband(IntPtr analog_port_pointer, int deadband, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAccumulatorValue")]
        public static extern long getAccumulatorValue(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAccumulatorCount")]
        public static extern uint getAccumulatorCount(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAccumulatorOutput")]
        public static extern void getAccumulatorOutput(IntPtr analog_port_pointer, ref long value, ref uint count, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeAnalogTrigger")]
        public static extern IntPtr initializeAnalogTrigger(IntPtr port_pointer, ref uint index, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "cleanAnalogTrigger")]
        public static extern void cleanAnalogTrigger(IntPtr analog_trigger_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogTriggerLimitsRaw")]
        public static extern void setAnalogTriggerLimitsRaw(IntPtr analog_trigger_pointer, int lower, int upper, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogTriggerLimitsVoltage")]
        public static extern void setAnalogTriggerLimitsVoltage(IntPtr analog_trigger_pointer, double lower, double upper, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogTriggerAveraged")]
        public static extern void setAnalogTriggerAveraged(IntPtr analog_trigger_pointer, [MarshalAs(UnmanagedType.I1)] bool useAveragedValue, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogTriggerFiltered")]
        public static extern void setAnalogTriggerFiltered(IntPtr analog_trigger_pointer, [MarshalAs(UnmanagedType.I1)] bool useFilteredValue, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogTriggerInWindow")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getAnalogTriggerInWindow(IntPtr analog_trigger_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogTriggerTriggerState")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getAnalogTriggerTriggerState(IntPtr analog_trigger_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogTriggerOutput")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getAnalogTriggerOutput(IntPtr analog_trigger_pointer, AnalogTriggerType type, ref int status);
    }
}
