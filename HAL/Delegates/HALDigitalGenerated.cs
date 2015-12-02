//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using HAL;

// ReSharper disable CheckNamespace

namespace HAL
{
    public partial class HALDigital
    {
        static HALDigital()
        {
            global::HAL.HAL.Initialize();
        }

        public delegate IntPtr InitializeDigitalPortDelegate(IntPtr port_pointer, ref int status);
        public static InitializeDigitalPortDelegate InitializeDigitalPort;

        public delegate void FreeDigitalPortDelegate(IntPtr digital_port_pointer);
        public static FreeDigitalPortDelegate FreeDigitalPort;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool CheckPWMChannelDelegate(IntPtr digital_port_pointer);
        public static CheckPWMChannelDelegate CheckPWMChannel;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool CheckRelayChannelDelegate(IntPtr digital_port_pointer);
        public static CheckRelayChannelDelegate CheckRelayChannel;

        public delegate void SetPWMDelegate(IntPtr digital_port_pointer, ushort value, ref int status);
        public static SetPWMDelegate SetPWM;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool AllocatePWMChannelDelegate(IntPtr digital_port_pointer, ref int status);
        public static AllocatePWMChannelDelegate AllocatePWMChannel;

        public delegate void FreePWMChannelDelegate(IntPtr digital_port_pointer, ref int status);
        public static FreePWMChannelDelegate FreePWMChannel;

        public delegate ushort GetPWMDelegate(IntPtr digital_port_pointer, ref int status);
        public static GetPWMDelegate GetPWM;

        public delegate void LatchPWMZeroDelegate(IntPtr digital_port_pointer, ref int status);
        public static LatchPWMZeroDelegate LatchPWMZero;

        public delegate void SetPWMPeriodScaleDelegate(IntPtr digital_port_pointer, uint squelchMask, ref int status);
        public static SetPWMPeriodScaleDelegate SetPWMPeriodScale;

        public delegate IntPtr AllocatePWMDelegate(ref int status);
        public static AllocatePWMDelegate AllocatePWM;

        public delegate void FreePWMDelegate(IntPtr pwmGenerator, ref int status);
        public static FreePWMDelegate FreePWM;

        public delegate void SetPWMRateDelegate(double rate, ref int status);
        public static SetPWMRateDelegate SetPWMRate;

        public delegate void SetPWMDutyCycleDelegate(IntPtr pwmGenerator, double dutyCycle, ref int status);
        public static SetPWMDutyCycleDelegate SetPWMDutyCycle;

        public delegate void SetPWMOutputChannelDelegate(IntPtr pwmGenerator, uint pin, ref int status);
        public static SetPWMOutputChannelDelegate SetPWMOutputChannel;

        public delegate void SetRelayForwardDelegate(IntPtr digital_port_pointer, [MarshalAs(UnmanagedType.I1)]bool on, ref int status);
        public static SetRelayForwardDelegate SetRelayForward;

        public delegate void SetRelayReverseDelegate(IntPtr digital_port_pointer, [MarshalAs(UnmanagedType.I1)]bool on, ref int status);
        public static SetRelayReverseDelegate SetRelayReverse;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetRelayForwardDelegate(IntPtr digital_port_pointer, ref int status);
        public static GetRelayForwardDelegate GetRelayForward;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetRelayReverseDelegate(IntPtr digital_port_pointer, ref int status);
        public static GetRelayReverseDelegate GetRelayReverse;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool AllocateDIODelegate(IntPtr digital_port_pointer, [MarshalAs(UnmanagedType.I1)]bool input, ref int status);
        public static AllocateDIODelegate AllocateDIO;

        public delegate void FreeDIODelegate(IntPtr digital_port_pointer, ref int status);
        public static FreeDIODelegate FreeDIO;

        public delegate void SetDIODelegate(IntPtr digital_port_pointer, short value, ref int status);
        public static SetDIODelegate SetDIO;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetDIODelegate(IntPtr digital_port_pointer, ref int status);
        public static GetDIODelegate GetDIO;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetDIODirectionDelegate(IntPtr digital_port_pointer, ref int status);
        public static GetDIODirectionDelegate GetDIODirection;

