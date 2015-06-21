using System;
using System.Linq;
using System.Runtime.InteropServices;
using HAL_Base;
using static HAL_Simulator.SimData;
using static HAL_Simulator.PortConverters;

namespace HAL_Simulator
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
        internal const uint Timebase = 40000000;
        internal const int DefaultOversampleBits = 0;
        internal const double DefaultSampleRate = 50000.0;
        internal const int AnalogInputPins = 8;
        internal const int AnalogOutputPins = 2;
        internal const int VOLTAGE_OUT_OF_RANGE = 1002;
        internal const int NO_AVAILABLE_RESOURCES = -1004;

        internal const int AccumulatorNumChannels = 2;
        internal static readonly int[] AccumulatorChannels = {0, 1};

        internal const int ANALOG_TRIGGER_PULSE_OUTPUT_ERROR = -1011;

        public static IntPtr initializeAnalogOutputPort(IntPtr port_pointer, ref int status)
        {
            status = 0;
            AnalogPort p = new AnalogPort()
            {
                port = GetHalPort(port_pointer)
            };
            halData["analog_out"][p.port.pin]["initialized"] = true;

            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        public static void setAnalogOutput(IntPtr analog_port_pointer, double voltage, ref int status)
        {
            status = 0;
            halData["analog_out"][GetAnalogPort(analog_port_pointer).port.pin]["output"] = voltage;
        }

        public static double getAnalogOutput(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_out"][GetAnalogPort(analog_port_pointer).port.pin]["output"];
        }

        public static bool checkAnalogOutputChannel(uint pin)
        {
            return pin < AnalogOutputPins;
        }

        public static IntPtr initializeAnalogInputPort(IntPtr port_pointer, ref int status)
        {
            status = 0;
            AnalogPort p = new AnalogPort()
            {
                port = GetHalPort(port_pointer)
            };
            halData["analog_in"][p.port.pin]["initialized"] = true;

            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        public static bool checkAnalogModule(byte module)
        {
            return module == 1;
        }

        public static bool checkAnalogInputChannel(uint pin)
        {
            return pin < AnalogInputPins;
        }

        public static void setAnalogSampleRate(double samplesPerSecond, ref int status)
        {
            status = 0;
            halData["analog_sample_rate"] = samplesPerSecond;
        }

        public static float getAnalogSampleRate(ref int status)
        {
            status = 0;
            return halData["analog_sample_rate"];
        }

        public static void setAnalogAverageBits(IntPtr analog_port_pointer, uint bits, ref int status)
        {
            status = 0;
            halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["avg_bits"] = bits;
        }

        public static uint getAnalogAverageBits(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["avg_bits"];
        }

        public static void setAnalogOversampleBits(IntPtr analog_port_pointer, uint bits, ref int status)
        {
            status = 0;
            halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["oversample_bits"] = bits;
        }

        public static uint getAnalogOversampleBits(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["oversample_bits"];
        }

        public static short getAnalogValue(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["value"];
        }

        public static int getAnalogAverageValue(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["avg_value"];
        }

        public static int getAnalogVoltsToValue(IntPtr analog_port_pointer, double voltage, ref int status)
        {
            status = 0;
            if (voltage > 5.0)
            {
                voltage = 5.0;
                status = VOLTAGE_OUT_OF_RANGE;
            }
            else if (voltage < 0.0)
            {
                voltage = 0.0;
                status = VOLTAGE_OUT_OF_RANGE;
            }

            var LSBWeight = getAnalogLSBWeight(analog_port_pointer, ref status);
            var offset = getAnalogOffset(analog_port_pointer, ref status);
            return (int) ((voltage + offset*1.0e-9)/(LSBWeight*1.0e-9));
        }

        public static float getAnalogVoltage(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["voltage"];
        }

        public static float getAnalogAverageVoltage(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["avg_voltage"];
        }

        public static uint getAnalogLSBWeight(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["lsb_weight"];
        }

        public static int getAnalogOffset(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["offset"];
        }

        public static bool isAccumulatorChannel(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return AccumulatorChannels.Contains(GetAnalogPort(analog_port_pointer).port.pin);
        }

        public static void initAccumulator(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["accumulator_initialized"] = true;
        }

        public static void resetAccumulator(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            int pin = GetAnalogPort(analog_port_pointer).port.pin;
            halData["analog_in"][pin]["accumulator_center"] = 0;
            halData["analog_in"][pin]["accumulator_count"] = 1;
            halData["analog_in"][pin]["accumulator_value"] = 0;
        }

        public static void setAccumulatorCenter(IntPtr analog_port_pointer, int center, ref int status)
        {
            status = 0;
            halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["accumulator_center"] = center;
        }

        public static void setAccumulatorDeadband(IntPtr analog_port_pointer, int deadband, ref int status)
        {
            status = 0;
            halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["accumulator_deadband"] = deadband;
        }

        public static long getAccumulatorValue(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["accumulator_value"];
        }

        public static uint getAccumulatorCount(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["accumulator_count"];
        }

        public static void getAccumulatorOutput(IntPtr analog_port_pointer, ref long value, ref uint count,
            ref int status)
        {
            status = 0;
            count = halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["accumulator_count"];
            value = halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["accumulator_value"];
        }

        public static IntPtr initializeAnalogTrigger(IntPtr port_pointer, ref uint index, ref int status)
        {
            status = 0;

            Port pt = GetHalPort(port_pointer);
            for (int i = 0; i < halData["analog_trigger"].Count; i++)
            {
                var cnt = halData["analog_trigger"][i];
                if (cnt["initialized"] == false)
                {
                    cnt["initialized"] = true;
                    cnt["port"] = pt;
                    AnalogTrigger trig = new AnalogTrigger()
                    {
                        port = pt,
                        index = (uint) i,
                    };
                    IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(trig));
                    Marshal.StructureToPtr(trig, ptr, true);
                    return ptr;
                }
            }
            status = NO_AVAILABLE_RESOURCES;
            return  IntPtr.Zero;
        }

        public static void cleanAnalogTrigger(IntPtr analog_trigger_pointer, ref int status)
        {
            status = 0;
            halData["analog_trigger"][GetAnalogTrigger(analog_trigger_pointer).index]["initialized"] = false;
        }


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


        public static bool getAnalogTriggerOutput(IntPtr analog_trigger_pointer, AnalogTriggerType type, ref int status)
        {
            if (type == AnalogTriggerType.InWindow)
                return getAnalogTriggerInWindow(analog_trigger_pointer, ref status);
            if (type == AnalogTriggerType.State)
                return getAnalogTriggerTriggerState(analog_trigger_pointer, ref status);
            else
            {
                status = ANALOG_TRIGGER_PULSE_OUTPUT_ERROR;
                return false;
            }
        }


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
