using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using HAL_Base;
using HAL_Simulator.Data;
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
            var pwm = PWM[GetDigitalPort(digital_port_pointer).port.pin];
            pwm.RawValue = value;
            pwm.Value = MotorRawToValue(pwm);
        }

        [CalledSimFunction]
        public static bool allocatePWMChannel(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            var pin = GetDigitalPort(digital_port_pointer).port.pin;
            var mxp_port = RemapMXPPWMChannel(pin);
            if (pin >= NumHeaders)
            {
                if (MXP[mxp_port].Initialized)
                {
                    status = RESOURCE_IS_ALLOCATED;
                    return false;
                }
            }
            if (PWM[pin].Initialized)
            {
                status = RESOURCE_IS_ALLOCATED;
                return false;
            }
            PWM[pin].Initialized = true;

            if (pin > NumHeaders)
            {
                MXP[mxp_port].Initialized = true;
            }
            return true;
        }

        [CalledSimFunction]
        public static void freePWMChannel(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            var pin = GetDigitalPort(digital_port_pointer).port.pin;
            PWM[pin].Initialized = false;
            PWM[pin].RawValue = 0;
            PWM[pin].Value = 0;
            PWM[pin].PeriodScale = 0;
            PWM[pin].ZeroLatch = false;

            if (pin > NumHeaders)
            {
                var mxp_port = RemapMXPPWMChannel(pin);
                MXP[mxp_port].Initialized = false;
            }
        }

        [CalledSimFunction]
        public static ushort getPWM(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return (ushort)PWM[GetDigitalPort(digital_port_pointer).port.pin].RawValue;
        }

        [CalledSimFunction]
        public static void latchPWMZero(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            PWM[GetDigitalPort(digital_port_pointer).port.pin].ZeroLatch = true;
        }

        [CalledSimFunction]
        public static void setPWMPeriodScale(IntPtr digital_port_pointer, uint squelchMask, ref int status)
        {
            status = 0;
            PWM[GetDigitalPort(digital_port_pointer).port.pin].PeriodScale = squelchMask;
        }

        [CalledSimFunction]
        public static IntPtr allocatePWM(ref int status)
        {
            status = 0;
            bool found = false;
            int i = 0;
            for (i = 0; i < DigitalPWM.Count; i++)
            {
                if (DigitalPWM[i] == null)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                return IntPtr.Zero;

            DigitalPWM[i] = new DigitalPWMData
            {
                DutyCycle = 0,
                Pin = 0,
            };

            DigitalPWMStruct p = new DigitalPWMStruct { idx = i };
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        [CalledSimFunction]
        public static void freePWM(IntPtr pwmGenerator, ref int status)
        {
            status = 0;
            DigitalPWM[GetPWM(pwmGenerator).idx] = null;
        }



        [CalledSimFunction]
        public static void setPWMRate(double rate, ref int status)
        {
            status = 0;
            SimData.GlobalData.DigitalPWMRate = rate;
        }

        [CalledSimFunction]
        public static void setPWMDutyCycle(IntPtr pwmGenerator, double dutyCycle, ref int status)
        {
            status = 0;
            DigitalPWM[GetPWM(pwmGenerator).idx].DutyCycle = dutyCycle;
        }

        [CalledSimFunction]
        public static void setPWMOutputChannel(IntPtr pwmGenerator, uint pin, ref int status)
        {
            status = 0;
            DigitalPWM[GetPWM(pwmGenerator).idx].Pin = pin;
        }


        [CalledSimFunction]
        public static void setRelayForward(IntPtr digital_port_pointer, bool on, ref int status)
        {
            status = 0;
            var dPort = GetDigitalPort(digital_port_pointer);
            var relay = Relay[dPort.port.pin];
            relay.Initialized = true;
            relay.Forward = on;
        }

        [CalledSimFunction]
        public static void setRelayReverse(IntPtr digital_port_pointer, bool on, ref int status)
        {
            status = 0;
            var dPort = GetDigitalPort(digital_port_pointer);
            var relay = Relay[dPort.port.pin];
            relay.Initialized = true;
            relay.Reverse = on;
        }

        [CalledSimFunction]
        public static bool getRelayForward(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return Relay[GetDigitalPort(digital_port_pointer).port.pin].Forward;
        }

        [CalledSimFunction]
        public static bool getRelayReverse(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return Relay[GetDigitalPort(digital_port_pointer).port.pin].Reverse;
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
                if (MXP[mxpPort].Initialized)
                {
                    status = RESOURCE_IS_ALLOCATED;
                    return false;
                }
            }
            var dio = DIO[pin];
            if (dio.Initialized)
            {
                status = RESOURCE_IS_ALLOCATED;
                return false;
            }
            if (pin >= NumHeaders)
            {
                MXP[mxpPort].Initialized = true;
            }
            dio.Initialized = true;
            dio.IsInput = input;
            return true;
        }

        [CalledSimFunction]
        public static void freeDIO(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            var pin = GetDigitalPort(digital_port_pointer).port.pin;
            DIO[pin].Initialized = false;
            if (pin >= NumHeaders)
            {
                MXP[RemapMXPChannel(pin)].Initialized = false;
            }

        }

        [CalledSimFunction]
        public static void setDIO(IntPtr digital_port_pointer, short value, ref int status)
        {
            status = 0;
            DIO[GetDigitalPort(digital_port_pointer).port.pin].Value = value != 0;
        }

        [CalledSimFunction]
        public static bool getDIO(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return DIO[GetDigitalPort(digital_port_pointer).port.pin].Value;
        }

        [CalledSimFunction]
        public static bool getDIODirection(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return DIO[GetDigitalPort(digital_port_pointer).port.pin].IsInput;
        }

        [CalledSimFunction]
        public static void pulse(IntPtr digital_port_pointer, double pulseLength, ref int status)
        {
            status = 0;
            DIO[GetDigitalPort(digital_port_pointer).port.pin].PulseLength = pulseLength;

        }

        [CalledSimFunction]
        public static bool isPulsing(IntPtr digital_port_pointer, ref int status)
        {
            status = 0;
            return DIO[GetDigitalPort(digital_port_pointer).port.pin].PulseLength != 0;
        }

        [CalledSimFunction]
        public static bool isAnyPulsing(ref int status)
        {
            status = 0;
            return DIO.Any(p => p != null && p.PulseLength != 0);
        }

        [CalledSimFunction]
        public static IntPtr initializeCounter(Mode mode, ref uint index, ref int status)
        {
            status = 0;
            int i = 0;
            for (i = 0; i < Counter.Count; i++)
            {
                var cnt = Counter[i];
                if (!cnt.Initialized)
                {
                    cnt.Initialized = true;
                    cnt.Mode = mode;
                    cnt.UpdateWhenEmpty = false;

                    CounterStruct c = new CounterStruct() { idx = i };
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
            Counter[GetCounter(counter_pointer).idx].Initialized = false;

            Marshal.FreeHGlobal(counter_pointer);
        }

        [CalledSimFunction]
        public static void setCounterAverageSize(IntPtr counter_pointer, int size, ref int status)
        {
            status = 0;
            Counter[GetCounter(counter_pointer).idx].AverageSize = size;
        }

        [CalledSimFunction]
        public static void setCounterUpSource(IntPtr counter_pointer, uint pin, bool analogTrigger, ref int status)
        {
            var idx = GetCounter(counter_pointer).idx;
            status = 0;

            var counter = Counter[idx];
            counter.UpSourceChannel = pin;
            counter.UpSourceTrigger = analogTrigger;

            if (counter.Mode == Mode.ExternalDirection)
            {
                setCounterUpSourceEdge(counter_pointer, true, false, ref status);
            }
            else if (counter.Mode == Mode.TwoPulse)
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

        private static void SetCounterUpAsTwoPulseAnalog(CounterData counter, int pin)
        {
            if (!counter.UpSourceTrigger)
            {
                throw new InvalidOperationException("Analog should only be called for IsAnalog triggers");
            }
            pin = pin - 1;
            int trigIndex = (pin >> 2);
            int analogIn = SimData.AnalogTrigger[trigIndex].AnalogPin;//halData["analog_trigger"][trigIndex]["pin"];

            if (analogIn == -1)
            {
                throw new InvalidOperationException("Analog Trigger has not been allocated");
            }

            int status = 0;
            bool prevTrigValue =
                HALAnalog.getAnalogTriggerTriggerState((IntPtr)SimData.AnalogTrigger[trigIndex].TriggerPointer,
                    ref status);

            double prevAnalogVoltage = AnalogIn[analogIn].Voltage;//halData["analog_in"][analogIn]["voltage"];

            Action<string, dynamic> upCallback = (key, value) =>
            {
                //If our IsAnalog has actually changed
                if (prevAnalogVoltage != value)
                {
                    //Grab our trigger state.
                    bool trigValue =
                        HALAnalog.getAnalogTriggerTriggerState(
                            (IntPtr)SimData.AnalogTrigger[trigIndex].TriggerPointer, ref status);

                    //Was low
                    if (!prevTrigValue)
                    {
                        //if still low ignore
                        if (!trigValue)
                            return;
                        //Otherwise if we count on rising edge add 1
                        if (counter.UpRisingEdge)
                        {
                            counter.Count++;
                        }
                    }
                    //Was High
                    else
                    {
                        //if still high ignore
                        if (trigValue)
                            return;
                        //Otherwise if we count on falling edge add 1
                        if (counter.UpFallingEdge)
                        {
                            counter.Count++;
                        }
                    }
                    prevTrigValue = trigValue;
                    prevAnalogVoltage = value;
                }
            };

            counter.UpCallback= upCallback;
            AnalogIn[analogIn].Register("Voltage", upCallback);
        }

        private static void SetCounterUpAsTwoPulseDigital(CounterData counter, int pin)
        {
            if (counter.UpSourceTrigger)
            {
                throw new InvalidOperationException("Digital should only be called for digital ports");
            }

            bool prevValue = DIO[pin].Value;

            Action<string, dynamic> upCallback = (key, value) =>
            {
                //Was low
                if (!prevValue)
                {
                    //if still low ignore
                    if (!value)
                        return;
                    //Otherwise if we count on rising edge add 1
                    if (counter.UpRisingEdge)
                    {
                        counter.Count++;
                    }
                }
                //Was High
                else
                {
                    //if still high ignore
                    if (value)
                        return;
                    //Otherwise if we count on falling edge add 1
                    if (counter.UpFallingEdge)
                    {
                        counter.Count++;
                    }
                }
                prevValue = value;
            };

            counter.UpCallback = upCallback;
            DIO[pin].Register("Value", upCallback);
        }

        [CalledSimFunction]
        public static void setCounterUpSourceEdge(IntPtr counter_pointer, bool risingEdge, bool fallingEdge,
            ref int status)
        {
            status = 0;
            var idx = GetCounter(counter_pointer).idx;
            Counter[idx].UpRisingEdge = risingEdge;
            Counter[idx].UpFallingEdge = fallingEdge;
        }

        [CalledSimFunction]
        public static void clearCounterUpSource(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            var counter = Counter[GetCounter((counter_pointer)).idx];

            if (counter.UpCallback != null)
            {
                if (counter.UpSourceTrigger)
                {
                    AnalogIn[(int)counter.UpSourceChannel].Cancel("Voltage", counter.UpCallback);
                }
                else
                {
                    DIO[(int)counter.UpSourceChannel].Cancel("Value", counter.UpCallback);
                }
                
                counter.UpCallback = null;
            }

            counter.UpRisingEdge = false;
            counter.UpFallingEdge = false;
            counter.UpSourceChannel = 0;
            counter.UpSourceTrigger = false;
			
			
        }

        [CalledSimFunction]
        public static void setCounterDownSource(IntPtr counter_pointer, uint pin, bool analogTrigger, ref int status)
        {
            var idx = GetCounter(counter_pointer).idx;
            status = 0;

            if (Counter[idx].Mode != Mode.ExternalDirection &&
                Counter[idx].Mode != Mode.TwoPulse)
            {
                status = PARAMETER_OUT_OF_RANGE;
                return;
            }

            Counter[idx].DownSourceChannel = pin;
            Counter[idx].DownSourceTrigger = analogTrigger;

            var counter = Counter[idx];

            if (counter.Mode == Mode.ExternalDirection)
            {
            }
            else if (counter.Mode == Mode.TwoPulse)
            {
                if (!analogTrigger)
                {
                    SetCounterDownAsTwoPulseDigital(counter, (int)pin);
                }
                else
                {
                    SetCounterDownAsTwoPulseAnalog(counter, (int)pin);
                }
            }
        }

        private static void SetCounterDownAsTwoPulseDigital(CounterData counter, int pin)
        {
            bool prevValue = DIO[(int)pin].Value;

            Action<string, dynamic> downCallback = (key, value) =>
            {
                //Was low
                if (!prevValue)
                {
                    //if still low ignore
                    if (!value)
                        return;
                    //Otherwise if we count on rising edge add 1
                    if (counter.DownRisingEdge)
                    {
                        counter.Count--;
                    }
                }
                //Was High
                else
                {
                    //if still high ignore
                    if (value)
                        return;
                    //Otherwise if we count on falling edge add 1
                    if (counter.DownFallingEdge)
                    {
                        counter.Count--;
                    }
                }
                prevValue = value;
            };

            counter.DownCallback = downCallback;

            DIO[(int)pin].Register("Value", downCallback);
        }

        private static void SetCounterDownAsTwoPulseAnalog(CounterData counter, int pin)
        {
            if (!counter.DownSourceTrigger)
            {
                throw new InvalidOperationException("Analog should only be called for Analog triggers");
            }
            pin = pin - 1;
            int trigIndex = (pin >> 2);
            int analogIn = SimData.AnalogTrigger[trigIndex].AnalogPin;

            if (analogIn == -1)
            {
                throw new InvalidOperationException("Analog Trigger has not been allocated");
            }

            int status = 0;
            bool prevTrigValue =
                HALAnalog.getAnalogTriggerTriggerState((IntPtr)SimData.AnalogTrigger[trigIndex].TriggerPointer,
                    ref status);

            double prevAnalogVoltage = AnalogIn[analogIn].Voltage;

            Action<dynamic, dynamic> downCallback = (key, value) =>
            {
                //If our IsAnalog has actually changed
                if (prevAnalogVoltage != value)
                {
                    //Grab our trigger state.
                    bool trigValue =
                        HALAnalog.getAnalogTriggerTriggerState(
                            (IntPtr)SimData.AnalogTrigger[trigIndex].TriggerPointer, ref status);

                    //Was low
                    if (!prevTrigValue)
                    {
                        //if still low ignore
                        if (!trigValue)
                            return;
                        //Otherwise if we count on rising edge add 1
                        if (counter.DownRisingEdge)
                        {
                            counter.Count--;
                        }
                    }
                    //Was High
                    else
                    {
                        //if still high ignore
                        if (trigValue)
                            return;
                        //Otherwise if we count on falling edge add 1
                        if (counter.DownFallingEdge)
                        {
                            counter.Count--;
                        }
                    }
                    prevTrigValue = trigValue;
                    prevAnalogVoltage = value;
                }
            };

            counter.DownCallback = downCallback;

            AnalogIn[analogIn].Register("Voltage", downCallback);
        }

        [CalledSimFunction]
        public static void setCounterDownSourceEdge(IntPtr counter_pointer, bool risingEdge, bool fallingEdge,
            ref int status)
        {
            status = 0;
            var idx = GetCounter(counter_pointer).idx;
            Counter[idx].DownRisingEdge = risingEdge;
            Counter[idx].DownFallingEdge = fallingEdge;
        }

        [CalledSimFunction]
        public static void clearCounterDownSource(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            var counter = Counter[GetCounter((counter_pointer)).idx];

            if (counter.DownCallback != null)
            {
                if (counter.DownSourceTrigger)
                {
                    AnalogIn[(int)counter.DownSourceChannel].Cancel("Voltage", counter.DownCallback);
                }
                else
                {
                    DIO[(int)counter.DownSourceChannel].Cancel("Value", counter.DownCallback);
                }
                
                counter.DownCallback = null;
            }

            counter.DownRisingEdge = false;
            counter.DownFallingEdge = false;
            counter.DownSourceChannel = 0;
            counter.DownSourceTrigger = false;
			
			
        }

        [CalledSimFunction]
        public static void setCounterUpDownMode(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            Counter[GetCounter(counter_pointer).idx].Mode = (int)Mode.TwoPulse;
        }

        [CalledSimFunction]
        public static void setCounterExternalDirectionMode(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            Counter[GetCounter(counter_pointer).idx].Mode = Mode.ExternalDirection;
        }

        [CalledSimFunction]
        public static void setCounterSemiPeriodMode(IntPtr counter_pointer, bool highSemiPeriod, ref int status)
        {
            status = 0;
            var counter = Counter[GetCounter(counter_pointer).idx];
            counter.Mode = Mode.Semiperiod;
            counter.UpRisingEdge = highSemiPeriod;
            counter.UpdateWhenEmpty = false;
        }

        [CalledSimFunction]
        public static void setCounterPulseLengthMode(IntPtr counter_pointer, double threshold, ref int status)
        {
            status = 0;
            var counter = Counter[GetCounter(counter_pointer).idx];
            counter.Mode = Mode.PulseLength;
            counter.PulseLengthThreshold = threshold;
        }

        [CalledSimFunction]
        public static int getCounterSamplesToAverage(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            return (int)Counter[GetCounter(counter_pointer).idx].SamplesToAverage;
        }

        [CalledSimFunction]
        public static void setCounterSamplesToAverage(IntPtr counter_pointer, int samplesToAverage, ref int status)
        {
            status = 0;
            Counter[GetCounter(counter_pointer).idx].SamplesToAverage = (uint)samplesToAverage;
        }

        [CalledSimFunction]
        public static void resetCounter(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            Counter[GetCounter(counter_pointer).idx].Count = 0;
            Counter[GetCounter(counter_pointer).idx].Period = double.MaxValue;
            Counter[GetCounter(counter_pointer).idx].Reset = true;
        }


        [CalledSimFunction]
        public static int getCounter(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            return Counter[GetCounter(counter_pointer).idx].Count;
        }

        [CalledSimFunction]
        public static double getCounterPeriod(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            return Counter[GetCounter(counter_pointer).idx].Period;
        }



        [CalledSimFunction]
        public static void setCounterMaxPeriod(IntPtr counter_pointer, double maxPeriod, ref int status)
        {
            status = 0;
            Counter[GetCounter(counter_pointer).idx].MaxPeriod = maxPeriod;
        }

        [CalledSimFunction]
        public static void setCounterUpdateWhenEmpty(IntPtr counter_pointer, bool enabled, ref int status)
        {
            status = 0;
            Counter[GetCounter(counter_pointer).idx].UpdateWhenEmpty = enabled;
        }

        [CalledSimFunction]
        public static bool getCounterStopped(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            var cnt = Counter[GetCounter(counter_pointer).idx];
            return cnt.Period > cnt.MaxPeriod;
        }

        [CalledSimFunction]
        public static bool getCounterDirection(IntPtr counter_pointer, ref int status)
        {
            status = 0;
            return Counter[GetCounter(counter_pointer).idx].Direction;
        }

        [CalledSimFunction]
        public static void setCounterReverseDirection(IntPtr counter_pointer, bool reverseDirection, ref int status)
        {
            status = 0;
            Counter[GetCounter(counter_pointer).idx].ReverseDirection = reverseDirection;
        }


        [CalledSimFunction]
        public static IntPtr initializeEncoder(byte port_a_module, uint port_a_pin, bool port_a_analog_trigger,
            byte port_b_module, uint port_b_pin, bool port_b_analog_trigger, bool reverseDirection, ref int index,
            ref int status)
        {
            status = 0;
            for (int i = 0; i < Encoder.Count; i++)
            {
                var enc = Encoder[i];
                if (!enc.Initialized)
                {
                    enc.Initialized = true;
                    enc.Config = new Dictionary<string, dynamic>()
                    {
                        ["ASource_Module"] = port_a_module,
                        ["ASource_Channel"] = port_a_pin,
                        ["ASource_AnalogTrigger"] = port_a_analog_trigger,
                        ["BSource_Module"] = port_b_module,
                        ["BSource_Channel"] = port_b_pin,
                        ["BSource_AnalogTrigger"] = port_b_analog_trigger,
                    };

                    enc.ReverseDirection = reverseDirection;

                    EncoderStruct e = new EncoderStruct { idx = i };
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
            Encoder[GetEncoder(encoder_pointer).idx].Initialized = false;

            Marshal.FreeHGlobal(encoder_pointer);
        }

        [CalledSimFunction]
        public static void resetEncoder(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            Encoder[GetEncoder(encoder_pointer).idx].Count = 0;
            Encoder[GetEncoder(encoder_pointer).idx].Period = double.MaxValue;
            Encoder[GetEncoder(encoder_pointer).idx].Reset = true;
        }

        [CalledSimFunction]
        public static int getEncoder(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            return (int)Encoder[GetEncoder(encoder_pointer).idx].Count;
        }

        [CalledSimFunction]
        public static double getEncoderPeriod(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            return (double)Encoder[GetEncoder(encoder_pointer).idx].Period;
        }


        [CalledSimFunction]
        public static void setEncoderMaxPeriod(IntPtr encoder_pointer, double maxPeriod, ref int status)
        {
            status = 0;
            Encoder[GetEncoder(encoder_pointer).idx].MaxPeriod = maxPeriod;
        }

        [CalledSimFunction]
        public static bool getEncoderStopped(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            var enc = Encoder[GetEncoder(encoder_pointer).idx];
            return enc.Period > enc.MaxPeriod;
        }

        [CalledSimFunction]
        public static bool getEncoderDirection(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            return Encoder[GetEncoder(encoder_pointer).idx].Direction;
        }

        [CalledSimFunction]
        public static void setEncoderReverseDirection(IntPtr encoder_pointer, bool reverseDirection, ref int status)
        {
            status = 0;
            Encoder[GetEncoder(encoder_pointer).idx].ReverseDirection = reverseDirection;
        }

        [CalledSimFunction]
        public static void setEncoderSamplesToAverage(IntPtr encoder_pointer, uint samplesToAverage, ref int status)
        {
            status = 0;
            Encoder[GetEncoder(encoder_pointer).idx].SamplesToAverage = samplesToAverage;
        }

        [CalledSimFunction]
        public static uint getEncoderSamplesToAverage(IntPtr encoder_pointer, ref int status)
        {
            status = 0;
            return Encoder[GetEncoder(encoder_pointer).idx].SamplesToAverage;
        }


        [CalledSimFunction]
        public static void setEncoderIndexSource(IntPtr encoder_pointer, uint pin, bool analogTrigger,
            bool activeHigh, bool edgeSensitive, ref int status)
        {
            status = 0;
            var enc = Encoder[GetEncoder(encoder_pointer).idx].Config;
            enc["IndexSource_Channel"] = pin;
            enc["IndexSource_Module"] = 0;
            enc["IndexSource_AnalogTrigger"] = analogTrigger;
            enc["IndexActiveHigh"] = activeHigh;
            enc["IndexEdgeSensitive"] = edgeSensitive;
        }


        [CalledSimFunction]
        public static ushort getLoopTiming(ref int status)
        {
            return SimData.GlobalData.PWMLoopTiming;
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
