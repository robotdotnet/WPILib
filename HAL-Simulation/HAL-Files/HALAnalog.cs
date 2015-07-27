using System;
using System.Linq;
using System.Runtime.InteropServices;
using HAL_Base;
using static HAL_Simulator.SimData;
using static HAL_Simulator.PortConverters;
using static HAL_Simulator.HALErrorConstants;

// ReSharper disable RedundantAssignment
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    internal class HALAnalog
    {
        internal const uint Timebase = 40000000;
        internal const int DefaultOversampleBits = 0;
        internal const double DefaultSampleRate = 50000.0;
        internal const int AnalogInputPins = 8;
        internal const int AnalogOutputPins = 2;
        internal const int AccumulatorNumChannels = 2;

        internal static readonly int[] AccumulatorChannels = { 0, 1 };


        [CalledSimFunction]
        internal static IntPtr initializeAnalogOutputPort(IntPtr port_pointer, ref int status)
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

        [CalledSimFunction]
        internal static void setAnalogOutput(IntPtr analog_port_pointer, double voltage, ref int status)
        {
            status = 0;
            halData["analog_out"][GetAnalogPort(analog_port_pointer).port.pin]["output"] = voltage;
        }

        [CalledSimFunction]
        internal static double getAnalogOutput(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_out"][GetAnalogPort(analog_port_pointer).port.pin]["output"];
        }

        [CalledSimFunction]
        internal static bool checkAnalogOutputChannel(uint pin)
        {
            return pin < AnalogOutputPins;
        }

        [CalledSimFunction]
        internal static IntPtr initializeAnalogInputPort(IntPtr port_pointer, ref int status)
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

        [CalledSimFunction]
        internal static bool checkAnalogModule(byte module)
        {
            return module == 1;
        }

        [CalledSimFunction]
        internal static bool checkAnalogInputChannel(uint pin)
        {
            return pin < AnalogInputPins;
        }

        [CalledSimFunction]
        internal static void setAnalogSampleRate(double samplesPerSecond, ref int status)
        {
            status = 0;
            halData["analog_sample_rate"] = samplesPerSecond;
        }

        [CalledSimFunction]
        internal static float getAnalogSampleRate(ref int status)
        {
            status = 0;
            return (float)halData["analog_sample_rate"];
        }

        [CalledSimFunction]
        internal static void setAnalogAverageBits(IntPtr analog_port_pointer, uint bits, ref int status)
        {
            status = 0;
            halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["avg_bits"] = bits;
        }

        [CalledSimFunction]
        internal static uint getAnalogAverageBits(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["avg_bits"];
        }

        [CalledSimFunction]
        internal static void setAnalogOversampleBits(IntPtr analog_port_pointer, uint bits, ref int status)
        {
            status = 0;
            halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["oversample_bits"] = bits;
        }

        [CalledSimFunction]
        internal static uint getAnalogOversampleBits(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["oversample_bits"];
        }

        [CalledSimFunction]
        internal static short getAnalogValue(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["value"];
        }

        [CalledSimFunction]
        internal static int getAnalogAverageValue(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["avg_value"];
        }

        [CalledSimFunction]
        internal static int getAnalogVoltsToValue(IntPtr analog_port_pointer, double voltage, ref int status)
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
            return (int)((voltage + offset * 1.0e-9) / (LSBWeight * 1.0e-9));
        }

        [CalledSimFunction]
        internal static float getAnalogVoltage(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return (float)halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["voltage"];
        }

        [CalledSimFunction]
        internal static float getAnalogAverageVoltage(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return (float)halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["avg_voltage"];
        }

        [CalledSimFunction]
        internal static uint getAnalogLSBWeight(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return (uint)halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["lsb_weight"];
        }

        [CalledSimFunction]
        internal static int getAnalogOffset(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["offset"];
        }

        [CalledSimFunction]
        internal static bool isAccumulatorChannel(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return AccumulatorChannels.Contains(GetAnalogPort(analog_port_pointer).port.pin);
        }

        [CalledSimFunction]
        internal static void initAccumulator(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["accumulator_initialized"] = true;
        }

        [CalledSimFunction]
        internal static void resetAccumulator(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            int pin = GetAnalogPort(analog_port_pointer).port.pin;
            halData["analog_in"][pin]["accumulator_center"] = 0;
            halData["analog_in"][pin]["accumulator_count"] = 1;
            halData["analog_in"][pin]["accumulator_value"] = 0;
        }

        [CalledSimFunction]
        internal static void setAccumulatorCenter(IntPtr analog_port_pointer, int center, ref int status)
        {
            status = 0;
            halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["accumulator_center"] = center;
        }

        [CalledSimFunction]
        internal static void setAccumulatorDeadband(IntPtr analog_port_pointer, int deadband, ref int status)
        {
            status = 0;
            halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["accumulator_deadband"] = deadband;
        }

        [CalledSimFunction]
        internal static long getAccumulatorValue(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["accumulator_value"];
        }

        [CalledSimFunction]
        internal static uint getAccumulatorCount(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["accumulator_count"];
        }

        [CalledSimFunction]
        internal static void getAccumulatorOutput(IntPtr analog_port_pointer, ref long value, ref uint count,
            ref int status)
        {
            status = 0;
            count = (uint)halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["accumulator_count"];
            value = halData["analog_in"][GetAnalogPort(analog_port_pointer).port.pin]["accumulator_value"];
        }

        [CalledSimFunction]
        internal static IntPtr initializeAnalogTrigger(IntPtr port_pointer, ref uint index, ref int status)
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
                        portPtr = port_pointer,
                        port = pt,
                        index = i,
                    };
                    IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(trig));
                    Marshal.StructureToPtr(trig, ptr, true);
                    return ptr;
                }
            }
            status = NO_AVAILABLE_RESOURCES;
            return IntPtr.Zero;
        }

        [CalledSimFunction]
        internal static void cleanAnalogTrigger(IntPtr analog_trigger_pointer, ref int status)
        {
            status = 0;
            halData["analog_trigger"][GetAnalogTrigger(analog_trigger_pointer).index]["initialized"] = false;
        }

        [CalledSimFunction]
        internal static void setAnalogTriggerLimitsRaw(IntPtr analog_trigger_pointer, int lower, int upper, ref int status)
        {
            if (lower > upper)
            {
                status = ANALOG_TRIGGER_LIMIT_ORDER_ERROR;
            }
            else
            {
                status = 0;
                halData["analog_trigger"][GetAnalogTrigger(analog_trigger_pointer).index]["trig_lower"] = lower;
                halData["analog_trigger"][GetAnalogTrigger(analog_trigger_pointer).index]["trig_upper"] = upper;
            }
        }

        [CalledSimFunction]
        internal static void setAnalogTriggerLimitsVoltage(IntPtr analog_trigger_pointer, double lower,
            double upper, ref int status)
        {
            if (lower > upper)
            {
                status = ANALOG_TRIGGER_LIMIT_ORDER_ERROR;
            }
            else
            {
                status = 0;
                var port = GetAnalogTrigger(analog_trigger_pointer).portPtr;
                halData["analog_trigger"][GetAnalogTrigger(analog_trigger_pointer).index]["trig_lower"] = getAnalogVoltsToValue(port, lower, ref status);
                halData["analog_trigger"][GetAnalogTrigger(analog_trigger_pointer).index]["trig_upper"] = getAnalogVoltsToValue(port, upper, ref status);
            }
        }

        [CalledSimFunction]
        internal static void setAnalogTriggerAveraged(IntPtr analog_trigger_pointer, bool useAveragedValue, ref int status)
        {
            var trigPort = GetAnalogTrigger(analog_trigger_pointer);
            if (halData["analog_trigger"][trigPort.index]["trig_type"] == "filtered")
            {
                status = INCOMPATIBLE_STATE;
            }
            else
            {
                status = 0;
                string val = useAveragedValue ? "averaged" : null;
                halData["analog_trigger"][trigPort.index]["trig_type"] = val;
            }
        }

        [CalledSimFunction]
        internal static void setAnalogTriggerFiltered(IntPtr analog_trigger_pointer,
            bool useFilteredValue, ref int status)
        {
            var trigPort = GetAnalogTrigger(analog_trigger_pointer);
            if (halData["analog_trigger"][trigPort.index]["trig_type"] == "averaged")
            {
                status = INCOMPATIBLE_STATE;
            }
            else
            {
                status = 0;
                string val = useFilteredValue ? "filtered" : null;
                halData["analog_trigger"][trigPort.index]["trig_type"] = val;
            }
        }

        private static dynamic getTriggerValue(AnalogTrigger trigger)
        {
            var ain = halData["analog_in"][trigger.port.pin];
            var atr = halData["analog_trigger"][trigger.index];
            var trigType = atr["trig_type"];
            if (trigType == null)
            {
                return ain["value"];
            }
            if (trigType == "averaged")
            {
                return ain["avg_value"];
            }
            if (trigType == "filtered")
            {
                return ain["value"];
            }
            throw new ArgumentOutOfRangeException(nameof(trigger), "Analog Trigger must be either filtered, averaged or null.");

        }

        [CalledSimFunction]
        internal static bool getAnalogTriggerInWindow(IntPtr analog_trigger_pointer, ref int status)
        {
            status = 0;
            var trig = GetAnalogTrigger(analog_trigger_pointer);
            var val = getTriggerValue(trig);
            var atr = halData["analog_trigger"][trig.index];
            return val >= atr["trig_lower"] && val <= atr["trig_upper"];
        }

        [CalledSimFunction]
        internal static bool getAnalogTriggerTriggerState(IntPtr analog_trigger_pointer, ref int status)
        {
            status = 0;
            var trig = GetAnalogTrigger(analog_trigger_pointer);
            var val = getTriggerValue(trig);
            var atr = halData["analog_trigger"][trig.index];
            if (val < atr["trig_lower"])
            {
                atr["trig_state"] = false;
                return false;
            }
            if (val > atr["trig_upper"])
            {
                atr["trig_state"] = true;
                return true;
            }
            return atr["trig_state"];
        }


        [CalledSimFunction]
        internal static bool getAnalogTriggerOutput(IntPtr analog_trigger_pointer, AnalogTriggerType type, ref int status)
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
    }
}
