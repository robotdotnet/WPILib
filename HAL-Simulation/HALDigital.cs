

using System;
using System.Runtime.InteropServices;

namespace HAL_FRC
{
    public enum Mode
    {
        /// kTwoPulse -> 0
        kTwoPulse = 0,

        /// kSemiperiod -> 1
        kSemiperiod = 1,

        /// kPulseLength -> 2
        kPulseLength = 2,

        /// kExternalDirection -> 3
        kExternalDirection = 3,
    }

    public class HALDigital
    {
        /// Return Type: void*
        ///port_pointer: void*
        ///status: int*
        //[DllImport("libHALAthena_shared.so", EntryPoint = "initializeDigitalPort")]
        //public static extern System.IntPtr initializeDigitalPort(System.IntPtr port_pointer, ref int status);

        public static IntPtr initializeDigitalPort(IntPtr port_pointer, ref int status)
        {
            DigitalPort p = new DigitalPort {port = (Port) Marshal.PtrToStructure(port_pointer, typeof (Port))};
            status = 0;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        /// Return Type: boolean
        ///digital_port_pointer: void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "checkPWMChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkPWMChannel(IntPtr digital_port_pointer);


        /// Return Type: boolean
        ///digital_port_pointer: void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "checkRelayChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool checkRelayChannel(IntPtr digital_port_pointer);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///value: unsigned short
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setPWM")]
        public static extern void setPWM(IntPtr digital_port_pointer, ushort value, ref int status);


        /// Return Type: boolean
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "allocatePWMChannel")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool allocatePWMChannel(IntPtr digital_port_pointer, ref int status);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "freePWMChannel")]
        public static extern void freePWMChannel(IntPtr digital_port_pointer, ref int status);


        /// Return Type: unsigned short
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPWM")]
        public static extern ushort getPWM(IntPtr digital_port_pointer, ref int status);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "latchPWMZero")]
        public static extern void latchPWMZero(IntPtr digital_port_pointer, ref int status);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///squelchMask: unsigned int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setPWMPeriodScale")]
        public static extern void setPWMPeriodScale(IntPtr digital_port_pointer, uint squelchMask, ref int status);


        /// Return Type: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "allocatePWM")]
        public static extern IntPtr allocatePWM(ref int status);


        /// Return Type: void
        ///pwmGenerator: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "freePWM")]
        public static extern void freePWM(IntPtr pwmGenerator, ref int status);


        /// Return Type: void
        ///rate: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setPWMRate")]
        public static extern void setPWMRate(double rate, ref int status);


        /// Return Type: void
        ///pwmGenerator: void*
        ///dutyCycle: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setPWMDutyCycle")]
        public static extern void setPWMDutyCycle(IntPtr pwmGenerator, double dutyCycle, ref int status);


        /// Return Type: void
        ///pwmGenerator: void*
        ///pin: unsigned int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setPWMOutputChannel")]
        public static extern void setPWMOutputChannel(IntPtr pwmGenerator, uint pin, ref int status);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///on: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setRelayForward")]
        public static extern void setRelayForward(IntPtr digital_port_pointer, [MarshalAs(UnmanagedType.I1)] bool on, ref int status);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///on: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setRelayReverse")]
        public static extern void setRelayReverse(IntPtr digital_port_pointer, [MarshalAs(UnmanagedType.I1)] bool on, ref int status);


        /// Return Type: boolean
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getRelayForward")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getRelayForward(IntPtr digital_port_pointer, ref int status);


        /// Return Type: boolean
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getRelayReverse")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getRelayReverse(IntPtr digital_port_pointer, ref int status);


        /// Return Type: boolean
        ///digital_port_pointer: void*
        ///input: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "allocateDIO")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool allocateDIO(IntPtr digital_port_pointer, [MarshalAs(UnmanagedType.I1)] bool input, ref int status);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "freeDIO")]
        public static extern void freeDIO(IntPtr digital_port_pointer, ref int status);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///value: short
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setDIO")]
        public static extern void setDIO(IntPtr digital_port_pointer, short value, ref int status);


        /// Return Type: boolean
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getDIO")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getDIO(IntPtr digital_port_pointer, ref int status);


        /// Return Type: boolean
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getDIODirection")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getDIODirection(IntPtr digital_port_pointer, ref int status);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///pulseLength: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "pulse")]
        public static extern void pulse(IntPtr digital_port_pointer, double pulseLength, ref int status);


        /// Return Type: boolean
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "isPulsing")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool isPulsing(IntPtr digital_port_pointer, ref int status);


        /// Return Type: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "isAnyPulsing")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool isAnyPulsing(ref int status);


        /// Return Type: void*
        ///mode: Mode
        ///index: unsigned int*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeCounter")]
        public static extern IntPtr initializeCounter(Mode mode, ref uint index, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "freeCounter")]
        public static extern void freeCounter(IntPtr counter_pointer, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///size: int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterAverageSize")]
        public static extern void setCounterAverageSize(IntPtr counter_pointer, int size, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///pin: unsigned int
        ///analogTrigger: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterUpSource")]
        public static extern void setCounterUpSource(IntPtr counter_pointer, uint pin, [MarshalAs(UnmanagedType.I1)] bool analogTrigger, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///risingEdge: boolean
        ///fallingEdge: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterUpSourceEdge")]
        public static extern void setCounterUpSourceEdge(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)] bool risingEdge, [MarshalAs(UnmanagedType.I1)] bool fallingEdge, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "clearCounterUpSource")]
        public static extern void clearCounterUpSource(IntPtr counter_pointer, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///pin: unsigned int
        ///analogTrigger: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterDownSource")]
        public static extern void setCounterDownSource(IntPtr counter_pointer, uint pin, [MarshalAs(UnmanagedType.I1)] bool analogTrigger, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///risingEdge: boolean
        ///fallingEdge: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterDownSourceEdge")]
        public static extern void setCounterDownSourceEdge(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)] bool risingEdge, [MarshalAs(UnmanagedType.I1)] bool fallingEdge, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "clearCounterDownSource")]
        public static extern void clearCounterDownSource(IntPtr counter_pointer, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterUpDownMode")]
        public static extern void setCounterUpDownMode(IntPtr counter_pointer, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterExternalDirectionMode")]
        public static extern void setCounterExternalDirectionMode(IntPtr counter_pointer, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///highSemiPeriod: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterSemiPeriodMode")]
        public static extern void setCounterSemiPeriodMode(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)] bool highSemiPeriod, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///threshold: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterPulseLengthMode")]
        public static extern void setCounterPulseLengthMode(IntPtr counter_pointer, double threshold, ref int status);


        /// Return Type: int
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getCounterSamplesToAverage")]
        public static extern int getCounterSamplesToAverage(IntPtr counter_pointer, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///samplesToAverage: int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterSamplesToAverage")]
        public static extern void setCounterSamplesToAverage(IntPtr counter_pointer, int samplesToAverage, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "resetCounter")]
        public static extern void resetCounter(IntPtr counter_pointer, ref int status);


        /// Return Type: int
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getCounter")]
        public static extern int getCounter(IntPtr counter_pointer, ref int status);


        /// Return Type: double
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getCounterPeriod")]
        public static extern double getCounterPeriod(IntPtr counter_pointer, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///maxPeriod: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterMaxPeriod")]
        public static extern void setCounterMaxPeriod(IntPtr counter_pointer, double maxPeriod, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///enabled: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterUpdateWhenEmpty")]
        public static extern void setCounterUpdateWhenEmpty(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)] bool enabled, ref int status);


        /// Return Type: boolean
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getCounterStopped")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getCounterStopped(IntPtr counter_pointer, ref int status);


        /// Return Type: boolean
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getCounterDirection")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getCounterDirection(IntPtr counter_pointer, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///reverseDirection: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterReverseDirection")]
        public static extern void setCounterReverseDirection(IntPtr counter_pointer, [MarshalAs(UnmanagedType.I1)] bool reverseDirection, ref int status);


        /// Return Type: void*
        ///port_a_module: byte
        ///port_a_pin: unsigned int
        ///port_a_analog_trigger: boolean
        ///port_b_module: byte
        ///port_b_pin: unsigned int
        ///port_b_analog_trigger: boolean
        ///reverseDirection: boolean
        ///index: int*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeEncoder")]
        public static extern IntPtr initializeEncoder(byte port_a_module, uint port_a_pin, [MarshalAs(UnmanagedType.I1)] bool port_a_analog_trigger, byte port_b_module, uint port_b_pin, [MarshalAs(UnmanagedType.I1)] bool port_b_analog_trigger, [MarshalAs(UnmanagedType.I1)] bool reverseDirection, ref int index, ref int status);


        /// Return Type: void
        ///encoder_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "freeEncoder")]
        public static extern void freeEncoder(IntPtr encoder_pointer, ref int status);


        /// Return Type: void
        ///encoder_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "resetEncoder")]
        public static extern void resetEncoder(IntPtr encoder_pointer, ref int status);


        /// Return Type: int
        ///encoder_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getEncoder")]
        public static extern int getEncoder(IntPtr encoder_pointer, ref int status);


        /// Return Type: double
        ///encoder_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getEncoderPeriod")]
        public static extern double getEncoderPeriod(IntPtr encoder_pointer, ref int status);


        /// Return Type: void
        ///encoder_pointer: void*
        ///maxPeriod: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setEncoderMaxPeriod")]
        public static extern void setEncoderMaxPeriod(IntPtr encoder_pointer, double maxPeriod, ref int status);


        /// Return Type: boolean
        ///encoder_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getEncoderStopped")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getEncoderStopped(IntPtr encoder_pointer, ref int status);


        /// Return Type: boolean
        ///encoder_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getEncoderDirection")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getEncoderDirection(IntPtr encoder_pointer, ref int status);


        /// Return Type: void
        ///encoder_pointer: void*
        ///reverseDirection: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setEncoderReverseDirection")]
        public static extern void setEncoderReverseDirection(IntPtr encoder_pointer, [MarshalAs(UnmanagedType.I1)] bool reverseDirection, ref int status);


        /// Return Type: void
        ///encoder_pointer: void*
        ///samplesToAverage: unsigned int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setEncoderSamplesToAverage")]
        public static extern void setEncoderSamplesToAverage(IntPtr encoder_pointer, uint samplesToAverage, ref int status);


        /// Return Type: unsigned int
        ///encoder_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getEncoderSamplesToAverage")]
        public static extern uint getEncoderSamplesToAverage(IntPtr encoder_pointer, ref int status);


        /// Return Type: void
        ///encoder_pointer: void*
        ///pin: unsigned int
        ///analogTrigger: boolean
        ///activeHigh: boolean
        ///edgeSensitive: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setEncoderIndexSource")]
        public static extern void setEncoderIndexSource(IntPtr encoder_pointer, uint pin, [MarshalAs(UnmanagedType.I1)] bool analogTrigger, [MarshalAs(UnmanagedType.I1)] bool activeHigh, [MarshalAs(UnmanagedType.I1)] bool edgeSensitive, ref int status);


        /// Return Type: unsigned short
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getLoopTiming")]
        public static extern ushort getLoopTiming(ref int status);


        /// Return Type: void
        ///port: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiInitialize")]
        public static extern void spiInitialize(byte port, ref int status);


        /// Return Type: int
        ///port: byte
        ///dataToSend: byte*
        ///dataReceived: byte*
        ///size: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiTransaction")]
        public static extern int spiTransaction(byte port, byte[] dataToSend, byte[] dataReceived, byte size);


        /// Return Type: int
        ///port: byte
        ///dataToSend: byte*
        ///sendSize: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiWrite")]
        public static extern int spiWrite(byte port, byte[] dataToSend, byte sendSize);


        /// Return Type: int
        ///port: byte
        ///buffer: byte*
        ///count: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiRead")]
        public static extern int spiRead(byte port, byte[] buffer, byte count);


        /// Return Type: void
        ///port: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiClose")]
        public static extern void spiClose(byte port);


        /// Return Type: void
        ///port: byte
        ///speed: unsigned int
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiSetSpeed")]
        public static extern void spiSetSpeed(byte port, uint speed);


        /// Return Type: void
        ///port: byte
        ///bpw: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiSetBitsPerWord")]
        public static extern void spiSetBitsPerWord(byte port, byte bpw);


        /// Return Type: void
        ///port: byte
        ///msb_first: int
        ///sample_on_trailing: int
        ///clk_idle_high: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiSetOpts")]
        public static extern void spiSetOpts(byte port, int msb_first, int sample_on_trailing, int clk_idle_high);


        /// Return Type: void
        ///port: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiSetChipSelectActiveHigh")]
        public static extern void spiSetChipSelectActiveHigh(byte port, ref int status);


        /// Return Type: void
        ///port: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiSetChipSelectActiveLow")]
        public static extern void spiSetChipSelectActiveLow(byte port, ref int status);


        /// Return Type: int
        ///port: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiGetHandle")]
        public static extern int spiGetHandle(byte port);


        /// Return Type: void
        ///port: byte
        ///handle: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiSetHandle")]
        public static extern void spiSetHandle(byte port, int handle);

        //Actually returns MUTEX_ID
        /// Return Type: int
        ///port: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiGetSemaphore")]
        public static extern IntPtr spiGetSemaphore(byte port);


        //Actually takes MUTEX_ID
        /// Return Type: void
        ///port: byte
        ///semaphore: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiSetSemaphore")]
        public static extern void spiSetSemaphore(byte port, IntPtr semaphore);


        /// Return Type: void
        ///port: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "i2CInitialize")]
        public static extern void i2CInitialize(byte port, ref int status);


        /// Return Type: int
        ///port: byte
        ///deviceAddress: byte
        ///dataToSend: byte*
        ///sendSize: byte
        ///dataReceived: byte*
        ///receiveSize: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "i2CTransaction")]
        public static extern int i2CTransaction(byte port, byte deviceAddress, byte[] dataToSend, byte sendSize, byte[] dataReceived, byte receiveSize);


        /// Return Type: int
        ///port: byte
        ///deviceAddress: byte
        ///dataToSend: byte*
        ///sendSize: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "i2CWrite")]
        public static extern int i2CWrite(byte port, byte deviceAddress, byte[] dataToSend, byte sendSize);


        /// Return Type: int
        ///port: byte
        ///deviceAddress: byte
        ///buffer: byte*
        ///count: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "i2CRead")]
        public static extern int i2CRead(byte port, byte deviceAddress, byte[] buffer, byte count);


        /// Return Type: void
        ///port: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "i2CClose")]
        public static extern void i2CClose(byte port);


        /// Return Type: void
        ///rate: int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setPWMRateIntHack")]
        public static extern void setPWMRateIntHack(int rate, ref int status);


        /// Return Type: void
        ///pwmGenerator: void*
        ///dutyCycle: int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setPWMDutyCycleIntHack")]
        public static extern void setPWMDutyCycleIntHack(IntPtr pwmGenerator, int dutyCycle, ref int status);
    }

    /*
    [StructLayout(LayoutKind.Explicit)]
    public struct MUTEX_ID
    {
        
    }
     * */
}
