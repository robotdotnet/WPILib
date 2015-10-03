using System;
using System.Linq;
using System.Runtime.InteropServices;
using HAL_Base;
using HAL_Simulator.Data;
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
        internal const int DefaultAverageBits = 7;
        internal const long DefaultLSBWeight = 1220703;
        internal const int DefaultOffset = 0;

        internal static readonly int[] AccumulatorChannels = { 0, 1 };

        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALAnalog.InitializeAnalogOutputPort = initializeAnalogOutputPort;
            HAL_Base.HALAnalog.SetAnalogOutput = setAnalogOutput;
            HAL_Base.HALAnalog.GetAnalogOutput = getAnalogOutput;
            HAL_Base.HALAnalog.CheckAnalogOutputChannel = checkAnalogOutputChannel;
            HAL_Base.HALAnalog.InitializeAnalogInputPort = initializeAnalogInputPort;
            HAL_Base.HALAnalog.CheckAnalogModule = checkAnalogModule;
            HAL_Base.HALAnalog.CheckAnalogInputChannel = checkAnalogInputChannel;
            HAL_Base.HALAnalog.SetAnalogSampleRate = setAnalogSampleRate;
            HAL_Base.HALAnalog.GetAnalogSampleRate = getAnalogSampleRate;
            HAL_Base.HALAnalog.SetAnalogAverageBits = setAnalogAverageBits;
            HAL_Base.HALAnalog.GetAnalogAverageBits = getAnalogAverageBits;
            HAL_Base.HALAnalog.SetAnalogOversampleBits = setAnalogOversampleBits;
            HAL_Base.HALAnalog.GetAnalogOversampleBits = getAnalogOversampleBits;
            HAL_Base.HALAnalog.GetAnalogValue = getAnalogValue;
            HAL_Base.HALAnalog.GetAnalogAverageValue = getAnalogAverageValue;
            HAL_Base.HALAnalog.GetAnalogVoltsToValue = getAnalogVoltsToValue;
            HAL_Base.HALAnalog.GetAnalogVoltage = getAnalogVoltage;
            HAL_Base.HALAnalog.GetAnalogAverageVoltage = getAnalogAverageVoltage;
            HAL_Base.HALAnalog.GetAnalogLSBWeight = getAnalogLSBWeight;
            HAL_Base.HALAnalog.GetAnalogOffset = getAnalogOffset;
            HAL_Base.HALAnalog.IsAccumulatorChannel = isAccumulatorChannel;
            HAL_Base.HALAnalog.InitAccumulator = initAccumulator;
            HAL_Base.HALAnalog.ResetAccumulator = resetAccumulator;
            HAL_Base.HALAnalog.SetAccumulatorCenter = setAccumulatorCenter;
            HAL_Base.HALAnalog.SetAccumulatorDeadband = setAccumulatorDeadband;
            HAL_Base.HALAnalog.GetAccumulatorValue = getAccumulatorValue;
            HAL_Base.HALAnalog.GetAccumulatorCount = getAccumulatorCount;
            HAL_Base.HALAnalog.GetAccumulatorOutput = getAccumulatorOutput;
            HAL_Base.HALAnalog.InitializeAnalogTrigger = initializeAnalogTrigger;
            HAL_Base.HALAnalog.CleanAnalogTrigger = cleanAnalogTrigger;
            HAL_Base.HALAnalog.SetAnalogTriggerLimitsRaw = setAnalogTriggerLimitsRaw;
            HAL_Base.HALAnalog.SetAnalogTriggerLimitsVoltage = setAnalogTriggerLimitsVoltage;
            HAL_Base.HALAnalog.SetAnalogTriggerAveraged = setAnalogTriggerAveraged;
            HAL_Base.HALAnalog.SetAnalogTriggerFiltered = setAnalogTriggerFiltered;
            HAL_Base.HALAnalog.GetAnalogTriggerInWindow = getAnalogTriggerInWindow;
            HAL_Base.HALAnalog.GetAnalogTriggerTriggerState = getAnalogTriggerTriggerState;
            HAL_Base.HALAnalog.GetAnalogTriggerOutput = getAnalogTriggerOutput;
        }

        //The HAL by default stores raw values instead of voltage behind the scenes. We are using voltage
        [CalledSimFunction]
        public static IntPtr initializeAnalogOutputPort(IntPtr port_pointer, ref int status)
        {
            status = 0;
            AnalogPort p = new AnalogPort()
            {
                port = GetHalPort(port_pointer)
            };
            AnalogOut[p.port.pin].Initialized = true;

            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        [CalledSimFunction]
        public static void setAnalogOutput(IntPtr analog_port_pointer, double voltage, ref int status)
        {
            status = 0;
            AnalogOut[GetAnalogPort(analog_port_pointer).port.pin].Voltage = voltage;
        }

        [CalledSimFunction]
        public static double getAnalogOutput(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return AnalogOut[GetAnalogPort(analog_port_pointer).port.pin].Voltage;
        }

        [CalledSimFunction]
        public static bool checkAnalogOutputChannel(uint pin)
        {
            return pin < AnalogOutputPins;
        }

        [CalledSimFunction]
        public static IntPtr initializeAnalogInputPort(IntPtr port_pointer, ref int status)
        {
            status = 0;
            AnalogPort p = new AnalogPort()
            {
                port = GetHalPort(port_pointer)
            };
            AnalogIn[p.port.pin].Initialized = true;

            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        [CalledSimFunction]
        public static bool checkAnalogModule(byte module)
        {
            return module == 1;
        }

        [CalledSimFunction]
        public static bool checkAnalogInputChannel(uint pin)
        {
            return pin < AnalogInputPins;
        }

        [CalledSimFunction]
        public static void setAnalogSampleRate(double samplesPerSecond, ref int status)
        {
            status = 0;
            SimData.GlobalData.AnalogSampleRate = samplesPerSecond;
        }

        [CalledSimFunction]
        public static float getAnalogSampleRate(ref int status)
        {
            status = 0;
            return (float)SimData.GlobalData.AnalogSampleRate;
        }

        [CalledSimFunction]
        public static void setAnalogAverageBits(IntPtr analog_port_pointer, uint bits, ref int status)
        {
            status = 0;
            AnalogIn[GetAnalogPort(analog_port_pointer).port.pin].AverageBits = bits;
        }

        [CalledSimFunction]
        public static uint getAnalogAverageBits(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return AnalogIn[GetAnalogPort(analog_port_pointer).port.pin].AverageBits;
        }

        [CalledSimFunction]
        public static void setAnalogOversampleBits(IntPtr analog_port_pointer, uint bits, ref int status)
        {
            status = 0;
            AnalogIn[GetAnalogPort(analog_port_pointer).port.pin].OversampleBits = bits;
        }

        [CalledSimFunction]
        public static uint getAnalogOversampleBits(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return AnalogIn[GetAnalogPort(analog_port_pointer).port.pin].OversampleBits;
        }

        [CalledSimFunction]
        public static short getAnalogValue(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            //Will need to port this to use voltage and scale it
            var value = getAnalogVoltsToValue(analog_port_pointer,
                AnalogIn[GetAnalogPort(analog_port_pointer).port.pin].Voltage, ref status);
            return (short)value;
        }

        [CalledSimFunction]
        public static int getAnalogAverageValue(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            //Just use regular voltage. Averaging doesn't work without constant updating
            return getAnalogValue(analog_port_pointer, ref status);
        }

        [CalledSimFunction]
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
            return (int)((voltage + offset * 1.0e-9) / (LSBWeight * 1.0e-9));
        }


        [CalledSimFunction]
        public static float getAnalogVoltage(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return (float)AnalogIn[GetAnalogPort(analog_port_pointer).port.pin].Voltage;
        }

        [CalledSimFunction]
        public static float getAnalogAverageVoltage(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            //Just use regular voltage. Averaging doesn't work without constant updating
            return (float)AnalogIn[GetAnalogPort(analog_port_pointer).port.pin].Voltage;
        }

        [CalledSimFunction]
        public static uint getAnalogLSBWeight(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return (uint)AnalogIn[GetAnalogPort(analog_port_pointer).port.pin].LSBWeight;
        }

        [CalledSimFunction]
        public static int getAnalogOffset(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return AnalogIn[GetAnalogPort(analog_port_pointer).port.pin].Offset;
        }

        [CalledSimFunction]
        public static bool isAccumulatorChannel(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return AccumulatorChannels.Contains(GetAnalogPort(analog_port_pointer).port.pin);
        }

        [CalledSimFunction]
        public static void initAccumulator(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            AnalogIn[GetAnalogPort(analog_port_pointer).port.pin].AccumulatorInitialized = true;
        }

        [CalledSimFunction]
        public static void resetAccumulator(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            int pin = GetAnalogPort(analog_port_pointer).port.pin;
            AnalogIn[pin].AccumulatorCenter = 0;
            AnalogIn[pin].AccumulatorCount = 1;
            AnalogIn[pin].AccumulatorValue = 0;
        }

        [CalledSimFunction]
        public static void setAccumulatorCenter(IntPtr analog_port_pointer, int center, ref int status)
        {
            status = 0;
            AnalogIn[GetAnalogPort(analog_port_pointer).port.pin].AccumulatorCenter = center;
        }

        [CalledSimFunction]
        public static void setAccumulatorDeadband(IntPtr analog_port_pointer, int deadband, ref int status)
        {
            status = 0;
            AnalogIn[GetAnalogPort(analog_port_pointer).port.pin].AccumulatorDeadband = deadband;
        }

        [CalledSimFunction]
        public static long getAccumulatorValue(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return AnalogIn[GetAnalogPort(analog_port_pointer).port.pin].AccumulatorValue;
        }

        [CalledSimFunction]
        public static uint getAccumulatorCount(IntPtr analog_port_pointer, ref int status)
        {
            if (!isAccumulatorChannel(analog_port_pointer, ref status))
            {
                status = -1004;
                return 0;
            }
            status = 0;
            return AnalogIn[GetAnalogPort(analog_port_pointer).port.pin].AccumulatorCount;
        }

        [CalledSimFunction]
        public static void getAccumulatorOutput(IntPtr analog_port_pointer, ref long value, ref uint count,
            ref int status)
        {
            status = 0;
            count = AnalogIn[GetAnalogPort(analog_port_pointer).port.pin].AccumulatorCount;
            value = AnalogIn[GetAnalogPort(analog_port_pointer).port.pin].AccumulatorValue;
        }

        [CalledSimFunction]
        public static IntPtr initializeAnalogTrigger(IntPtr port_pointer, ref uint index, ref int status)
        {
            status = 0;

            for (int i = 0; i < SimData.AnalogTrigger.Count; i++)
            {
                var cnt = SimData.AnalogTrigger[i];
                if (cnt.Initialized == false)
                {
                    IntPtr aPt = initializeAnalogInputPort(port_pointer, ref status);
                    cnt.Initialized = true;
                    cnt.AnalogPin = GetHalPort(port_pointer).pin;
                    AnalogTrigger trig = new AnalogTrigger()
                    {
                        analogPortPointer = aPt,
                        
                    };
                    index = (uint)i;
                    trig.index = i;
                    IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(trig));
                    Marshal.StructureToPtr(trig, ptr, true);
                    cnt.TriggerPointer = ptr.ToInt64();
                    
                    return ptr;
                }
            }
            status = NO_AVAILABLE_RESOURCES;
            return IntPtr.Zero;
        }

        [CalledSimFunction]
        public static void cleanAnalogTrigger(IntPtr analog_trigger_pointer, ref int status)
        {
            status = 0;
            SimData.AnalogTrigger[GetAnalogTrigger(analog_trigger_pointer).index].Initialized = false;
        }

        private static double getaAnalogValueToVoltage(IntPtr analog_port_pointer, int value, ref int status)
        {
            uint LSBWeight = getAnalogLSBWeight(analog_port_pointer, ref status);
            int offset = getAnalogOffset(analog_port_pointer, ref status);

            double voltage = LSBWeight * 1.0e-9 * value - offset * 1.0e-9;
            return voltage;
        }

        [CalledSimFunction]
        public static void setAnalogTriggerLimitsRaw(IntPtr analog_trigger_pointer, int lower, int upper, ref int status)
        {
            if (lower > upper)
            {
                status = ANALOG_TRIGGER_LIMIT_ORDER_ERROR;
            }
            else
            {
                var port = GetAnalogTrigger(analog_trigger_pointer).analogPortPointer;
                status = 0;
                SimData.AnalogTrigger[GetAnalogTrigger(analog_trigger_pointer).index].TrigLower
                    = getaAnalogValueToVoltage(port, lower, ref status);
                SimData.AnalogTrigger[GetAnalogTrigger(analog_trigger_pointer).index].TrigUpper
                    = getaAnalogValueToVoltage(port, upper, ref status);
            }
        }

        [CalledSimFunction]
        public static void setAnalogTriggerLimitsVoltage(IntPtr analog_trigger_pointer, double lower,
            double upper, ref int status)
        {
            if (lower > upper)
            {
                status = ANALOG_TRIGGER_LIMIT_ORDER_ERROR;
            }
            else
            {
                status = 0;
                SimData.AnalogTrigger[GetAnalogTrigger(analog_trigger_pointer).index].TrigLower = lower;
                SimData.AnalogTrigger[GetAnalogTrigger(analog_trigger_pointer).index].TrigUpper = upper;
            }
        }

        [CalledSimFunction]
        public static void setAnalogTriggerAveraged(IntPtr analog_trigger_pointer, bool useAveragedValue, ref int status)
        {
            var trigPort = GetAnalogTrigger(analog_trigger_pointer);
            if (SimData.AnalogTrigger[trigPort.index].TrigType == TrigerType.Filtered)
            {
                status = INCOMPATIBLE_STATE;
            }
            else
            {
                status = 0;
                TrigerType val = useAveragedValue ? TrigerType.Averaged : TrigerType.Unassigned;
                SimData.AnalogTrigger[trigPort.index].TrigType = val;
            }
        }

        [CalledSimFunction]
        public static void setAnalogTriggerFiltered(IntPtr analog_trigger_pointer,
            bool useFilteredValue, ref int status)
        {
            var trigPort = GetAnalogTrigger(analog_trigger_pointer);
            if (SimData.AnalogTrigger[trigPort.index].TrigType == TrigerType.Averaged)
            {
                status = INCOMPATIBLE_STATE;
            }
            else
            {
                status = 0;
                TrigerType val = useFilteredValue ? TrigerType.Filtered : TrigerType.Unassigned;
                SimData.AnalogTrigger[trigPort.index].TrigType = val;
            }
        }

        private static double getTriggerValue(AnalogTrigger trigger)
        {
            var ain = AnalogIn[GetAnalogPort(trigger.analogPortPointer).port.pin];
            var atr = SimData.AnalogTrigger[trigger.index];
            var trigType = atr.TrigType;
            switch (trigType)
            {
                case TrigerType.Unassigned:
                    return ain.Voltage;
                case TrigerType.Averaged:
                    return ain.Voltage;
                case TrigerType.Filtered:
                    return ain.Voltage;
            }
            throw new ArgumentOutOfRangeException(nameof(trigger), "Analog Trigger must be either filtered, averaged or null.");

        }

        [CalledSimFunction]
        public static bool getAnalogTriggerInWindow(IntPtr analog_trigger_pointer, ref int status)
        {
            status = 0;
            var trig = GetAnalogTrigger(analog_trigger_pointer);
            var val = getTriggerValue(trig);
            var atr = SimData.AnalogTrigger[trig.index];
            return val >= atr.TrigLower && val <= atr.TrigUpper;
        }

        [CalledSimFunction]
        public static bool getAnalogTriggerTriggerState(IntPtr analog_trigger_pointer, ref int status)
        {
            status = 0;
            var trig = GetAnalogTrigger(analog_trigger_pointer);
            var val = getTriggerValue(trig);
            var atr = SimData.AnalogTrigger[trig.index];
            if (val < atr.TrigLower)
            {
                atr.TrigState = false;
                return false;
            }
            if (val > atr.TrigUpper)
            {
                atr.TrigState = true;
                return true;
            }
            return atr.TrigState;
        }


        [CalledSimFunction]
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
    }
}
