//File automatically generated using robotdotnet-tools. Please do not modify.
using HAL_Base;
namespace HAL_RoboRIO
{
    public class HALDigital
    {

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "initializeDigitalPort")]
        public static extern System.IntPtr initializeDigitalPort(System.IntPtr port_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "checkPWMChannel")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool checkPWMChannel(System.IntPtr digital_port_pointer);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "checkRelayChannel")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool checkRelayChannel(System.IntPtr digital_port_pointer);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setPWM")]
        public static extern void setPWM(System.IntPtr digital_port_pointer, ushort value, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "allocatePWMChannel")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool allocatePWMChannel(System.IntPtr digital_port_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "freePWMChannel")]
        public static extern void freePWMChannel(System.IntPtr digital_port_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getPWM")]
        public static extern ushort getPWM(System.IntPtr digital_port_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "latchPWMZero")]
        public static extern void latchPWMZero(System.IntPtr digital_port_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setPWMPeriodScale")]
        public static extern void setPWMPeriodScale(System.IntPtr digital_port_pointer, uint squelchMask, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "allocatePWM")]
        public static extern System.IntPtr allocatePWM(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "freePWM")]
        public static extern void freePWM(System.IntPtr pwmGenerator, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setPWMRate")]
        public static extern void setPWMRate(double rate, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setPWMDutyCycle")]
        public static extern void setPWMDutyCycle(System.IntPtr pwmGenerator, double dutyCycle, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setPWMOutputChannel")]
        public static extern void setPWMOutputChannel(System.IntPtr pwmGenerator, uint pin, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setRelayForward")]
        public static extern void setRelayForward(System.IntPtr digital_port_pointer, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool on, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setRelayReverse")]
        public static extern void setRelayReverse(System.IntPtr digital_port_pointer, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool on, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getRelayForward")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getRelayForward(System.IntPtr digital_port_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getRelayReverse")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getRelayReverse(System.IntPtr digital_port_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "allocateDIO")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool allocateDIO(System.IntPtr digital_port_pointer, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool input, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "freeDIO")]
        public static extern void freeDIO(System.IntPtr digital_port_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setDIO")]
        public static extern void setDIO(System.IntPtr digital_port_pointer, short value, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getDIO")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getDIO(System.IntPtr digital_port_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getDIODirection")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getDIODirection(System.IntPtr digital_port_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "pulse")]
        public static extern void pulse(System.IntPtr digital_port_pointer, double pulseLength, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "isPulsing")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool isPulsing(System.IntPtr digital_port_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "isAnyPulsing")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool isAnyPulsing(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "initializeCounter")]
        public static extern System.IntPtr initializeCounter(Mode mode, ref uint index, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "freeCounter")]
        public static extern void freeCounter(System.IntPtr counter_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setCounterAverageSize")]
        public static extern void setCounterAverageSize(System.IntPtr counter_pointer, int size, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setCounterUpSource")]
        public static extern void setCounterUpSource(System.IntPtr counter_pointer, uint pin, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool analogTrigger, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setCounterUpSourceEdge")]
        public static extern void setCounterUpSourceEdge(System.IntPtr counter_pointer, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool risingEdge, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool fallingEdge, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "clearCounterUpSource")]
        public static extern void clearCounterUpSource(System.IntPtr counter_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setCounterDownSource")]
        public static extern void setCounterDownSource(System.IntPtr counter_pointer, uint pin, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool analogTrigger, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setCounterDownSourceEdge")]
        public static extern void setCounterDownSourceEdge(System.IntPtr counter_pointer, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool risingEdge, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool fallingEdge, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "clearCounterDownSource")]
        public static extern void clearCounterDownSource(System.IntPtr counter_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setCounterUpDownMode")]
        public static extern void setCounterUpDownMode(System.IntPtr counter_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setCounterExternalDirectionMode")]
        public static extern void setCounterExternalDirectionMode(System.IntPtr counter_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setCounterSemiPeriodMode")]
        public static extern void setCounterSemiPeriodMode(System.IntPtr counter_pointer, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool highSemiPeriod, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setCounterPulseLengthMode")]
        public static extern void setCounterPulseLengthMode(System.IntPtr counter_pointer, double threshold, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getCounterSamplesToAverage")]
        public static extern int getCounterSamplesToAverage(System.IntPtr counter_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setCounterSamplesToAverage")]
        public static extern void setCounterSamplesToAverage(System.IntPtr counter_pointer, int samplesToAverage, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "resetCounter")]
        public static extern void resetCounter(System.IntPtr counter_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getCounter")]
        public static extern int getCounter(System.IntPtr counter_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getCounterPeriod")]
        public static extern double getCounterPeriod(System.IntPtr counter_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setCounterMaxPeriod")]
        public static extern void setCounterMaxPeriod(System.IntPtr counter_pointer, double maxPeriod, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setCounterUpdateWhenEmpty")]
        public static extern void setCounterUpdateWhenEmpty(System.IntPtr counter_pointer, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool enabled, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getCounterStopped")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCounterStopped(System.IntPtr counter_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getCounterDirection")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCounterDirection(System.IntPtr counter_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setCounterReverseDirection")]
        public static extern void setCounterReverseDirection(System.IntPtr counter_pointer, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool reverseDirection, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "initializeEncoder")]
        public static extern System.IntPtr initializeEncoder(byte port_a_module, uint port_a_pin, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool port_a_analog_trigger, byte port_b_module, uint port_b_pin, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool port_b_analog_trigger, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool reverseDirection, ref int index, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "resetEncoder")]
        public static extern void resetEncoder(System.IntPtr encoder_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getEncoder")]
        public static extern int getEncoder(System.IntPtr encoder_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getEncoderPeriod")]
        public static extern double getEncoderPeriod(System.IntPtr encoder_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setEncoderMaxPeriod")]
        public static extern void setEncoderMaxPeriod(System.IntPtr encoder_pointer, double maxPeriod, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getEncoderStopped")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getEncoderStopped(System.IntPtr encoder_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getEncoderDirection")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getEncoderDirection(System.IntPtr encoder_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setEncoderReverseDirection")]
        public static extern void setEncoderReverseDirection(System.IntPtr encoder_pointer, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool reverseDirection, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setEncoderSamplesToAverage")]
        public static extern void setEncoderSamplesToAverage(System.IntPtr encoder_pointer, uint samplesToAverage, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getEncoderSamplesToAverage")]
        public static extern uint getEncoderSamplesToAverage(System.IntPtr encoder_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setEncoderIndexSource")]
        public static extern void setEncoderIndexSource(System.IntPtr encoder_pointer, uint pin, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool analogTrigger, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool activeHigh, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool edgeSensitive, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "getLoopTiming")]
        public static extern ushort getLoopTiming(ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "spiInitialize")]
        public static extern void spiInitialize(byte port, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "spiTransaction")]
        public static extern int spiTransaction(byte port, ref byte dataToSend, ref byte dataReceived, byte size);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "spiWrite")]
        public static extern int spiWrite(byte port, ref byte dataToSend, byte sendSize);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "spiRead")]
        public static extern int spiRead(byte port, ref byte buffer, byte count);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "spiClose")]
        public static extern void spiClose(byte port);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "spiSetSpeed")]
        public static extern void spiSetSpeed(byte port, uint speed);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "spiSetBitsPerWord")]
        public static extern void spiSetBitsPerWord(byte port, byte bpw);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "spiSetOpts")]
        public static extern void spiSetOpts(byte port, int msb_first, int sample_on_trailing, int clk_idle_high);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "spiSetChipSelectActiveHigh")]
        public static extern void spiSetChipSelectActiveHigh(byte port, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "spiSetChipSelectActiveLow")]
        public static extern void spiSetChipSelectActiveLow(byte port, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "spiGetHandle")]
        public static extern int spiGetHandle(byte port);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "spiSetHandle")]
        public static extern void spiSetHandle(byte port, int handle);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "spiGetSemaphore")]
        public static extern System.IntPtr spiGetSemaphore(byte port);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "spiSetSemaphore")]
        public static extern void spiSetSemaphore(byte port, System.IntPtr semaphore);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "i2CInitialize")]
        public static extern void i2CInitialize(byte port, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "i2CTransaction")]
        public static extern int i2CTransaction(byte port, byte deviceAddress, ref byte dataToSend, byte sendSize, ref byte dataReceived, byte receiveSize);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "i2CWrite")]
        public static extern int i2CWrite(byte port, byte deviceAddress, ref byte dataToSend, byte sendSize);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "i2CRead")]
        public static extern int i2CRead(byte port, byte deviceAddress, ref byte buffer, byte count);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "i2CClose")]
        public static extern void i2CClose(byte port);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setPWMRateIntHack")]
        public static extern void setPWMRateIntHack(int rate, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "setPWMDutyCycleIntHack")]
        public static extern void setPWMDutyCycleIntHack(System.IntPtr pwmGenerator, int dutyCycle, ref int status);
    }
}
