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
        internal static void Initialize(IntPtr library, IDllLoader loader)
        {
            HAL_Base.HALDigital.InitializeDigitalPort = (HAL_Base.HALDigital.InitializeDigitalPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "initializeDigitalPort"), typeof(HAL_Base.HALDigital.InitializeDigitalPortDelegate));

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

            HAL_Base.HALDigital.SpiSetBitsPerWord = (HAL_Base.HALDigital.SpiSetBitsPerWordDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetBitsPerWord"), typeof(HAL_Base.HALDigital.SpiSetBitsPerWordDelegate));

            HAL_Base.HALDigital.SpiSetOpts = (HAL_Base.HALDigital.SpiSetOptsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetOpts"), typeof(HAL_Base.HALDigital.SpiSetOptsDelegate));

            HAL_Base.HALDigital.SpiSetChipSelectActiveHigh = (HAL_Base.HALDigital.SpiSetChipSelectActiveHighDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetChipSelectActiveHigh"), typeof(HAL_Base.HALDigital.SpiSetChipSelectActiveHighDelegate));

            HAL_Base.HALDigital.SpiSetChipSelectActiveLow = (HAL_Base.HALDigital.SpiSetChipSelectActiveLowDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetChipSelectActiveLow"), typeof(HAL_Base.HALDigital.SpiSetChipSelectActiveLowDelegate));

            HAL_Base.HALDigital.SpiGetHandle = (HAL_Base.HALDigital.SpiGetHandleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiGetHandle"), typeof(HAL_Base.HALDigital.SpiGetHandleDelegate));

            HAL_Base.HALDigital.SpiSetHandle = (HAL_Base.HALDigital.SpiSetHandleDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetHandle"), typeof(HAL_Base.HALDigital.SpiSetHandleDelegate));

            HAL_Base.HALDigital.SpiGetSemaphore = (HAL_Base.HALDigital.SpiGetSemaphoreDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiGetSemaphore"), typeof(HAL_Base.HALDigital.SpiGetSemaphoreDelegate));

            HAL_Base.HALDigital.SpiSetSemaphore = (HAL_Base.HALDigital.SpiSetSemaphoreDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "spiSetSemaphore"), typeof(HAL_Base.HALDigital.SpiSetSemaphoreDelegate));

            HAL_Base.HALDigital.I2CInitialize = (HAL_Base.HALDigital.I2CInitializeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CInitialize"), typeof(HAL_Base.HALDigital.I2CInitializeDelegate));

            HAL_Base.HALDigital.I2CTransaction = (HAL_Base.HALDigital.I2CTransactionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CTransaction"), typeof(HAL_Base.HALDigital.I2CTransactionDelegate));

            HAL_Base.HALDigital.I2CWrite = (HAL_Base.HALDigital.I2CWriteDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CWrite"), typeof(HAL_Base.HALDigital.I2CWriteDelegate));

            HAL_Base.HALDigital.I2CRead = (HAL_Base.HALDigital.I2CReadDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CRead"), typeof(HAL_Base.HALDigital.I2CReadDelegate));

            HAL_Base.HALDigital.I2CClose = (HAL_Base.HALDigital.I2CCloseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "i2CClose"), typeof(HAL_Base.HALDigital.I2CCloseDelegate));

        }


        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeDigitalPort")]
        public static extern IntPtr initializeDigitalPort(IntPtr port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "checkPWMChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkPWMChannel(IntPtr digital_port_pointer);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "checkRelayChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkRelayChannel(IntPtr digital_port_pointer);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setPWM")]
        public static extern void setPWM(IntPtr digital_port_pointer, ushort value, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "allocatePWMChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool allocatePWMChannel(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "freePWMChannel")]
        public static extern void freePWMChannel(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPWM")]
        public static extern ushort getPWM(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "latchPWMZero")]
        public static extern void latchPWMZero(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setPWMPeriodScale")]
        public static extern void setPWMPeriodScale(IntPtr digital_port_pointer, uint squelchMask, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "allocatePWM")]
        public static extern IntPtr allocatePWM(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "freePWM")]
        public static extern void freePWM(IntPtr pwmGenerator, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setPWMRate")]
        public static extern void setPWMRate(double rate, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setPWMDutyCycle")]
        public static extern void setPWMDutyCycle(IntPtr pwmGenerator, double dutyCycle, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setPWMOutputChannel")]
        public static extern void setPWMOutputChannel(IntPtr pwmGenerator, uint pin, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setRelayForward")]
        public static extern void setRelayForward(IntPtr digital_port_pointer, [MarshalAs(UnmanagedType.I1)] bool on, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setRelayReverse")]
        public static extern void setRelayReverse(IntPtr digital_port_pointer, [MarshalAs(UnmanagedType.I1)] bool on, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getRelayForward")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getRelayForward(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getRelayReverse")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getRelayReverse(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "allocateDIO")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool allocateDIO(IntPtr digital_port_pointer, [MarshalAs(UnmanagedType.I1)] bool input, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "freeDIO")]
        public static extern void freeDIO(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setDIO")]
        public static extern void setDIO(IntPtr digital_port_pointer, short value, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getDIO")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getDIO(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getDIODirection")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getDIODirection(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "pulse")]
        public static extern void pulse(IntPtr digital_port_pointer, double pulseLength, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "isPulsing")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool isPulsing(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "isAnyPulsing")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool isAnyPulsing(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeCounter")]
        public static extern IntPtr initializeCounter(Mode mode, ref uint index, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "freeCounter")]
        public static extern void freeCounter(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterAverageSize")]
        public static extern void setCounterAverageSize(IntPtr counter_pointer, int size, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterUpSource")]
        public static extern void setCounterUpSource(IntPtr counter_pointer, uint pin, [MarshalAs(UnmanagedType.I1)] bool analogTrigger, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterUpSourceEdge")]
        public static extern void setCounterUpSourceEdge(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)] bool risingEdge, [MarshalAs(UnmanagedType.I1)] bool fallingEdge, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "clearCounterUpSource")]
        public static extern void clearCounterUpSource(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterDownSource")]
        public static extern void setCounterDownSource(IntPtr counter_pointer, uint pin, [MarshalAs(UnmanagedType.I1)] bool analogTrigger, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterDownSourceEdge")]
        public static extern void setCounterDownSourceEdge(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)] bool risingEdge, [MarshalAs(UnmanagedType.I1)] bool fallingEdge, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "clearCounterDownSource")]
        public static extern void clearCounterDownSource(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterUpDownMode")]
        public static extern void setCounterUpDownMode(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterExternalDirectionMode")]
        public static extern void setCounterExternalDirectionMode(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterSemiPeriodMode")]
        public static extern void setCounterSemiPeriodMode(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)] bool highSemiPeriod, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterPulseLengthMode")]
        public static extern void setCounterPulseLengthMode(IntPtr counter_pointer, double threshold, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCounterSamplesToAverage")]
        public static extern int getCounterSamplesToAverage(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterSamplesToAverage")]
        public static extern void setCounterSamplesToAverage(IntPtr counter_pointer, int samplesToAverage, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "resetCounter")]
        public static extern void resetCounter(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCounter")]
        public static extern int getCounter(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCounterPeriod")]
        public static extern double getCounterPeriod(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterMaxPeriod")]
        public static extern void setCounterMaxPeriod(IntPtr counter_pointer, double maxPeriod, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterUpdateWhenEmpty")]
        public static extern void setCounterUpdateWhenEmpty(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)] bool enabled, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCounterStopped")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getCounterStopped(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCounterDirection")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getCounterDirection(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterReverseDirection")]
        public static extern void setCounterReverseDirection(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)] bool reverseDirection, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeEncoder")]
        public static extern IntPtr initializeEncoder(byte port_a_module, uint port_a_pin, [MarshalAs(UnmanagedType.I1)] bool port_a_analog_trigger, byte port_b_module, uint port_b_pin, [MarshalAs(UnmanagedType.I1)] bool port_b_analog_trigger, [MarshalAs(UnmanagedType.I1)] bool reverseDirection, ref int index, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "resetEncoder")]
        public static extern void resetEncoder(IntPtr encoder_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "freeEncoder")]
        public static extern void freeEncoder(IntPtr encoder_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getEncoder")]
        public static extern int getEncoder(IntPtr encoder_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getEncoderPeriod")]
        public static extern double getEncoderPeriod(IntPtr encoder_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setEncoderMaxPeriod")]
        public static extern void setEncoderMaxPeriod(IntPtr encoder_pointer, double maxPeriod, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getEncoderStopped")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getEncoderStopped(IntPtr encoder_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getEncoderDirection")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getEncoderDirection(IntPtr encoder_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setEncoderReverseDirection")]
        public static extern void setEncoderReverseDirection(IntPtr encoder_pointer, [MarshalAs(UnmanagedType.I1)] bool reverseDirection, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setEncoderSamplesToAverage")]
        public static extern void setEncoderSamplesToAverage(IntPtr encoder_pointer, uint samplesToAverage, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getEncoderSamplesToAverage")]
        public static extern uint getEncoderSamplesToAverage(IntPtr encoder_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setEncoderIndexSource")]
        public static extern void setEncoderIndexSource(IntPtr encoder_pointer, uint pin, [MarshalAs(UnmanagedType.I1)] bool analogTrigger, [MarshalAs(UnmanagedType.I1)] bool activeHigh, [MarshalAs(UnmanagedType.I1)] bool edgeSensitive, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getLoopTiming")]
        public static extern ushort getLoopTiming(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiInitialize")]
        public static extern void spiInitialize(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiTransaction")]
        public static extern int spiTransaction(byte port, byte[] dataToSend, [Out] byte[] dataReceived, byte size);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiWrite")]
        public static extern int spiWrite(byte port, byte[] dataToSend, byte sendSize);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiRead")]
        public static extern int spiRead(byte port, [Out] byte[] buffer, byte count);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiClose")]
        public static extern void spiClose(byte port);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiSetSpeed")]
        public static extern void spiSetSpeed(byte port, uint speed);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiSetBitsPerWord")]
        public static extern void spiSetBitsPerWord(byte port, byte bpw);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiSetOpts")]
        public static extern void spiSetOpts(byte port, int msb_first, int sample_on_trailing, int clk_idle_high);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiSetChipSelectActiveHigh")]
        public static extern void spiSetChipSelectActiveHigh(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiSetChipSelectActiveLow")]
        public static extern void spiSetChipSelectActiveLow(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiGetHandle")]
        public static extern int spiGetHandle(byte port);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiSetHandle")]
        public static extern void spiSetHandle(byte port, int handle);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiGetSemaphore")]
        public static extern IntPtr spiGetSemaphore(byte port);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiSetSemaphore")]
        public static extern void spiSetSemaphore(byte port, IntPtr semaphore);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "i2CInitialize")]
        public static extern void i2CInitialize(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "i2CTransaction")]
        public static extern int i2CTransaction(byte port, byte deviceAddress, byte[] dataToSend, byte sendSize, [Out] byte[] dataReceived, byte receiveSize);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "i2CWrite")]
        public static extern int i2CWrite(byte port, byte deviceAddress, byte[] dataToSend, byte sendSize);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "i2CRead")]
        public static extern int i2CRead(byte port, byte deviceAddress, [Out] byte[] buffer, byte count);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "i2CClose")]
        public static extern void i2CClose(byte port);


    }
}
