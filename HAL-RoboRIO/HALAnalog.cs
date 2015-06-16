//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using HAL_Base;

namespace HAL_RoboRIO
{
    public class HALAnalog
    {

        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeAnalogOutputPort")]
        public static extern IntPtr initializeAnalogOutputPort(IntPtr port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogOutput")]
        public static extern void setAnalogOutput(IntPtr analog_port_pointer, double voltage, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogOutput")]
        public static extern double getAnalogOutput(IntPtr analog_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "checkAnalogOutputChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkAnalogOutputChannel(uint pin);

        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeAnalogInputPort")]
        public static extern IntPtr initializeAnalogInputPort(IntPtr port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "checkAnalogModule")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkAnalogModule(byte module);

        [DllImport("libHALAthena_shared.so", EntryPoint = "checkAnalogInputChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkAnalogInputChannel(uint pin);

        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogSampleRate")]
        public static extern void setAnalogSampleRate(double samplesPerSecond, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogSampleRate")]
        public static extern float getAnalogSampleRate(ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogAverageBits")]
        public static extern void setAnalogAverageBits(IntPtr analog_port_pointer, uint bits, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogAverageBits")]
        public static extern uint getAnalogAverageBits(IntPtr analog_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogOversampleBits")]
        public static extern void setAnalogOversampleBits(IntPtr analog_port_pointer, uint bits, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogOversampleBits")]
        public static extern uint getAnalogOversampleBits(IntPtr analog_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogValue")]
        public static extern short getAnalogValue(IntPtr analog_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogAverageValue")]
        public static extern int getAnalogAverageValue(IntPtr analog_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogVoltsToValue")]
        public static extern int getAnalogVoltsToValue(IntPtr analog_port_pointer, double voltage, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogVoltage")]
        public static extern float getAnalogVoltage(IntPtr analog_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogAverageVoltage")]
        public static extern float getAnalogAverageVoltage(IntPtr analog_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogLSBWeight")]
        public static extern uint getAnalogLSBWeight(IntPtr analog_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogOffset")]
        public static extern int getAnalogOffset(IntPtr analog_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "isAccumulatorChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool isAccumulatorChannel(IntPtr analog_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "initAccumulator")]
        public static extern void initAccumulator(IntPtr analog_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "resetAccumulator")]
        public static extern void resetAccumulator(IntPtr analog_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "setAccumulatorCenter")]
        public static extern void setAccumulatorCenter(IntPtr analog_port_pointer, int center, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "setAccumulatorDeadband")]
        public static extern void setAccumulatorDeadband(IntPtr analog_port_pointer, int deadband, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAccumulatorValue")]
        public static extern long getAccumulatorValue(IntPtr analog_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAccumulatorCount")]
        public static extern uint getAccumulatorCount(IntPtr analog_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAccumulatorOutput")]
        public static extern void getAccumulatorOutput(IntPtr analog_port_pointer, ref long value, ref uint count, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeAnalogTrigger")]
        public static extern IntPtr initializeAnalogTrigger(IntPtr port_pointer, ref uint index, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "cleanAnalogTrigger")]
        public static extern void cleanAnalogTrigger(IntPtr analog_trigger_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogTriggerLimitsRaw")]
        public static extern void setAnalogTriggerLimitsRaw(IntPtr analog_trigger_pointer, int lower, int upper, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogTriggerLimitsVoltage")]
        public static extern void setAnalogTriggerLimitsVoltage(IntPtr analog_trigger_pointer, double lower, double upper, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogTriggerAveraged")]
        public static extern void setAnalogTriggerAveraged(IntPtr analog_trigger_pointer, [MarshalAs(UnmanagedType.I1)] bool useAveragedValue, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogTriggerFiltered")]
        public static extern void setAnalogTriggerFiltered(IntPtr analog_trigger_pointer, [MarshalAs(UnmanagedType.I1)] bool useFilteredValue, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogTriggerInWindow")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getAnalogTriggerInWindow(IntPtr analog_trigger_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogTriggerTriggerState")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getAnalogTriggerTriggerState(IntPtr analog_trigger_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogTriggerOutput")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getAnalogTriggerOutput(IntPtr analog_trigger_pointer, AnalogTriggerType type, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogSampleRateIntHack")]
        public static extern int getAnalogSampleRateIntHack(ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogVoltageIntHack")]
        public static extern int getAnalogVoltageIntHack(IntPtr analog_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogAverageVoltageIntHack")]
        public static extern int getAnalogAverageVoltageIntHack(IntPtr analog_port_pointer, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogSampleRateIntHack")]
        public static extern void setAnalogSampleRateIntHack(int samplesPerSecond, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogVoltsToValueIntHack")]
        public static extern int getAnalogVoltsToValueIntHack(IntPtr analog_port_pointer, int voltage, ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogTriggerLimitsVoltageIntHack")]
        public static extern void setAnalogTriggerLimitsVoltageIntHack(IntPtr analog_trigger_pointer, int lower, int upper, ref int status);
    }
}
