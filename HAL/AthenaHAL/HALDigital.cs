//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;

namespace HAL.AthenaHAL
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALDigital
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALDigital.InitializeDigitalPort = (Base.HALDigital.InitializeDigitalPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeDigitalPort"), typeof(Base.HALDigital.InitializeDigitalPortDelegate));

            Base.HALDigital.FreeDigitalPort = (Base.HALDigital.FreeDigitalPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeDigitalPort"), typeof
                (Base.HALDigital.FreeDigitalPortDelegate));

            Base.HALDigital.CheckPWMChannel = (Base.HALDigital.CheckPWMChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkPWMChannel"), typeof(Base.HALDigital.CheckPWMChannelDelegate));

            Base.HALDigital.CheckRelayChannel = (Base.HALDigital.CheckRelayChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkRelayChannel"), typeof(Base.HALDigital.CheckRelayChannelDelegate));

            Base.HALDigital.SetPWM = (Base.HALDigital.SetPWMDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setPWM"), typeof(Base.HALDigital.SetPWMDelegate));

            Base.HALDigital.AllocatePWMChannel = (Base.HALDigital.AllocatePWMChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "allocatePWMChannel"), typeof(Base.HALDigital.AllocatePWMChannelDelegate));

            Base.HALDigital.FreePWMChannel = (Base.HALDigital.FreePWMChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freePWMChannel"), typeof(Base.HALDigital.FreePWMChannelDelegate));

            Base.HALDigital.GetPWM = (Base.HALDigital.GetPWMDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPWM"), typeof(Base.HALDigital.GetPWMDelegate));

            Base.HALDigital.LatchPWMZero = (Base.HALDigital.LatchPWMZeroDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "latchPWMZero"), typeof(Base.HALDigital.LatchPWMZeroDelegate));

            Base.HALDigital.SetPWMPeriodScale = (Base.HALDigital.SetPWMPeriodScaleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setPWMPeriodScale"), typeof(Base.HALDigital.SetPWMPeriodScaleDelegate));

            Base.HALDigital.AllocatePWM = (Base.HALDigital.AllocatePWMDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "allocatePWM"), typeof(Base.HALDigital.AllocatePWMDelegate));

            Base.HALDigital.FreePWM = (Base.HALDigital.FreePWMDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freePWM"), typeof(Base.HALDigital.FreePWMDelegate));

            Base.HALDigital.SetPWMRate = (Base.HALDigital.SetPWMRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setPWMRate"), typeof(Base.HALDigital.SetPWMRateDelegate));

            Base.HALDigital.SetPWMDutyCycle = (Base.HALDigital.SetPWMDutyCycleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setPWMDutyCycle"), typeof(Base.HALDigital.SetPWMDutyCycleDelegate));

            Base.HALDigital.SetPWMOutputChannel = (Base.HALDigital.SetPWMOutputChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setPWMOutputChannel"), typeof(Base.HALDigital.SetPWMOutputChannelDelegate));

            Base.HALDigital.SetRelayForward = (Base.HALDigital.SetRelayForwardDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setRelayForward"), typeof(Base.HALDigital.SetRelayForwardDelegate));

            Base.HALDigital.SetRelayReverse = (Base.HALDigital.SetRelayReverseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setRelayReverse"), typeof(Base.HALDigital.SetRelayReverseDelegate));

            Base.HALDigital.GetRelayForward = (Base.HALDigital.GetRelayForwardDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getRelayForward"), typeof(Base.HALDigital.GetRelayForwardDelegate));

            Base.HALDigital.GetRelayReverse = (Base.HALDigital.GetRelayReverseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getRelayReverse"), typeof(Base.HALDigital.GetRelayReverseDelegate));

            Base.HALDigital.AllocateDIO = (Base.HALDigital.AllocateDIODelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "allocateDIO"), typeof(Base.HALDigital.AllocateDIODelegate));

            Base.HALDigital.FreeDIO = (Base.HALDigital.FreeDIODelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeDIO"), typeof(Base.HALDigital.FreeDIODelegate));

            Base.HALDigital.SetDIO = (Base.HALDigital.SetDIODelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setDIO"), typeof(Base.HALDigital.SetDIODelegate));

            Base.HALDigital.GetDIO = (Base.HALDigital.GetDIODelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getDIO"), typeof(Base.HALDigital.GetDIODelegate));

            Base.HALDigital.GetDIODirection = (Base.HALDigital.GetDIODirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getDIODirection"), typeof(Base.HALDigital.GetDIODirectionDelegate));

            Base.HALDigital.Pulse = (Base.HALDigital.PulseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "pulse"), typeof(Base.HALDigital.PulseDelegate));

            Base.HALDigital.IsPulsing = (Base.HALDigital.IsPulsingDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "isPulsing"), typeof(Base.HALDigital.IsPulsingDelegate));

            Base.HALDigital.IsAnyPulsing = (Base.HALDigital.IsAnyPulsingDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "isAnyPulsing"), typeof(Base.HALDigital.IsAnyPulsingDelegate));
            /*
            Base.HALDigital.SetFilterSelect = (Base.HALDigital.SetFilterSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setFilterSelect"), typeof(Base.HALDigital.SetFilterSelectDelegate));

            Base.HALDigital.GetFilterSelect = (Base.HALDigital.GetFilterSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFilterSelect"), typeof(Base.HALDigital.GetFilterSelectDelegate));

            Base.HALDigital.SetFilterPeriod = (Base.HALDigital.SetFilterPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setFilterPeriod"), typeof(Base.HALDigital.SetFilterPeriodDelegate));

            Base.HALDigital.GetFilterPeriod = (Base.HALDigital.GetFilterPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFilterPeriod"), typeof(Base.HALDigital.GetFilterPeriodDelegate));
            */
            Base.HALDigital.InitializeCounter = (Base.HALDigital.InitializeCounterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeCounter"), typeof(Base.HALDigital.InitializeCounterDelegate));

            Base.HALDigital.FreeCounter = (Base.HALDigital.FreeCounterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeCounter"), typeof(Base.HALDigital.FreeCounterDelegate));

            Base.HALDigital.SetCounterAverageSize = (Base.HALDigital.SetCounterAverageSizeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterAverageSize"), typeof(Base.HALDigital.SetCounterAverageSizeDelegate));

            Base.HALDigital.SetCounterUpSource = (Base.HALDigital.SetCounterUpSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterUpSource"), typeof(Base.HALDigital.SetCounterUpSourceDelegate));

            Base.HALDigital.SetCounterUpSourceEdge = (Base.HALDigital.SetCounterUpSourceEdgeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterUpSourceEdge"), typeof(Base.HALDigital.SetCounterUpSourceEdgeDelegate));

            Base.HALDigital.ClearCounterUpSource = (Base.HALDigital.ClearCounterUpSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "clearCounterUpSource"), typeof(Base.HALDigital.ClearCounterUpSourceDelegate));

            Base.HALDigital.SetCounterDownSource = (Base.HALDigital.SetCounterDownSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterDownSource"), typeof(Base.HALDigital.SetCounterDownSourceDelegate));

            Base.HALDigital.SetCounterDownSourceEdge = (Base.HALDigital.SetCounterDownSourceEdgeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterDownSourceEdge"), typeof(Base.HALDigital.SetCounterDownSourceEdgeDelegate));

            Base.HALDigital.ClearCounterDownSource = (Base.HALDigital.ClearCounterDownSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "clearCounterDownSource"), typeof(Base.HALDigital.ClearCounterDownSourceDelegate));

            Base.HALDigital.SetCounterUpDownMode = (Base.HALDigital.SetCounterUpDownModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterUpDownMode"), typeof(Base.HALDigital.SetCounterUpDownModeDelegate));

            Base.HALDigital.SetCounterExternalDirectionMode = (Base.HALDigital.SetCounterExternalDirectionModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterExternalDirectionMode"), typeof(Base.HALDigital.SetCounterExternalDirectionModeDelegate));

            Base.HALDigital.SetCounterSemiPeriodMode = (Base.HALDigital.SetCounterSemiPeriodModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterSemiPeriodMode"), typeof(Base.HALDigital.SetCounterSemiPeriodModeDelegate));

            Base.HALDigital.SetCounterPulseLengthMode = (Base.HALDigital.SetCounterPulseLengthModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterPulseLengthMode"), typeof(Base.HALDigital.SetCounterPulseLengthModeDelegate));

            Base.HALDigital.GetCounterSamplesToAverage = (Base.HALDigital.GetCounterSamplesToAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCounterSamplesToAverage"), typeof(Base.HALDigital.GetCounterSamplesToAverageDelegate));

            Base.HALDigital.SetCounterSamplesToAverage = (Base.HALDigital.SetCounterSamplesToAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterSamplesToAverage"), typeof(Base.HALDigital.SetCounterSamplesToAverageDelegate));

            Base.HALDigital.ResetCounter = (Base.HALDigital.ResetCounterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "resetCounter"), typeof(Base.HALDigital.ResetCounterDelegate));

            Base.HALDigital.GetCounter = (Base.HALDigital.GetCounterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCounter"), typeof(Base.HALDigital.GetCounterDelegate));

            Base.HALDigital.GetCounterPeriod = (Base.HALDigital.GetCounterPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCounterPeriod"), typeof(Base.HALDigital.GetCounterPeriodDelegate));

            Base.HALDigital.SetCounterMaxPeriod = (Base.HALDigital.SetCounterMaxPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterMaxPeriod"), typeof(Base.HALDigital.SetCounterMaxPeriodDelegate));

            Base.HALDigital.SetCounterUpdateWhenEmpty = (Base.HALDigital.SetCounterUpdateWhenEmptyDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterUpdateWhenEmpty"), typeof(Base.HALDigital.SetCounterUpdateWhenEmptyDelegate));

            Base.HALDigital.GetCounterStopped = (Base.HALDigital.GetCounterStoppedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCounterStopped"), typeof(Base.HALDigital.GetCounterStoppedDelegate));

            Base.HALDigital.GetCounterDirection = (Base.HALDigital.GetCounterDirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCounterDirection"), typeof(Base.HALDigital.GetCounterDirectionDelegate));

            Base.HALDigital.SetCounterReverseDirection = (Base.HALDigital.SetCounterReverseDirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterReverseDirection"), typeof(Base.HALDigital.SetCounterReverseDirectionDelegate));

            Base.HALDigital.InitializeEncoder = (Base.HALDigital.InitializeEncoderDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeEncoder"), typeof(Base.HALDigital.InitializeEncoderDelegate));

            Base.HALDigital.ResetEncoder = (Base.HALDigital.ResetEncoderDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "resetEncoder"), typeof(Base.HALDigital.ResetEncoderDelegate));

            Base.HALDigital.FreeEncoder = (Base.HALDigital.FreeEncoderDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeEncoder"), typeof(Base.HALDigital.FreeEncoderDelegate));

            Base.HALDigital.GetEncoder = (Base.HALDigital.GetEncoderDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getEncoder"), typeof(Base.HALDigital.GetEncoderDelegate));

            Base.HALDigital.GetEncoderPeriod = (Base.HALDigital.GetEncoderPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getEncoderPeriod"), typeof(Base.HALDigital.GetEncoderPeriodDelegate));

            Base.HALDigital.SetEncoderMaxPeriod = (Base.HALDigital.SetEncoderMaxPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setEncoderMaxPeriod"), typeof(Base.HALDigital.SetEncoderMaxPeriodDelegate));

            Base.HALDigital.GetEncoderStopped = (Base.HALDigital.GetEncoderStoppedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getEncoderStopped"), typeof(Base.HALDigital.GetEncoderStoppedDelegate));

            Base.HALDigital.GetEncoderDirection = (Base.HALDigital.GetEncoderDirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getEncoderDirection"), typeof(Base.HALDigital.GetEncoderDirectionDelegate));

            Base.HALDigital.SetEncoderReverseDirection = (Base.HALDigital.SetEncoderReverseDirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setEncoderReverseDirection"), typeof(Base.HALDigital.SetEncoderReverseDirectionDelegate));

            Base.HALDigital.SetEncoderSamplesToAverage = (Base.HALDigital.SetEncoderSamplesToAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setEncoderSamplesToAverage"), typeof(Base.HALDigital.SetEncoderSamplesToAverageDelegate));

            Base.HALDigital.GetEncoderSamplesToAverage = (Base.HALDigital.GetEncoderSamplesToAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getEncoderSamplesToAverage"), typeof(Base.HALDigital.GetEncoderSamplesToAverageDelegate));

            Base.HALDigital.SetEncoderIndexSource = (Base.HALDigital.SetEncoderIndexSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setEncoderIndexSource"), typeof(Base.HALDigital.SetEncoderIndexSourceDelegate));

            Base.HALDigital.GetLoopTiming = (Base.HALDigital.GetLoopTimingDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getLoopTiming"), typeof(Base.HALDigital.GetLoopTimingDelegate));

            Base.HALDigital.SpiInitialize = (Base.HALDigital.SpiInitializeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiInitialize"), typeof(Base.HALDigital.SpiInitializeDelegate));

            Base.HALDigital.SpiTransaction = (Base.HALDigital.SpiTransactionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiTransaction"), typeof(Base.HALDigital.SpiTransactionDelegate));

            Base.HALDigital.SpiWrite = (Base.HALDigital.SpiWriteDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiWrite"), typeof(Base.HALDigital.SpiWriteDelegate));

            Base.HALDigital.SpiRead = (Base.HALDigital.SpiReadDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiRead"), typeof(Base.HALDigital.SpiReadDelegate));

            Base.HALDigital.SpiClose = (Base.HALDigital.SpiCloseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiClose"), typeof(Base.HALDigital.SpiCloseDelegate));

            Base.HALDigital.SpiSetSpeed = (Base.HALDigital.SpiSetSpeedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetSpeed"), typeof(Base.HALDigital.SpiSetSpeedDelegate));

            Base.HALDigital.SpiSetOpts = (Base.HALDigital.SpiSetOptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetOpts"), typeof(Base.HALDigital.SpiSetOptsDelegate));

            Base.HALDigital.SpiSetChipSelectActiveHigh = (Base.HALDigital.SpiSetChipSelectActiveHighDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetChipSelectActiveHigh"), typeof(Base.HALDigital.SpiSetChipSelectActiveHighDelegate));

            Base.HALDigital.SpiSetChipSelectActiveLow = (Base.HALDigital.SpiSetChipSelectActiveLowDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetChipSelectActiveLow"), typeof(Base.HALDigital.SpiSetChipSelectActiveLowDelegate));

            Base.HALDigital.SpiGetHandle = (Base.HALDigital.SpiGetHandleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiGetHandle"), typeof(Base.HALDigital.SpiGetHandleDelegate));

            Base.HALDigital.SpiSetHandle = (Base.HALDigital.SpiSetHandleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(Base.HALDigital.SpiSetHandleDelegate));


            /*
            Base.HALDigital.SpiInitAccumulator = (Base.HALDigital.SpiInitAccumulatorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(Base.HALDigital.SpiInitAccumulatorDelegate));
            Base.HALDigital.SpiFreeAccumulator = (Base.HALDigital.SpiFreeAccumulatorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(Base.HALDigital.SpiFreeAccumulatorDelegate));
            Base.HALDigital.SpiResetAccumulator = (Base.HALDigital.SpiResetAccumulatorDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(Base.HALDigital.SpiResetAccumulatorDelegate));
            Base.HALDigital.SpiSetAccumulatorCenter = (Base.HALDigital.SpiSetAccumulatorCenterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(Base.HALDigital.SpiSetAccumulatorCenterDelegate));
            Base.HALDigital.SpiSetAccumulatorDeadband = (Base.HALDigital.SpiSetAccumulatorDeadbandDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(Base.HALDigital.SpiSetAccumulatorDeadbandDelegate));
            Base.HALDigital.SpiGetAccumulatorLastValue = (Base.HALDigital.SpiGetAccumulatorLastValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(Base.HALDigital.SpiGetAccumulatorLastValueDelegate));
            Base.HALDigital.SpiGetAccumulatorValue = (Base.HALDigital.SpiGetAccumulatorValueDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(Base.HALDigital.SpiGetAccumulatorValueDelegate));

            Base.HALDigital.SpiGetAccumulatorCount = (Base.HALDigital.SpiGetAccumulatorCountDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(Base.HALDigital.SpiGetAccumulatorCountDelegate));
            Base.HALDigital.SpiGetAccumulatorAverage = (Base.HALDigital.SpiGetAccumulatorAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(Base.HALDigital.SpiGetAccumulatorAverageDelegate));
            Base.HALDigital.SpiGetAccumulatorOutput = (Base.HALDigital.SpiGetAccumulatorOutputDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(Base.HALDigital.SpiGetAccumulatorOutputDelegate));
            */


            Base.HALDigital.I2CInitialize = (Base.HALDigital.I2CInitializeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CInitialize"), typeof(Base.HALDigital.I2CInitializeDelegate));

            Base.HALDigital.I2CTransaction = (Base.HALDigital.I2CTransactionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CTransaction"), typeof(Base.HALDigital.I2CTransactionDelegate));

            Base.HALDigital.I2CWrite = (Base.HALDigital.I2CWriteDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CWrite"), typeof(Base.HALDigital.I2CWriteDelegate));

            Base.HALDigital.I2CRead = (Base.HALDigital.I2CReadDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CRead"), typeof(Base.HALDigital.I2CReadDelegate));

            Base.HALDigital.I2CClose = (Base.HALDigital.I2CCloseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CClose"), typeof(Base.HALDigital.I2CCloseDelegate));

        }
    }
}
