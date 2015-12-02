//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;

namespace HAL.Athena
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALAnalog
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALAnalog.InitializeAnalogOutputPort = (Base.HALAnalog.InitializeAnalogOutputPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeAnalogOutputPort"), typeof(Base.HALAnalog.InitializeAnalogOutputPortDelegate));

            Base.HALAnalog.FreeAnalogOutputPort = (Base.HALAnalog.FreeAnalogOutputPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeAnalogOutputPort"), typeof
                (Base.HALAnalog.FreeAnalogOutputPortDelegate));

            Base.HALAnalog.FreeAnalogInputPort = (Base.HALAnalog.FreeAnalogInputPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeAnalogInputPort"), typeof
                (Base.HALAnalog.FreeAnalogInputPortDelegate));

            Base.HALAnalog.SetAnalogOutput = (Base.HALAnalog.SetAnalogOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogOutput"), typeof(Base.HALAnalog.SetAnalogOutputDelegate));

            Base.HALAnalog.GetAnalogOutput = (Base.HALAnalog.GetAnalogOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogOutput"), typeof(Base.HALAnalog.GetAnalogOutputDelegate));

            Base.HALAnalog.CheckAnalogOutputChannel = (Base.HALAnalog.CheckAnalogOutputChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkAnalogOutputChannel"), typeof(Base.HALAnalog.CheckAnalogOutputChannelDelegate));

            Base.HALAnalog.InitializeAnalogInputPort = (Base.HALAnalog.InitializeAnalogInputPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeAnalogInputPort"), typeof(Base.HALAnalog.InitializeAnalogInputPortDelegate));

            Base.HALAnalog.CheckAnalogModule = (Base.HALAnalog.CheckAnalogModuleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkAnalogModule"), typeof(Base.HALAnalog.CheckAnalogModuleDelegate));

            Base.HALAnalog.CheckAnalogInputChannel = (Base.HALAnalog.CheckAnalogInputChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkAnalogInputChannel"), typeof(Base.HALAnalog.CheckAnalogInputChannelDelegate));

            Base.HALAnalog.SetAnalogSampleRate = (Base.HALAnalog.SetAnalogSampleRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogSampleRate"), typeof(Base.HALAnalog.SetAnalogSampleRateDelegate));

            Base.HALAnalog.GetAnalogSampleRate = (Base.HALAnalog.GetAnalogSampleRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogSampleRate"), typeof(Base.HALAnalog.GetAnalogSampleRateDelegate));

            Base.HALAnalog.SetAnalogAverageBits = (Base.HALAnalog.SetAnalogAverageBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogAverageBits"), typeof(Base.HALAnalog.SetAnalogAverageBitsDelegate));

            Base.HALAnalog.GetAnalogAverageBits = (Base.HALAnalog.GetAnalogAverageBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogAverageBits"), typeof(Base.HALAnalog.GetAnalogAverageBitsDelegate));

            Base.HALAnalog.SetAnalogOversampleBits = (Base.HALAnalog.SetAnalogOversampleBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogOversampleBits"), typeof(Base.HALAnalog.SetAnalogOversampleBitsDelegate));

            Base.HALAnalog.GetAnalogOversampleBits = (Base.HALAnalog.GetAnalogOversampleBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogOversampleBits"), typeof(Base.HALAnalog.GetAnalogOversampleBitsDelegate));

            Base.HALAnalog.GetAnalogValue = (Base.HALAnalog.GetAnalogValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogValue"), typeof(Base.HALAnalog.GetAnalogValueDelegate));

            Base.HALAnalog.GetAnalogAverageValue = (Base.HALAnalog.GetAnalogAverageValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogAverageValue"), typeof(Base.HALAnalog.GetAnalogAverageValueDelegate));

            Base.HALAnalog.GetAnalogVoltsToValue = (Base.HALAnalog.GetAnalogVoltsToValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogVoltsToValue"), typeof(Base.HALAnalog.GetAnalogVoltsToValueDelegate));

            Base.HALAnalog.GetAnalogVoltage = (Base.HALAnalog.GetAnalogVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogVoltage"), typeof(Base.HALAnalog.GetAnalogVoltageDelegate));

            Base.HALAnalog.GetAnalogAverageVoltage = (Base.HALAnalog.GetAnalogAverageVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogAverageVoltage"), typeof(Base.HALAnalog.GetAnalogAverageVoltageDelegate));

            Base.HALAnalog.GetAnalogLSBWeight = (Base.HALAnalog.GetAnalogLSBWeightDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogLSBWeight"), typeof(Base.HALAnalog.GetAnalogLSBWeightDelegate));

            Base.HALAnalog.GetAnalogOffset = (Base.HALAnalog.GetAnalogOffsetDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogOffset"), typeof(Base.HALAnalog.GetAnalogOffsetDelegate));

            Base.HALAnalog.IsAccumulatorChannel = (Base.HALAnalog.IsAccumulatorChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "isAccumulatorChannel"), typeof(Base.HALAnalog.IsAccumulatorChannelDelegate));

            Base.HALAnalog.InitAccumulator = (Base.HALAnalog.InitAccumulatorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initAccumulator"), typeof(Base.HALAnalog.InitAccumulatorDelegate));

            Base.HALAnalog.ResetAccumulator = (Base.HALAnalog.ResetAccumulatorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "resetAccumulator"), typeof(Base.HALAnalog.ResetAccumulatorDelegate));

            Base.HALAnalog.SetAccumulatorCenter = (Base.HALAnalog.SetAccumulatorCenterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAccumulatorCenter"), typeof(Base.HALAnalog.SetAccumulatorCenterDelegate));

            Base.HALAnalog.SetAccumulatorDeadband = (Base.HALAnalog.SetAccumulatorDeadbandDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAccumulatorDeadband"), typeof(Base.HALAnalog.SetAccumulatorDeadbandDelegate));

            Base.HALAnalog.GetAccumulatorValue = (Base.HALAnalog.GetAccumulatorValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccumulatorValue"), typeof(Base.HALAnalog.GetAccumulatorValueDelegate));

            Base.HALAnalog.GetAccumulatorCount = (Base.HALAnalog.GetAccumulatorCountDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccumulatorCount"), typeof(Base.HALAnalog.GetAccumulatorCountDelegate));

            Base.HALAnalog.GetAccumulatorOutput = (Base.HALAnalog.GetAccumulatorOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAccumulatorOutput"), typeof(Base.HALAnalog.GetAccumulatorOutputDelegate));

            Base.HALAnalog.InitializeAnalogTrigger = (Base.HALAnalog.InitializeAnalogTriggerDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeAnalogTrigger"), typeof(Base.HALAnalog.InitializeAnalogTriggerDelegate));

            Base.HALAnalog.CleanAnalogTrigger = (Base.HALAnalog.CleanAnalogTriggerDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "cleanAnalogTrigger"), typeof(Base.HALAnalog.CleanAnalogTriggerDelegate));

            Base.HALAnalog.SetAnalogTriggerLimitsRaw = (Base.HALAnalog.SetAnalogTriggerLimitsRawDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogTriggerLimitsRaw"), typeof(Base.HALAnalog.SetAnalogTriggerLimitsRawDelegate));

            Base.HALAnalog.SetAnalogTriggerLimitsVoltage = (Base.HALAnalog.SetAnalogTriggerLimitsVoltageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogTriggerLimitsVoltage"), typeof(Base.HALAnalog.SetAnalogTriggerLimitsVoltageDelegate));

            Base.HALAnalog.SetAnalogTriggerAveraged = (Base.HALAnalog.SetAnalogTriggerAveragedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogTriggerAveraged"), typeof(Base.HALAnalog.SetAnalogTriggerAveragedDelegate));

            Base.HALAnalog.SetAnalogTriggerFiltered = (Base.HALAnalog.SetAnalogTriggerFilteredDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setAnalogTriggerFiltered"), typeof(Base.HALAnalog.SetAnalogTriggerFilteredDelegate));

            Base.HALAnalog.GetAnalogTriggerInWindow = (Base.HALAnalog.GetAnalogTriggerInWindowDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogTriggerInWindow"), typeof(Base.HALAnalog.GetAnalogTriggerInWindowDelegate));

            Base.HALAnalog.GetAnalogTriggerTriggerState = (Base.HALAnalog.GetAnalogTriggerTriggerStateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogTriggerTriggerState"), typeof(Base.HALAnalog.GetAnalogTriggerTriggerStateDelegate));

            Base.HALAnalog.GetAnalogTriggerOutput = (Base.HALAnalog.GetAnalogTriggerOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getAnalogTriggerOutput"), typeof(Base.HALAnalog.GetAnalogTriggerOutputDelegate));

        }
    }
}
