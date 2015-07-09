using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.InteropServices;
using HAL_Base;
using static HAL_Simulator.PortConverters;
using static HAL_Simulator.SimData;
using static HAL_Simulator.PWMHelpers;
using static HAL_Simulator.HALErrorConstants;

namespace HAL_Simulator
{
    public class HALDigital
    {
        internal const int ExpectedLoopTiming = 40;
        internal const int DigitalPins = 26;
        internal const int PwmPins = 20;
        internal const int RelayPins = 4;
        internal const int NumHeaders = 10;

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
            bool found = false;
            int i = 0;
            for (i = 0; i < halData["d0_pwm"].Length; i++)
            {
                if (halData["d0_pwm"][i] == null)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                return IntPtr.Zero;

            halData["d0_pwm"][i] = new Dictionary<dynamic, dynamic>()
            {
                ["duty_cycle"] = null,
                ["pin"] = null,
            };

            PWM p = new PWM {idx = (uint) i};
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        public static void freePWM(IntPtr pwmGenerator, ref int status)
        {
            status = 0;
            halData["d0_pwm"][GetPWM(pwmGenerator).idx] = null;
        }



        public static void setPWMRate(double rate, ref int status)
        {
            status = 0;
            halData["d0_pwm_rate"] = rate;
        }

        public static void setPWMDutyCycle(IntPtr pwmGenerator, double dutyCycle, ref int status)
        {
            status = 0;
            halData["d0_pwm"][GetPWM(pwmGenerator).idx]["duty_cycle"] = dutyCycle;
        }

        public static void setPWMOutputChannel(IntPtr pwmGenerator, uint pin, ref int status)
        {
            status = 0;
            halData["d0_pwm"][GetPWM(pwmGenerator).idx]["pin"] = pin;
        }


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

        public static IntPtr initializeCounter(Mode mode, ref uint index, ref int status)
        {
            status = 0;
            int i = 0;
            for (i = 0; i < halData["counter"].Length; i++)
            {
                var cnt = halData["counter"][i];
                if (!cnt["initialized"])
                {
                    cnt["initialized"] = true;
                    cnt["mode"] = mode;

                    Counter c = new Counter() {idx = (uint)i};
                    IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(c));
                    Marshal.StructureToPtr(c, ptr, true);
                    index = (uint)i;

                    return ptr;

                }
            }

            status = NO_AVAILABLE_RESOURCES;
            return IntPtr.Zero;
        }

        public static void freeCounter(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            halData["counter"][GetCounter(counter_pointer).idx]["initialized"] = false;

            Marshal.FreeHGlobal(counter_pointer);
        }

        public static void setCounterAverageSize(IntPtr counter_pointer, int size, ref int status)
        {
            status = 0;
            halData["counter"][GetCounter(counter_pointer).idx]["average_size"] = size;
        }


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

        public static void resetCounter(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            halData["counter"][GetCounter(counter_pointer).idx]["count"] = 0;
            halData["counter"][GetCounter(counter_pointer).idx]["period"] = 0;
        }


        public static int getCounter(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            return halData["counter"][GetCounter(counter_pointer).idx]["count"];
        }

        public static double getCounterPeriod(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            return halData["counter"][GetCounter(counter_pointer).idx]["period"];
        }


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


        public static IntPtr initializeEncoder(byte port_a_module, uint port_a_pin, bool port_a_analog_trigger,
            byte port_b_module, uint port_b_pin, bool port_b_analog_trigger, bool reverseDirection, ref int index,
            ref int status)
        {
            status = 0;
            for (int i = 0; i < halData["encoder"].Length; i++)
            {
                var enc = halData["encoder"][i];
                if (!enc["initialized"])
                {
                    enc["initialized"] = true;
                    enc["config"] = new Dictionary<dynamic, dynamic>()
                    {
                        ["ASource_Module"] = port_a_module,
                        ["ASource_Channel"] = port_a_pin,
                        ["ASource_AnalogTrigger"] = port_a_analog_trigger,
                        ["BSource_Module"] = port_b_module,
                        ["BSource_Channel"] = port_b_pin,
                        ["BSource_AnalogTrigger"] = port_b_analog_trigger,
                    };

                    enc["reverse_direction"] = reverseDirection;

                    Encoder e = new Encoder() { idx = (uint)i};
                    IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(e));
                    Marshal.StructureToPtr(e, ptr, true);

                    return ptr;

                }
            }

            status = NO_AVAILABLE_RESOURCES;
            return IntPtr.Zero;
        }

        public static void freeEncoder(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            halData["encoder"][GetEncoder(encoder_pointer).idx]["initialized"] = false;

            Marshal.FreeHGlobal(encoder_pointer);
        }