        public delegate void PulseDelegate(IntPtr digital_port_pointer, double pulseLength, ref int status);
        public static PulseDelegate Pulse;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool IsPulsingDelegate(IntPtr digital_port_pointer, ref int status);
        public static IsPulsingDelegate IsPulsing;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool IsAnyPulsingDelegate(ref int status);
        public static IsAnyPulsingDelegate IsAnyPulsing;

        public delegate void SetFilterSelectDelegate(IntPtr digital_port_pointer, int filter_index, ref int status);
        public static SetFilterSelectDelegate SetFilterSelect;

        public delegate int GetFilterSelectDelegate(IntPtr digital_port_pointer, ref int status);
        public static GetFilterSelectDelegate GetFilterSelect;

        public delegate void SetFilterPeriodDelegate(int filter_index, uint value, ref int status);
        public static SetFilterPeriodDelegate SetFilterPeriod;

        public delegate uint GetFilterPeriodDelegate(int filter_index, ref int status);
        public static GetFilterPeriodDelegate GetFilterPeriod;

        public delegate IntPtr InitializeCounterDelegate(Mode mode, ref uint index, ref int status);
        public static InitializeCounterDelegate InitializeCounter;

        public delegate void FreeCounterDelegate(IntPtr counter_pointer, ref int status);
        public static FreeCounterDelegate FreeCounter;

        public delegate void SetCounterAverageSizeDelegate(IntPtr counter_pointer, int size, ref int status);
        public static SetCounterAverageSizeDelegate SetCounterAverageSize;

        public delegate void SetCounterUpSourceDelegate(IntPtr counter_pointer, uint pin, [MarshalAs(UnmanagedType.I1)]bool analogTrigger, ref int status);
        public static SetCounterUpSourceDelegate SetCounterUpSource;

