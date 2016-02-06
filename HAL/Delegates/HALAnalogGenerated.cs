//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace

namespace HAL.Base
{
    public partial class HALAnalog
    {
        static HALAnalog()
        {
            HAL.Initialize();
        }

        public delegate AnalogOutputPortSafeHandle InitializeAnalogOutputPortDelegate(HALPortSafeHandle port_pointer, ref int status);
        public static InitializeAnalogOutputPortDelegate InitializeAnalogOutputPort;

        public delegate void FreeAnalogOutputPortDelegate(AnalogOutputPortSafeHandle analog_port_pointer);
        public static FreeAnalogOutputPortDelegate FreeAnalogOutputPort;


        public delegate void SetAnalogOutputDelegate(AnalogOutputPortSafeHandle analog_port_pointer, double voltage, ref int status);
        public static SetAnalogOutputDelegate SetAnalogOutput;

        public delegate double GetAnalogOutputDelegate(AnalogOutputPortSafeHandle analog_port_pointer, ref int status);
        public static GetAnalogOutputDelegate GetAnalogOutput;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool CheckAnalogOutputChannelDelegate(uint pin);
        public static CheckAnalogOutputChannelDelegate CheckAnalogOutputChannel;

        public delegate AnalogInputPortSafeHandle InitializeAnalogInputPortDelegate(HALPortSafeHandle port_pointer, ref int status);
        public static InitializeAnalogInputPortDelegate InitializeAnalogInputPort;

        public delegate void FreeAnalogInputPortDelegate(AnalogInputPortSafeHandle analog_port_pointer);
        public static FreeAnalogInputPortDelegate FreeAnalogInputPort;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool CheckAnalogModuleDelegate(byte module);
        public static CheckAnalogModuleDelegate CheckAnalogModule;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool CheckAnalogInputChannelDelegate(uint pin);
        public static CheckAnalogInputChannelDelegate CheckAnalogInputChannel;

        public delegate void SetAnalogSampleRateDelegate(double samplesPerSecond, ref int status);
        public static SetAnalogSampleRateDelegate SetAnalogSampleRate;

        public delegate float GetAnalogSampleRateDelegate(ref int status);
        public static GetAnalogSampleRateDelegate GetAnalogSampleRate;

        public delegate void SetAnalogAverageBitsDelegate(AnalogInputPortSafeHandle analog_port_pointer, uint bits, ref int status);
        public static SetAnalogAverageBitsDelegate SetAnalogAverageBits;

        public delegate uint GetAnalogAverageBitsDelegate(AnalogInputPortSafeHandle analog_port_pointer, ref int status);
        public static GetAnalogAverageBitsDelegate GetAnalogAverageBits;

        public delegate void SetAnalogOversampleBitsDelegate(AnalogInputPortSafeHandle analog_port_pointer, uint bits, ref int status);
        public static SetAnalogOversampleBitsDelegate SetAnalogOversampleBits;

        public delegate uint GetAnalogOversampleBitsDelegate(AnalogInputPortSafeHandle analog_port_pointer, ref int status);
        public static GetAnalogOversampleBitsDelegate GetAnalogOversampleBits;

        public delegate short GetAnalogValueDelegate(AnalogInputPortSafeHandle analog_port_pointer, ref int status);
        public static GetAnalogValueDelegate GetAnalogValue;

        public delegate int GetAnalogAverageValueDelegate(AnalogInputPortSafeHandle analog_port_pointer, ref int status);
        public static GetAnalogAverageValueDelegate GetAnalogAverageValue;

        public delegate int GetAnalogVoltsToValueDelegate(AnalogInputPortSafeHandle analog_port_pointer, double voltage, ref int status);
        public static GetAnalogVoltsToValueDelegate GetAnalogVoltsToValue;

        public delegate float GetAnalogVoltageDelegate(AnalogInputPortSafeHandle analog_port_pointer, ref int status);
        public static GetAnalogVoltageDelegate GetAnalogVoltage;

        public delegate float GetAnalogAverageVoltageDelegate(AnalogInputPortSafeHandle analog_port_pointer, ref int status);
        public static GetAnalogAverageVoltageDelegate GetAnalogAverageVoltage;

        public delegate uint GetAnalogLSBWeightDelegate(AnalogInputPortSafeHandle analog_port_pointer, ref int status);
        public static GetAnalogLSBWeightDelegate GetAnalogLSBWeight;

