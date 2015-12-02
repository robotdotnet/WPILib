using System;
using System.Linq;
using System.Runtime.InteropServices;
using HAL.Simulator;
using HAL.Simulator.Data;
using static HAL.Simulator.SimData;

// ReSharper disable RedundantAssignment
#pragma warning disable 1591

namespace HAL.SimulatorHAL
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
            global::HAL.HALAnalog.InitializeAnalogOutputPort = initializeAnalogOutputPort;
            global::HAL.HALAnalog.FreeAnalogOutputPort = freeAnalogOutputPort;
            global::HAL.HALAnalog.FreeAnalogInputPort = freeAnalogInputPort;
            global::HAL.HALAnalog.SetAnalogOutput = setAnalogOutput;
            global::HAL.HALAnalog.GetAnalogOutput = getAnalogOutput;
            global::HAL.HALAnalog.CheckAnalogOutputChannel = checkAnalogOutputChannel;
            global::HAL.HALAnalog.InitializeAnalogInputPort = initializeAnalogInputPort;
            global::HAL.HALAnalog.CheckAnalogModule = checkAnalogModule;
            global::HAL.HALAnalog.CheckAnalogInputChannel = checkAnalogInputChannel;
            global::HAL.HALAnalog.SetAnalogSampleRate = setAnalogSampleRate;
            global::HAL.HALAnalog.GetAnalogSampleRate = getAnalogSampleRate;
            global::HAL.HALAnalog.SetAnalogAverageBits = setAnalogAverageBits;
            global::HAL.HALAnalog.GetAnalogAverageBits = getAnalogAverageBits;
            global::HAL.HALAnalog.SetAnalogOversampleBits = setAnalogOversampleBits;
            global::HAL.HALAnalog.GetAnalogOversampleBits = getAnalogOversampleBits;
            global::HAL.HALAnalog.GetAnalogValue = getAnalogValue;
            global::HAL.HALAnalog.GetAnalogAverageValue = getAnalogAverageValue;
            global::HAL.HALAnalog.GetAnalogVoltsToValue = getAnalogVoltsToValue;
            global::HAL.HALAnalog.GetAnalogVoltage = getAnalogVoltage;
            global::HAL.HALAnalog.GetAnalogAverageVoltage = getAnalogAverageVoltage;
            global::HAL.HALAnalog.GetAnalogLSBWeight = getAnalogLSBWeight;
            global::HAL.HALAnalog.GetAnalogOffset = getAnalogOffset;
            global::HAL.HALAnalog.IsAccumulatorChannel = isAccumulatorChannel;
            global::HAL.HALAnalog.InitAccumulator = initAccumulator;
            global::HAL.HALAnalog.ResetAccumulator = resetAccumulator;
            global::HAL.HALAnalog.SetAccumulatorCenter = setAccumulatorCenter;
            global::HAL.HALAnalog.SetAccumulatorDeadband = setAccumulatorDeadband;
            global::HAL.HALAnalog.GetAccumulatorValue = getAccumulatorValue;
            global::HAL.HALAnalog.GetAccumulatorCount = getAccumulatorCount;
            global::HAL.HALAnalog.GetAccumulatorOutput = getAccumulatorOutput;
            global::HAL.HALAnalog.InitializeAnalogTrigger = initializeAnalogTrigger;
            global::HAL.HALAnalog.CleanAnalogTrigger = cleanAnalogTrigger;
            global::HAL.HALAnalog.SetAnalogTriggerLimitsRaw = setAnalogTriggerLimitsRaw;
            global::HAL.HALAnalog.SetAnalogTriggerLimitsVoltage = setAnalogTriggerLimitsVoltage;
            global::HAL.HALAnalog.SetAnalogTriggerAveraged = setAnalogTriggerAveraged;
            global::HAL.HALAnalog.SetAnalogTriggerFiltered = setAnalogTriggerFiltered;
            global::HAL.HALAnalog.GetAnalogTriggerInWindow = getAnalogTriggerInWindow;
            global::HAL.HALAnalog.GetAnalogTriggerTriggerState = getAnalogTriggerTriggerState;
            global::HAL.HALAnalog.GetAnalogTriggerOutput = getAnalogTriggerOutput;
        }

        //The HAL by default stores raw values instead of voltage behind the scenes. We are using voltage
        [CalledSimFunction]
        public static IntPtr initializeAnalogOutputPort(IntPtr port_pointer, ref int status)
        {
            status = 0;
            AnalogPort p = new AnalogPort()
            {
                port = PortConverters.GetHalPort(port_pointer)
            };
            AnalogOut[p.port.pin].Initialized = true;
            AnalogOut[p.port.pin].Voltage = 0.0;

            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        [CalledSimFunction]
        public static void freeAnalogOutputPort(IntPtr analog_port_pointer)
        {
            if (analog_port_pointer == IntPtr.Zero) return;
            AnalogOut[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].Initialized = false;
            Marshal.FreeHGlobal(analog_port_pointer);
        }

        [CalledSimFunction]
        public static void freeAnalogInputPort(IntPtr analog_port_pointer)
        {
            if (analog_port_pointer == IntPtr.Zero) return;
            AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].Initialized = false;
            AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AccumulatorInitialized = false;
            Marshal.FreeHGlobal(analog_port_pointer);
        }

        [CalledSimFunction]
        public static void setAnalogOutput(IntPtr analog_port_pointer, double voltage, ref int status)
        {
            status = 0;
            AnalogOut[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].Voltage = voltage;
        }

        [CalledSimFunction]
        public static double getAnalogOutput(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return AnalogOut[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].Voltage;
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
                port = PortConverters.GetHalPort(port_pointer)
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
            AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AverageBits = bits;
        }

        [CalledSimFunction]
        public static uint getAnalogAverageBits(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AverageBits;
        }

        [CalledSimFunction]
        public static void setAnalogOversampleBits(IntPtr analog_port_pointer, uint bits, ref int status)
        {
            status = 0;
            AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].OversampleBits = bits;
        }

        [CalledSimFunction]
        public static uint getAnalogOversampleBits(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].OversampleBits;
        }

        [CalledSimFunction]
        public static short getAnalogValue(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            //Will need to port this to use voltage and scale it
            var value = getAnalogVoltsToValue(analog_port_pointer,
                AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].Voltage, ref status);
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
                status = HALErrorConstants.VOLTAGE_OUT_OF_RANGE;
            }
            else if (voltage < 0.0)
            {
                voltage = 0.0;
                status = HALErrorConstants.VOLTAGE_OUT_OF_RANGE;
            }

            var LSBWeight = getAnalogLSBWeight(analog_port_pointer, ref status);
            var offset = getAnalogOffset(analog_port_pointer, ref status);
            return (int)((voltage + offset * 1.0e-9) / (LSBWeight * 1.0e-9));
        }


        [CalledSimFunction]
        public static float getAnalogVoltage(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return (float)AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].Voltage;
        }

        [CalledSimFunction]
        public static float getAnalogAverageVoltage(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            //Just use regular voltage. Averaging doesn't work without constant updating
            return (float)AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].Voltage;
        }

        [CalledSimFunction]
        public static uint getAnalogLSBWeight(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return (uint)AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].LSBWeight;
        }

        [CalledSimFunction]
        public static int getAnalogOffset(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].Offset;
        }

        [CalledSimFunction]
        public static bool isAccumulatorChannel(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return AccumulatorChannels.Contains(PortConverters.GetAnalogPort(analog_port_pointer).port.pin);
        }

        [CalledSimFunction]
        public static void initAccumulator(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AccumulatorInitialized = true;
        }

        [CalledSimFunction]
        public static void resetAccumulator(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            int pin = PortConverters.GetAnalogPort(analog_port_pointer).port.pin;
            AnalogIn[pin].AccumulatorCenter = 0;
            AnalogIn[pin].AccumulatorCount = 0;
            AnalogIn[pin].AccumulatorValue = 0;
        }

        [CalledSimFunction]
        public static void setAccumulatorCenter(IntPtr analog_port_pointer, int center, ref int status)
        {
            status = 0;
            AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AccumulatorCenter = center;
        }

        [CalledSimFunction]
        public static void setAccumulatorDeadband(IntPtr analog_port_pointer, int deadband, ref int status)
        {
            status = 0;
            AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AccumulatorDeadband = deadband;
        }

        [CalledSimFunction]
        public static long getAccumulatorValue(IntPtr analog_port_pointer, ref int status)
        {
            status = 0;
            return AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AccumulatorValue;
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
            return AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AccumulatorCount;
        }

        [CalledSimFunction]
        public static void getAccumulatorOutput(IntPtr analog_port_pointer, ref long value, ref uint count,
            ref int status)
        {
            status = 0;
            count = AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AccumulatorCount;
            value = AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AccumulatorValue;
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
                    cnt.AnalogPin = PortConverters.GetHalPort(port_pointer).pin;
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
            status = HALErrorConstants.NO_AVAILABLE_RESOURCES;
            return IntPtr.Zero;
        }

        [CalledSimFunction]
        public static void cleanAnalogTrigger(IntPtr analog_trigger_pointer, ref int status)
        {
            if (analog_trigger_pointer == IntPtr.Zero) return;
            status = 0;
            var trig = PortConverters.GetAnalogTrigger(analog_trigger_pointer);
            SimData.AnalogTrigger[trig.index].Initialized = false;
            freeAnalogInputPort(trig.analogPortPointer);
            Marshal.FreeHGlobal(analog_trigger_pointer);
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
                status = HALErrorConstants.ANALOG_TRIGGER_LIMIT_ORDER_ERROR;
            }
            else
            {
                var port = PortConverters.GetAnalogTrigger(analog_trigger_pointer).analogPortPointer;
                status = 0;
                SimData.AnalogTrigger[PortConverters.GetAnalogTrigger(analog_trigger_pointer).index].TrigLower
                    = getaAnalogValueToVoltage(port, lower, ref status);
                SimData.AnalogTrigger[PortConverters.GetAnalogTrigger(analog_trigger_pointer).index].TrigUpper
                    = getaAnalogValueToVoltage(port, upper, ref status);
            }
        }

        [CalledSimFunction]
        public static void setAnalogTriggerLimitsVoltage(IntPtr analog_trigger_pointer, double lower,
            double upper, ref int status)
        {
            if (lower > upper)
            {
                status = HALErrorConstants.ANALOG_TRIGGER_LIMIT_ORDER_ERROR;
            }
            else
            {
                status = 0;
                SimData.AnalogTrigger[PortConverters.GetAnalogTrigger(analog_trigger_pointer).index].TrigLower = lower;
                SimData.AnalogTrigger[PortConverters.GetAnalogTrigger(analog_trigger_pointer).index].TrigUpper = upper;
            }
        }

        [CalledSimFunction]
        public static void setAnalogTriggerAveraged(IntPtr analog_trigger_pointer, bool useAveragedValue, ref int status)
        {
            var trigPort = PortConverters.GetAnalogTrigger(analog_trigger_pointer);
            if (SimData.AnalogTrigger[trigPort.index].TrigType == TrigerType.Filtered)
            {
                status = HALErrorConstants.INCOMPATIBLE_STATE;
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
            var trigPort = PortConverters.GetAnalogTrigger(analog_trigger_pointer);
            if (SimData.AnalogTrigger[trigPort.index].TrigType == TrigerType.Averaged)
            {
                status = HALErrorConstants.INCOMPATIBLE_STATE;
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
            var ain = AnalogIn[PortConverters.GetAnalogPort(trigger.analogPortPointer).port.pin];
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
            var trig = PortConverters.GetAnalogTrigger(analog_trigger_pointer);
            var val = getTriggerValue(trig);
            var atr = SimData.AnalogTrigger[trig.index];
            return val >= atr.TrigLower && val <= atr.TrigUpper;
        }

        [CalledSimFunction]
        public static bool getAnalogTriggerTriggerState(IntPtr analog_trigger_pointer, ref int status)
        {
            status = 0;
            var trig = PortConverters.GetAnalogTrigger(analog_trigger_pointer);
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
                status = HALErrorConstants.ANALOG_TRIGGER_PULSE_OUTPUT_ERROR;
                return false;
            }
        }
    }
}
