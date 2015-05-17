using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System;

namespace HAL_Base
{
    public enum Mode
    {
        /// kTwoPulse -> 0
        TwoPulse = 0,

        /// kSemiperiod -> 1
        Semiperiod = 1,

        /// kPulseLength -> 2
        PulseLength = 2,

        /// kExternalDirection -> 3
        ExternalDirection = 3,
    }

    public class HALDigital
    {
        internal static void SetupDelegates()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HAL.HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;

            Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);

            InitializeDigitalPort = (InitializeDigitalPortDelegate)Delegate.CreateDelegate(typeof(InitializeDigitalPortDelegate), type.GetMethod("initializeDigitalPort"));

            CheckPWMChannel = (CheckPWMChannelDelegate)Delegate.CreateDelegate(typeof(CheckPWMChannelDelegate), type.GetMethod("checkPWMChannel"));

            CheckRelayChannel = (CheckRelayChannelDelegate)Delegate.CreateDelegate(typeof(CheckRelayChannelDelegate), type.GetMethod("checkRelayChannel"));

            SetPWM = (SetPWMDelegate)Delegate.CreateDelegate(typeof(SetPWMDelegate), type.GetMethod("setPWM"));

            AllocatePWMChannel = (AllocatePWMChannelDelegate)Delegate.CreateDelegate(typeof(AllocatePWMChannelDelegate), type.GetMethod("allocatePWMChannel"));

            FreePWMChannel = (FreePWMChannelDelegate)Delegate.CreateDelegate(typeof(FreePWMChannelDelegate), type.GetMethod("freePWMChannel"));

            GetPWM = (GetPWMDelegate)Delegate.CreateDelegate(typeof(GetPWMDelegate), type.GetMethod("getPWM"));

            LatchPWMZero = (LatchPWMZeroDelegate)Delegate.CreateDelegate(typeof(LatchPWMZeroDelegate), type.GetMethod("latchPWMZero"));

            SetPWMPeriodScale = (SetPWMPeriodScaleDelegate)Delegate.CreateDelegate(typeof(SetPWMPeriodScaleDelegate), type.GetMethod("setPWMPeriodScale"));

            AllocatePWM = (AllocatePWMDelegate)Delegate.CreateDelegate(typeof(AllocatePWMDelegate), type.GetMethod("allocatePWM"));

            FreePWM = (FreePWMDelegate)Delegate.CreateDelegate(typeof(FreePWMDelegate), type.GetMethod("freePWM"));

            SetPWMRate = (SetPWMRateDelegate)Delegate.CreateDelegate(typeof(SetPWMRateDelegate), type.GetMethod("setPWMRate"));

            SetPWMDutyCycle = (SetPWMDutyCycleDelegate)Delegate.CreateDelegate(typeof(SetPWMDutyCycleDelegate), type.GetMethod("setPWMDutyCycle"));

            SetPWMOutputChannel = (SetPWMOutputChannelDelegate)Delegate.CreateDelegate(typeof(SetPWMOutputChannelDelegate), type.GetMethod("setPWMOutputChannel"));

            SetRelayForward = (SetRelayForwardDelegate)Delegate.CreateDelegate(typeof(SetRelayForwardDelegate), type.GetMethod("setRelayForward"));

            SetRelayReverse = (SetRelayReverseDelegate)Delegate.CreateDelegate(typeof(SetRelayReverseDelegate), type.GetMethod("setRelayReverse"));

            GetRelayForward = (GetRelayForwardDelegate)Delegate.CreateDelegate(typeof(GetRelayForwardDelegate), type.GetMethod("getRelayForward"));

            GetRelayReverse = (GetRelayReverseDelegate)Delegate.CreateDelegate(typeof(GetRelayReverseDelegate), type.GetMethod("getRelayReverse"));

            AllocateDIO = (AllocateDIODelegate)Delegate.CreateDelegate(typeof(AllocateDIODelegate), type.GetMethod("allocateDIO"));