        public delegate int GetAnalogOffsetDelegate(AnalogInputPortSafeHandle analog_port_pointer, ref int status);
        public static GetAnalogOffsetDelegate GetAnalogOffset;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool IsAccumulatorChannelDelegate(AnalogInputPortSafeHandle analog_port_pointer, ref int status);
        public static IsAccumulatorChannelDelegate IsAccumulatorChannel;

        public delegate void InitAccumulatorDelegate(AnalogInputPortSafeHandle analog_port_pointer, ref int status);
        public static InitAccumulatorDelegate InitAccumulator;

        public delegate void ResetAccumulatorDelegate(AnalogInputPortSafeHandle analog_port_pointer, ref int status);
        public static ResetAccumulatorDelegate ResetAccumulator;

        public delegate void SetAccumulatorCenterDelegate(AnalogInputPortSafeHandle analog_port_pointer, int center, ref int status);
        public static SetAccumulatorCenterDelegate SetAccumulatorCenter;

        public delegate void SetAccumulatorDeadbandDelegate(AnalogInputPortSafeHandle analog_port_pointer, int deadband, ref int status);
        public static SetAccumulatorDeadbandDelegate SetAccumulatorDeadband;

        public delegate long GetAccumulatorValueDelegate(AnalogInputPortSafeHandle analog_port_pointer, ref int status);
        public static GetAccumulatorValueDelegate GetAccumulatorValue;

        public delegate uint GetAccumulatorCountDelegate(AnalogInputPortSafeHandle analog_port_pointer, ref int status);
        public static GetAccumulatorCountDelegate GetAccumulatorCount;

        public delegate void GetAccumulatorOutputDelegate(AnalogInputPortSafeHandle analog_port_pointer, ref long value, ref uint count, ref int status);
        public static GetAccumulatorOutputDelegate GetAccumulatorOutput;

        public delegate AnalogTriggerPortSafeHandle InitializeAnalogTriggerDelegate(HALPortSafeHandle port_pointer, ref uint index, ref int status);
        public static InitializeAnalogTriggerDelegate InitializeAnalogTrigger;

        public delegate void CleanAnalogTriggerDelegate(AnalogTriggerPortSafeHandle analog_trigger_pointer, ref int status);
        public static CleanAnalogTriggerDelegate CleanAnalogTrigger;

        public delegate void SetAnalogTriggerLimitsRawDelegate(AnalogTriggerPortSafeHandle analog_trigger_pointer, int lower, int upper, ref int status);
        public static SetAnalogTriggerLimitsRawDelegate SetAnalogTriggerLimitsRaw;

        public delegate void SetAnalogTriggerLimitsVoltageDelegate(AnalogTriggerPortSafeHandle analog_trigger_pointer, double lower, double upper, ref int status);
        public static SetAnalogTriggerLimitsVoltageDelegate SetAnalogTriggerLimitsVoltage;

        public delegate void SetAnalogTriggerAveragedDelegate(AnalogTriggerPortSafeHandle analog_trigger_pointer, [MarshalAs(UnmanagedType.I1)]bool useAveragedValue, ref int status);
        public static SetAnalogTriggerAveragedDelegate SetAnalogTriggerAveraged;

        public delegate void SetAnalogTriggerFilteredDelegate(AnalogTriggerPortSafeHandle analog_trigger_pointer, [MarshalAs(UnmanagedType.I1)]bool useFilteredValue, ref int status);
        public static SetAnalogTriggerFilteredDelegate SetAnalogTriggerFiltered;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetAnalogTriggerInWindowDelegate(AnalogTriggerPortSafeHandle analog_trigger_pointer, ref int status);
        public static GetAnalogTriggerInWindowDelegate GetAnalogTriggerInWindow;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetAnalogTriggerTriggerStateDelegate(AnalogTriggerPortSafeHandle analog_trigger_pointer, ref int status);
        public static GetAnalogTriggerTriggerStateDelegate GetAnalogTriggerTriggerState;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetAnalogTriggerOutputDelegate(AnalogTriggerPortSafeHandle analog_trigger_pointer, AnalogTriggerType type, ref int status);
        public static GetAnalogTriggerOutputDelegate GetAnalogTriggerOutput;
    }
}
