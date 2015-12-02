//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace HAL.Athena
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALAnalog
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALAnalog.InitializeAnalogOutputPort = (global::HAL.HALAnalog.InitializeAnalogOutputPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeAnalogOutputPort"), typeof(global::HAL.HALAnalog.InitializeAnalogOutputPortDelegate));

            global::HAL.HALAnalog.FreeAnalogOutputPort = (global::HAL.HALAnalog.FreeAnalogOutputPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeAnalogOutputPort"), typeof
                (global::HAL.HALAnalog.FreeAnalogOutputPortDelegate));

            global::HAL.HALAnalog.FreeAnalogInputPort = (global::HAL.HALAnalog.FreeAnalogInputPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeAnalogInputPort"), typeof
                (global::HAL.HALAnalog.FreeAnalogInputPortDelegate));

            global::HAL.HALAnalog.SetAnalogOutput = (global::HAL.HALAnalog.SetAnalogOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogOutput"), typeof(global::HAL.HALAnalog.SetAnalogOutputDelegate));

            global::HAL.HALAnalog.GetAnalogOutput = (global::HAL.HALAnalog.GetAnalogOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogOutput"), typeof(global::HAL.HALAnalog.GetAnalogOutputDelegate));

            global::HAL.HALAnalog.CheckAnalogOutputChannel = (global::HAL.HALAnalog.CheckAnalogOutputChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkAnalogOutputChannel"), typeof(global::HAL.HALAnalog.CheckAnalogOutputChannelDelegate));

            global::HAL.HALAnalog.InitializeAnalogInputPort = (global::HAL.HALAnalog.InitializeAnalogInputPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeAnalogInputPort"), typeof(global::HAL.HALAnalog.InitializeAnalogInputPortDelegate));

            global::HAL.HALAnalog.CheckAnalogModule = (global::HAL.HALAnalog.CheckAnalogModuleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkAnalogModule"), typeof(global::HAL.HALAnalog.CheckAnalogModuleDelegate));

            global::HAL.HALAnalog.CheckAnalogInputChannel = (global::HAL.HALAnalog.CheckAnalogInputChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkAnalogInputChannel"), typeof(global::HAL.HALAnalog.CheckAnalogInputChannelDelegate));

            global::HAL.HALAnalog.SetAnalogSampleRate = (global::HAL.HALAnalog.SetAnalogSampleRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogSampleRate"), typeof(global::HAL.HALAnalog.SetAnalogSampleRateDelegate));

            global::HAL.HALAnalog.GetAnalogSampleRate = (global::HAL.HALAnalog.GetAnalogSampleRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogSampleRate"), typeof(global::HAL.HALAnalog.GetAnalogSampleRateDelegate));

            global::HAL.HALAnalog.SetAnalogAverageBits = (global::HAL.HALAnalog.SetAnalogAverageBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogAverageBits"), typeof(global::HAL.HALAnalog.SetAnalogAverageBitsDelegate));

            global::HAL.HALAnalog.GetAnalogAverageBits = (global::HAL.HALAnalog.GetAnalogAverageBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogAverageBits"), typeof(global::HAL.HALAnalog.GetAnalogAverageBitsDelegate));

            global::HAL.HALAnalog.SetAnalogOversampleBits = (global::HAL.HALAnalog.SetAnalogOversampleBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogOversampleBits"), typeof(global::HAL.HALAnalog.SetAnalogOversampleBitsDelegate));

            global::HAL.HALAnalog.GetAnalogOversampleBits = (global::HAL.HALAnalog.GetAnalogOversampleBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogOversampleBits"), typeof(global::HAL.HALAnalog.GetAnalogOversampleBitsDelegate));

            global::HAL.HALAnalog.GetAnalogValue = (global::HAL.HALAnalog.GetAnalogValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogValue"), typeof(global::HAL.HALAnalog.GetAnalogValueDelegate));

            global::HAL.HALAnalog.GetAnalogAverageValue = (global::HAL.HALAnalog.GetAnalogAverageValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogAverageValue"), typeof(global::HAL.HALAnalog.GetAnalogAverageValueDelegate));

            global::HAL.HALAnalog.GetAnalogVoltsToValue = (global::HAL.HALAnalog.GetAnalogVoltsToValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogVoltsToValue"), typeof(global::HAL.HALAnalog.GetAnalogVoltsToValueDelegate));

            global::HAL.HALAnalog.GetAnalogVoltage = (global::HAL.HALAnalog.GetAnalogVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogVoltage"), typeof(global::HAL.HALAnalog.GetAnalogVoltageDelegate));

            global::HAL.HALAnalog.GetAnalogAverageVoltage = (global::HAL.HALAnalog.GetAnalogAverageVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogAverageVoltage"), typeof(global::HAL.HALAnalog.GetAnalogAverageVoltageDelegate));

            global::HAL.HALAnalog.GetAnalogLSBWeight = (global::HAL.HALAnalog.GetAnalogLSBWeightDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogLSBWeight"), typeof(global::HAL.HALAnalog.GetAnalogLSBWeightDelegate));

            global::HAL.HALAnalog.GetAnalogOffset = (global::HAL.HALAnalog.GetAnalogOffsetDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogOffset"), typeof(global::HAL.HALAnalog.GetAnalogOffsetDelegate));

            global::HAL.HALAnalog.IsAccumulatorChannel = (global::HAL.HALAnalog.IsAccumulatorChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "isAccumulatorChannel"), typeof(global::HAL.HALAnalog.IsAccumulatorChannelDelegate));

            global::HAL.HALAnalog.InitAccumulator = (global::HAL.HALAnalog.InitAccumulatorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initAccumulator"), typeof(global::HAL.HALAnalog.InitAccumulatorDelegate));

            global::HAL.HALAnalog.ResetAccumulator = (global::HAL.HALAnalog.ResetAccumulatorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "resetAccumulator"), typeof(global::HAL.HALAnalog.ResetAccumulatorDelegate));

            global::HAL.HALAnalog.SetAccumulatorCenter = (global::HAL.HALAnalog.SetAccumulatorCenterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAccumulatorCenter"), typeof(global::HAL.HALAnalog.SetAccumulatorCenterDelegate));

            global::HAL.HALAnalog.SetAccumulatorDeadband = (global::HAL.HALAnalog.SetAccumulatorDeadbandDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAccumulatorDeadband"), typeof(global::HAL.HALAnalog.SetAccumulatorDeadbandDelegate));

            global::HAL.HALAnalog.GetAccumulatorValue = (global::HAL.HALAnalog.GetAccumulatorValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccumulatorValue"), typeof(global::HAL.HALAnalog.GetAccumulatorValueDelegate));

            global::HAL.HALAnalog.GetAccumulatorCount = (global::HAL.HALAnalog.GetAccumulatorCountDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccumulatorCount"), typeof(global::HAL.HALAnalog.GetAccumulatorCountDelegate));

            global::HAL.HALAnalog.GetAccumulatorOutput = (global::HAL.HALAnalog.GetAccumulatorOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccumulatorOutput"), typeof(global::HAL.HALAnalog.GetAccumulatorOutputDelegate));

            global::HAL.HALAnalog.InitializeAnalogTrigger = (global::HAL.HALAnalog.InitializeAnalogTriggerDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeAnalogTrigger"), typeof(global::HAL.HALAnalog.InitializeAnalogTriggerDelegate));

            global::HAL.HALAnalog.CleanAnalogTrigger = (global::HAL.HALAnalog.CleanAnalogTriggerDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "cleanAnalogTrigger"), typeof(global::HAL.HALAnalog.CleanAnalogTriggerDelegate));

            global::HAL.HALAnalog.SetAnalogTriggerLimitsRaw = (global::HAL.HALAnalog.SetAnalogTriggerLimitsRawDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogTriggerLimitsRaw"), typeof(global::HAL.HALAnalog.SetAnalogTriggerLimitsRawDelegate));

            global::HAL.HALAnalog.SetAnalogTriggerLimitsVoltage = (global::HAL.HALAnalog.SetAnalogTriggerLimitsVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogTriggerLimitsVoltage"), typeof(global::HAL.HALAnalog.SetAnalogTriggerLimitsVoltageDelegate));

            global::HAL.HALAnalog.SetAnalogTriggerAveraged = (global::HAL.HALAnalog.SetAnalogTriggerAveragedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogTriggerAveraged"), typeof(global::HAL.HALAnalog.SetAnalogTriggerAveragedDelegate));

            global::HAL.HALAnalog.SetAnalogTriggerFiltered = (global::HAL.HALAnalog.SetAnalogTriggerFilteredDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogTriggerFiltered"), typeof(global::HAL.HALAnalog.SetAnalogTriggerFilteredDelegate));

            global::HAL.HALAnalog.GetAnalogTriggerInWindow = (global::HAL.HALAnalog.GetAnalogTriggerInWindowDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogTriggerInWindow"), typeof(global::HAL.HALAnalog.GetAnalogTriggerInWindowDelegate));

            global::HAL.HALAnalog.GetAnalogTriggerTriggerState = (global::HAL.HALAnalog.GetAnalogTriggerTriggerStateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogTriggerTriggerState"), typeof(global::HAL.HALAnalog.GetAnalogTriggerTriggerStateDelegate));

            global::HAL.HALAnalog.GetAnalogTriggerOutput = (global::HAL.HALAnalog.GetAnalogTriggerOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogTriggerOutput"), typeof(global::HAL.HALAnalog.GetAnalogTriggerOutputDelegate));

        }
    }
}