            FreeDIO = (FreeDIODelegate)Delegate.CreateDelegate(typeof(FreeDIODelegate), type.GetMethod("freeDIO"));

            SetDIO = (SetDIODelegate)Delegate.CreateDelegate(typeof(SetDIODelegate), type.GetMethod("setDIO"));

            GetDIO = (GetDIODelegate)Delegate.CreateDelegate(typeof(GetDIODelegate), type.GetMethod("getDIO"));

            GetDIODirection = (GetDIODirectionDelegate)Delegate.CreateDelegate(typeof(GetDIODirectionDelegate), type.GetMethod("getDIODirection"));

            Pulse = (PulseDelegate)Delegate.CreateDelegate(typeof(PulseDelegate), type.GetMethod("pulse"));

            IsPulsing = (IsPulsingDelegate)Delegate.CreateDelegate(typeof(IsPulsingDelegate), type.GetMethod("isPulsing"));

            IsAnyPulsing = (IsAnyPulsingDelegate)Delegate.CreateDelegate(typeof(IsAnyPulsingDelegate), type.GetMethod("isAnyPulsing"));

            InitializeCounter = (InitializeCounterDelegate)Delegate.CreateDelegate(typeof(InitializeCounterDelegate), type.GetMethod("initializeCounter"));

            FreeCounter = (FreeCounterDelegate)Delegate.CreateDelegate(typeof(FreeCounterDelegate), type.GetMethod("freeCounter"));

            SetCounterAverageSize = (SetCounterAverageSizeDelegate)Delegate.CreateDelegate(typeof(SetCounterAverageSizeDelegate), type.GetMethod("setCounterAverageSize"));

            SetCounterUpSource = (SetCounterUpSourceDelegate)Delegate.CreateDelegate(typeof(SetCounterUpSourceDelegate), type.GetMethod("setCounterUpSource"));

            SetCounterUpSourceEdge = (SetCounterUpSourceEdgeDelegate)Delegate.CreateDelegate(typeof(SetCounterUpSourceEdgeDelegate), type.GetMethod("setCounterUpSourceEdge"));

            ClearCounterUpSource = (ClearCounterUpSourceDelegate)Delegate.CreateDelegate(typeof(ClearCounterUpSourceDelegate), type.GetMethod("clearCounterUpSource"));

            SetCounterDownSource = (SetCounterDownSourceDelegate)Delegate.CreateDelegate(typeof(SetCounterDownSourceDelegate), type.GetMethod("setCounterDownSource"));

            SetCounterDownSourceEdge = (SetCounterDownSourceEdgeDelegate)Delegate.CreateDelegate(typeof(SetCounterDownSourceEdgeDelegate), type.GetMethod("setCounterDownSourceEdge"));

            ClearCounterDownSource = (ClearCounterDownSourceDelegate)Delegate.CreateDelegate(typeof(ClearCounterDownSourceDelegate), type.GetMethod("clearCounterDownSource"));

            SetCounterUpDownMode = (SetCounterUpDownModeDelegate)Delegate.CreateDelegate(typeof(SetCounterUpDownModeDelegate), type.GetMethod("setCounterUpDownMode"));

            SetCounterExternalDirectionMode = (SetCounterExternalDirectionModeDelegate)Delegate.CreateDelegate(typeof(SetCounterExternalDirectionModeDelegate), type.GetMethod("setCounterExternalDirectionMode"));

            SetCounterSemiPeriodMode = (SetCounterSemiPeriodModeDelegate)Delegate.CreateDelegate(typeof(SetCounterSemiPeriodModeDelegate), type.GetMethod("setCounterSemiPeriodMode"));

            SetCounterPulseLengthMode = (SetCounterPulseLengthModeDelegate)Delegate.CreateDelegate(typeof(SetCounterPulseLengthModeDelegate), type.GetMethod("setCounterPulseLengthMode"));

