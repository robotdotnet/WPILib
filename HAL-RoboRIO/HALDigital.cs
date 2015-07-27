//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using HAL_Base;

namespace HAL_RoboRIO
{
    internal class HALDigital
    {

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeDigitalPort")]
        internal static extern IntPtr initializeDigitalPort(IntPtr port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "checkPWMChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool checkPWMChannel(IntPtr digital_port_pointer);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "checkRelayChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool checkRelayChannel(IntPtr digital_port_pointer);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setPWM")]
        internal static extern void setPWM(IntPtr digital_port_pointer, ushort value, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "allocatePWMChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool allocatePWMChannel(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "freePWMChannel")]
        internal static extern void freePWMChannel(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getPWM")]
        internal static extern ushort getPWM(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "latchPWMZero")]
        internal static extern void latchPWMZero(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setPWMPeriodScale")]
        internal static extern void setPWMPeriodScale(IntPtr digital_port_pointer, uint squelchMask, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "allocatePWM")]
        internal static extern IntPtr allocatePWM(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "freePWM")]
        internal static extern void freePWM(IntPtr pwmGenerator, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setPWMRate")]
        internal static extern void setPWMRate(double rate, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setPWMDutyCycle")]
        internal static extern void setPWMDutyCycle(IntPtr pwmGenerator, double dutyCycle, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setPWMOutputChannel")]
        internal static extern void setPWMOutputChannel(IntPtr pwmGenerator, uint pin, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setRelayForward")]
        internal static extern void setRelayForward(IntPtr digital_port_pointer, [MarshalAs(UnmanagedType.I1)] bool on, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setRelayReverse")]
        internal static extern void setRelayReverse(IntPtr digital_port_pointer, [MarshalAs(UnmanagedType.I1)] bool on, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getRelayForward")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getRelayForward(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getRelayReverse")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getRelayReverse(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "allocateDIO")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool allocateDIO(IntPtr digital_port_pointer, [MarshalAs(UnmanagedType.I1)] bool input, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "freeDIO")]
        internal static extern void freeDIO(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setDIO")]
        internal static extern void setDIO(IntPtr digital_port_pointer, short value, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getDIO")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getDIO(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getDIODirection")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getDIODirection(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "pulse")]
        internal static extern void pulse(IntPtr digital_port_pointer, double pulseLength, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "isPulsing")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool isPulsing(IntPtr digital_port_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "isAnyPulsing")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool isAnyPulsing(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeCounter")]
        internal static extern IntPtr initializeCounter(Mode mode, ref uint index, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "freeCounter")]
        internal static extern void freeCounter(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterAverageSize")]
        internal static extern void setCounterAverageSize(IntPtr counter_pointer, int size, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterUpSource")]
        internal static extern void setCounterUpSource(IntPtr counter_pointer, uint pin, [MarshalAs(UnmanagedType.I1)] bool analogTrigger, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterUpSourceEdge")]
        internal static extern void setCounterUpSourceEdge(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)] bool risingEdge, [MarshalAs(UnmanagedType.I1)] bool fallingEdge, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "clearCounterUpSource")]
        internal static extern void clearCounterUpSource(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterDownSource")]
        internal static extern void setCounterDownSource(IntPtr counter_pointer, uint pin, [MarshalAs(UnmanagedType.I1)] bool analogTrigger, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterDownSourceEdge")]
        internal static extern void setCounterDownSourceEdge(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)] bool risingEdge, [MarshalAs(UnmanagedType.I1)] bool fallingEdge, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "clearCounterDownSource")]
        internal static extern void clearCounterDownSource(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterUpDownMode")]
        internal static extern void setCounterUpDownMode(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterExternalDirectionMode")]
        internal static extern void setCounterExternalDirectionMode(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterSemiPeriodMode")]
        internal static extern void setCounterSemiPeriodMode(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)] bool highSemiPeriod, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterPulseLengthMode")]
        internal static extern void setCounterPulseLengthMode(IntPtr counter_pointer, double threshold, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCounterSamplesToAverage")]
        internal static extern int getCounterSamplesToAverage(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterSamplesToAverage")]
        internal static extern void setCounterSamplesToAverage(IntPtr counter_pointer, int samplesToAverage, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "resetCounter")]
        internal static extern void resetCounter(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCounter")]
        internal static extern int getCounter(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCounterPeriod")]
        internal static extern double getCounterPeriod(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterMaxPeriod")]
        internal static extern void setCounterMaxPeriod(IntPtr counter_pointer, double maxPeriod, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterUpdateWhenEmpty")]
        internal static extern void setCounterUpdateWhenEmpty(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)] bool enabled, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCounterStopped")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getCounterStopped(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getCounterDirection")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getCounterDirection(IntPtr counter_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setCounterReverseDirection")]
        internal static extern void setCounterReverseDirection(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)] bool reverseDirection, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "initializeEncoder")]
        internal static extern IntPtr initializeEncoder(byte port_a_module, uint port_a_pin, [MarshalAs(UnmanagedType.I1)] bool port_a_analog_trigger, byte port_b_module, uint port_b_pin, [MarshalAs(UnmanagedType.I1)] bool port_b_analog_trigger, [MarshalAs(UnmanagedType.I1)] bool reverseDirection, ref int index, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "resetEncoder")]
        internal static extern void resetEncoder(IntPtr encoder_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "freeEncoder")]
        internal static extern void freeEncoder(IntPtr encoder_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getEncoder")]
        internal static extern int getEncoder(IntPtr encoder_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getEncoderPeriod")]
        internal static extern double getEncoderPeriod(IntPtr encoder_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setEncoderMaxPeriod")]
        internal static extern void setEncoderMaxPeriod(IntPtr encoder_pointer, double maxPeriod, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getEncoderStopped")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getEncoderStopped(IntPtr encoder_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getEncoderDirection")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool getEncoderDirection(IntPtr encoder_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setEncoderReverseDirection")]
        internal static extern void setEncoderReverseDirection(IntPtr encoder_pointer, [MarshalAs(UnmanagedType.I1)] bool reverseDirection, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setEncoderSamplesToAverage")]
        internal static extern void setEncoderSamplesToAverage(IntPtr encoder_pointer, uint samplesToAverage, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getEncoderSamplesToAverage")]
        internal static extern uint getEncoderSamplesToAverage(IntPtr encoder_pointer, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "setEncoderIndexSource")]
        internal static extern void setEncoderIndexSource(IntPtr encoder_pointer, uint pin, [MarshalAs(UnmanagedType.I1)] bool analogTrigger, [MarshalAs(UnmanagedType.I1)] bool activeHigh, [MarshalAs(UnmanagedType.I1)] bool edgeSensitive, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "getLoopTiming")]
        internal static extern ushort getLoopTiming(ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiInitialize")]
        internal static extern void spiInitialize(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiTransaction")]
        internal static extern int spiTransaction(byte port, byte[] dataToSend, byte[] dataReceived, byte size);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiWrite")]
        internal static extern int spiWrite(byte port, byte[] dataToSend, byte sendSize);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiRead")]
        internal static extern int spiRead(byte port, byte[] buffer, byte count);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiClose")]
        internal static extern void spiClose(byte port);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiSetSpeed")]
        internal static extern void spiSetSpeed(byte port, uint speed);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiSetBitsPerWord")]
        internal static extern void spiSetBitsPerWord(byte port, byte bpw);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiSetOpts")]
        internal static extern void spiSetOpts(byte port, int msb_first, int sample_on_trailing, int clk_idle_high);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiSetChipSelectActiveHigh")]
        internal static extern void spiSetChipSelectActiveHigh(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiSetChipSelectActiveLow")]
        internal static extern void spiSetChipSelectActiveLow(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiGetHandle")]
        internal static extern int spiGetHandle(byte port);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiSetHandle")]
        internal static extern void spiSetHandle(byte port, int handle);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiGetSemaphore")]
        internal static extern IntPtr spiGetSemaphore(byte port);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "spiSetSemaphore")]
        internal static extern void spiSetSemaphore(byte port, IntPtr semaphore);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "i2CInitialize")]
        internal static extern void i2CInitialize(byte port, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "i2CTransaction")]
        internal static extern int i2CTransaction(byte port, byte deviceAddress, byte[] dataToSend, byte sendSize, byte[] dataReceived, byte receiveSize);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "i2CWrite")]
        internal static extern int i2CWrite(byte port, byte deviceAddress, byte[] dataToSend, byte sendSize);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "i2CRead")]
        internal static extern int i2CRead(byte port, byte deviceAddress, byte[] buffer, byte count);

        [DllImport(HAL.LibhalathenaSharedSo, EntryPoint = "i2CClose")]
        internal static extern void i2CClose(byte port);
    }
}
