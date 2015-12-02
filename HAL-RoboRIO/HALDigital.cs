//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALDigital
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALDigital.InitializeDigitalPort = (HAL_Base.HALDigital.InitializeDigitalPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeDigitalPort"), typeof(HAL_Base.HALDigital.InitializeDigitalPortDelegate));

            HAL_Base.HALDigital.FreeDigitalPort = (HAL_Base.HALDigital.FreeDigitalPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeDigitalPort"), typeof
                (HAL_Base.HALDigital.FreeDigitalPortDelegate));

            HAL_Base.HALDigital.CheckPWMChannel = (HAL_Base.HALDigital.CheckPWMChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkPWMChannel"), typeof(HAL_Base.HALDigital.CheckPWMChannelDelegate));

            HAL_Base.HALDigital.CheckRelayChannel = (HAL_Base.HALDigital.CheckRelayChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "checkRelayChannel"), typeof(HAL_Base.HALDigital.CheckRelayChannelDelegate));

            HAL_Base.HALDigital.SetPWM = (HAL_Base.HALDigital.SetPWMDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setPWM"), typeof(HAL_Base.HALDigital.SetPWMDelegate));

            HAL_Base.HALDigital.AllocatePWMChannel = (HAL_Base.HALDigital.AllocatePWMChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "allocatePWMChannel"), typeof(HAL_Base.HALDigital.AllocatePWMChannelDelegate));

            HAL_Base.HALDigital.FreePWMChannel = (HAL_Base.HALDigital.FreePWMChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freePWMChannel"), typeof(HAL_Base.HALDigital.FreePWMChannelDelegate));

            HAL_Base.HALDigital.GetPWM = (HAL_Base.HALDigital.GetPWMDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getPWM"), typeof(HAL_Base.HALDigital.GetPWMDelegate));

            HAL_Base.HALDigital.LatchPWMZero = (HAL_Base.HALDigital.LatchPWMZeroDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "latchPWMZero"), typeof(HAL_Base.HALDigital.LatchPWMZeroDelegate));

            HAL_Base.HALDigital.SetPWMPeriodScale = (HAL_Base.HALDigital.SetPWMPeriodScaleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setPWMPeriodScale"), typeof(HAL_Base.HALDigital.SetPWMPeriodScaleDelegate));

            HAL_Base.HALDigital.AllocatePWM = (HAL_Base.HALDigital.AllocatePWMDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "allocatePWM"), typeof(HAL_Base.HALDigital.AllocatePWMDelegate));

            HAL_Base.HALDigital.FreePWM = (HAL_Base.HALDigital.FreePWMDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freePWM"), typeof(HAL_Base.HALDigital.FreePWMDelegate));

            HAL_Base.HALDigital.SetPWMRate = (HAL_Base.HALDigital.SetPWMRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setPWMRate"), typeof(HAL_Base.HALDigital.SetPWMRateDelegate));

            HAL_Base.HALDigital.SetPWMDutyCycle = (HAL_Base.HALDigital.SetPWMDutyCycleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setPWMDutyCycle"), typeof(HAL_Base.HALDigital.SetPWMDutyCycleDelegate));

            HAL_Base.HALDigital.SetPWMOutputChannel = (HAL_Base.HALDigital.SetPWMOutputChannelDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setPWMOutputChannel"), typeof(HAL_Base.HALDigital.SetPWMOutputChannelDelegate));

            HAL_Base.HALDigital.SetRelayForward = (HAL_Base.HALDigital.SetRelayForwardDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setRelayForward"), typeof(HAL_Base.HALDigital.SetRelayForwardDelegate));

            HAL_Base.HALDigital.SetRelayReverse = (HAL_Base.HALDigital.SetRelayReverseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setRelayReverse"), typeof(HAL_Base.HALDigital.SetRelayReverseDelegate));

            HAL_Base.HALDigital.GetRelayForward = (HAL_Base.HALDigital.GetRelayForwardDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getRelayForward"), typeof(HAL_Base.HALDigital.GetRelayForwardDelegate));

            HAL_Base.HALDigital.GetRelayReverse = (HAL_Base.HALDigital.GetRelayReverseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getRelayReverse"), typeof(HAL_Base.HALDigital.GetRelayReverseDelegate));

            HAL_Base.HALDigital.AllocateDIO = (HAL_Base.HALDigital.AllocateDIODelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "allocateDIO"), typeof(HAL_Base.HALDigital.AllocateDIODelegate));

            HAL_Base.HALDigital.FreeDIO = (HAL_Base.HALDigital.FreeDIODelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeDIO"), typeof(HAL_Base.HALDigital.FreeDIODelegate));

            HAL_Base.HALDigital.SetDIO = (HAL_Base.HALDigital.SetDIODelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setDIO"), typeof(HAL_Base.HALDigital.SetDIODelegate));

            HAL_Base.HALDigital.GetDIO = (HAL_Base.HALDigital.GetDIODelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getDIO"), typeof(HAL_Base.HALDigital.GetDIODelegate));

            HAL_Base.HALDigital.GetDIODirection = (HAL_Base.HALDigital.GetDIODirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getDIODirection"), typeof(HAL_Base.HALDigital.GetDIODirectionDelegate));

            HAL_Base.HALDigital.Pulse = (HAL_Base.HALDigital.PulseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "pulse"), typeof(HAL_Base.HALDigital.PulseDelegate));

            HAL_Base.HALDigital.IsPulsing = (HAL_Base.HALDigital.IsPulsingDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "isPulsing"), typeof(HAL_Base.HALDigital.IsPulsingDelegate));

            HAL_Base.HALDigital.IsAnyPulsing = (HAL_Base.HALDigital.IsAnyPulsingDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "isAnyPulsing"), typeof(HAL_Base.HALDigital.IsAnyPulsingDelegate));

            HAL_Base.HALDigital.SetFilterSelect = (HAL_Base.HALDigital.SetFilterSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setFilterSelect"), typeof(HAL_Base.HALDigital.SetFilterSelectDelegate));

            HAL_Base.HALDigital.GetFilterSelect = (HAL_Base.HALDigital.GetFilterSelectDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFilterSelect"), typeof(HAL_Base.HALDigital.GetFilterSelectDelegate));

            HAL_Base.HALDigital.SetFilterPeriod = (HAL_Base.HALDigital.SetFilterPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setFilterPeriod"), typeof(HAL_Base.HALDigital.SetFilterPeriodDelegate));

            HAL_Base.HALDigital.GetFilterPeriod = (HAL_Base.HALDigital.GetFilterPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getFilterPeriod"), typeof(HAL_Base.HALDigital.GetFilterPeriodDelegate));

            HAL_Base.HALDigital.InitializeCounter = (HAL_Base.HALDigital.InitializeCounterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeCounter"), typeof(HAL_Base.HALDigital.InitializeCounterDelegate));

            HAL_Base.HALDigital.FreeCounter = (HAL_Base.HALDigital.FreeCounterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeCounter"), typeof(HAL_Base.HALDigital.FreeCounterDelegate));

            HAL_Base.HALDigital.SetCounterAverageSize = (HAL_Base.HALDigital.SetCounterAverageSizeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterAverageSize"), typeof(HAL_Base.HALDigital.SetCounterAverageSizeDelegate));

            HAL_Base.HALDigital.SetCounterUpSource = (HAL_Base.HALDigital.SetCounterUpSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterUpSource"), typeof(HAL_Base.HALDigital.SetCounterUpSourceDelegate));

            HAL_Base.HALDigital.SetCounterUpSourceEdge = (HAL_Base.HALDigital.SetCounterUpSourceEdgeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterUpSourceEdge"), typeof(HAL_Base.HALDigital.SetCounterUpSourceEdgeDelegate));

            HAL_Base.HALDigital.ClearCounterUpSource = (HAL_Base.HALDigital.ClearCounterUpSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "clearCounterUpSource"), typeof(HAL_Base.HALDigital.ClearCounterUpSourceDelegate));

            HAL_Base.HALDigital.SetCounterDownSource = (HAL_Base.HALDigital.SetCounterDownSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterDownSource"), typeof(HAL_Base.HALDigital.SetCounterDownSourceDelegate));

            HAL_Base.HALDigital.SetCounterDownSourceEdge = (HAL_Base.HALDigital.SetCounterDownSourceEdgeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterDownSourceEdge"), typeof(HAL_Base.HALDigital.SetCounterDownSourceEdgeDelegate));

            HAL_Base.HALDigital.ClearCounterDownSource = (HAL_Base.HALDigital.ClearCounterDownSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "clearCounterDownSource"), typeof(HAL_Base.HALDigital.ClearCounterDownSourceDelegate));

            HAL_Base.HALDigital.SetCounterUpDownMode = (HAL_Base.HALDigital.SetCounterUpDownModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterUpDownMode"), typeof(HAL_Base.HALDigital.SetCounterUpDownModeDelegate));

            HAL_Base.HALDigital.SetCounterExternalDirectionMode = (HAL_Base.HALDigital.SetCounterExternalDirectionModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterExternalDirectionMode"), typeof(HAL_Base.HALDigital.SetCounterExternalDirectionModeDelegate));

            HAL_Base.HALDigital.SetCounterSemiPeriodMode = (HAL_Base.HALDigital.SetCounterSemiPeriodModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterSemiPeriodMode"), typeof(HAL_Base.HALDigital.SetCounterSemiPeriodModeDelegate));

            HAL_Base.HALDigital.SetCounterPulseLengthMode = (HAL_Base.HALDigital.SetCounterPulseLengthModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterPulseLengthMode"), typeof(HAL_Base.HALDigital.SetCounterPulseLengthModeDelegate));

            HAL_Base.HALDigital.GetCounterSamplesToAverage = (HAL_Base.HALDigital.GetCounterSamplesToAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCounterSamplesToAverage"), typeof(HAL_Base.HALDigital.GetCounterSamplesToAverageDelegate));

            HAL_Base.HALDigital.SetCounterSamplesToAverage = (HAL_Base.HALDigital.SetCounterSamplesToAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterSamplesToAverage"), typeof(HAL_Base.HALDigital.SetCounterSamplesToAverageDelegate));

            HAL_Base.HALDigital.ResetCounter = (HAL_Base.HALDigital.ResetCounterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "resetCounter"), typeof(HAL_Base.HALDigital.ResetCounterDelegate));

            HAL_Base.HALDigital.GetCounter = (HAL_Base.HALDigital.GetCounterDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCounter"), typeof(HAL_Base.HALDigital.GetCounterDelegate));

            HAL_Base.HALDigital.GetCounterPeriod = (HAL_Base.HALDigital.GetCounterPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCounterPeriod"), typeof(HAL_Base.HALDigital.GetCounterPeriodDelegate));

            HAL_Base.HALDigital.SetCounterMaxPeriod = (HAL_Base.HALDigital.SetCounterMaxPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterMaxPeriod"), typeof(HAL_Base.HALDigital.SetCounterMaxPeriodDelegate));

            HAL_Base.HALDigital.SetCounterUpdateWhenEmpty = (HAL_Base.HALDigital.SetCounterUpdateWhenEmptyDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterUpdateWhenEmpty"), typeof(HAL_Base.HALDigital.SetCounterUpdateWhenEmptyDelegate));

            HAL_Base.HALDigital.GetCounterStopped = (HAL_Base.HALDigital.GetCounterStoppedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCounterStopped"), typeof(HAL_Base.HALDigital.GetCounterStoppedDelegate));

            HAL_Base.HALDigital.GetCounterDirection = (HAL_Base.HALDigital.GetCounterDirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getCounterDirection"), typeof(HAL_Base.HALDigital.GetCounterDirectionDelegate));

            HAL_Base.HALDigital.SetCounterReverseDirection = (HAL_Base.HALDigital.SetCounterReverseDirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setCounterReverseDirection"), typeof(HAL_Base.HALDigital.SetCounterReverseDirectionDelegate));

            HAL_Base.HALDigital.InitializeEncoder = (HAL_Base.HALDigital.InitializeEncoderDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeEncoder"), typeof(HAL_Base.HALDigital.InitializeEncoderDelegate));

            HAL_Base.HALDigital.ResetEncoder = (HAL_Base.HALDigital.ResetEncoderDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "resetEncoder"), typeof(HAL_Base.HALDigital.ResetEncoderDelegate));

            HAL_Base.HALDigital.FreeEncoder = (HAL_Base.HALDigital.FreeEncoderDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "freeEncoder"), typeof(HAL_Base.HALDigital.FreeEncoderDelegate));

            HAL_Base.HALDigital.GetEncoder = (HAL_Base.HALDigital.GetEncoderDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getEncoder"), typeof(HAL_Base.HALDigital.GetEncoderDelegate));

            HAL_Base.HALDigital.GetEncoderPeriod = (HAL_Base.HALDigital.GetEncoderPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getEncoderPeriod"), typeof(HAL_Base.HALDigital.GetEncoderPeriodDelegate));

            HAL_Base.HALDigital.SetEncoderMaxPeriod = (HAL_Base.HALDigital.SetEncoderMaxPeriodDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setEncoderMaxPeriod"), typeof(HAL_Base.HALDigital.SetEncoderMaxPeriodDelegate));

            HAL_Base.HALDigital.GetEncoderStopped = (HAL_Base.HALDigital.GetEncoderStoppedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getEncoderStopped"), typeof(HAL_Base.HALDigital.GetEncoderStoppedDelegate));

            HAL_Base.HALDigital.GetEncoderDirection = (HAL_Base.HALDigital.GetEncoderDirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getEncoderDirection"), typeof(HAL_Base.HALDigital.GetEncoderDirectionDelegate));

            HAL_Base.HALDigital.SetEncoderReverseDirection = (HAL_Base.HALDigital.SetEncoderReverseDirectionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setEncoderReverseDirection"), typeof(HAL_Base.HALDigital.SetEncoderReverseDirectionDelegate));

            HAL_Base.HALDigital.SetEncoderSamplesToAverage = (HAL_Base.HALDigital.SetEncoderSamplesToAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setEncoderSamplesToAverage"), typeof(HAL_Base.HALDigital.SetEncoderSamplesToAverageDelegate));

            HAL_Base.HALDigital.GetEncoderSamplesToAverage = (HAL_Base.HALDigital.GetEncoderSamplesToAverageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getEncoderSamplesToAverage"), typeof(HAL_Base.HALDigital.GetEncoderSamplesToAverageDelegate));

            HAL_Base.HALDigital.SetEncoderIndexSource = (HAL_Base.HALDigital.SetEncoderIndexSourceDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "setEncoderIndexSource"), typeof(HAL_Base.HALDigital.SetEncoderIndexSourceDelegate));

            HAL_Base.HALDigital.GetLoopTiming = (HAL_Base.HALDigital.GetLoopTimingDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "getLoopTiming"), typeof(HAL_Base.HALDigital.GetLoopTimingDelegate));

            HAL_Base.HALDigital.SpiInitialize = (HAL_Base.HALDigital.SpiInitializeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiInitialize"), typeof(HAL_Base.HALDigital.SpiInitializeDelegate));

            HAL_Base.HALDigital.SpiTransaction = (HAL_Base.HALDigital.SpiTransactionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiTransaction"), typeof(HAL_Base.HALDigital.SpiTransactionDelegate));

            HAL_Base.HALDigital.SpiWrite = (HAL_Base.HALDigital.SpiWriteDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiWrite"), typeof(HAL_Base.HALDigital.SpiWriteDelegate));

            HAL_Base.HALDigital.SpiRead = (HAL_Base.HALDigital.SpiReadDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiRead"), typeof(HAL_Base.HALDigital.SpiReadDelegate));

            HAL_Base.HALDigital.SpiClose = (HAL_Base.HALDigital.SpiCloseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiClose"), typeof(HAL_Base.HALDigital.SpiCloseDelegate));

            HAL_Base.HALDigital.SpiSetSpeed = (HAL_Base.HALDigital.SpiSetSpeedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetSpeed"), typeof(HAL_Base.HALDigital.SpiSetSpeedDelegate));

            HAL_Base.HALDigital.SpiSetOpts = (HAL_Base.HALDigital.SpiSetOptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetOpts"), typeof(HAL_Base.HALDigital.SpiSetOptsDelegate));

            HAL_Base.HALDigital.SpiSetChipSelectActiveHigh = (HAL_Base.HALDigital.SpiSetChipSelectActiveHighDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetChipSelectActiveHigh"), typeof(HAL_Base.HALDigital.SpiSetChipSelectActiveHighDelegate));

            HAL_Base.HALDigital.SpiSetChipSelectActiveLow = (HAL_Base.HALDigital.SpiSetChipSelectActiveLowDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetChipSelectActiveLow"), typeof(HAL_Base.HALDigital.SpiSetChipSelectActiveLowDelegate));

            HAL_Base.HALDigital.SpiGetHandle = (HAL_Base.HALDigital.SpiGetHandleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiGetHandle"), typeof(HAL_Base.HALDigital.SpiGetHandleDelegate));

            HAL_Base.HALDigital.SpiSetHandle = (HAL_Base.HALDigital.SpiSetHandleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(HAL_Base.HALDigital.SpiSetHandleDelegate));

            HAL_Base.HALDigital.I2CInitialize = (HAL_Base.HALDigital.I2CInitializeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CInitialize"), typeof(HAL_Base.HALDigital.I2CInitializeDelegate));

            HAL_Base.HALDigital.I2CTransaction = (HAL_Base.HALDigital.I2CTransactionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CTransaction"), typeof(HAL_Base.HALDigital.I2CTransactionDelegate));

            HAL_Base.HALDigital.I2CWrite = (HAL_Base.HALDigital.I2CWriteDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CWrite"), typeof(HAL_Base.HALDigital.I2CWriteDelegate));

            HAL_Base.HALDigital.I2CRead = (HAL_Base.HALDigital.I2CReadDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CRead"), typeof(HAL_Base.HALDigital.I2CReadDelegate));

            HAL_Base.HALDigital.I2CClose = (HAL_Base.HALDigital.I2CCloseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CClose"), typeof(HAL_Base.HALDigital.I2CCloseDelegate));

        }
    }
}