            GetCounterSamplesToAverage = (GetCounterSamplesToAverageDelegate)Delegate.CreateDelegate(typeof(GetCounterSamplesToAverageDelegate), type.GetMethod("getCounterSamplesToAverage"));

            SetCounterSamplesToAverage = (SetCounterSamplesToAverageDelegate)Delegate.CreateDelegate(typeof(SetCounterSamplesToAverageDelegate), type.GetMethod("setCounterSamplesToAverage"));

            ResetCounter = (ResetCounterDelegate)Delegate.CreateDelegate(typeof(ResetCounterDelegate), type.GetMethod("resetCounter"));

            GetCounter = (GetCounterDelegate)Delegate.CreateDelegate(typeof(GetCounterDelegate), type.GetMethod("getCounter"));

            GetCounterPeriod = (GetCounterPeriodDelegate)Delegate.CreateDelegate(typeof(GetCounterPeriodDelegate), type.GetMethod("getCounterPeriod"));

            SetCounterMaxPeriod = (SetCounterMaxPeriodDelegate)Delegate.CreateDelegate(typeof(SetCounterMaxPeriodDelegate), type.GetMethod("setCounterMaxPeriod"));

            SetCounterUpdateWhenEmpty = (SetCounterUpdateWhenEmptyDelegate)Delegate.CreateDelegate(typeof(SetCounterUpdateWhenEmptyDelegate), type.GetMethod("setCounterUpdateWhenEmpty"));

            GetCounterStopped = (GetCounterStoppedDelegate)Delegate.CreateDelegate(typeof(GetCounterStoppedDelegate), type.GetMethod("getCounterStopped"));

            GetCounterDirection = (GetCounterDirectionDelegate)Delegate.CreateDelegate(typeof(GetCounterDirectionDelegate), type.GetMethod("getCounterDirection"));

            SetCounterReverseDirection = (SetCounterReverseDirectionDelegate)Delegate.CreateDelegate(typeof(SetCounterReverseDirectionDelegate), type.GetMethod("setCounterReverseDirection"));

            InitializeEncoder = (InitializeEncoderDelegate)Delegate.CreateDelegate(typeof(InitializeEncoderDelegate), type.GetMethod("initializeEncoder"));

            FreeEncoder = (FreeEncoderDelegate)Delegate.CreateDelegate(typeof(FreeEncoderDelegate), type.GetMethod("freeEncoder"));

            ResetEncoder = (ResetEncoderDelegate)Delegate.CreateDelegate(typeof(ResetEncoderDelegate), type.GetMethod("resetEncoder"));

            GetEncoder = (GetEncoderDelegate)Delegate.CreateDelegate(typeof(GetEncoderDelegate), type.GetMethod("getEncoder"));

            GetEncoderPeriod = (GetEncoderPeriodDelegate)Delegate.CreateDelegate(typeof(GetEncoderPeriodDelegate), type.GetMethod("getEncoderPeriod"));

            SetEncoderMaxPeriod = (SetEncoderMaxPeriodDelegate)Delegate.CreateDelegate(typeof(SetEncoderMaxPeriodDelegate), type.GetMethod("setEncoderMaxPeriod"));

            GetEncoderStopped = (GetEncoderStoppedDelegate)Delegate.CreateDelegate(typeof(GetEncoderStoppedDelegate), type.GetMethod("getEncoderStopped"));

            GetEncoderDirection = (GetEncoderDirectionDelegate)Delegate.CreateDelegate(typeof(GetEncoderDirectionDelegate), type.GetMethod("getEncoderDirection"));

            SetEncoderReverseDirection = (SetEncoderReverseDirectionDelegate)Delegate.CreateDelegate(typeof(SetEncoderReverseDirectionDelegate), type.GetMethod("setEncoderReverseDirection"));

