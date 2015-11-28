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

            HAL_Base.HALAnalog.FreeAnalogOutputPort = (HAL_Base.HALAnalog.FreeAnalogOutputPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeAnalogOutputPort"), typeof
                (HAL_Base.HALAnalog.FreeAnalogOutputPortDelegate));

            HAL_Base.HALAnalog.FreeAnalogInputPort = (HAL_Base.HALAnalog.FreeAnalogInputPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeAnalogInputPort"), typeof
                (HAL_Base.HALAnalog.FreeAnalogInputPortDelegate));

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
    }
}
