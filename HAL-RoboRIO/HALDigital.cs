//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALDigital
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALDigital.InitializeDigitalPort = (global::HAL.HALDigital.InitializeDigitalPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeDigitalPort"), typeof(global::HAL.HALDigital.InitializeDigitalPortDelegate));

            global::HAL.HALDigital.FreeDigitalPort = (global::HAL.HALDigital.FreeDigitalPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeDigitalPort"), typeof
                (global::HAL.HALDigital.FreeDigitalPortDelegate));

            global::HAL.HALDigital.CheckPWMChannel = (global::HAL.HALDigital.CheckPWMChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkPWMChannel"), typeof(global::HAL.HALDigital.CheckPWMChannelDelegate));

            global::HAL.HALDigital.CheckRelayChannel = (global::HAL.HALDigital.CheckRelayChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkRelayChannel"), typeof(global::HAL.HALDigital.CheckRelayChannelDelegate));

            global::HAL.HALDigital.SetPWM = (global::HAL.HALDigital.SetPWMDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setPWM"), typeof(global::HAL.HALDigital.SetPWMDelegate));

            global::HAL.HALDigital.AllocatePWMChannel = (global::HAL.HALDigital.AllocatePWMChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "allocatePWMChannel"), typeof(global::HAL.HALDigital.AllocatePWMChannelDelegate));

            global::HAL.HALDigital.FreePWMChannel = (global::HAL.HALDigital.FreePWMChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freePWMChannel"), typeof(global::HAL.HALDigital.FreePWMChannelDelegate));

            global::HAL.HALDigital.GetPWM = (global::HAL.HALDigital.GetPWMDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPWM"), typeof(global::HAL.HALDigital.GetPWMDelegate));

            global::HAL.HALDigital.LatchPWMZero = (global::HAL.HALDigital.LatchPWMZeroDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "latchPWMZero"), typeof(global::HAL.HALDigital.LatchPWMZeroDelegate));

            global::HAL.HALDigital.SetPWMPeriodScale = (global::HAL.HALDigital.SetPWMPeriodScaleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setPWMPeriodScale"), typeof(global::HAL.HALDigital.SetPWMPeriodScaleDelegate));

            global::HAL.HALDigital.AllocatePWM = (global::HAL.HALDigital.AllocatePWMDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "allocatePWM"), typeof(global::HAL.HALDigital.AllocatePWMDelegate));

            global::HAL.HALDigital.FreePWM = (global::HAL.HALDigital.FreePWMDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freePWM"), typeof(global::HAL.HALDigital.FreePWMDelegate));

            global::HAL.HALDigital.SetPWMRate = (global::HAL.HALDigital.SetPWMRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setPWMRate"), typeof(global::HAL.HALDigital.SetPWMRateDelegate));

            global::HAL.HALDigital.SetPWMDutyCycle = (global::HAL.HALDigital.SetPWMDutyCycleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setPWMDutyCycle"), typeof(global::HAL.HALDigital.SetPWMDutyCycleDelegate));

            global::HAL.HALDigital.SetPWMOutputChannel = (global::HAL.HALDigital.SetPWMOutputChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setPWMOutputChannel"), typeof(global::HAL.HALDigital.SetPWMOutputChannelDelegate));

            global::HAL.HALDigital.SetRelayForward = (global::HAL.HALDigital.SetRelayForwardDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setRelayForward"), typeof(global::HAL.HALDigital.SetRelayForwardDelegate));

            global::HAL.HALDigital.SetRelayReverse = (global::HAL.HALDigital.SetRelayReverseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setRelayReverse"), typeof(global::HAL.HALDigital.SetRelayReverseDelegate));

            global::HAL.HALDigital.GetRelayForward = (global::HAL.HALDigital.GetRelayForwardDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getRelayForward"), typeof(global::HAL.HALDigital.GetRelayForwardDelegate));

            global::HAL.HALDigital.GetRelayReverse = (global::HAL.HALDigital.GetRelayReverseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getRelayReverse"), typeof(global::HAL.HALDigital.GetRelayReverseDelegate));

            global::HAL.HALDigital.AllocateDIO = (global::HAL.HALDigital.AllocateDIODelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "allocateDIO"), typeof(global::HAL.HALDigital.AllocateDIODelegate));

            global::HAL.HALDigital.FreeDIO = (global::HAL.HALDigital.FreeDIODelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeDIO"), typeof(global::HAL.HALDigital.FreeDIODelegate));

            global::HAL.HALDigital.SetDIO = (global::HAL.HALDigital.SetDIODelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setDIO"), typeof(global::HAL.HALDigital.SetDIODelegate));

            global::HAL.HALDigital.GetDIO = (global::HAL.HALDigital.GetDIODelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getDIO"), typeof(global::HAL.HALDigital.GetDIODelegate));

            global::HAL.HALDigital.GetDIODirection = (global::HAL.HALDigital.GetDIODirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getDIODirection"), typeof(global::HAL.HALDigital.GetDIODirectionDelegate));

            global::HAL.HALDigital.Pulse = (global::HAL.HALDigital.PulseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "pulse"), typeof(global::HAL.HALDigital.PulseDelegate));

            global::HAL.HALDigital.IsPulsing = (global::HAL.HALDigital.IsPulsingDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "isPulsing"), typeof(global::HAL.HALDigital.IsPulsingDelegate));

            global::HAL.HALDigital.IsAnyPulsing = (global::HAL.HALDigital.IsAnyPulsingDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "isAnyPulsing"), typeof(global::HAL.HALDigital.IsAnyPulsingDelegate));

            global::HAL.HALDigital.SetFilterSelect = (global::HAL.HALDigital.SetFilterSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setFilterSelect"), typeof(global::HAL.HALDigital.SetFilterSelectDelegate));

            global::HAL.HALDigital.GetFilterSelect = (global::HAL.HALDigital.GetFilterSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFilterSelect"), typeof(global::HAL.HALDigital.GetFilterSelectDelegate));

            global::HAL.HALDigital.SetFilterPeriod = (global::HAL.HALDigital.SetFilterPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setFilterPeriod"), typeof(global::HAL.HALDigital.SetFilterPeriodDelegate));

            global::HAL.HALDigital.GetFilterPeriod = (global::HAL.HALDigital.GetFilterPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFilterPeriod"), typeof(global::HAL.HALDigital.GetFilterPeriodDelegate));

            global::HAL.HALDigital.InitializeCounter = (global::HAL.HALDigital.InitializeCounterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeCounter"), typeof(global::HAL.HALDigital.InitializeCounterDelegate));

            global::HAL.HALDigital.FreeCounter = (global::HAL.HALDigital.FreeCounterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeCounter"), typeof(global::HAL.HALDigital.FreeCounterDelegate));

            global::HAL.HALDigital.SetCounterAverageSize = (global::HAL.HALDigital.SetCounterAverageSizeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterAverageSize"), typeof(global::HAL.HALDigital.SetCounterAverageSizeDelegate));

            global::HAL.HALDigital.SetCounterUpSource = (global::HAL.HALDigital.SetCounterUpSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterUpSource"), typeof(global::HAL.HALDigital.SetCounterUpSourceDelegate));

            global::HAL.HALDigital.SetCounterUpSourceEdge = (global::HAL.HALDigital.SetCounterUpSourceEdgeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterUpSourceEdge"), typeof(global::HAL.HALDigital.SetCounterUpSourceEdgeDelegate));

            global::HAL.HALDigital.ClearCounterUpSource = (global::HAL.HALDigital.ClearCounterUpSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "clearCounterUpSource"), typeof(global::HAL.HALDigital.ClearCounterUpSourceDelegate));

            global::HAL.HALDigital.SetCounterDownSource = (global::HAL.HALDigital.SetCounterDownSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterDownSource"), typeof(global::HAL.HALDigital.SetCounterDownSourceDelegate));

            global::HAL.HALDigital.SetCounterDownSourceEdge = (global::HAL.HALDigital.SetCounterDownSourceEdgeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterDownSourceEdge"), typeof(global::HAL.HALDigital.SetCounterDownSourceEdgeDelegate));

            global::HAL.HALDigital.ClearCounterDownSource = (global::HAL.HALDigital.ClearCounterDownSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "clearCounterDownSource"), typeof(global::HAL.HALDigital.ClearCounterDownSourceDelegate));

            global::HAL.HALDigital.SetCounterUpDownMode = (global::HAL.HALDigital.SetCounterUpDownModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterUpDownMode"), typeof(global::HAL.HALDigital.SetCounterUpDownModeDelegate));

            global::HAL.HALDigital.SetCounterExternalDirectionMode = (global::HAL.HALDigital.SetCounterExternalDirectionModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterExternalDirectionMode"), typeof(global::HAL.HALDigital.SetCounterExternalDirectionModeDelegate));

            global::HAL.HALDigital.SetCounterSemiPeriodMode = (global::HAL.HALDigital.SetCounterSemiPeriodModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterSemiPeriodMode"), typeof(global::HAL.HALDigital.SetCounterSemiPeriodModeDelegate));

            global::HAL.HALDigital.SetCounterPulseLengthMode = (global::HAL.HALDigital.SetCounterPulseLengthModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterPulseLengthMode"), typeof(global::HAL.HALDigital.SetCounterPulseLengthModeDelegate));

            global::HAL.HALDigital.GetCounterSamplesToAverage = (global::HAL.HALDigital.GetCounterSamplesToAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCounterSamplesToAverage"), typeof(global::HAL.HALDigital.GetCounterSamplesToAverageDelegate));

            global::HAL.HALDigital.SetCounterSamplesToAverage = (global::HAL.HALDigital.SetCounterSamplesToAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterSamplesToAverage"), typeof(global::HAL.HALDigital.SetCounterSamplesToAverageDelegate));

            global::HAL.HALDigital.ResetCounter = (global::HAL.HALDigital.ResetCounterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "resetCounter"), typeof(global::HAL.HALDigital.ResetCounterDelegate));

            global::HAL.HALDigital.GetCounter = (global::HAL.HALDigital.GetCounterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCounter"), typeof(global::HAL.HALDigital.GetCounterDelegate));

            global::HAL.HALDigital.GetCounterPeriod = (global::HAL.HALDigital.GetCounterPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCounterPeriod"), typeof(global::HAL.HALDigital.GetCounterPeriodDelegate));

            global::HAL.HALDigital.SetCounterMaxPeriod = (global::HAL.HALDigital.SetCounterMaxPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterMaxPeriod"), typeof(global::HAL.HALDigital.SetCounterMaxPeriodDelegate));

            global::HAL.HALDigital.SetCounterUpdateWhenEmpty = (global::HAL.HALDigital.SetCounterUpdateWhenEmptyDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterUpdateWhenEmpty"), typeof(global::HAL.HALDigital.SetCounterUpdateWhenEmptyDelegate));

            global::HAL.HALDigital.GetCounterStopped = (global::HAL.HALDigital.GetCounterStoppedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCounterStopped"), typeof(global::HAL.HALDigital.GetCounterStoppedDelegate));

            global::HAL.HALDigital.GetCounterDirection = (global::HAL.HALDigital.GetCounterDirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCounterDirection"), typeof(global::HAL.HALDigital.GetCounterDirectionDelegate));

            global::HAL.HALDigital.SetCounterReverseDirection = (global::HAL.HALDigital.SetCounterReverseDirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterReverseDirection"), typeof(global::HAL.HALDigital.SetCounterReverseDirectionDelegate));

            global::HAL.HALDigital.InitializeEncoder = (global::HAL.HALDigital.InitializeEncoderDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeEncoder"), typeof(global::HAL.HALDigital.InitializeEncoderDelegate));

            global::HAL.HALDigital.ResetEncoder = (global::HAL.HALDigital.ResetEncoderDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "resetEncoder"), typeof(global::HAL.HALDigital.ResetEncoderDelegate));

            global::HAL.HALDigital.FreeEncoder = (global::HAL.HALDigital.FreeEncoderDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeEncoder"), typeof(global::HAL.HALDigital.FreeEncoderDelegate));

            global::HAL.HALDigital.GetEncoder = (global::HAL.HALDigital.GetEncoderDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getEncoder"), typeof(global::HAL.HALDigital.GetEncoderDelegate));

            global::HAL.HALDigital.GetEncoderPeriod = (global::HAL.HALDigital.GetEncoderPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getEncoderPeriod"), typeof(global::HAL.HALDigital.GetEncoderPeriodDelegate));

            global::HAL.HALDigital.SetEncoderMaxPeriod = (global::HAL.HALDigital.SetEncoderMaxPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setEncoderMaxPeriod"), typeof(global::HAL.HALDigital.SetEncoderMaxPeriodDelegate));

            global::HAL.HALDigital.GetEncoderStopped = (global::HAL.HALDigital.GetEncoderStoppedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getEncoderStopped"), typeof(global::HAL.HALDigital.GetEncoderStoppedDelegate));

            global::HAL.HALDigital.GetEncoderDirection = (global::HAL.HALDigital.GetEncoderDirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getEncoderDirection"), typeof(global::HAL.HALDigital.GetEncoderDirectionDelegate));

            global::HAL.HALDigital.SetEncoderReverseDirection = (global::HAL.HALDigital.SetEncoderReverseDirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setEncoderReverseDirection"), typeof(global::HAL.HALDigital.SetEncoderReverseDirectionDelegate));

            global::HAL.HALDigital.SetEncoderSamplesToAverage = (global::HAL.HALDigital.SetEncoderSamplesToAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setEncoderSamplesToAverage"), typeof(global::HAL.HALDigital.SetEncoderSamplesToAverageDelegate));

            global::HAL.HALDigital.GetEncoderSamplesToAverage = (global::HAL.HALDigital.GetEncoderSamplesToAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getEncoderSamplesToAverage"), typeof(global::HAL.HALDigital.GetEncoderSamplesToAverageDelegate));

            global::HAL.HALDigital.SetEncoderIndexSource = (global::HAL.HALDigital.SetEncoderIndexSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setEncoderIndexSource"), typeof(global::HAL.HALDigital.SetEncoderIndexSourceDelegate));

            global::HAL.HALDigital.GetLoopTiming = (global::HAL.HALDigital.GetLoopTimingDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getLoopTiming"), typeof(global::HAL.HALDigital.GetLoopTimingDelegate));

            global::HAL.HALDigital.SpiInitialize = (global::HAL.HALDigital.SpiInitializeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiInitialize"), typeof(global::HAL.HALDigital.SpiInitializeDelegate));

            global::HAL.HALDigital.SpiTransaction = (global::HAL.HALDigital.SpiTransactionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiTransaction"), typeof(global::HAL.HALDigital.SpiTransactionDelegate));

            global::HAL.HALDigital.SpiWrite = (global::HAL.HALDigital.SpiWriteDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiWrite"), typeof(global::HAL.HALDigital.SpiWriteDelegate));

            global::HAL.HALDigital.SpiRead = (global::HAL.HALDigital.SpiReadDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiRead"), typeof(global::HAL.HALDigital.SpiReadDelegate));

            global::HAL.HALDigital.SpiClose = (global::HAL.HALDigital.SpiCloseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiClose"), typeof(global::HAL.HALDigital.SpiCloseDelegate));

            global::HAL.HALDigital.SpiSetSpeed = (global::HAL.HALDigital.SpiSetSpeedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetSpeed"), typeof(global::HAL.HALDigital.SpiSetSpeedDelegate));

            global::HAL.HALDigital.SpiSetOpts = (global::HAL.HALDigital.SpiSetOptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetOpts"), typeof(global::HAL.HALDigital.SpiSetOptsDelegate));

            global::HAL.HALDigital.SpiSetChipSelectActiveHigh = (global::HAL.HALDigital.SpiSetChipSelectActiveHighDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetChipSelectActiveHigh"), typeof(global::HAL.HALDigital.SpiSetChipSelectActiveHighDelegate));

            global::HAL.HALDigital.SpiSetChipSelectActiveLow = (global::HAL.HALDigital.SpiSetChipSelectActiveLowDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetChipSelectActiveLow"), typeof(global::HAL.HALDigital.SpiSetChipSelectActiveLowDelegate));

            global::HAL.HALDigital.SpiGetHandle = (global::HAL.HALDigital.SpiGetHandleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiGetHandle"), typeof(global::HAL.HALDigital.SpiGetHandleDelegate));

            global::HAL.HALDigital.SpiSetHandle = (global::HAL.HALDigital.SpiSetHandleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(global::HAL.HALDigital.SpiSetHandleDelegate));



            global::HAL.HALDigital.SpiInitAccumulator = (global::HAL.HALDigital.SpiInitAccumulatorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(global::HAL.HALDigital.SpiInitAccumulatorDelegate));
            global::HAL.HALDigital.SpiFreeAccumulator = (global::HAL.HALDigital.SpiFreeAccumulatorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(global::HAL.HALDigital.SpiFreeAccumulatorDelegate));
            global::HAL.HALDigital.SpiResetAccumulator = (global::HAL.HALDigital.SpiResetAccumulatorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(global::HAL.HALDigital.SpiResetAccumulatorDelegate));
            global::HAL.HALDigital.SpiSetAccumulatorCenter = (global::HAL.HALDigital.SpiSetAccumulatorCenterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(global::HAL.HALDigital.SpiSetAccumulatorCenterDelegate));
            global::HAL.HALDigital.SpiSetAccumulatorDeadband = (global::HAL.HALDigital.SpiSetAccumulatorDeadbandDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(global::HAL.HALDigital.SpiSetAccumulatorDeadbandDelegate));
            global::HAL.HALDigital.SpiGetAccumulatorLastValue = (global::HAL.HALDigital.SpiGetAccumulatorLastValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(global::HAL.HALDigital.SpiGetAccumulatorLastValueDelegate));
            global::HAL.HALDigital.SpiGetAccumulatorValue = (global::HAL.HALDigital.SpiGetAccumulatorValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(global::HAL.HALDigital.SpiGetAccumulatorValueDelegate));

            global::HAL.HALDigital.SpiGetAccumulatorCount = (global::HAL.HALDigital.SpiGetAccumulatorCountDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(global::HAL.HALDigital.SpiGetAccumulatorCountDelegate));
            global::HAL.HALDigital.SpiGetAccumulatorAverage = (global::HAL.HALDigital.SpiGetAccumulatorAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(global::HAL.HALDigital.SpiGetAccumulatorAverageDelegate));
            global::HAL.HALDigital.SpiGetAccumulatorOutput = (global::HAL.HALDigital.SpiGetAccumulatorOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(global::HAL.HALDigital.SpiGetAccumulatorOutputDelegate));



            global::HAL.HALDigital.I2CInitialize = (global::HAL.HALDigital.I2CInitializeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CInitialize"), typeof(global::HAL.HALDigital.I2CInitializeDelegate));

            global::HAL.HALDigital.I2CTransaction = (global::HAL.HALDigital.I2CTransactionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CTransaction"), typeof(global::HAL.HALDigital.I2CTransactionDelegate));

            global::HAL.HALDigital.I2CWrite = (global::HAL.HALDigital.I2CWriteDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CWrite"), typeof(global::HAL.HALDigital.I2CWriteDelegate));

            global::HAL.HALDigital.I2CRead = (global::HAL.HALDigital.I2CReadDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CRead"), typeof(global::HAL.HALDigital.I2CReadDelegate));

            global::HAL.HALDigital.I2CClose = (global::HAL.HALDigital.I2CCloseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CClose"), typeof(global::HAL.HALDigital.I2CCloseDelegate));

        }
    }
}