            SetEncoderSamplesToAverage = (SetEncoderSamplesToAverageDelegate)Delegate.CreateDelegate(typeof(SetEncoderSamplesToAverageDelegate), type.GetMethod("setEncoderSamplesToAverage"));

            GetEncoderSamplesToAverage = (GetEncoderSamplesToAverageDelegate)Delegate.CreateDelegate(typeof(GetEncoderSamplesToAverageDelegate), type.GetMethod("getEncoderSamplesToAverage"));

            SetEncoderIndexSource = (SetEncoderIndexSourceDelegate)Delegate.CreateDelegate(typeof(SetEncoderIndexSourceDelegate), type.GetMethod("setEncoderIndexSource"));

            GetLoopTiming = (GetLoopTimingDelegate)Delegate.CreateDelegate(typeof(GetLoopTimingDelegate), type.GetMethod("getLoopTiming"));

            SpiInitialize = (SpiInitializeDelegate)Delegate.CreateDelegate(typeof(SpiInitializeDelegate), type.GetMethod("spiInitialize"));

            SpiTransaction = (SpiTransactionDelegate)Delegate.CreateDelegate(typeof(SpiTransactionDelegate), type.GetMethod("spiTransaction"));

            SpiWrite = (SpiWriteDelegate)Delegate.CreateDelegate(typeof(SpiWriteDelegate), type.GetMethod("spiWrite"));

            SpiRead = (SpiReadDelegate)Delegate.CreateDelegate(typeof(SpiReadDelegate), type.GetMethod("spiRead"));

            SpiClose = (SpiCloseDelegate)Delegate.CreateDelegate(typeof(SpiCloseDelegate), type.GetMethod("spiClose"));

            SpiSetSpeed = (SpiSetSpeedDelegate)Delegate.CreateDelegate(typeof(SpiSetSpeedDelegate), type.GetMethod("spiSetSpeed"));

            SpiSetBitsPerWord = (SpiSetBitsPerWordDelegate)Delegate.CreateDelegate(typeof(SpiSetBitsPerWordDelegate), type.GetMethod("spiSetBitsPerWord"));

            SpiSetOpts = (SpiSetOptsDelegate)Delegate.CreateDelegate(typeof(SpiSetOptsDelegate), type.GetMethod("spiSetOpts"));

            SpiSetChipSelectActiveHigh = (SpiSetChipSelectActiveHighDelegate)Delegate.CreateDelegate(typeof(SpiSetChipSelectActiveHighDelegate), type.GetMethod("spiSetChipSelectActiveHigh"));

            SpiSetChipSelectActiveLow = (SpiSetChipSelectActiveLowDelegate)Delegate.CreateDelegate(typeof(SpiSetChipSelectActiveLowDelegate), type.GetMethod("spiSetChipSelectActiveLow"));

            SpiGetHandle = (SpiGetHandleDelegate)Delegate.CreateDelegate(typeof(SpiGetHandleDelegate), type.GetMethod("spiGetHandle"));

            SpiSetHandle = (SpiSetHandleDelegate)Delegate.CreateDelegate(typeof(SpiSetHandleDelegate), type.GetMethod("spiSetHandle"));

            SpiGetSemaphore = (SpiGetSemaphoreDelegate)Delegate.CreateDelegate(typeof(SpiGetSemaphoreDelegate), type.GetMethod("spiGetSemaphore"));

            SpiSetSemaphore = (SpiSetSemaphoreDelegate)Delegate.CreateDelegate(typeof(SpiSetSemaphoreDelegate), type.GetMethod("spiSetSemaphore"));

            I2CInitialize = (I2CInitializeDelegate)Delegate.CreateDelegate(typeof(I2CInitializeDelegate), type.GetMethod("i2CInitialize"));

            I2CTransaction = (I2CTransactionDelegate)Delegate.CreateDelegate(typeof(I2CTransactionDelegate), type.GetMethod("i2CTransaction"));

