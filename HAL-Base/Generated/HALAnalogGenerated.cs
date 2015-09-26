//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Linq;
using System.Reflection;
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

        internal static void SetupDelegates()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HAL.HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;
            Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);
            InitializeAnalogOutputPort = (InitializeAnalogOutputPortDelegate)Delegate.CreateDelegate(typeof(InitializeAnalogOutputPortDelegate), type.GetMethod("initializeAnalogOutputPort"));
            SetAnalogOutput = (SetAnalogOutputDelegate)Delegate.CreateDelegate(typeof(SetAnalogOutputDelegate), type.GetMethod("setAnalogOutput"));
            GetAnalogOutput = (GetAnalogOutputDelegate)Delegate.CreateDelegate(typeof(GetAnalogOutputDelegate), type.GetMethod("getAnalogOutput"));
            CheckAnalogOutputChannel = (CheckAnalogOutputChannelDelegate)Delegate.CreateDelegate(typeof(CheckAnalogOutputChannelDelegate), type.GetMethod("checkAnalogOutputChannel"));
            InitializeAnalogInputPort = (InitializeAnalogInputPortDelegate)Delegate.CreateDelegate(typeof(InitializeAnalogInputPortDelegate), type.GetMethod("initializeAnalogInputPort"));
            CheckAnalogModule = (CheckAnalogModuleDelegate)Delegate.CreateDelegate(typeof(CheckAnalogModuleDelegate), type.GetMethod("checkAnalogModule"));
            CheckAnalogInputChannel = (CheckAnalogInputChannelDelegate)Delegate.CreateDelegate(typeof(CheckAnalogInputChannelDelegate), type.GetMethod("checkAnalogInputChannel"));
            SetAnalogSampleRate = (SetAnalogSampleRateDelegate)Delegate.CreateDelegate(typeof(SetAnalogSampleRateDelegate), type.GetMethod("setAnalogSampleRate"));
            GetAnalogSampleRate = (GetAnalogSampleRateDelegate)Delegate.CreateDelegate(typeof(GetAnalogSampleRateDelegate), type.GetMethod("getAnalogSampleRate"));
            SetAnalogAverageBits = (SetAnalogAverageBitsDelegate)Delegate.CreateDelegate(typeof(SetAnalogAverageBitsDelegate), type.GetMethod("setAnalogAverageBits"));
            GetAnalogAverageBits = (GetAnalogAverageBitsDelegate)Delegate.CreateDelegate(typeof(GetAnalogAverageBitsDelegate), type.GetMethod("getAnalogAverageBits"));
            SetAnalogOversampleBits = (SetAnalogOversampleBitsDelegate)Delegate.CreateDelegate(typeof(SetAnalogOversampleBitsDelegate), type.GetMethod("setAnalogOversampleBits"));
            GetAnalogOversampleBits = (GetAnalogOversampleBitsDelegate)Delegate.CreateDelegate(typeof(GetAnalogOversampleBitsDelegate), type.GetMethod("getAnalogOversampleBits"));
            GetAnalogValue = (GetAnalogValueDelegate)Delegate.CreateDelegate(typeof(GetAnalogValueDelegate), type.GetMethod("getAnalogValue"));
            GetAnalogAverageValue = (GetAnalogAverageValueDelegate)Delegate.CreateDelegate(typeof(GetAnalogAverageValueDelegate), type.GetMethod("getAnalogAverageValue"));
            GetAnalogVoltsToValue = (GetAnalogVoltsToValueDelegate)Delegate.CreateDelegate(typeof(GetAnalogVoltsToValueDelegate), type.GetMethod("getAnalogVoltsToValue"));
            GetAnalogVoltage = (GetAnalogVoltageDelegate)Delegate.CreateDelegate(typeof(GetAnalogVoltageDelegate), type.GetMethod("getAnalogVoltage"));
            GetAnalogAverageVoltage = (GetAnalogAverageVoltageDelegate)Delegate.CreateDelegate(typeof(GetAnalogAverageVoltageDelegate), type.GetMethod("getAnalogAverageVoltage"));
            GetAnalogLSBWeight = (GetAnalogLSBWeightDelegate)Delegate.CreateDelegate(typeof(GetAnalogLSBWeightDelegate), type.GetMethod("getAnalogLSBWeight"));
            GetAnalogOffset = (GetAnalogOffsetDelegate)Delegate.CreateDelegate(typeof(GetAnalogOffsetDelegate), type.GetMethod("getAnalogOffset"));
            IsAccumulatorChannel = (IsAccumulatorChannelDelegate)Delegate.CreateDelegate(typeof(IsAccumulatorChannelDelegate), type.GetMethod("isAccumulatorChannel"));
            InitAccumulator = (InitAccumulatorDelegate)Delegate.CreateDelegate(typeof(InitAccumulatorDelegate), type.GetMethod("initAccumulator"));
            ResetAccumulator = (ResetAccumulatorDelegate)Delegate.CreateDelegate(typeof(ResetAccumulatorDelegate), type.GetMethod("resetAccumulator"));
            SetAccumulatorCenter = (SetAccumulatorCenterDelegate)Delegate.CreateDelegate(typeof(SetAccumulatorCenterDelegate), type.GetMethod("setAccumulatorCenter"));
            SetAccumulatorDeadband = (SetAccumulatorDeadbandDelegate)Delegate.CreateDelegate(typeof(SetAccumulatorDeadbandDelegate), type.GetMethod("setAccumulatorDeadband"));
            GetAccumulatorValue = (GetAccumulatorValueDelegate)Delegate.CreateDelegate(typeof(GetAccumulatorValueDelegate), type.GetMethod("getAccumulatorValue"));
            GetAccumulatorCount = (GetAccumulatorCountDelegate)Delegate.CreateDelegate(typeof(GetAccumulatorCountDelegate), type.GetMethod("getAccumulatorCount"));
            GetAccumulatorOutput = (GetAccumulatorOutputDelegate)Delegate.CreateDelegate(typeof(GetAccumulatorOutputDelegate), type.GetMethod("getAccumulatorOutput"));
            InitializeAnalogTrigger = (InitializeAnalogTriggerDelegate)Delegate.CreateDelegate(typeof(InitializeAnalogTriggerDelegate), type.GetMethod("initializeAnalogTrigger"));
            CleanAnalogTrigger = (CleanAnalogTriggerDelegate)Delegate.CreateDelegate(typeof(CleanAnalogTriggerDelegate), type.GetMethod("cleanAnalogTrigger"));
            SetAnalogTriggerLimitsRaw = (SetAnalogTriggerLimitsRawDelegate)Delegate.CreateDelegate(typeof(SetAnalogTriggerLimitsRawDelegate), type.GetMethod("setAnalogTriggerLimitsRaw"));
            SetAnalogTriggerLimitsVoltage = (SetAnalogTriggerLimitsVoltageDelegate)Delegate.CreateDelegate(typeof(SetAnalogTriggerLimitsVoltageDelegate), type.GetMethod("setAnalogTriggerLimitsVoltage"));
            SetAnalogTriggerAveraged = (SetAnalogTriggerAveragedDelegate)Delegate.CreateDelegate(typeof(SetAnalogTriggerAveragedDelegate), type.GetMethod("setAnalogTriggerAveraged"));
            SetAnalogTriggerFiltered = (SetAnalogTriggerFilteredDelegate)Delegate.CreateDelegate(typeof(SetAnalogTriggerFilteredDelegate), type.GetMethod("setAnalogTriggerFiltered"));
            GetAnalogTriggerInWindow = (GetAnalogTriggerInWindowDelegate)Delegate.CreateDelegate(typeof(GetAnalogTriggerInWindowDelegate), type.GetMethod("getAnalogTriggerInWindow"));
            GetAnalogTriggerTriggerState = (GetAnalogTriggerTriggerStateDelegate)Delegate.CreateDelegate(typeof(GetAnalogTriggerTriggerStateDelegate), type.GetMethod("getAnalogTriggerTriggerState"));
            GetAnalogTriggerOutput = (GetAnalogTriggerOutputDelegate)Delegate.CreateDelegate(typeof(GetAnalogTriggerOutputDelegate), type.GetMethod("getAnalogTriggerOutput"));
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
