using System;
using System.Linq;
using System.Runtime.InteropServices;
using HAL.Base;
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
            Base.HALAnalog.InitializeAnalogOutputPort = initializeAnalogOutputPort;
            Base.HALAnalog.FreeAnalogOutputPort = freeAnalogOutputPort;
            Base.HALAnalog.FreeAnalogInputPort = freeAnalogInputPort;
            Base.HALAnalog.SetAnalogOutput = setAnalogOutput;
            Base.HALAnalog.GetAnalogOutput = getAnalogOutput;
            Base.HALAnalog.CheckAnalogOutputChannel = checkAnalogOutputChannel;
            Base.HALAnalog.InitializeAnalogInputPort = initializeAnalogInputPort;
            Base.HALAnalog.CheckAnalogModule = checkAnalogModule;
            Base.HALAnalog.CheckAnalogInputChannel = checkAnalogInputChannel;
            Base.HALAnalog.SetAnalogSampleRate = setAnalogSampleRate;
            Base.HALAnalog.GetAnalogSampleRate = getAnalogSampleRate;
            Base.HALAnalog.SetAnalogAverageBits = setAnalogAverageBits;
            Base.HALAnalog.GetAnalogAverageBits = getAnalogAverageBits;
            Base.HALAnalog.SetAnalogOversampleBits = setAnalogOversampleBits;
            Base.HALAnalog.GetAnalogOversampleBits = getAnalogOversampleBits;
            Base.HALAnalog.GetAnalogValue = getAnalogValue;
            Base.HALAnalog.GetAnalogAverageValue = getAnalogAverageValue;
            Base.HALAnalog.GetAnalogVoltsToValue = getAnalogVoltsToValue;
            Base.HALAnalog.GetAnalogVoltage = getAnalogVoltage;
            Base.HALAnalog.GetAnalogAverageVoltage = getAnalogAverageVoltage;
            Base.HALAnalog.GetAnalogLSBWeight = getAnalogLSBWeight;
            Base.HALAnalog.GetAnalogOffset = getAnalogOffset;
            Base.HALAnalog.IsAccumulatorChannel = isAccumulatorChannel;
            Base.HALAnalog.InitAccumulator = initAccumulator;
            Base.HALAnalog.ResetAccumulator = resetAccumulator;
            Base.HALAnalog.SetAccumulatorCenter = setAccumulatorCenter;
            Base.HALAnalog.SetAccumulatorDeadband = setAccumulatorDeadband;
            Base.HALAnalog.GetAccumulatorValue = getAccumulatorValue;
            Base.HALAnalog.GetAccumulatorCount = getAccumulatorCount;
            Base.HALAnalog.GetAccumulatorOutput = getAccumulatorOutput;
            Base.HALAnalog.InitializeAnalogTrigger = initializeAnalogTrigger;
            Base.HALAnalog.CleanAnalogTrigger = cleanAnalogTrigger;
            Base.HALAnalog.SetAnalogTriggerLimitsRaw = setAnalogTriggerLimitsRaw;
            Base.HALAnalog.SetAnalogTriggerLimitsVoltage = setAnalogTriggerLimitsVoltage;
            Base.HALAnalog.SetAnalogTriggerAveraged = setAnalogTriggerAveraged;
            Base.HALAnalog.SetAnalogTriggerFiltered = setAnalogTriggerFiltered;
            Base.HALAnalog.GetAnalogTriggerInWindow = getAnalogTriggerInWindow;
            Base.HALAnalog.GetAnalogTriggerTriggerState = getAnalogTriggerTriggerState;
            Base.HALAnalog.GetAnalogTriggerOutput = getAnalogTriggerOutput;
        }

        //The HAL by default stores raw values instead of voltage behind the scenes. We are using voltage
        [CalledSimFunction]
        public static AnalogOutputPortSafeHandle initializeAnalogOutputPort(HALPortSafeHandle port_pointer, ref int status)
        {
            status = 0;
            AnalogPort p = new AnalogPort()
            {
                port = PortConverters.GetHalPort(port_pointer)
            };
            AnalogOut[p.port.pin].Initialized = true;
            AnalogOut[p.port.pin].Voltage = 0.0;

            return new AnalogOutputPortSafeHandle(p);
        }

        [CalledSimFunction]
        public static void freeAnalogOutputPort(AnalogOutputPortSafeHandle analog_port_pointer)
        {
            if (analog_port_pointer == null) return;
            AnalogOut[PortConverters.GetAnalogOutputPort(analog_port_pointer).port.pin].Initialized = false;
        }

        [CalledSimFunction]
        public static void freeAnalogInputPort(AnalogInputPortSafeHandle analog_port_pointer)
        {
            if (analog_port_pointer == null) return;
            AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].Initialized = false;
            AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AccumulatorInitialized = false;
            //Marshal.FreeHGlobal(analog_port_pointer);
        }

        [CalledSimFunction]
        public static void setAnalogOutput(AnalogOutputPortSafeHandle analog_port_pointer, double voltage, ref int status)
        {
            status = 0;
            AnalogOut[PortConverters.GetAnalogOutputPort(analog_port_pointer).port.pin].Voltage = voltage;
        }

        [CalledSimFunction]
        public static double getAnalogOutput(AnalogOutputPortSafeHandle analog_port_pointer, ref int status)
        {
            status = 0;
            return AnalogOut[PortConverters.GetAnalogOutputPort(analog_port_pointer).port.pin].Voltage;
        }

        [CalledSimFunction]
        public static bool checkAnalogOutputChannel(uint pin)
        {
            return pin < AnalogOutputPins;
        }

        [CalledSimFunction]
        public static AnalogInputPortSafeHandle initializeAnalogInputPort(HALPortSafeHandle port_pointer, ref int status)
        {
            status = 0;
            AnalogPort p = new AnalogPort()
            {
                port = PortConverters.GetHalPort(port_pointer)
            };
            AnalogIn[p.port.pin].Initialized = true;
            return new AnalogInputPortSafeHandle(p);
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
        public static void setAnalogAverageBits(AnalogInputPortSafeHandle analog_port_pointer, uint bits, ref int status)
        {
            status = 0;
            AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AverageBits = bits;
        }

        [CalledSimFunction]
        public static uint getAnalogAverageBits(AnalogInputPortSafeHandle analog_port_pointer, ref int status)
        {
            status = 0;
            return AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AverageBits;
        }

        [CalledSimFunction]
        public static void setAnalogOversampleBits(AnalogInputPortSafeHandle analog_port_pointer, uint bits, ref int status)
        {
            status = 0;
            AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].OversampleBits = bits;
        }

        [CalledSimFunction]
        public static uint getAnalogOversampleBits(AnalogInputPortSafeHandle analog_port_pointer, ref int status)
        {
            status = 0;
            return AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].OversampleBits;
        }

        [CalledSimFunction]
        public static short getAnalogValue(AnalogInputPortSafeHandle analog_port_pointer, ref int status)
        {
            status = 0;
            //Will need to port this to use voltage and scale it
            var value = getAnalogVoltsToValue(analog_port_pointer,
                AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].Voltage, ref status);
            return (short)value;
        }

        [CalledSimFunction]
        public static int getAnalogAverageValue(AnalogInputPortSafeHandle analog_port_pointer, ref int status)
        {
            status = 0;
            //Just use regular voltage. Averaging doesn't work without constant updating
            return getAnalogValue(analog_port_pointer, ref status);
        }

        [CalledSimFunction]
        public static int getAnalogVoltsToValue(AnalogInputPortSafeHandle analog_port_pointer, double voltage, ref int status)
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
        public static float getAnalogVoltage(AnalogInputPortSafeHandle analog_port_pointer, ref int status)
        {
            status = 0;
            return (float)AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].Voltage;
        }

        [CalledSimFunction]
        public static float getAnalogAverageVoltage(AnalogInputPortSafeHandle analog_port_pointer, ref int status)
        {
            status = 0;
            //Just use regular voltage. Averaging doesn't work without constant updating
            return (float)AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].Voltage;
        }

        [CalledSimFunction]
        public static uint getAnalogLSBWeight(AnalogInputPortSafeHandle analog_port_pointer, ref int status)
        {
            status = 0;
            return (uint)AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].LSBWeight;
        }

        [CalledSimFunction]
        public static int getAnalogOffset(AnalogInputPortSafeHandle analog_port_pointer, ref int status)
        {
            status = 0;
            return AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].Offset;
        }

        [CalledSimFunction]
        public static bool isAccumulatorChannel(AnalogInputPortSafeHandle analog_port_pointer, ref int status)
        {
            status = 0;
            return AccumulatorChannels.Contains(PortConverters.GetAnalogPort(analog_port_pointer).port.pin);
        }

        [CalledSimFunction]
        public static void initAccumulator(AnalogInputPortSafeHandle analog_port_pointer, ref int status)
        {
            status = 0;
            AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AccumulatorInitialized = true;
        }

        [CalledSimFunction]
        public static void resetAccumulator(AnalogInputPortSafeHandle analog_port_pointer, ref int status)
        {
            status = 0;
            int pin = PortConverters.GetAnalogPort(analog_port_pointer).port.pin;
            AnalogIn[pin].AccumulatorCenter = 0;
            AnalogIn[pin].AccumulatorCount = 0;
            AnalogIn[pin].AccumulatorValue = 0;
        }

        [CalledSimFunction]
        public static void setAccumulatorCenter(AnalogInputPortSafeHandle analog_port_pointer, int center, ref int status)
        {
            status = 0;
            AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AccumulatorCenter = center;
        }

        [CalledSimFunction]
        public static void setAccumulatorDeadband(AnalogInputPortSafeHandle analog_port_pointer, int deadband, ref int status)
        {
            status = 0;
            AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AccumulatorDeadband = deadband;
        }

        [CalledSimFunction]
        public static long getAccumulatorValue(AnalogInputPortSafeHandle analog_port_pointer, ref int status)
        {
            status = 0;
            return AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AccumulatorValue;
        }

        [CalledSimFunction]
        public static uint getAccumulatorCount(AnalogInputPortSafeHandle analog_port_pointer, ref int status)
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
        public static void getAccumulatorOutput(AnalogInputPortSafeHandle analog_port_pointer, ref long value, ref uint count,
            ref int status)
        {
            status = 0;
            count = AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AccumulatorCount;
            value = AnalogIn[PortConverters.GetAnalogPort(analog_port_pointer).port.pin].AccumulatorValue;
        }

        [CalledSimFunction]
        public static AnalogTriggerPortSafeHandle initializeAnalogTrigger(HALPortSafeHandle port_pointer, ref uint index, ref int status)
        {
            status = 0;

            for (int i = 0; i < SimData.AnalogTrigger.Count; i++)
            {
                var cnt = SimData.AnalogTrigger[i];
                if (cnt.Initialized == false)
                {
                    var port = PortConverters.GetHalPort(port_pointer);
                    bool preInit = SimData.AnalogIn[port.pin].Initialized;

                    AnalogInputPortSafeHandle aPt = initializeAnalogInputPort(port_pointer, ref status);
                    cnt.Initialized = true;
                    cnt.AnalogPin = port.pin;
                    AnalogTrigger trig = new AnalogTrigger()
                    {
                        analogPortPointer = aPt,
                        precreatedAnalogInput = preInit,
                    };
                    index = (uint)i;
                    trig.index = i;
                    AnalogTriggerPortSafeHandle ptr = new AnalogTriggerPortSafeHandle(trig);
                    cnt.TriggerPointer = ptr;

                    return ptr;
                }
            }
            status = HALErrorConstants.NO_AVAILABLE_RESOURCES;
            return null;
        }

        [CalledSimFunction]
        public static void cleanAnalogTrigger(AnalogTriggerPortSafeHandle analog_trigger_pointer, ref int status)
        {
            if (analog_trigger_pointer == null) return;
            status = 0;
            var trig = PortConverters.GetAnalogTrigger(analog_trigger_pointer);
            SimData.AnalogTrigger[trig.index].Initialized = false;
            if (trig.precreatedAnalogInput)
            {
                freeAnalogInputPortTrigger(trig.analogPortPointer);
            }
            else
            {
                freeAnalogInputPort(trig.analogPortPointer);
            }
            //Marshal.FreeHGlobal(analog_trigger_pointer);
        }

        private static void freeAnalogInputPortTrigger(AnalogInputPortSafeHandle analog_port_pointer)
        {
            //if (analog_port_pointer == IntPtr.Zero) return;
            //Marshal.FreeHGlobal(analog_port_pointer);
        }

        internal static double getaAnalogValueToVoltage(AnalogInputPortSafeHandle analog_port_pointer, int value, ref int status)
        {
            uint LSBWeight = getAnalogLSBWeight(analog_port_pointer, ref status);
            int offset = getAnalogOffset(analog_port_pointer, ref status);

            double voltage = LSBWeight * 1.0e-9 * value - offset * 1.0e-9;
            return voltage;
        }

        [CalledSimFunction]
        public static void setAnalogTriggerLimitsRaw(AnalogTriggerPortSafeHandle analog_trigger_pointer, int lower, int upper, ref int status)
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
        public static void setAnalogTriggerLimitsVoltage(AnalogTriggerPortSafeHandle analog_trigger_pointer, double lower,
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
        public static void setAnalogTriggerAveraged(AnalogTriggerPortSafeHandle analog_trigger_pointer, bool useAveragedValue, ref int status)
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
        public static void setAnalogTriggerFiltered(AnalogTriggerPortSafeHandle analog_trigger_pointer,
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
        public static bool getAnalogTriggerInWindow(AnalogTriggerPortSafeHandle analog_trigger_pointer, ref int status)
        {
            status = 0;
            var trig = PortConverters.GetAnalogTrigger(analog_trigger_pointer);
            var val = getTriggerValue(trig);
            var atr = SimData.AnalogTrigger[trig.index];
            return val >= atr.TrigLower && val <= atr.TrigUpper;
        }

        [CalledSimFunction]
        public static bool getAnalogTriggerTriggerState(AnalogTriggerPortSafeHandle analog_trigger_pointer, ref int status)
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
        public static bool getAnalogTriggerOutput(AnalogTriggerPortSafeHandle analog_trigger_pointer, AnalogTriggerType type, ref int status)
        {
            if (type == AnalogTriggerType.InWindow)
                return Base.HALAnalog.GetAnalogTriggerInWindow(analog_trigger_pointer, ref status);
            if (type == AnalogTriggerType.State)
                return Base.HALAnalog.GetAnalogTriggerTriggerState(analog_trigger_pointer, ref status);
            else
            {
                status = HALErrorConstants.ANALOG_TRIGGER_PULSE_OUTPUT_ERROR;
                return false;
            }
        }
    }
}