            I2CWrite = (I2CWriteDelegate)Delegate.CreateDelegate(typeof(I2CWriteDelegate), type.GetMethod("i2CWrite"));

            I2CRead = (I2CReadDelegate)Delegate.CreateDelegate(typeof(I2CReadDelegate), type.GetMethod("i2CRead"));

            I2CClose = (I2CCloseDelegate)Delegate.CreateDelegate(typeof(I2CCloseDelegate), type.GetMethod("i2CClose"));

            SetPWMRateIntHack = (SetPWMRateIntHackDelegate)Delegate.CreateDelegate(typeof(SetPWMRateIntHackDelegate), type.GetMethod("setPWMRateIntHack"));

            SetPWMDutyCycleIntHack = (SetPWMDutyCycleIntHackDelegate)Delegate.CreateDelegate(typeof(SetPWMDutyCycleIntHackDelegate), type.GetMethod("setPWMDutyCycleIntHack"));
        }

        public delegate System.IntPtr InitializeDigitalPortDelegate(System.IntPtr portPointer, ref int status);
        public static InitializeDigitalPortDelegate InitializeDigitalPort;

        public delegate bool CheckPWMChannelDelegate(System.IntPtr digitalPortPointer);
        public static CheckPWMChannelDelegate CheckPWMChannel;

        public delegate bool CheckRelayChannelDelegate(System.IntPtr digitalPortPointer);
        public static CheckRelayChannelDelegate CheckRelayChannel;

        public delegate void SetPWMDelegate(System.IntPtr digitalPortPointer, ushort value, ref int status);
        public static SetPWMDelegate SetPWM;

        public delegate bool AllocatePWMChannelDelegate(System.IntPtr digitalPortPointer, ref int status);
        public static AllocatePWMChannelDelegate AllocatePWMChannel;

        public delegate void FreePWMChannelDelegate(System.IntPtr digitalPortPointer, ref int status);
        public static FreePWMChannelDelegate FreePWMChannel;

        public delegate ushort GetPWMDelegate(System.IntPtr digitalPortPointer, ref int status);
        public static GetPWMDelegate GetPWM;

        public delegate void LatchPWMZeroDelegate(System.IntPtr digitalPortPointer, ref int status);
        public static LatchPWMZeroDelegate LatchPWMZero;

        public delegate void SetPWMPeriodScaleDelegate(System.IntPtr digitalPortPointer, uint squelchMask, ref int status);
        public static SetPWMPeriodScaleDelegate SetPWMPeriodScale;

        public delegate System.IntPtr AllocatePWMDelegate(ref int status);
        public static AllocatePWMDelegate AllocatePWM;

        public delegate void FreePWMDelegate(System.IntPtr pwmGenerator, ref int status);
        public static FreePWMDelegate FreePWM;

        public delegate void SetPWMRateDelegate(double rate, ref int status);
        public static SetPWMRateDelegate SetPWMRate;

        public delegate void SetPWMDutyCycleDelegate(System.IntPtr pwmGenerator, double dutyCycle, ref int status);
        public static SetPWMDutyCycleDelegate SetPWMDutyCycle;

        public delegate void SetPWMOutputChannelDelegate(System.IntPtr pwmGenerator, uint pin, ref int status);
        public static SetPWMOutputChannelDelegate SetPWMOutputChannel;

        public delegate void SetRelayForwardDelegate(System.IntPtr digitalPortPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool on, ref int status);
        public static SetRelayForwardDelegate SetRelayForward;

        public delegate void SetRelayReverseDelegate(System.IntPtr digitalPortPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool on, ref int status);
        public static SetRelayReverseDelegate SetRelayReverse;

        public delegate bool GetRelayForwardDelegate(System.IntPtr digitalPortPointer, ref int status);
        public static GetRelayForwardDelegate GetRelayForward;

        public delegate bool GetRelayReverseDelegate(System.IntPtr digitalPortPointer, ref int status);
        public static GetRelayReverseDelegate GetRelayReverse;

        public delegate bool AllocateDIODelegate(System.IntPtr digitalPortPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool input, ref int status);
        public static AllocateDIODelegate AllocateDIO;

        public delegate void FreeDIODelegate(System.IntPtr digitalPortPointer, ref int status);
        public static FreeDIODelegate FreeDIO;

        public delegate void SetDIODelegate(System.IntPtr digitalPortPointer, short value, ref int status);
        public static SetDIODelegate SetDIO;

        public delegate bool GetDIODelegate(System.IntPtr digitalPortPointer, ref int status);
        public static GetDIODelegate GetDIO;

        public delegate bool GetDIODirectionDelegate(System.IntPtr digitalPortPointer, ref int status);
        public static GetDIODirectionDelegate GetDIODirection;

        public delegate void PulseDelegate(System.IntPtr digitalPortPointer, double pulseLength, ref int status);
        public static PulseDelegate Pulse;

        public delegate bool IsPulsingDelegate(System.IntPtr digitalPortPointer, ref int status);
        public static IsPulsingDelegate IsPulsing;

        public delegate bool IsAnyPulsingDelegate(ref int status);
        public static IsAnyPulsingDelegate IsAnyPulsing;

        public delegate System.IntPtr InitializeCounterDelegate(Mode mode, ref uint index, ref int status);
        public static InitializeCounterDelegate InitializeCounter;

        public delegate void FreeCounterDelegate(System.IntPtr counterPointer, ref int status);
        public static FreeCounterDelegate FreeCounter;

        public delegate void SetCounterAverageSizeDelegate(System.IntPtr counterPointer, int size, ref int status);
        public static SetCounterAverageSizeDelegate SetCounterAverageSize;

        public delegate void SetCounterUpSourceDelegate(System.IntPtr counterPointer, uint pin, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool analogTrigger, ref int status);
        public static SetCounterUpSourceDelegate SetCounterUpSource;

        public delegate void SetCounterUpSourceEdgeDelegate(System.IntPtr counterPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool risingEdge, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool fallingEdge, ref int status);
        public static SetCounterUpSourceEdgeDelegate SetCounterUpSourceEdge;

        public delegate void ClearCounterUpSourceDelegate(System.IntPtr counterPointer, ref int status);
        public static ClearCounterUpSourceDelegate ClearCounterUpSource;

        public delegate void SetCounterDownSourceDelegate(System.IntPtr counterPointer, uint pin, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool analogTrigger, ref int status);
        public static SetCounterDownSourceDelegate SetCounterDownSource;

        public delegate void SetCounterDownSourceEdgeDelegate(System.IntPtr counterPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool risingEdge, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool fallingEdge, ref int status);
        public static SetCounterDownSourceEdgeDelegate SetCounterDownSourceEdge;

        public delegate void ClearCounterDownSourceDelegate(System.IntPtr counterPointer, ref int status);
        public static ClearCounterDownSourceDelegate ClearCounterDownSource;

        public delegate void SetCounterUpDownModeDelegate(System.IntPtr counterPointer, ref int status);
        public static SetCounterUpDownModeDelegate SetCounterUpDownMode;

        public delegate void SetCounterExternalDirectionModeDelegate(System.IntPtr counterPointer, ref int status);
        public static SetCounterExternalDirectionModeDelegate SetCounterExternalDirectionMode;

        public delegate void SetCounterSemiPeriodModeDelegate(System.IntPtr counterPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool highSemiPeriod, ref int status);
        public static SetCounterSemiPeriodModeDelegate SetCounterSemiPeriodMode;

        public delegate void SetCounterPulseLengthModeDelegate(System.IntPtr counterPointer, double threshold, ref int status);
        public static SetCounterPulseLengthModeDelegate SetCounterPulseLengthMode;

        public delegate int GetCounterSamplesToAverageDelegate(System.IntPtr counterPointer, ref int status);
        public static GetCounterSamplesToAverageDelegate GetCounterSamplesToAverage;

        public delegate void SetCounterSamplesToAverageDelegate(System.IntPtr counterPointer, int samplesToAverage, ref int status);
        public static SetCounterSamplesToAverageDelegate SetCounterSamplesToAverage;

        public delegate void ResetCounterDelegate(System.IntPtr counterPointer, ref int status);
        public static ResetCounterDelegate ResetCounter;

        public delegate int GetCounterDelegate(System.IntPtr counterPointer, ref int status);
        public static GetCounterDelegate GetCounter;

        public delegate double GetCounterPeriodDelegate(System.IntPtr counterPointer, ref int status);
        public static GetCounterPeriodDelegate GetCounterPeriod;

        public delegate void SetCounterMaxPeriodDelegate(System.IntPtr counterPointer, double maxPeriod, ref int status);
        public static SetCounterMaxPeriodDelegate SetCounterMaxPeriod;

        public delegate void SetCounterUpdateWhenEmptyDelegate(System.IntPtr counterPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool enabled, ref int status);
        public static SetCounterUpdateWhenEmptyDelegate SetCounterUpdateWhenEmpty;

        public delegate bool GetCounterStoppedDelegate(System.IntPtr counterPointer, ref int status);
        public static GetCounterStoppedDelegate GetCounterStopped;

        public delegate bool GetCounterDirectionDelegate(System.IntPtr counterPointer, ref int status);
        public static GetCounterDirectionDelegate GetCounterDirection;

        public delegate void SetCounterReverseDirectionDelegate(System.IntPtr counterPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool reverseDirection, ref int status);
        public static SetCounterReverseDirectionDelegate SetCounterReverseDirection;

        public delegate System.IntPtr InitializeEncoderDelegate(byte portAModule, uint portAPin, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool portAAnalogTrigger, byte portBModule, uint portBPin, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool portBAnalogTrigger, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool reverseDirection, ref int index, ref int status);
        public static InitializeEncoderDelegate InitializeEncoder;

        public delegate void FreeEncoderDelegate(System.IntPtr encoderPointer, ref int status);
        public static FreeEncoderDelegate FreeEncoder;

        public delegate void ResetEncoderDelegate(System.IntPtr encoderPointer, ref int status);
        public static ResetEncoderDelegate ResetEncoder;

        public delegate int GetEncoderDelegate(System.IntPtr encoderPointer, ref int status);
        public static GetEncoderDelegate GetEncoder;

        public delegate double GetEncoderPeriodDelegate(System.IntPtr encoderPointer, ref int status);
        public static GetEncoderPeriodDelegate GetEncoderPeriod;

        public delegate void SetEncoderMaxPeriodDelegate(System.IntPtr encoderPointer, double maxPeriod, ref int status);
        public static SetEncoderMaxPeriodDelegate SetEncoderMaxPeriod;

        public delegate bool GetEncoderStoppedDelegate(System.IntPtr encoderPointer, ref int status);
        public static GetEncoderStoppedDelegate GetEncoderStopped;

        public delegate bool GetEncoderDirectionDelegate(System.IntPtr encoderPointer, ref int status);
        public static GetEncoderDirectionDelegate GetEncoderDirection;

        public delegate void SetEncoderReverseDirectionDelegate(System.IntPtr encoderPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool reverseDirection, ref int status);
        public static SetEncoderReverseDirectionDelegate SetEncoderReverseDirection;

        public delegate void SetEncoderSamplesToAverageDelegate(System.IntPtr encoderPointer, uint samplesToAverage, ref int status);
        public static SetEncoderSamplesToAverageDelegate SetEncoderSamplesToAverage;

        public delegate uint GetEncoderSamplesToAverageDelegate(System.IntPtr encoderPointer, ref int status);
        public static GetEncoderSamplesToAverageDelegate GetEncoderSamplesToAverage;

        public delegate void SetEncoderIndexSourceDelegate(System.IntPtr encoderPointer, uint pin, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool analogTrigger, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool activeHigh, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool edgeSensitive, ref int status);
        public static SetEncoderIndexSourceDelegate SetEncoderIndexSource;

        public delegate ushort GetLoopTimingDelegate(ref int status);
        public static GetLoopTimingDelegate GetLoopTiming;

        public delegate void SpiInitializeDelegate(byte port, ref int status);
        public static SpiInitializeDelegate SpiInitialize;

        public delegate int SpiTransactionDelegate(byte port, ref byte dataToSend, ref byte dataReceived, byte size);
        public static SpiTransactionDelegate SpiTransaction;

        public delegate int SpiWriteDelegate(byte port, ref byte dataToSend, byte sendSize);
        public static SpiWriteDelegate SpiWrite;

        public delegate int SpiReadDelegate(byte port, ref byte buffer, byte count);
        public static SpiReadDelegate SpiRead;

        public delegate void SpiCloseDelegate(byte port);
        public static SpiCloseDelegate SpiClose;

        public delegate void SpiSetSpeedDelegate(byte port, uint speed);
        public static SpiSetSpeedDelegate SpiSetSpeed;

        public delegate void SpiSetBitsPerWordDelegate(byte port, byte bpw);
        public static SpiSetBitsPerWordDelegate SpiSetBitsPerWord;

        public delegate void SpiSetOptsDelegate(byte port, int msbFirst, int sampleOnTrailing, int clkIdleHigh);
        public static SpiSetOptsDelegate SpiSetOpts;

        public delegate void SpiSetChipSelectActiveHighDelegate(byte port, ref int status);
        public static SpiSetChipSelectActiveHighDelegate SpiSetChipSelectActiveHigh;

        public delegate void SpiSetChipSelectActiveLowDelegate(byte port, ref int status);
        public static SpiSetChipSelectActiveLowDelegate SpiSetChipSelectActiveLow;

        public delegate int SpiGetHandleDelegate(byte port);
        public static SpiGetHandleDelegate SpiGetHandle;

        public delegate void SpiSetHandleDelegate(byte port, int handle);
        public static SpiSetHandleDelegate SpiSetHandle;

        public delegate System.IntPtr SpiGetSemaphoreDelegate(byte port);
        public static SpiGetSemaphoreDelegate SpiGetSemaphore;

        public delegate void SpiSetSemaphoreDelegate(byte port, System.IntPtr semaphore);
        public static SpiSetSemaphoreDelegate SpiSetSemaphore;

        public delegate void I2CInitializeDelegate(byte port, ref int status);
        public static I2CInitializeDelegate I2CInitialize;

        public delegate int I2CTransactionDelegate(byte port, byte deviceAddress, ref byte dataToSend, byte sendSize, ref byte dataReceived, byte receiveSize);
        public static I2CTransactionDelegate I2CTransaction;

        public delegate int I2CWriteDelegate(byte port, byte deviceAddress, ref byte dataToSend, byte sendSize);
        public static I2CWriteDelegate I2CWrite;

        public delegate int I2CReadDelegate(byte port, byte deviceAddress, ref byte buffer, byte count);
        public static I2CReadDelegate I2CRead;

        public delegate void I2CCloseDelegate(byte port);
        public static I2CCloseDelegate I2CClose;

        public delegate void SetPWMRateIntHackDelegate(int rate, ref int status);
        public static SetPWMRateIntHackDelegate SetPWMRateIntHack;

        public delegate void SetPWMDutyCycleIntHackDelegate(System.IntPtr pwmGenerator, int dutyCycle, ref int status);
        public static SetPWMDutyCycleIntHackDelegate SetPWMDutyCycleIntHack;
    }
}
