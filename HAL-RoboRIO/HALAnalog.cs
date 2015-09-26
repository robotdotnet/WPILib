//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALAnalog
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALAnalog.InitializeAnalogOutputPort = (HAL_Base.HALAnalog.InitializeAnalogOutputPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeAnalogOutputPort"), typeof(HAL_Base.HALAnalog.InitializeAnalogOutputPortDelegate));

            HAL_Base.HALAnalog.SetAnalogOutput = (HAL_Base.HALAnalog.SetAnalogOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogOutput"), typeof(HAL_Base.HALAnalog.SetAnalogOutputDelegate));

            HAL_Base.HALAnalog.GetAnalogOutput = (HAL_Base.HALAnalog.GetAnalogOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogOutput"), typeof(HAL_Base.HALAnalog.GetAnalogOutputDelegate));

            HAL_Base.HALAnalog.CheckAnalogOutputChannel = (HAL_Base.HALAnalog.CheckAnalogOutputChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkAnalogOutputChannel"), typeof(HAL_Base.HALAnalog.CheckAnalogOutputChannelDelegate));

            HAL_Base.HALAnalog.InitializeAnalogInputPort = (HAL_Base.HALAnalog.InitializeAnalogInputPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeAnalogInputPort"), typeof(HAL_Base.HALAnalog.InitializeAnalogInputPortDelegate));

            HAL_Base.HALAnalog.CheckAnalogModule = (HAL_Base.HALAnalog.CheckAnalogModuleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkAnalogModule"), typeof(HAL_Base.HALAnalog.CheckAnalogModuleDelegate));

            HAL_Base.HALAnalog.CheckAnalogInputChannel = (HAL_Base.HALAnalog.CheckAnalogInputChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkAnalogInputChannel"), typeof(HAL_Base.HALAnalog.CheckAnalogInputChannelDelegate));

            HAL_Base.HALAnalog.SetAnalogSampleRate = (HAL_Base.HALAnalog.SetAnalogSampleRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogSampleRate"), typeof(HAL_Base.HALAnalog.SetAnalogSampleRateDelegate));

            HAL_Base.HALAnalog.GetAnalogSampleRate = (HAL_Base.HALAnalog.GetAnalogSampleRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogSampleRate"), typeof(HAL_Base.HALAnalog.GetAnalogSampleRateDelegate));

            HAL_Base.HALAnalog.SetAnalogAverageBits = (HAL_Base.HALAnalog.SetAnalogAverageBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogAverageBits"), typeof(HAL_Base.HALAnalog.SetAnalogAverageBitsDelegate));

            HAL_Base.HALAnalog.GetAnalogAverageBits = (HAL_Base.HALAnalog.GetAnalogAverageBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogAverageBits"), typeof(HAL_Base.HALAnalog.GetAnalogAverageBitsDelegate));

            HAL_Base.HALAnalog.SetAnalogOversampleBits = (HAL_Base.HALAnalog.SetAnalogOversampleBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogOversampleBits"), typeof(HAL_Base.HALAnalog.SetAnalogOversampleBitsDelegate));

            HAL_Base.HALAnalog.GetAnalogOversampleBits = (HAL_Base.HALAnalog.GetAnalogOversampleBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogOversampleBits"), typeof(HAL_Base.HALAnalog.GetAnalogOversampleBitsDelegate));

            HAL_Base.HALAnalog.GetAnalogValue = (HAL_Base.HALAnalog.GetAnalogValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogValue"), typeof(HAL_Base.HALAnalog.GetAnalogValueDelegate));

            HAL_Base.HALAnalog.GetAnalogAverageValue = (HAL_Base.HALAnalog.GetAnalogAverageValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogAverageValue"), typeof(HAL_Base.HALAnalog.GetAnalogAverageValueDelegate));

            HAL_Base.HALAnalog.GetAnalogVoltsToValue = (HAL_Base.HALAnalog.GetAnalogVoltsToValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogVoltsToValue"), typeof(HAL_Base.HALAnalog.GetAnalogVoltsToValueDelegate));

            HAL_Base.HALAnalog.GetAnalogVoltage = (HAL_Base.HALAnalog.GetAnalogVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogVoltage"), typeof(HAL_Base.HALAnalog.GetAnalogVoltageDelegate));

            HAL_Base.HALAnalog.GetAnalogAverageVoltage = (HAL_Base.HALAnalog.GetAnalogAverageVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogAverageVoltage"), typeof(HAL_Base.HALAnalog.GetAnalogAverageVoltageDelegate));

            HAL_Base.HALAnalog.GetAnalogLSBWeight = (HAL_Base.HALAnalog.GetAnalogLSBWeightDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogLSBWeight"), typeof(HAL_Base.HALAnalog.GetAnalogLSBWeightDelegate));

            HAL_Base.HALAnalog.GetAnalogOffset = (HAL_Base.HALAnalog.GetAnalogOffsetDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogOffset"), typeof(HAL_Base.HALAnalog.GetAnalogOffsetDelegate));

            HAL_Base.HALAnalog.IsAccumulatorChannel = (HAL_Base.HALAnalog.IsAccumulatorChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "isAccumulatorChannel"), typeof(HAL_Base.HALAnalog.IsAccumulatorChannelDelegate));

            HAL_Base.HALAnalog.InitAccumulator = (HAL_Base.HALAnalog.InitAccumulatorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initAccumulator"), typeof(HAL_Base.HALAnalog.InitAccumulatorDelegate));

            HAL_Base.HALAnalog.ResetAccumulator = (HAL_Base.HALAnalog.ResetAccumulatorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "resetAccumulator"), typeof(HAL_Base.HALAnalog.ResetAccumulatorDelegate));

            HAL_Base.HALAnalog.SetAccumulatorCenter = (HAL_Base.HALAnalog.SetAccumulatorCenterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAccumulatorCenter"), typeof(HAL_Base.HALAnalog.SetAccumulatorCenterDelegate));

            HAL_Base.HALAnalog.SetAccumulatorDeadband = (HAL_Base.HALAnalog.SetAccumulatorDeadbandDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAccumulatorDeadband"), typeof(HAL_Base.HALAnalog.SetAccumulatorDeadbandDelegate));

            HAL_Base.HALAnalog.GetAccumulatorValue = (HAL_Base.HALAnalog.GetAccumulatorValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccumulatorValue"), typeof(HAL_Base.HALAnalog.GetAccumulatorValueDelegate));

            HAL_Base.HALAnalog.GetAccumulatorCount = (HAL_Base.HALAnalog.GetAccumulatorCountDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccumulatorCount"), typeof(HAL_Base.HALAnalog.GetAccumulatorCountDelegate));

            HAL_Base.HALAnalog.GetAccumulatorOutput = (HAL_Base.HALAnalog.GetAccumulatorOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccumulatorOutput"), typeof(HAL_Base.HALAnalog.GetAccumulatorOutputDelegate));

            HAL_Base.HALAnalog.InitializeAnalogTrigger = (HAL_Base.HALAnalog.InitializeAnalogTriggerDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeAnalogTrigger"), typeof(HAL_Base.HALAnalog.InitializeAnalogTriggerDelegate));

            HAL_Base.HALAnalog.CleanAnalogTrigger = (HAL_Base.HALAnalog.CleanAnalogTriggerDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "cleanAnalogTrigger"), typeof(HAL_Base.HALAnalog.CleanAnalogTriggerDelegate));

            HAL_Base.HALAnalog.SetAnalogTriggerLimitsRaw = (HAL_Base.HALAnalog.SetAnalogTriggerLimitsRawDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogTriggerLimitsRaw"), typeof(HAL_Base.HALAnalog.SetAnalogTriggerLimitsRawDelegate));

            HAL_Base.HALAnalog.SetAnalogTriggerLimitsVoltage = (HAL_Base.HALAnalog.SetAnalogTriggerLimitsVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogTriggerLimitsVoltage"), typeof(HAL_Base.HALAnalog.SetAnalogTriggerLimitsVoltageDelegate));

            HAL_Base.HALAnalog.SetAnalogTriggerAveraged = (HAL_Base.HALAnalog.SetAnalogTriggerAveragedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogTriggerAveraged"), typeof(HAL_Base.HALAnalog.SetAnalogTriggerAveragedDelegate));

            HAL_Base.HALAnalog.SetAnalogTriggerFiltered = (HAL_Base.HALAnalog.SetAnalogTriggerFilteredDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogTriggerFiltered"), typeof(HAL_Base.HALAnalog.SetAnalogTriggerFilteredDelegate));

            HAL_Base.HALAnalog.GetAnalogTriggerInWindow = (HAL_Base.HALAnalog.GetAnalogTriggerInWindowDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogTriggerInWindow"), typeof(HAL_Base.HALAnalog.GetAnalogTriggerInWindowDelegate));

            HAL_Base.HALAnalog.GetAnalogTriggerTriggerState = (HAL_Base.HALAnalog.GetAnalogTriggerTriggerStateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogTriggerTriggerState"), typeof(HAL_Base.HALAnalog.GetAnalogTriggerTriggerStateDelegate));

            HAL_Base.HALAnalog.GetAnalogTriggerOutput = (HAL_Base.HALAnalog.GetAnalogTriggerOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogTriggerOutput"), typeof(HAL_Base.HALAnalog.GetAnalogTriggerOutputDelegate));

        }

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeAnalogOutputPort")]
        public static extern IntPtr initializeAnalogOutputPort(IntPtr port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogOutput")]
        public static extern void setAnalogOutput(IntPtr analog_port_pointer, double voltage, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogOutput")]
        public static extern double getAnalogOutput(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "checkAnalogOutputChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkAnalogOutputChannel(uint pin);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeAnalogInputPort")]
        public static extern IntPtr initializeAnalogInputPort(IntPtr port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "checkAnalogModule")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkAnalogModule(byte module);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "checkAnalogInputChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkAnalogInputChannel(uint pin);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogSampleRate")]
        public static extern void setAnalogSampleRate(double samplesPerSecond, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogSampleRate")]
        public static extern float getAnalogSampleRate(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogAverageBits")]
        public static extern void setAnalogAverageBits(IntPtr analog_port_pointer, uint bits, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogAverageBits")]
        public static extern uint getAnalogAverageBits(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogOversampleBits")]
        public static extern void setAnalogOversampleBits(IntPtr analog_port_pointer, uint bits, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogOversampleBits")]
        public static extern uint getAnalogOversampleBits(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogValue")]
        public static extern short getAnalogValue(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogAverageValue")]
        public static extern int getAnalogAverageValue(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogVoltsToValue")]
        public static extern int getAnalogVoltsToValue(IntPtr analog_port_pointer, double voltage, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogVoltage")]
        public static extern float getAnalogVoltage(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogAverageVoltage")]
        public static extern float getAnalogAverageVoltage(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogLSBWeight")]
        public static extern uint getAnalogLSBWeight(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogOffset")]
        public static extern int getAnalogOffset(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "isAccumulatorChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool isAccumulatorChannel(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initAccumulator")]
        public static extern void initAccumulator(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "resetAccumulator")]
        public static extern void resetAccumulator(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAccumulatorCenter")]
        public static extern void setAccumulatorCenter(IntPtr analog_port_pointer, int center, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAccumulatorDeadband")]
        public static extern void setAccumulatorDeadband(IntPtr analog_port_pointer, int deadband, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAccumulatorValue")]
        public static extern long getAccumulatorValue(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAccumulatorCount")]
        public static extern uint getAccumulatorCount(IntPtr analog_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAccumulatorOutput")]
        public static extern void getAccumulatorOutput(IntPtr analog_port_pointer, ref long value, ref uint count, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeAnalogTrigger")]
        public static extern IntPtr initializeAnalogTrigger(IntPtr port_pointer, ref uint index, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "cleanAnalogTrigger")]
        public static extern void cleanAnalogTrigger(IntPtr analog_trigger_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogTriggerLimitsRaw")]
        public static extern void setAnalogTriggerLimitsRaw(IntPtr analog_trigger_pointer, int lower, int upper, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogTriggerLimitsVoltage")]
        public static extern void setAnalogTriggerLimitsVoltage(IntPtr analog_trigger_pointer, double lower, double upper, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogTriggerAveraged")]
        public static extern void setAnalogTriggerAveraged(IntPtr analog_trigger_pointer, [MarshalAs(UnmanagedType.I1)] bool useAveragedValue, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setAnalogTriggerFiltered")]
        public static extern void setAnalogTriggerFiltered(IntPtr analog_trigger_pointer, [MarshalAs(UnmanagedType.I1)] bool useFilteredValue, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogTriggerInWindow")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getAnalogTriggerInWindow(IntPtr analog_trigger_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogTriggerTriggerState")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getAnalogTriggerTriggerState(IntPtr analog_trigger_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getAnalogTriggerOutput")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getAnalogTriggerOutput(IntPtr analog_trigger_pointer, AnalogTriggerType type, ref int status);
    }
}
