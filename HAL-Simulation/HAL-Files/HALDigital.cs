using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HAL_Base;
using static HAL_Simulator.PortConverters;
using static HAL_Simulator.SimData;
using static HAL_Simulator.PWMHelpers;
using static HAL_Simulator.HALErrorConstants;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    internal class HALDigital
    {
        internal const int ExpectedLoopTiming = 40;
        internal const int DigitalPins = 26;
        internal const int PwmPins = 20;
        internal const int RelayPins = 4;
        internal const int NumHeaders = 10;

        [CalledSimFunction]
        public static IntPtr initializeDigitalPort(IntPtr port_pointer, ref int status)
        {
            DigitalPort p = new DigitalPort { port = (Port)Marshal.PtrToStructure(port_pointer, typeof(Port)) };
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


        [CalledSimFunction]
        public static bool checkPWMChannel(IntPtr digital_port_pointer)
        {
            var dPort = GetDigitalPort(digital_port_pointer);
            return dPort.port.pin < PwmPins;
        }


        [CalledSimFunction]
        public static bool checkRelayChannel(IntPtr digital_port_pointer)
        {
            var dPort = GetDigitalPort(digital_port_pointer);
            return dPort.port.pin < RelayPins;
        }


        [CalledSimFunction]
        public static void setPWM(IntPtr digital_port_pointer, ushort value, ref int status)
        {
            status = 0;
            var dPort = GetDigitalPort(digital_port_pointer);
            halData["pwm"][dPort.port.pin]["raw_value"] = value;
            halData["pwm"][dPort.port.pin]["value"] = ReverseByType(dPort.port.pin);
        }

        [CalledSimFunction]
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

        [CalledSimFunction]
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

        [CalledSimFunction]
        public static ushort getPWM(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return (ushort)halData["pwm"][GetDigitalPort(digital_port_pointer).port.pin]["raw_value"];
        }

        [CalledSimFunction]
        public static void latchPWMZero(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            halData["pwm"][GetDigitalPort(digital_port_pointer).port.pin]["zero_latch"] = true;
        }

        [CalledSimFunction]
        public static void setPWMPeriodScale(IntPtr digital_port_pointer, uint squelchMask, ref int status)
        {
            status = 0;
            halData["pwm"][GetDigitalPort(digital_port_pointer).port.pin]["period_scale"] = squelchMask;
        }

        [CalledSimFunction]
        public static IntPtr allocatePWM(ref int status)
        {
            status = 0;
            bool found = false;
            int i = 0;
            for (i = 0; i < halData["d0_pwm"].Count; i++)
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

            PWM p = new PWM { idx = i };
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        [CalledSimFunction]
        public static void freePWM(IntPtr pwmGenerator, ref int status)
        {
            status = 0;
            halData["d0_pwm"][GetPWM(pwmGenerator).idx] = null;
        }



        [CalledSimFunction]
        public static void setPWMRate(double rate, ref int status)
        {
            status = 0;
            halData["d0_pwm_rate"] = rate;
        }

        [CalledSimFunction]
        public static void setPWMDutyCycle(IntPtr pwmGenerator, double dutyCycle, ref int status)
        {
            status = 0;
            halData["d0_pwm"][GetPWM(pwmGenerator).idx]["duty_cycle"] = dutyCycle;
        }

        [CalledSimFunction]
        public static void setPWMOutputChannel(IntPtr pwmGenerator, uint pin, ref int status)
        {
            status = 0;
            halData["d0_pwm"][GetPWM(pwmGenerator).idx]["pin"] = pin;
        }


        [CalledSimFunction]
        public static void setRelayForward(IntPtr digital_port_pointer, bool on, ref int status)
        {
            status = 0;
            var dPort = GetDigitalPort(digital_port_pointer);
            var relay = halData["relay"][dPort.port.pin];
            relay["initialized"] = true;
            relay["fwd"] = on;
        }

        [CalledSimFunction]
        public static void setRelayReverse(IntPtr digital_port_pointer, bool on, ref int status)
        {
            status = 0;
            var dPort = GetDigitalPort(digital_port_pointer);
            var relay = halData["relay"][dPort.port.pin];
            relay["initialized"] = true;
            relay["rev"] = on;
        }

        [CalledSimFunction]
        public static bool getRelayForward(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return halData["relay"][GetDigitalPort(digital_port_pointer).port.pin]["fwd"];
        }

        [CalledSimFunction]
        public static bool getRelayReverse(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return halData["relay"][GetDigitalPort(digital_port_pointer).port.pin]["rev"];
        }

        [CalledSimFunction]
        public static bool allocateDIO(IntPtr digital_port_pointer, bool input,
            ref int status)
        {
            status = 0;
            var pin = GetDigitalPort(digital_port_pointer).port.pin;
            var mxpPort = RemapMXPChannel(pin);
            if (pin >= NumHeaders)
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
            if (pin >= NumHeaders)
            {
                halData["mxp"][mxpPort]["initialized"] = true;
            }
            dio["initialized"] = true;
            dio["is_input"] = input;
            return true;
        }

        [CalledSimFunction]
        public static void freeDIO(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            var pin = GetDigitalPort(digital_port_pointer).port.pin;
            halData["dio"][pin]["initialized"] = false;
            if (pin >= NumHeaders)
            {
                halData["mxp"][RemapMXPChannel(pin)]["initialized"] = false;
            }

        }

        [CalledSimFunction]
        public static void setDIO(IntPtr digital_port_pointer, short value, ref int status)
        {
            status = 0;
            halData["dio"][GetDigitalPort(digital_port_pointer).port.pin]["value"] = value != 0;
        }

        [CalledSimFunction]
        public static bool getDIO(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return halData["dio"][GetDigitalPort(digital_port_pointer).port.pin]["value"];
        }

        [CalledSimFunction]
        public static bool getDIODirection(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return halData["dio"][GetDigitalPort(digital_port_pointer).port.pin]["is_input"];
        }

        [CalledSimFunction]
        public static void pulse(IntPtr digital_port_pointer, double pulseLength, ref int status)
        {
            status = 0;
            halData["dio"][GetDigitalPort(digital_port_pointer).port.pin]["pulse_length"] = pulseLength;

        }

        [CalledSimFunction]
        public static bool isPulsing(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return halData["dio"][GetDigitalPort(digital_port_pointer).port.pin]["pulse_length"] != 0;
        }

        [CalledSimFunction]
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

        [CalledSimFunction]
        public static IntPtr initializeCounter(Mode mode, ref uint index, ref int status)
        {
            status = 0;
            int i = 0;
            for (i = 0; i < halData["counter"].Count; i++)
            {
                var cnt = halData["counter"][i];
                if (!cnt["initialized"])
                {
                    cnt["initialized"] = true;
                    cnt["mode"] = (int)mode;
                    cnt["update_when_empty"] = false;

                    Counter c = new Counter() { idx = i };
                    IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(c));
                    Marshal.StructureToPtr(c, ptr, true);
                    index = (uint)i;

                    return ptr;

                }
            }

            status = NO_AVAILABLE_RESOURCES;
            return IntPtr.Zero;
        }

        [CalledSimFunction]
        public static void freeCounter(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            clearCounterUpSource(counter_pointer, ref status);
            clearCounterDownSource(counter_pointer, ref status);
            halData["counter"][GetCounter(counter_pointer).idx]["initialized"] = false;

            Marshal.FreeHGlobal(counter_pointer);
        }

        [CalledSimFunction]
        public static void setCounterAverageSize(IntPtr counter_pointer, int size, ref int status)
        {
            status = 0;
            halData["counter"][GetCounter(counter_pointer).idx]["average_size"] = size;
        }

        [CalledSimFunction]
        public static void setCounterUpSource(IntPtr counter_pointer, uint pin, bool analogTrigger, ref int status)
        {
            var idx = GetCounter(counter_pointer).idx;
            status = 0;

            var counter = halData["counter"][idx];
            counter["up_source_channel"] = (int)pin;
            counter["up_source_trigger"] = analogTrigger;

            if (counter["mode"] == (int) Mode.ExternalDirection)
            {
                setCounterUpSourceEdge(counter_pointer, true, false, ref status);
            }
            else if (counter["mode"] == (int)Mode.TwoPulse)
            {
                if (!analogTrigger)
                {
                    SetCounterUpAsTwoPulseDigital(counter, (int) pin);
                }
                else
                {
                    SetCounterUpAsTwoPulseAnalog(counter, (int) pin);
                }
                setCounterUpSourceEdge(counter_pointer, true, false, ref status);
            }
        }

        private static void SetCounterUpAsTwoPulseAnalog(dynamic counter, int pin)
        {
            if (!counter["up_source_trigger"])
            {
                throw new InvalidOperationException("Analog should only be called for analog triggers");
            }
            pin = pin - 1;
            int trigIndex = (pin >> 2);
            int analogIn = halData["analog_trigger"][trigIndex]["pin"];

            if (analogIn == -1)
            {
                throw new InvalidOperationException("Analog Trigger has not been allocated");
            }

            int status = 0;
            bool prevTrigValue =
                HALAnalog.getAnalogTriggerTriggerState((IntPtr) halData["analog_trigger"][trigIndex]["pointer"],
                    ref status);

            double prevAnalogVoltage = halData["analog_in"][analogIn]["voltage"];

            Action<dynamic, dynamic> upCallback = (key, value) =>
            {
                //If our analog has actually changed
                if (prevAnalogVoltage != value)
                {
                    //Grab our trigger state.
                    bool trigValue =
                        HALAnalog.getAnalogTriggerTriggerState(
                            (IntPtr) halData["analog_trigger"][trigIndex]["pointer"], ref status);

                    //Was low
                    if (!prevTrigValue)
                    {
                        //if still low ignore
                        if (!trigValue)
                            return;
                        //Otherwise if we count on rising edge add 1
                        if (counter["up_rising_edge"])
                        {
                            counter["count"]++;
                        }
                    }
                    //Was High
                    else
                    {
                        //if still high ignore
                        if (trigValue)
                            return;
                        //Otherwise if we count on falling edge add 1
                        if (counter["up_falling_edge"])
                        {
                            counter["count"]++;
                        }
                    }
                    prevTrigValue = trigValue;
                    prevAnalogVoltage = value;
                }
            };

            counter["up_callback"] = upCallback;

            halData["analog_in"][analogIn].Register("voltage", upCallback);
        }

        private static void SetCounterUpAsTwoPulseDigital(dynamic counter, int pin)
        {
            if (counter["up_source_trigger"])
            {
                throw new InvalidOperationException("Digital should only be called for digital ports");
            }

            bool prevValue = halData["dio"][pin]["value"];

            Action<dynamic, dynamic> upCallback = (key, value) =>
            {
                //Was low
                if (!prevValue)
                {
                    //if still low ignore
                    if (!value)
                        return;
                    //Otherwise if we count on rising edge add 1
                    if (counter["up_rising_edge"])
                    {
                        counter["count"]++;
                    }
                }
                //Was High
                else
                {
                    //if still high ignore
                    if (value)
                        return;
                    //Otherwise if we count on falling edge add 1
                    if (counter["up_falling_edge"])
                    {
                        counter["count"]++;
                    }
                }
                prevValue = value;
            };

            counter["up_callback"] = upCallback;

            halData["dio"][pin].Register("value", upCallback);
        }

        [CalledSimFunction]
        public static void setCounterUpSourceEdge(IntPtr counter_pointer, bool risingEdge, bool fallingEdge,
            ref int status)
        {
            status = 0;
            var idx = GetCounter(counter_pointer).idx;
            halData["counter"][idx]["up_rising_edge"] = risingEdge;
            halData["counter"][idx]["up_falling_edge"] = fallingEdge;
        }

        [CalledSimFunction]
        public static void clearCounterUpSource(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            var counter = halData["counter"][GetCounter((counter_pointer)).idx];

            if (counter.ContainsKey("up_callback") && counter["up_callback"] != null)
            {
                if (counter["up_source_trigger"])
                {
                    halData["analog_in"][counter["up_source_channel"]].Cancel("voltage", counter["up_callback"]);
                }
                else
                {
                    halData["dio"][counter["up_source_channel"]].Cancel("value", counter["up_callback"]);
                }
                
                counter["up_callback"] = null;
            }

            counter["up_rising_edge"] = false;
            counter["up_falling_edge"] = false;
            counter["up_source_channel"] = 0;
            counter["up_source_trigger"] = false;
			
			
        }

        [CalledSimFunction]
        public static void setCounterDownSource(IntPtr counter_pointer, uint pin, bool analogTrigger, ref int status)
        {
            var idx = GetCounter(counter_pointer).idx;
            status = 0;

            if (halData["counter"][idx]["mode"] != (int)Mode.ExternalDirection &&
                halData["counter"][idx]["mode"] != (int)Mode.TwoPulse)
            {
                status = PARAMETER_OUT_OF_RANGE;
                return;
            }

            halData["counter"][idx]["down_source_channel"] = (int)pin;
            halData["counter"][idx]["down_source_trigger"] = analogTrigger;

            var counter = halData["counter"][idx];

            if (counter["mode"] == (int)Mode.ExternalDirection)
            {
            }
            else if (counter["mode"] == (int)Mode.TwoPulse)
            {
                if (!analogTrigger)
                {
                    SetCounterDownAsTwoPulseDigital(counter, (int)pin);
                }
                else
                {
                    SetCounterDownAsTwoPulseAnalog(counter, (int)pin);
                }
                //SetCounterDownAsTwoPulseDigital(counter, (int)pin);
            }


            


        }

        private static void SetCounterDownAsTwoPulseDigital(dynamic counter, int pin)
        {
            bool prevValue = halData["dio"][(int)pin]["value"];

            Action<dynamic, dynamic> downCallback = (key, value) =>
            {
                //Was low
                if (!prevValue)
                {
                    //if still low ignore
                    if (!value)
                        return;
                    //Otherwise if we count on rising edge add 1
                    if (counter["down_rising_edge"])
                    {
                        counter["count"]--;
                    }
                }
                //Was High
                else
                {
                    //if still high ignore
                    if (value)
                        return;
                    //Otherwise if we count on falling edge add 1
                    if (counter["down_falling_edge"])
                    {
                        counter["count"]--;
                    }
                }
                prevValue = value;
            };

            counter["down_callback"] = downCallback;

            halData["dio"][(int)pin].Register("value", downCallback);
        }

        private static void SetCounterDownAsTwoPulseAnalog(dynamic counter, int pin)
        {
            if (!counter["down_source_trigger"])
            {
                throw new InvalidOperationException("Analog should only be called for analog triggers");
            }
            pin = pin - 1;
            int trigIndex = (pin >> 2);
            int analogIn = halData["analog_trigger"][trigIndex]["pin"];

            if (analogIn == -1)
            {
                throw new InvalidOperationException("Analog Trigger has not been allocated");
            }

            int status = 0;
            bool prevTrigValue =
                HALAnalog.getAnalogTriggerTriggerState((IntPtr)halData["analog_trigger"][trigIndex]["pointer"],
                    ref status);

            double prevAnalogVoltage = halData["analog_in"][analogIn]["voltage"];

            Action<dynamic, dynamic> downCallback = (key, value) =>
            {
                //If our analog has actually changed
                if (prevAnalogVoltage != value)
                {
                    //Grab our trigger state.
                    bool trigValue =
                        HALAnalog.getAnalogTriggerTriggerState(
                            (IntPtr)halData["analog_trigger"][trigIndex]["pointer"], ref status);

                    //Was low
                    if (!prevTrigValue)
                    {
                        //if still low ignore
                        if (!trigValue)
                            return;
                        //Otherwise if we count on rising edge add 1
                        if (counter["down_rising_edge"])
                        {
                            counter["count"]--;
                        }
                    }
                    //Was High
                    else
                    {
                        //if still high ignore
                        if (trigValue)
                            return;
                        //Otherwise if we count on falling edge add 1
                        if (counter["down_falling_edge"])
                        {
                            counter["count"]--;
                        }
                    }
                    prevTrigValue = trigValue;
                    prevAnalogVoltage = value;
                }
            };

            counter["down_callback"] = downCallback;

            halData["analog_in"][analogIn].Register("voltage", downCallback);
        }

        [CalledSimFunction]
        public static void setCounterDownSourceEdge(IntPtr counter_pointer, bool risingEdge, bool fallingEdge,
            ref int status)
        {
            status = 0;
            var idx = GetCounter(counter_pointer).idx;
            halData["counter"][idx]["down_rising_edge"] = risingEdge;
            halData["counter"][idx]["down_falling_edge"] = fallingEdge;
        }

        [CalledSimFunction]
        public static void clearCounterDownSource(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            var counter = halData["counter"][GetCounter((counter_pointer)).idx];

            if (counter.ContainsKey("down_callback") && counter["down_callback"] != null)
            {
                if (counter["down_source_trigger"])
                {
                    halData["analog_in"][counter["down_source_channel"]].Cancel("voltage", counter["down_callback"]);
                }
                else
                {
                    halData["dio"][counter["down_source_channel"]].Cancel("value", counter["down_callback"]);
                }
                
                counter["down_callback"] = null;
            }

            counter["down_rising_edge"] = false;
            counter["down_falling_edge"] = false;
            counter["down_source_channel"] = 0;
            counter["down_source_trigger"] = false;
			
			
        }

        [CalledSimFunction]
        public static void setCounterUpDownMode(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            halData["counter"][GetCounter(counter_pointer).idx]["mode"] = (int)Mode.TwoPulse;
        }

        [CalledSimFunction]
        public static void setCounterExternalDirectionMode(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            halData["counter"][GetCounter(counter_pointer).idx]["mode"] = (int)Mode.ExternalDirection;
        }

        [CalledSimFunction]
        public static void setCounterSemiPeriodMode(IntPtr counter_pointer, bool highSemiPeriod, ref int status)
        {
            status = 0;
            var counter = halData["counter"][GetCounter(counter_pointer).idx];
            counter["mode"] = (int)Mode.Semiperiod;
            counter["up_rising_edge"] = highSemiPeriod;
            counter["update_when_empty"] = false;
        }

        [CalledSimFunction]
        public static void setCounterPulseLengthMode(IntPtr counter_pointer, double threshold, ref int status)
        {
            status = 0;
            var counter = halData["counter"][GetCounter(counter_pointer).idx];
            counter["mode"] = (int)Mode.PulseLength;
            counter["pulse_length_threshold"] = threshold;
        }

        [CalledSimFunction]
        public static int getCounterSamplesToAverage(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            return (int)halData["counter"][GetCounter(counter_pointer).idx]["samples_to_average"];
        }

        [CalledSimFunction]
        public static void setCounterSamplesToAverage(IntPtr counter_pointer, int samplesToAverage, ref int status)
        {
            status = 0;
            halData["counter"][GetCounter(counter_pointer).idx]["samples_to_average"] = samplesToAverage;
        }

        [CalledSimFunction]
        public static void resetCounter(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            halData["counter"][GetCounter(counter_pointer).idx]["count"] = 0;
            halData["counter"][GetCounter(counter_pointer).idx]["period"] = double.MaxValue;
            halData["counter"][GetCounter(counter_pointer).idx]["reset"] = true;
        }


        [CalledSimFunction]
        public static int getCounter(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            return halData["counter"][GetCounter(counter_pointer).idx]["count"];
        }

        [CalledSimFunction]
        public static double getCounterPeriod(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            return halData["counter"][GetCounter(counter_pointer).idx]["period"];
        }



        [CalledSimFunction]
        public static void setCounterMaxPeriod(IntPtr counter_pointer, double maxPeriod, ref int status)
        {
            status = 0;
            halData["counter"][GetCounter(counter_pointer).idx]["max_period"] = maxPeriod;
        }

        [CalledSimFunction]
        public static void setCounterUpdateWhenEmpty(IntPtr counter_pointer, bool enabled, ref int status)
        {
            status = 0;
            halData["counter"][GetCounter(counter_pointer).idx]["update_when_empty"] = enabled;
        }

        [CalledSimFunction]
        public static bool getCounterStopped(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            var cnt = halData["counter"][GetCounter(counter_pointer).idx];
            return cnt["period"] > cnt["max_period"];
        }

        [CalledSimFunction]
        public static bool getCounterDirection(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            return halData["counter"][GetCounter(counter_pointer).idx]["direction"];
        }

        [CalledSimFunction]
        public static void setCounterReverseDirection(IntPtr counter_pointer, bool reverseDirection, ref int status)
        {
            status = 0;
            halData["counter"][GetCounter(counter_pointer).idx]["reverse_direction"] = reverseDirection;
        }


        [CalledSimFunction]
        public static IntPtr initializeEncoder(byte port_a_module, uint port_a_pin, bool port_a_analog_trigger,
            byte port_b_module, uint port_b_pin, bool port_b_analog_trigger, bool reverseDirection, ref int index,
            ref int status)
        {
            status = 0;
            for (int i = 0; i < halData["encoder"].Count; i++)
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

                    Encoder e = new Encoder { idx = i };
                    IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(e));
                    Marshal.StructureToPtr(e, ptr, true);

                    return ptr;

                }
            }

            status = NO_AVAILABLE_RESOURCES;
            return IntPtr.Zero;
        }

        [CalledSimFunction]
        public static void freeEncoder(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            halData["encoder"][GetEncoder(encoder_pointer).idx]["initialized"] = false;

            Marshal.FreeHGlobal(encoder_pointer);
        }

        [CalledSimFunction]
        public static void resetEncoder(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            halData["encoder"][GetEncoder(encoder_pointer).idx]["count"] = 0;
            halData["encoder"][GetEncoder(encoder_pointer).idx]["period "] = double.MaxValue;
            halData["encoder"][GetEncoder(encoder_pointer).idx]["reset"] = true;
        }

        [CalledSimFunction]
        public static int getEncoder(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            return (int)halData["encoder"][GetEncoder(encoder_pointer).idx]["count"];
        }

        [CalledSimFunction]
        public static double getEncoderPeriod(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            return (double)halData["encoder"][GetEncoder(encoder_pointer).idx]["period"];
        }


        [CalledSimFunction]
        public static void setEncoderMaxPeriod(IntPtr encoder_pointer, double maxPeriod, ref int status)
        {
            status = 0;
            halData["encoder"][GetEncoder(encoder_pointer).idx]["max_period"] = maxPeriod;
        }

        [CalledSimFunction]
        public static bool getEncoderStopped(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            var enc = halData["encoder"][GetEncoder(encoder_pointer).idx];
            return enc["period"] > enc["max_period"];
        }

        [CalledSimFunction]
        public static bool getEncoderDirection(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            return halData["encoder"][GetEncoder(encoder_pointer).idx]["direction"];
        }

        [CalledSimFunction]
        public static void setEncoderReverseDirection(IntPtr encoder_pointer, bool reverseDirection, ref int status)
        {
            status = 0;
            halData["encoder"][GetEncoder(encoder_pointer).idx]["reverse_direction"] = reverseDirection;
        }

        [CalledSimFunction]
        public static void setEncoderSamplesToAverage(IntPtr encoder_pointer, uint samplesToAverage, ref int status)
        {
            status = 0;
            halData["encoder"][GetEncoder(encoder_pointer).idx]["samples_to_average"] = samplesToAverage;
        }

        [CalledSimFunction]
        public static uint getEncoderSamplesToAverage(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            return halData["encoder"][GetEncoder(encoder_pointer).idx]["samples_to_average"];
        }


        [CalledSimFunction]
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


        [CalledSimFunction]
        public static ushort getLoopTiming(ref int status)
        {
            return (ushort)halData["pwm_loop_timing"];
        }

        [CalledSimFunction]
        public static void spiInitialize(byte port, ref int status)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static int spiTransaction(byte port, byte[] dataToSend, byte[] dataReceived, byte size)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static int spiWrite(byte port, byte[] dataToSend, byte sendSize)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static int spiRead(byte port, byte[] buffer, byte count)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static void spiClose(byte port)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static void spiSetSpeed(byte port, uint speed)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static void spiSetBitsPerWord(byte port, byte bpw)
        {
            throw new NotImplementedException();
        }


        [CalledSimFunction]
        public static void spiSetOpts(byte port, int msb_first, int sample_on_trailing, int clk_idle_high)
        {
            throw new NotImplementedException();
        }


        [CalledSimFunction]
        public static void spiSetChipSelectActiveHigh(byte port, ref int status)
        {
            throw new NotImplementedException();
        }


        [CalledSimFunction]
        public static void spiSetChipSelectActiveLow(byte port, ref int status)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static int spiGetHandle(byte port)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static void spiSetHandle(byte port, int handle)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static IntPtr spiGetSemaphore(byte port)
        {
            throw new NotImplementedException();
        }


        [CalledSimFunction]
        public static void spiSetSemaphore(byte port, IntPtr semaphore)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static void i2CInitialize(byte port, ref int status)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static int i2CTransaction(byte port, byte deviceAddress, byte[] dataToSend, byte sendSize,
            byte[] dataReceived, byte receiveSize)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static int i2CWrite(byte port, byte deviceAddress, byte[] dataToSend, byte sendSize)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static int i2CRead(byte port, byte deviceAddress, byte[] buffer, byte count)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        public static void i2CClose(byte port)
        {
            throw new NotImplementedException();
        }
    }
}
