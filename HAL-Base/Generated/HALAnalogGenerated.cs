//File automatically generated using robotdotnet-tools. Please do not modify.
using System;
using System.Linq;
using System.Reflection;

namespace HAL_Base
{
    public partial class HALAnalog
    {
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
            GetAnalogSampleRateIntHack = (GetAnalogSampleRateIntHackDelegate)Delegate.CreateDelegate(typeof(GetAnalogSampleRateIntHackDelegate), type.GetMethod("getAnalogSampleRateIntHack"));
            GetAnalogVoltageIntHack = (GetAnalogVoltageIntHackDelegate)Delegate.CreateDelegate(typeof(GetAnalogVoltageIntHackDelegate), type.GetMethod("getAnalogVoltageIntHack"));
            GetAnalogAverageVoltageIntHack = (GetAnalogAverageVoltageIntHackDelegate)Delegate.CreateDelegate(typeof(GetAnalogAverageVoltageIntHackDelegate), type.GetMethod("getAnalogAverageVoltageIntHack"));
            SetAnalogSampleRateIntHack = (SetAnalogSampleRateIntHackDelegate)Delegate.CreateDelegate(typeof(SetAnalogSampleRateIntHackDelegate), type.GetMethod("setAnalogSampleRateIntHack"));
            GetAnalogVoltsToValueIntHack = (GetAnalogVoltsToValueIntHackDelegate)Delegate.CreateDelegate(typeof(GetAnalogVoltsToValueIntHackDelegate), type.GetMethod("getAnalogVoltsToValueIntHack"));
            SetAnalogTriggerLimitsVoltageIntHack = (SetAnalogTriggerLimitsVoltageIntHackDelegate)Delegate.CreateDelegate(typeof(SetAnalogTriggerLimitsVoltageIntHackDelegate), type.GetMethod("setAnalogTriggerLimitsVoltageIntHack"));
        }

        public delegate System.IntPtr InitializeAnalogOutputPortDelegate(System.IntPtr port_pointer, ref int status);
        public static InitializeAnalogOutputPortDelegate InitializeAnalogOutputPort;

        public delegate void SetAnalogOutputDelegate(System.IntPtr analog_port_pointer, double voltage, ref int status);
        public static SetAnalogOutputDelegate SetAnalogOutput;

        public delegate double GetAnalogOutputDelegate(System.IntPtr analog_port_pointer, ref int status);
        public static GetAnalogOutputDelegate GetAnalogOutput;

        public delegate bool CheckAnalogOutputChannelDelegate(uint pin);
        public static CheckAnalogOutputChannelDelegate CheckAnalogOutputChannel;

        public delegate System.IntPtr InitializeAnalogInputPortDelegate(System.IntPtr port_pointer, ref int status);
        public static InitializeAnalogInputPortDelegate InitializeAnalogInputPort;

        public delegate bool CheckAnalogModuleDelegate(byte module);
        public static CheckAnalogModuleDelegate CheckAnalogModule;

        public delegate bool CheckAnalogInputChannelDelegate(uint pin);
        public static CheckAnalogInputChannelDelegate CheckAnalogInputChannel;

        public delegate void SetAnalogSampleRateDelegate(double samplesPerSecond, ref int status);
        public static SetAnalogSampleRateDelegate SetAnalogSampleRate;

        public delegate float GetAnalogSampleRateDelegate(ref int status);
        public static GetAnalogSampleRateDelegate GetAnalogSampleRate;

        public delegate void SetAnalogAverageBitsDelegate(System.IntPtr analog_port_pointer, uint bits, ref int status);
        public static SetAnalogAverageBitsDelegate SetAnalogAverageBits;

        public delegate uint GetAnalogAverageBitsDelegate(System.IntPtr analog_port_pointer, ref int status);
        public static GetAnalogAverageBitsDelegate GetAnalogAverageBits;

        public delegate void SetAnalogOversampleBitsDelegate(System.IntPtr analog_port_pointer, uint bits, ref int status);
        public static SetAnalogOversampleBitsDelegate SetAnalogOversampleBits;

        public delegate uint GetAnalogOversampleBitsDelegate(System.IntPtr analog_port_pointer, ref int status);
        public static GetAnalogOversampleBitsDelegate GetAnalogOversampleBits;

        public delegate short GetAnalogValueDelegate(System.IntPtr analog_port_pointer, ref int status);
        public static GetAnalogValueDelegate GetAnalogValue;

        public delegate int GetAnalogAverageValueDelegate(System.IntPtr analog_port_pointer, ref int status);
        public static GetAnalogAverageValueDelegate GetAnalogAverageValue;

        public delegate int GetAnalogVoltsToValueDelegate(System.IntPtr analog_port_pointer, double voltage, ref int status);
        public static GetAnalogVoltsToValueDelegate GetAnalogVoltsToValue;

        public delegate float GetAnalogVoltageDelegate(System.IntPtr analog_port_pointer, ref int status);
        public static GetAnalogVoltageDelegate GetAnalogVoltage;

        public delegate float GetAnalogAverageVoltageDelegate(System.IntPtr analog_port_pointer, ref int status);
        public static GetAnalogAverageVoltageDelegate GetAnalogAverageVoltage;

