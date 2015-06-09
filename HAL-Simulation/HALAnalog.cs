using System;
using System.Runtime.InteropServices;
using HAL_Base;

namespace HAL_FRC
{
    /*
    public enum AnalogTriggerType
    {
        /// kInWindow -> 0
        kInWindow = 0,

        /// kState -> 1
        kState = 1,

        /// kRisingPulse -> 2
        kRisingPulse = 2,

        /// kFallingPulse -> 3
        kFallingPulse = 3,
    }
     * */

    public class HALAnalog
    {
        /// Return Type: void*
        ///port_pointer: void*
        ///status: int*
        [DllImport("<>", EntryPoint = "initializeAnalogOutputPort")]
        public static extern IntPtr initializeAnalogOutputPort(IntPtr port_pointer, ref int status);


        /// Return Type: void
        ///analog_port_pointer: void*
        ///voltage: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogOutput")]
        public static extern void setAnalogOutput(IntPtr analog_port_pointer, double voltage, ref int status);


        /// Return Type: double
        ///analog_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogOutput")]
        public static extern double getAnalogOutput(IntPtr analog_port_pointer, ref int status);


        /// Return Type: boolean
        ///pin: unsigned int
        [DllImport("libHALAthena_shared.so", EntryPoint = "checkAnalogOutputChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkAnalogOutputChannel(uint pin);


        /// Return Type: void*
        ///port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeAnalogInputPort")]
        public static extern IntPtr initializeAnalogInputPort(IntPtr port_pointer, ref int status);


        /// Return Type: boolean
        ///module: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "checkAnalogModule")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkAnalogModule(byte module);


        /// Return Type: boolean
        ///pin: unsigned int
        [DllImport("libHALAthena_shared.so", EntryPoint = "checkAnalogInputChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkAnalogInputChannel(uint pin);


        /// Return Type: void
        ///samplesPerSecond: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogSampleRate")]
        public static extern void setAnalogSampleRate(double samplesPerSecond, ref int status);


        /// Return Type: float
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogSampleRate")]
        public static extern float getAnalogSampleRate(ref int status);


        /// Return Type: void
        ///analog_port_pointer: void*
        ///bits: unsigned int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogAverageBits")]
        public static extern void setAnalogAverageBits(IntPtr analog_port_pointer, uint bits, ref int status);


        /// Return Type: unsigned int
        ///analog_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogAverageBits")]
        public static extern uint getAnalogAverageBits(IntPtr analog_port_pointer, ref int status);


        /// Return Type: void
        ///analog_port_pointer: void*
        ///bits: unsigned int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogOversampleBits")]
        public static extern void setAnalogOversampleBits(IntPtr analog_port_pointer, uint bits, ref int status);


        /// Return Type: unsigned int
        ///analog_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogOversampleBits")]
        public static extern uint getAnalogOversampleBits(IntPtr analog_port_pointer, ref int status);


        /// Return Type: short
        ///analog_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogValue")]
        public static extern short getAnalogValue(IntPtr analog_port_pointer, ref int status);


        /// Return Type: int
        ///analog_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogAverageValue")]
        public static extern int getAnalogAverageValue(IntPtr analog_port_pointer, ref int status);


        /// Return Type: int
        ///analog_port_pointer: void*
        ///voltage: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogVoltsToValue")]
        public static extern int getAnalogVoltsToValue(IntPtr analog_port_pointer, double voltage, ref int status);


        /// Return Type: float
        ///analog_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogVoltage")]
        public static extern float getAnalogVoltage(IntPtr analog_port_pointer, ref int status);


        /// Return Type: float
        ///analog_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogAverageVoltage")]
        public static extern float getAnalogAverageVoltage(IntPtr analog_port_pointer, ref int status);


        /// Return Type: unsigned int
        ///analog_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogLSBWeight")]
        public static extern uint getAnalogLSBWeight(IntPtr analog_port_pointer, ref int status);


        /// Return Type: int
        ///analog_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogOffset")]
        public static extern int getAnalogOffset(IntPtr analog_port_pointer, ref int status);


        /// Return Type: boolean
        ///analog_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "isAccumulatorChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool isAccumulatorChannel(IntPtr analog_port_pointer, ref int status);


        /// Return Type: void
        ///analog_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "initAccumulator")]
        public static extern void initAccumulator(IntPtr analog_port_pointer, ref int status);


        /// Return Type: void
        ///analog_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "resetAccumulator")]
        public static extern void resetAccumulator(IntPtr analog_port_pointer, ref int status);


        /// Return Type: void
        ///analog_port_pointer: void*
        ///center: int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setAccumulatorCenter")]
        public static extern void setAccumulatorCenter(IntPtr analog_port_pointer, int center, ref int status);


        /// Return Type: void
        ///analog_port_pointer: void*
        ///deadband: int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setAccumulatorDeadband")]
        public static extern void setAccumulatorDeadband(IntPtr analog_port_pointer, int deadband, ref int status);


        /// Return Type: int
        ///analog_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAccumulatorValue")]
        public static extern long getAccumulatorValue(IntPtr analog_port_pointer, ref int status);


        /// Return Type: unsigned int
        ///analog_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAccumulatorCount")]
        public static extern uint getAccumulatorCount(IntPtr analog_port_pointer, ref int status);


        /// Return Type: void
        ///analog_port_pointer: void*
        ///value: int*
        ///count: unsigned int*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAccumulatorOutput")]
        public static extern void getAccumulatorOutput(IntPtr analog_port_pointer, ref long value, ref uint count, ref int status);


        /// Return Type: void*
        ///port_pointer: void*
        ///index: unsigned int*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeAnalogTrigger")]
        public static extern IntPtr initializeAnalogTrigger(IntPtr port_pointer, ref uint index, ref int status);


        /// Return Type: void
        ///analog_trigger_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "cleanAnalogTrigger")]
        public static extern void cleanAnalogTrigger(IntPtr analog_trigger_pointer, ref int status);


        /// Return Type: void
        ///analog_trigger_pointer: void*
        ///lower: int
        ///upper: int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogTriggerLimitsRaw")]
        public static extern void setAnalogTriggerLimitsRaw(IntPtr analog_trigger_pointer, int lower, int upper, ref int status);


        /// Return Type: void
        ///analog_trigger_pointer: void*
        ///lower: double
        ///upper: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogTriggerLimitsVoltage")]
        public static extern void setAnalogTriggerLimitsVoltage(IntPtr analog_trigger_pointer, double lower, double upper, ref int status);


        /// Return Type: void
        ///analog_trigger_pointer: void*
        ///useAveragedValue: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogTriggerAveraged")]
        public static extern void setAnalogTriggerAveraged(IntPtr analog_trigger_pointer, [MarshalAs(UnmanagedType.I1)] bool useAveragedValue, ref int status);


        /// Return Type: void
        ///analog_trigger_pointer: void*
        ///useFilteredValue: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogTriggerFiltered")]
        public static extern void setAnalogTriggerFiltered(IntPtr analog_trigger_pointer, [MarshalAs(UnmanagedType.I1)] bool useFilteredValue, ref int status);


        /// Return Type: boolean
        ///analog_trigger_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogTriggerInWindow")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getAnalogTriggerInWindow(IntPtr analog_trigger_pointer, ref int status);


        /// Return Type: boolean
        ///analog_trigger_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogTriggerTriggerState")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getAnalogTriggerTriggerState(IntPtr analog_trigger_pointer, ref int status);


        /// Return Type: boolean
        ///analog_trigger_pointer: void*
        ///type: AnalogTriggerType
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogTriggerOutput")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getAnalogTriggerOutput(IntPtr analog_trigger_pointer, AnalogTriggerType type, ref int status);


        /// Return Type: int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogSampleRateIntHack")]
        public static extern int getAnalogSampleRateIntHack(ref int status);


        /// Return Type: int
        ///analog_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogVoltageIntHack")]
        public static extern int getAnalogVoltageIntHack(IntPtr analog_port_pointer, ref int status);


        /// Return Type: int
        ///analog_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogAverageVoltageIntHack")]
        public static extern int getAnalogAverageVoltageIntHack(IntPtr analog_port_pointer, ref int status);


        /// Return Type: void
        ///samplesPerSecond: int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogSampleRateIntHack")]
        public static extern void setAnalogSampleRateIntHack(int samplesPerSecond, ref int status);


        /// Return Type: int
        ///analog_port_pointer: void*
        ///voltage: int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getAnalogVoltsToValueIntHack")]
        public static extern int getAnalogVoltsToValueIntHack(IntPtr analog_port_pointer, int voltage, ref int status);


        /// Return Type: void
        ///analog_trigger_pointer: void*
        ///lower: int
        ///upper: int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setAnalogTriggerLimitsVoltageIntHack")]
        public static extern void setAnalogTriggerLimitsVoltageIntHack(IntPtr analog_trigger_pointer, int lower, int upper, ref int status);
    }
}