        public delegate void SetCounterUpSourceEdgeDelegate(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)]bool risingEdge, [MarshalAs(UnmanagedType.I1)]bool fallingEdge, ref int status);
        public static SetCounterUpSourceEdgeDelegate SetCounterUpSourceEdge;

        public delegate void ClearCounterUpSourceDelegate(IntPtr counter_pointer, ref int status);
        public static ClearCounterUpSourceDelegate ClearCounterUpSource;

        public delegate void SetCounterDownSourceDelegate(IntPtr counter_pointer, uint pin, [MarshalAs(UnmanagedType.I1)]bool analogTrigger, ref int status);
        public static SetCounterDownSourceDelegate SetCounterDownSource;

        public delegate void SetCounterDownSourceEdgeDelegate(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)]bool risingEdge, [MarshalAs(UnmanagedType.I1)]bool fallingEdge, ref int status);
        public static SetCounterDownSourceEdgeDelegate SetCounterDownSourceEdge;

        public delegate void ClearCounterDownSourceDelegate(IntPtr counter_pointer, ref int status);
        public static ClearCounterDownSourceDelegate ClearCounterDownSource;

        public delegate void SetCounterUpDownModeDelegate(IntPtr counter_pointer, ref int status);
        public static SetCounterUpDownModeDelegate SetCounterUpDownMode;

        public delegate void SetCounterExternalDirectionModeDelegate(IntPtr counter_pointer, ref int status);
        public static SetCounterExternalDirectionModeDelegate SetCounterExternalDirectionMode;

        public delegate void SetCounterSemiPeriodModeDelegate(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)]bool highSemiPeriod, ref int status);
        public static SetCounterSemiPeriodModeDelegate SetCounterSemiPeriodMode;

        public delegate void SetCounterPulseLengthModeDelegate(IntPtr counter_pointer, double threshold, ref int status);
        public static SetCounterPulseLengthModeDelegate SetCounterPulseLengthMode;

        public delegate int GetCounterSamplesToAverageDelegate(IntPtr counter_pointer, ref int status);
        public static GetCounterSamplesToAverageDelegate GetCounterSamplesToAverage;

        public delegate void SetCounterSamplesToAverageDelegate(IntPtr counter_pointer, int samplesToAverage, ref int status);
        public static SetCounterSamplesToAverageDelegate SetCounterSamplesToAverage;

        public delegate void ResetCounterDelegate(IntPtr counter_pointer, ref int status);
        public static ResetCounterDelegate ResetCounter;

        public delegate int GetCounterDelegate(IntPtr counter_pointer, ref int status);
        public static GetCounterDelegate GetCounter;

        public delegate double GetCounterPeriodDelegate(IntPtr counter_pointer, ref int status);
        public static GetCounterPeriodDelegate GetCounterPeriod;

        public delegate void SetCounterMaxPeriodDelegate(IntPtr counter_pointer, double maxPeriod, ref int status);
        public static SetCounterMaxPeriodDelegate SetCounterMaxPeriod;

        public delegate void SetCounterUpdateWhenEmptyDelegate(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)]bool enabled, ref int status);
        public static SetCounterUpdateWhenEmptyDelegate SetCounterUpdateWhenEmpty;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetCounterStoppedDelegate(IntPtr counter_pointer, ref int status);
        public static GetCounterStoppedDelegate GetCounterStopped;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetCounterDirectionDelegate(IntPtr counter_pointer, ref int status);
        public static GetCounterDirectionDelegate GetCounterDirection;

        public delegate void SetCounterReverseDirectionDelegate(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)]bool reverseDirection, ref int status);
        public static SetCounterReverseDirectionDelegate SetCounterReverseDirection;

        public delegate IntPtr InitializeEncoderDelegate(byte port_a_module, uint port_a_pin, [MarshalAs(UnmanagedType.I1)]bool port_a_analog_trigger, byte port_b_module, uint port_b_pin, [MarshalAs(UnmanagedType.I1)]bool port_b_analog_trigger, [MarshalAs(UnmanagedType.I1)]bool reverseDirection, ref int index, ref int status);
        public static InitializeEncoderDelegate InitializeEncoder;

        public delegate void ResetEncoderDelegate(IntPtr encoder_pointer, ref int status);
        public static ResetEncoderDelegate ResetEncoder;

        public delegate void FreeEncoderDelegate(IntPtr encoder_pointer, ref int status);
        public static FreeEncoderDelegate FreeEncoder;

        public delegate int GetEncoderDelegate(IntPtr encoder_pointer, ref int status);
        public static GetEncoderDelegate GetEncoder;

        public delegate double GetEncoderPeriodDelegate(IntPtr encoder_pointer, ref int status);
        public static GetEncoderPeriodDelegate GetEncoderPeriod;

        public delegate void SetEncoderMaxPeriodDelegate(IntPtr encoder_pointer, double maxPeriod, ref int status);
        public static SetEncoderMaxPeriodDelegate SetEncoderMaxPeriod;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetEncoderStoppedDelegate(IntPtr encoder_pointer, ref int status);
        public static GetEncoderStoppedDelegate GetEncoderStopped;

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool GetEncoderDirectionDelegate(IntPtr encoder_pointer, ref int status);
        public static GetEncoderDirectionDelegate GetEncoderDirection;

        public delegate void SetEncoderReverseDirectionDelegate(IntPtr encoder_pointer, [MarshalAs(UnmanagedType.I1)]bool reverseDirection, ref int status);
        public static SetEncoderReverseDirectionDelegate SetEncoderReverseDirection;

        public delegate void SetEncoderSamplesToAverageDelegate(IntPtr encoder_pointer, uint samplesToAverage, ref int status);
        public static SetEncoderSamplesToAverageDelegate SetEncoderSamplesToAverage;

        public delegate uint GetEncoderSamplesToAverageDelegate(IntPtr encoder_pointer, ref int status);
        public static GetEncoderSamplesToAverageDelegate GetEncoderSamplesToAverage;

        public delegate void SetEncoderIndexSourceDelegate(IntPtr encoder_pointer, uint pin, [MarshalAs(UnmanagedType.I1)]bool analogTrigger, [MarshalAs(UnmanagedType.I1)]bool activeHigh, [MarshalAs(UnmanagedType.I1)]bool edgeSensitive, ref int status);
        public static SetEncoderIndexSourceDelegate SetEncoderIndexSource;

        public delegate ushort GetLoopTimingDelegate(ref int status);
        public static GetLoopTimingDelegate GetLoopTiming;

        public delegate void SpiInitializeDelegate(byte port, ref int status);
        public static SpiInitializeDelegate SpiInitialize;

        public delegate int SpiTransactionDelegate(byte port, byte[] dataToSend, byte[] dataReceived, byte size);
        public static SpiTransactionDelegate SpiTransaction;

        public delegate int SpiWriteDelegate(byte port, byte[] dataToSend, byte sendSize);
        public static SpiWriteDelegate SpiWrite;

        public delegate int SpiReadDelegate(byte port, byte[] buffer, byte count);
        public static SpiReadDelegate SpiRead;

        public delegate void SpiCloseDelegate(byte port);
        public static SpiCloseDelegate SpiClose;

        public delegate void SpiSetSpeedDelegate(byte port, uint speed);
        public static SpiSetSpeedDelegate SpiSetSpeed;

        public delegate void SpiSetOptsDelegate(byte port, int msb_first, int sample_on_trailing, int clk_idle_high);
        public static SpiSetOptsDelegate SpiSetOpts;

        public delegate void SpiSetChipSelectActiveHighDelegate(byte port, ref int status);
        public static SpiSetChipSelectActiveHighDelegate SpiSetChipSelectActiveHigh;

        public delegate void SpiSetChipSelectActiveLowDelegate(byte port, ref int status);
        public static SpiSetChipSelectActiveLowDelegate SpiSetChipSelectActiveLow;

        public delegate int SpiGetHandleDelegate(byte port);
        public static SpiGetHandleDelegate SpiGetHandle;

        public delegate void SpiSetHandleDelegate(byte port, int handle);
        public static SpiSetHandleDelegate SpiSetHandle;

        public delegate void SpiInitAccumulatorDelegate(byte port, uint period, uint cmd, byte xferSize,
            uint validMask, uint validValue, byte dataShift, byte dataSize, [MarshalAs(UnmanagedType.I1)]bool isSigned,
            [MarshalAs(UnmanagedType.I1)]bool bigEndian, ref int status);
        public static SpiInitAccumulatorDelegate SpiInitAccumulator;

        public delegate void SpiFreeAccumulatorDelegate(byte port, ref int status);
        public static SpiFreeAccumulatorDelegate SpiFreeAccumulator;

        public delegate void SpiResetAccumulatorDelegate(byte port, ref int status);
        public static SpiResetAccumulatorDelegate SpiResetAccumulator;

        public delegate void SpiSetAccumulatorCenterDelegate(byte port, int center, ref int status);
        public static SpiSetAccumulatorCenterDelegate SpiSetAccumulatorCenter;

        public delegate void SpiSetAccumulatorDeadbandDelegate(byte port, int deadband, ref int status);
        public static SpiSetAccumulatorDeadbandDelegate SpiSetAccumulatorDeadband;

        public delegate int SpiGetAccumulatorLastValueDelegate(byte port, ref int status);
        public static SpiGetAccumulatorLastValueDelegate SpiGetAccumulatorLastValue;

        public delegate long SpiGetAccumulatorValueDelegate(byte port, ref int status);
        public static SpiGetAccumulatorValueDelegate SpiGetAccumulatorValue;

        public delegate uint SpiGetAccumulatorCountDelegate(byte port, ref int status);
        public static SpiGetAccumulatorCountDelegate SpiGetAccumulatorCount;

        public delegate double SpiGetAccumulatorAverageDelegate(byte port, ref int status);
        public static SpiGetAccumulatorAverageDelegate SpiGetAccumulatorAverage;

        public delegate void SpiGetAccumulatorOutputDelegate(byte port, ref long value, ref uint count, ref int status);
        public static SpiGetAccumulatorOutputDelegate SpiGetAccumulatorOutput;

        public delegate void I2CInitializeDelegate(byte port, ref int status);
        public static I2CInitializeDelegate I2CInitialize;

        public delegate int I2CTransactionDelegate(byte port, byte deviceAddress, byte[] dataToSend, byte sendSize, byte[] dataReceived, byte receiveSize);
        public static I2CTransactionDelegate I2CTransaction;

        public delegate int I2CWriteDelegate(byte port, byte deviceAddress, byte[] dataToSend, byte sendSize);
        public static I2CWriteDelegate I2CWrite;

        public delegate int I2CReadDelegate(byte port, byte deviceAddress, byte[] buffer, byte count);
        public static I2CReadDelegate I2CRead;

        public delegate void I2CCloseDelegate(byte port);
        public static I2CCloseDelegate I2CClose;
    }
}