        public static void resetEncoder(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            halData["encoder"][GetEncoder(encoder_pointer).idx]["count"] = 0;
            halData["encoder"][GetEncoder(encoder_pointer).idx]["period "] = 0;
        }

        public static int getEncoder(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            return halData["encoder"][GetEncoder(encoder_pointer).idx]["count"];
        }

        public static double getEncoderPeriod(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            return halData["encoder"][GetEncoder(encoder_pointer).idx]["period"];
        }


        public static void setEncoderMaxPeriod(IntPtr encoder_pointer, double maxPeriod, ref int status)
        {
            status = 0;
            halData["encoder"][GetEncoder(encoder_pointer).idx]["max_period"] = maxPeriod;
        }

        public static bool getEncoderStopped(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            var enc = halData["encoder"][GetEncoder(encoder_pointer).idx];
            return enc["period"] > enc["max_period"];
        }

        public static bool getEncoderDirection(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            return halData["encoder"][GetEncoder(encoder_pointer).idx]["direction"];
        }

        public static void setEncoderReverseDirection(IntPtr encoder_pointer, bool reverseDirection, ref int status)
        {
            status = 0;
            halData["encoder"][GetEncoder(encoder_pointer).idx]["reverse_direction"] = reverseDirection;
        }

        public static void setEncoderSamplesToAverage(IntPtr encoder_pointer, uint samplesToAverage, ref int status)
        {
            status = 0;
            halData["encoder"][GetEncoder(encoder_pointer).idx]["samples_to_average"] = samplesToAverage;
        }

        public static uint getEncoderSamplesToAverage(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            return halData["encoder"][GetEncoder(encoder_pointer).idx]["samples_to_average"];
        }


        public static void setEncoderIndexSource(IntPtr encoder_pointer, uint pin, bool analogTrigger,
            bool activeHigh, bool edgeSensitive, ref int status)
        {
            status = 0;
            var enc = halData["encoder"][GetEncoder(encoder_pointer).idx]["config"];
            enc["IndexSource_Channel"] = pin;
            enc["IndexSource_Module"] = 0;
            enc["IndexSource_AnalogTrigger"] = analogTrigger;
            enc["IndexActiveHigh"] = activeHigh;
            enc["IndexEdgeSensitive"] = edgeSensitive;
        }


        public static ushort getLoopTiming(ref int status)
        {
            return (ushort) halData["pwm_loop_timing"];
        }

        public static void spiInitialize(byte port, ref int status)
        {
            throw new NotImplementedException();
        }

        public static int spiTransaction(byte port, byte[] dataToSend, byte[] dataReceived, byte size)
        {
            throw new NotImplementedException();
        }

        public static int spiWrite(byte port, byte[] dataToSend, byte sendSize)
        {
            throw new NotImplementedException();
        }

        public static int spiRead(byte port, byte[] buffer, byte count)
        {
            throw new NotImplementedException();
        }

        public static void spiClose(byte port)
        {
            throw new NotImplementedException();
        }

        public static void spiSetSpeed(byte port, uint speed)
        {
            throw new NotImplementedException();
        }

        public static void spiSetBitsPerWord(byte port, byte bpw)
        {
            throw new NotImplementedException();
        }


        public static void spiSetOpts(byte port, int msb_first, int sample_on_trailing, int clk_idle_high)
        {
            throw new NotImplementedException();
        }


        public static void spiSetChipSelectActiveHigh(byte port, ref int status)
        {
            throw new NotImplementedException();
        }


        public static void spiSetChipSelectActiveLow(byte port, ref int status)
        {
            throw new NotImplementedException();
        }

        public static int spiGetHandle(byte port)
        {
            throw new NotImplementedException();
        }

        public static void spiSetHandle(byte port, int handle)
        {
            throw new NotImplementedException();
        }

        public static IntPtr spiGetSemaphore(byte port)
        {
            throw new NotImplementedException();
        }


        public static void spiSetSemaphore(byte port, IntPtr semaphore)
        {
            throw new NotImplementedException();
        }

        public static void i2CInitialize(byte port, ref int status)
        {
            throw new NotImplementedException();
        }

        public static int i2CTransaction(byte port, byte deviceAddress, byte[] dataToSend, byte sendSize,
            byte[] dataReceived, byte receiveSize)
        {
            throw new NotImplementedException();
        }

        public static int i2CWrite(byte port, byte deviceAddress, byte[] dataToSend, byte sendSize)
        {
            throw new NotImplementedException();
        }

        public static int i2CRead(byte port, byte deviceAddress, byte[] buffer, byte count)
        {
            throw new NotImplementedException();
        }

        public static void i2CClose(byte port)
        {
            throw new NotImplementedException();
        }


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
