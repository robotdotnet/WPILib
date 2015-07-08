

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web.Profile;
using static HAL_Simulator.PortConverters;
using static HAL_Simulator.SimData;
using static HAL_Simulator.PWMHelpers;

namespace HAL_Simulator
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
        internal const int ExpectedLoopTiming = 40;
        internal const int DigitalPins = 26;
        internal const int PwmPins = 20;
        internal const int RelayPins = 4;
        internal const int NumHeaders = 10;

        internal const int RESOURCE_IS_ALLOCATED = -1029;

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

        private static int RemapMXPChannel(int pin)
        {
            return pin - 10;
        }

        private static int RemapMXPPWMChannel(int pin)
        {
            if (pin < 14)
                return pin - 10;
            else
                return pin - 6;
        }


        public static bool checkPWMChannel(IntPtr digital_port_pointer)
        {
            var dPort = GetDigitalPort(digital_port_pointer);
            return dPort.port.pin < PwmPins;
        }


        public static bool checkRelayChannel(IntPtr digital_port_pointer)
        {
            var dPort = GetDigitalPort(digital_port_pointer);
            return dPort.port.pin < RelayPins;
        }


        public static void setPWM(IntPtr digital_port_pointer, ushort value, ref int status)
        {
            status = 0;
            var dPort = GetDigitalPort(digital_port_pointer);
            halData["pwm"][dPort.port.pin]["raw_value"] = value;
            halData["pwm"][dPort.port.pin]["value"] = ReverseByType(dPort.port.pin);
        }

        public static bool allocatePWMChannel(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            var pin = GetDigitalPort(digital_port_pointer).port.pin;
            var mxp_port = RemapMXPPWMChannel(pin);
            if (pin >= NumHeaders)
            {
                if (halData["mxp"][mxp_port]["initialized"])
                {
                    status = RESOURCE_IS_ALLOCATED;
                    return false;
                }
            }
            if (halData["pwm"][pin]["initialized"])
            {
                status = RESOURCE_IS_ALLOCATED;
                return false;
            }
            halData["pwm"][pin]["initialized"] = true;

            if (pin > NumHeaders)
            {
                halData["mxp"][mxp_port]["initialized"] = true;
            }
            return true;
        }

        public static void freePWMChannel(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            var pin = GetDigitalPort(digital_port_pointer).port.pin;
            halData["pwm"][pin]["initialized"] = false;
            halData["pwm"][pin]["raw_value"] = 0;
            halData["pwm"][pin]["value"] = 0;
            halData["pwm"][pin]["period_scale"] = false;
            halData["pwm"][pin]["zero_latch"] = false;

            if (pin > NumHeaders)
            {
                var mxp_port = RemapMXPPWMChannel(pin);
                halData["mxp"][mxp_port]["initialized"] = false;
            }
        }

        public static ushort getPWM(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return (ushort) halData["pwm"][GetDigitalPort(digital_port_pointer).port.pin]["raw_value"];
        }

        public static void latchPWMZero(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            halData["pwm"][GetDigitalPort(digital_port_pointer).port.pin]["zero_latch"] = true;
        }

        public static void setPWMPeriodScale(IntPtr digital_port_pointer, uint squelchMask, ref int status)
        {
            status = 0;
            halData["pwm"][GetDigitalPort(digital_port_pointer).port.pin]["period_scale"] = squelchMask;
        }

        public static IntPtr allocatePWM(ref int status)
        {
            status = 0;
            //TODO: Figure this code out 
            /*
                    for i, v in enumerate(hal_data['d0_pwm']):
                if v is None:
                    break
            else:
                return None
            */
            return IntPtr.Zero;
        }

        public static void freePWM(IntPtr pwmGenerator, ref int status)
        {
            status = 0;
            halData["d0_pwm"][GetPWM(pwmGenerator).idx] = null;
        }


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


        public static void setRelayForward(IntPtr digital_port_pointer, bool on, ref int status)
        {
            status = 0;
            var dPort = GetDigitalPort(digital_port_pointer);
            var relay = halData["relay"][dPort.port.pin];
            relay["initialized"] = true;
            relay["fwd"] = on;
        }

        public static void setRelayReverse(IntPtr digital_port_pointer, bool on, ref int status)
        {
            status = 0;
            var dPort = GetDigitalPort(digital_port_pointer);
            var relay = halData["relay"][dPort.port.pin];
            relay["initialized"] = true;
            relay["rev"] = on;
        }

        public static bool getRelayForward(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return halData["relay"][GetDigitalPort(digital_port_pointer).port.pin]["fwd"];
        }

        public static bool getRelayReverse(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return halData["relay"][GetDigitalPort(digital_port_pointer).port.pin]["rev"];
        }

        public static bool allocateDIO(IntPtr digital_port_pointer, bool input,
            ref int status)
        {
            status = 0;
            var pin = GetDigitalPort(digital_port_pointer).port.pin;
            var mxpPort = RemapMXPChannel(pin);
            if (pin > NumHeaders)
            {
                if (halData["mxp"][mxpPort]["initialized"])
                {
                    status = RESOURCE_IS_ALLOCATED;
                    return false;
                }
            }
            var dio = halData["dio"][pin];
            if (dio["initialized"])
            {
                status = RESOURCE_IS_ALLOCATED;
                return false;
            }
            if (pin > NumHeaders)
            {
                halData["mxp"][mxpPort]["initialized"] = true;
            }
            dio["initialized"] = true;
            dio["is_input"] = input;
            return true;
        }

        public static void freeDIO(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            var pin = GetDigitalPort(digital_port_pointer).port.pin;
            halData["dio"][pin]["initialized"] = false;
            if (pin > NumHeaders)
            {
                halData["mxp"][RemapMXPChannel(pin)]["initialized"] = false;
            }

        }

        public static void setDIO(IntPtr digital_port_pointer, short value, ref int status)
        {
            status = 0;
            halData["dio"][GetDigitalPort(digital_port_pointer).port.pin]["value"] = value != 0;
        }

        public static bool getDIO(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return halData["dio"][GetDigitalPort(digital_port_pointer).port.pin]["value"];
        }

        public static bool getDIODirection(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return halData["dio"][GetDigitalPort(digital_port_pointer).port.pin]["is_input"];
        }

        public static void pulse(IntPtr digital_port_pointer, double pulseLength, ref int status)
        {
            status = 0;
            halData["dio"][GetDigitalPort(digital_port_pointer).port.pin]["pulse_length"] = pulseLength;

        }

        public static bool isPulsing(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return halData["dio"][GetDigitalPort(digital_port_pointer).port.pin]["pulse_length"] != 0;
        }

        public static bool isAnyPulsing(ref int status)
        {
            status = 0;
            foreach (var p in halData["dio"])
            {
                if (p != null && p["pulse_lenght"] != null)
                    return true;
            }
            return false;
        }


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


        public static ushort getLoopTiming(ref int status)
        {
            return (ushort) halData["pwm_loop_timing"];
        }


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