        public delegate uint GetAnalogLSBWeightDelegate(System.IntPtr analog_port_pointer, ref int status);
        public static GetAnalogLSBWeightDelegate GetAnalogLSBWeight;

        public delegate int GetAnalogOffsetDelegate(System.IntPtr analog_port_pointer, ref int status);
        public static GetAnalogOffsetDelegate GetAnalogOffset;

        public delegate bool IsAccumulatorChannelDelegate(System.IntPtr analog_port_pointer, ref int status);
        public static IsAccumulatorChannelDelegate IsAccumulatorChannel;

        public delegate void InitAccumulatorDelegate(System.IntPtr analog_port_pointer, ref int status);
        public static InitAccumulatorDelegate InitAccumulator;

        public delegate void ResetAccumulatorDelegate(System.IntPtr analog_port_pointer, ref int status);
        public static ResetAccumulatorDelegate ResetAccumulator;

        public delegate void SetAccumulatorCenterDelegate(System.IntPtr analog_port_pointer, int center, ref int status);
        public static SetAccumulatorCenterDelegate SetAccumulatorCenter;

        public delegate void SetAccumulatorDeadbandDelegate(System.IntPtr analog_port_pointer, int deadband, ref int status);
        public static SetAccumulatorDeadbandDelegate SetAccumulatorDeadband;

        public delegate long GetAccumulatorValueDelegate(System.IntPtr analog_port_pointer, ref int status);
        public static GetAccumulatorValueDelegate GetAccumulatorValue;

        public delegate uint GetAccumulatorCountDelegate(System.IntPtr analog_port_pointer, ref int status);
        public static GetAccumulatorCountDelegate GetAccumulatorCount;

        public delegate void GetAccumulatorOutputDelegate(System.IntPtr analog_port_pointer, ref long value, ref uint count, ref int status);
        public static GetAccumulatorOutputDelegate GetAccumulatorOutput;

        public delegate System.IntPtr InitializeAnalogTriggerDelegate(System.IntPtr port_pointer, ref uint index, ref int status);
        public static InitializeAnalogTriggerDelegate InitializeAnalogTrigger;

        public delegate void CleanAnalogTriggerDelegate(System.IntPtr analog_trigger_pointer, ref int status);
        public static CleanAnalogTriggerDelegate CleanAnalogTrigger;

        public delegate void SetAnalogTriggerLimitsRawDelegate(System.IntPtr analog_trigger_pointer, int lower, int upper, ref int status);
        public static SetAnalogTriggerLimitsRawDelegate SetAnalogTriggerLimitsRaw;

        public delegate void SetAnalogTriggerLimitsVoltageDelegate(System.IntPtr analog_trigger_pointer, double lower, double upper, ref int status);
        public static SetAnalogTriggerLimitsVoltageDelegate SetAnalogTriggerLimitsVoltage;

        public delegate void SetAnalogTriggerAveragedDelegate(System.IntPtr analog_trigger_pointer, bool useAveragedValue, ref int status);
        public static SetAnalogTriggerAveragedDelegate SetAnalogTriggerAveraged;

        public delegate void SetAnalogTriggerFilteredDelegate(System.IntPtr analog_trigger_pointer, bool useFilteredValue, ref int status);
        public static SetAnalogTriggerFilteredDelegate SetAnalogTriggerFiltered;

        public delegate bool GetAnalogTriggerInWindowDelegate(System.IntPtr analog_trigger_pointer, ref int status);
        public static GetAnalogTriggerInWindowDelegate GetAnalogTriggerInWindow;

        public delegate bool GetAnalogTriggerTriggerStateDelegate(System.IntPtr analog_trigger_pointer, ref int status);
        public static GetAnalogTriggerTriggerStateDelegate GetAnalogTriggerTriggerState;

        public delegate bool GetAnalogTriggerOutputDelegate(System.IntPtr analog_trigger_pointer, AnalogTriggerType type, ref int status);
        public static GetAnalogTriggerOutputDelegate GetAnalogTriggerOutput;

        public delegate int GetAnalogSampleRateIntHackDelegate(ref int status);
        public static GetAnalogSampleRateIntHackDelegate GetAnalogSampleRateIntHack;

        public delegate int GetAnalogVoltageIntHackDelegate(System.IntPtr analog_port_pointer, ref int status);
        public static GetAnalogVoltageIntHackDelegate GetAnalogVoltageIntHack;

        public delegate int GetAnalogAverageVoltageIntHackDelegate(System.IntPtr analog_port_pointer, ref int status);
        public static GetAnalogAverageVoltageIntHackDelegate GetAnalogAverageVoltageIntHack;

        public delegate void SetAnalogSampleRateIntHackDelegate(int samplesPerSecond, ref int status);
        public static SetAnalogSampleRateIntHackDelegate SetAnalogSampleRateIntHack;

        public delegate int GetAnalogVoltsToValueIntHackDelegate(System.IntPtr analog_port_pointer, int voltage, ref int status);
        public static GetAnalogVoltsToValueIntHackDelegate GetAnalogVoltsToValueIntHack;

        public delegate void SetAnalogTriggerLimitsVoltageIntHackDelegate(System.IntPtr analog_trigger_pointer, int lower, int upper, ref int status);
        public static SetAnalogTriggerLimitsVoltageIntHackDelegate SetAnalogTriggerLimitsVoltageIntHack;
    }
}
