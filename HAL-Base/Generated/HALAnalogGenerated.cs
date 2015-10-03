//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace

namespace HAL_Base
{
    public partial class HALAnalog
    {
        static HALAnalog()
        {
            HAL.Initialize();
        }

        public delegate IntPtr InitializeAnalogOutputPortDelegate(IntPtr port_pointer, ref int status);
        public static InitializeAnalogOutputPortDelegate InitializeAnalogOutputPort;

        public delegate void SetAnalogOutputDelegate(IntPtr analog_port_pointer, double voltage, ref int status);
        public static SetAnalogOutputDelegate SetAnalogOutput;

        public delegate double GetAnalogOutputDelegate(IntPtr analog_port_pointer, ref int status);
        public static GetAnalogOutputDelegate GetAnalogOutput;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool CheckAnalogOutputChannelDelegate(uint pin);
        public static CheckAnalogOutputChannelDelegate CheckAnalogOutputChannel;

        public delegate IntPtr InitializeAnalogInputPortDelegate(IntPtr port_pointer, ref int status);
        public static InitializeAnalogInputPortDelegate InitializeAnalogInputPort;

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

        public delegate void SetAnalogAverageBitsDelegate(IntPtr analog_port_pointer, uint bits, ref int status);
        public static SetAnalogAverageBitsDelegate SetAnalogAverageBits;

        public delegate uint GetAnalogAverageBitsDelegate(IntPtr analog_port_pointer, ref int status);
        public static GetAnalogAverageBitsDelegate GetAnalogAverageBits;

        public delegate void SetAnalogOversampleBitsDelegate(IntPtr analog_port_pointer, uint bits, ref int status);
        public static SetAnalogOversampleBitsDelegate SetAnalogOversampleBits;

        public delegate uint GetAnalogOversampleBitsDelegate(IntPtr analog_port_pointer, ref int status);
        public static GetAnalogOversampleBitsDelegate GetAnalogOversampleBits;

        public delegate short GetAnalogValueDelegate(IntPtr analog_port_pointer, ref int status);
        public static GetAnalogValueDelegate GetAnalogValue;

        public delegate int GetAnalogAverageValueDelegate(IntPtr analog_port_pointer, ref int status);
        public static GetAnalogAverageValueDelegate GetAnalogAverageValue;

        public delegate int GetAnalogVoltsToValueDelegate(IntPtr analog_port_pointer, double voltage, ref int status);
        public static GetAnalogVoltsToValueDelegate GetAnalogVoltsToValue;

        public delegate float GetAnalogVoltageDelegate(IntPtr analog_port_pointer, ref int status);
        public static GetAnalogVoltageDelegate GetAnalogVoltage;

        public delegate float GetAnalogAverageVoltageDelegate(IntPtr analog_port_pointer, ref int status);
        public static GetAnalogAverageVoltageDelegate GetAnalogAverageVoltage;

        public delegate uint GetAnalogLSBWeightDelegate(IntPtr analog_port_pointer, ref int status);
        public static GetAnalogLSBWeightDelegate GetAnalogLSBWeight;

        public delegate int GetAnalogOffsetDelegate(IntPtr analog_port_pointer, ref int status);
        public static GetAnalogOffsetDelegate GetAnalogOffset;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool IsAccumulatorChannelDelegate(IntPtr analog_port_pointer, ref int status);
        public static IsAccumulatorChannelDelegate IsAccumulatorChannel;

        public delegate void InitAccumulatorDelegate(IntPtr analog_port_pointer, ref int status);
        public static InitAccumulatorDelegate InitAccumulator;

        public delegate void ResetAccumulatorDelegate(IntPtr analog_port_pointer, ref int status);
        public static ResetAccumulatorDelegate ResetAccumulator;

        public delegate void SetAccumulatorCenterDelegate(IntPtr analog_port_pointer, int center, ref int status);
        public static SetAccumulatorCenterDelegate SetAccumulatorCenter;

        public delegate void SetAccumulatorDeadbandDelegate(IntPtr analog_port_pointer, int deadband, ref int status);
        public static SetAccumulatorDeadbandDelegate SetAccumulatorDeadband;

        public delegate long GetAccumulatorValueDelegate(IntPtr analog_port_pointer, ref int status);
        public static GetAccumulatorValueDelegate GetAccumulatorValue;

        public delegate uint GetAccumulatorCountDelegate(IntPtr analog_port_pointer, ref int status);
        public static GetAccumulatorCountDelegate GetAccumulatorCount;

        public delegate void GetAccumulatorOutputDelegate(IntPtr analog_port_pointer, ref long value, ref uint count, ref int status);
        public static GetAccumulatorOutputDelegate GetAccumulatorOutput;

        public delegate IntPtr InitializeAnalogTriggerDelegate(IntPtr port_pointer, ref uint index, ref int status);
        public static InitializeAnalogTriggerDelegate InitializeAnalogTrigger;

        public delegate void CleanAnalogTriggerDelegate(IntPtr analog_trigger_pointer, ref int status);
        public static CleanAnalogTriggerDelegate CleanAnalogTrigger;

        public delegate void SetAnalogTriggerLimitsRawDelegate(IntPtr analog_trigger_pointer, int lower, int upper, ref int status);
        public static SetAnalogTriggerLimitsRawDelegate SetAnalogTriggerLimitsRaw;

        public delegate void SetAnalogTriggerLimitsVoltageDelegate(IntPtr analog_trigger_pointer, double lower, double upper, ref int status);
        public static SetAnalogTriggerLimitsVoltageDelegate SetAnalogTriggerLimitsVoltage;

        public delegate void SetAnalogTriggerAveragedDelegate(IntPtr analog_trigger_pointer, [MarshalAs(UnmanagedType.I1)]bool useAveragedValue, ref int status);
        public static SetAnalogTriggerAveragedDelegate SetAnalogTriggerAveraged;

        public delegate void SetAnalogTriggerFilteredDelegate(IntPtr analog_trigger_pointer, [MarshalAs(UnmanagedType.I1)]bool useFilteredValue, ref int status);
        public static SetAnalogTriggerFilteredDelegate SetAnalogTriggerFiltered;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetAnalogTriggerInWindowDelegate(IntPtr analog_trigger_pointer, ref int status);
        public static GetAnalogTriggerInWindowDelegate GetAnalogTriggerInWindow;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetAnalogTriggerTriggerStateDelegate(IntPtr analog_trigger_pointer, ref int status);
        public static GetAnalogTriggerTriggerStateDelegate GetAnalogTriggerTriggerState;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetAnalogTriggerOutputDelegate(IntPtr analog_trigger_pointer, AnalogTriggerType type, ref int status);
        public static GetAnalogTriggerOutputDelegate GetAnalogTriggerOutput;
    }
}
