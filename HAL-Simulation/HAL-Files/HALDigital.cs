using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using HAL;
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

        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALDigital.InitializeDigitalPort = initializeDigitalPort;
            global::HAL.HALDigital.FreeDigitalPort = freeDigitalPort;
            global::HAL.HALDigital.CheckPWMChannel = checkPWMChannel;
            global::HAL.HALDigital.CheckRelayChannel = checkRelayChannel;
            global::HAL.HALDigital.SetPWM = setPWM;
            global::HAL.HALDigital.AllocatePWMChannel = allocatePWMChannel;
            global::HAL.HALDigital.FreePWMChannel = freePWMChannel;
            global::HAL.HALDigital.GetPWM = getPWM;
            global::HAL.HALDigital.LatchPWMZero = latchPWMZero;
            global::HAL.HALDigital.SetPWMPeriodScale = setPWMPeriodScale;
            global::HAL.HALDigital.AllocatePWM = allocatePWM;
            global::HAL.HALDigital.FreePWM = freePWM;
            global::HAL.HALDigital.SetPWMRate = setPWMRate;
            global::HAL.HALDigital.SetPWMDutyCycle = setPWMDutyCycle;
            global::HAL.HALDigital.SetPWMOutputChannel = setPWMOutputChannel;
            global::HAL.HALDigital.SetRelayForward = setRelayForward;
            global::HAL.HALDigital.SetRelayReverse = setRelayReverse;
            global::HAL.HALDigital.GetRelayForward = getRelayForward;
            global::HAL.HALDigital.GetRelayReverse = getRelayReverse;
            global::HAL.HALDigital.AllocateDIO = allocateDIO;
            global::HAL.HALDigital.FreeDIO = freeDIO;
            global::HAL.HALDigital.SetDIO = setDIO;
            global::HAL.HALDigital.GetDIO = getDIO;
            global::HAL.HALDigital.GetDIODirection = getDIODirection;
            global::HAL.HALDigital.Pulse = pulse;
            global::HAL.HALDigital.IsPulsing = isPulsing;
            global::HAL.HALDigital.IsAnyPulsing = isAnyPulsing;
            global::HAL.HALDigital.SetFilterPeriod = setFilterPeriod;
            global::HAL.HALDigital.GetFilterPeriod = getFilterPeriod;
            global::HAL.HALDigital.SetFilterSelect = setFilterSelect;
            global::HAL.HALDigital.GetFilterSelect = getFilterSelect;
            global::HAL.HALDigital.InitializeCounter = initializeCounter;
            global::HAL.HALDigital.FreeCounter = freeCounter;
            global::HAL.HALDigital.SetCounterAverageSize = setCounterAverageSize;
            global::HAL.HALDigital.SetCounterUpSource = setCounterUpSource;
            global::HAL.HALDigital.SetCounterUpSourceEdge = setCounterUpSourceEdge;
            global::HAL.HALDigital.ClearCounterUpSource = clearCounterUpSource;
            global::HAL.HALDigital.SetCounterDownSource = setCounterDownSource;
            global::HAL.HALDigital.SetCounterDownSourceEdge = setCounterDownSourceEdge;
            global::HAL.HALDigital.ClearCounterDownSource = clearCounterDownSource;
            global::HAL.HALDigital.SetCounterUpDownMode = setCounterUpDownMode;
            global::HAL.HALDigital.SetCounterExternalDirectionMode = setCounterExternalDirectionMode;
            global::HAL.HALDigital.SetCounterSemiPeriodMode = setCounterSemiPeriodMode;
            global::HAL.HALDigital.SetCounterPulseLengthMode = setCounterPulseLengthMode;
            global::HAL.HALDigital.GetCounterSamplesToAverage = getCounterSamplesToAverage;
            global::HAL.HALDigital.SetCounterSamplesToAverage = setCounterSamplesToAverage;
            global::HAL.HALDigital.ResetCounter = resetCounter;
            global::HAL.HALDigital.GetCounter = getCounter;
            global::HAL.HALDigital.GetCounterPeriod = getCounterPeriod;
            global::HAL.HALDigital.SetCounterMaxPeriod = setCounterMaxPeriod;
            global::HAL.HALDigital.SetCounterUpdateWhenEmpty = setCounterUpdateWhenEmpty;
            global::HAL.HALDigital.GetCounterStopped = getCounterStopped;
            global::HAL.HALDigital.GetCounterDirection = getCounterDirection;
            global::HAL.HALDigital.SetCounterReverseDirection = setCounterReverseDirection;
            global::HAL.HALDigital.InitializeEncoder = initializeEncoder;
            global::HAL.HALDigital.ResetEncoder = resetEncoder;
            global::HAL.HALDigital.FreeEncoder = freeEncoder;
            global::HAL.HALDigital.GetEncoder = getEncoder;
            global::HAL.HALDigital.GetEncoderPeriod = getEncoderPeriod;
            global::HAL.HALDigital.SetEncoderMaxPeriod = setEncoderMaxPeriod;
            global::HAL.HALDigital.GetEncoderStopped = getEncoderStopped;
            global::HAL.HALDigital.GetEncoderDirection = getEncoderDirection;
            global::HAL.HALDigital.SetEncoderReverseDirection = setEncoderReverseDirection;
            global::HAL.HALDigital.SetEncoderSamplesToAverage = setEncoderSamplesToAverage;
            global::HAL.HALDigital.GetEncoderSamplesToAverage = getEncoderSamplesToAverage;
            global::HAL.HALDigital.SetEncoderIndexSource = setEncoderIndexSource;
            global::HAL.HALDigital.GetLoopTiming = getLoopTiming;
            global::HAL.HALDigital.SpiInitialize = spiInitialize;
            global::HAL.HALDigital.SpiTransaction = spiTransaction;
            global::HAL.HALDigital.SpiWrite = spiWrite;
            global::HAL.HALDigital.SpiRead = spiRead;
            global::HAL.HALDigital.SpiClose = spiClose;
            global::HAL.HALDigital.SpiSetSpeed = spiSetSpeed;
            global::HAL.HALDigital.SpiSetOpts = spiSetOpts;
            global::HAL.HALDigital.SpiSetChipSelectActiveHigh = spiSetChipSelectActiveHigh;
            global::HAL.HALDigital.SpiSetChipSelectActiveLow = spiSetChipSelectActiveLow;
            global::HAL.HALDigital.SpiGetHandle = spiGetHandle;
            global::HAL.HALDigital.SpiSetHandle = spiSetHandle;
            global::HAL.HALDigital.SpiInitAccumulator = spiInitAccumulator;
            global::HAL.HALDigital.SpiFreeAccumulator = spiFreeAccumulator;
            global::HAL.HALDigital.SpiResetAccumulator = spiResetAccumulator;
            global::HAL.HALDigital.SpiSetAccumulatorCenter = spiSetAccumulatorCenter;
            global::HAL.HALDigital.SpiSetAccumulatorDeadband = spiSetAccumulatorDeadband;
            global::HAL.HALDigital.SpiGetAccumulatorLastValue = spiGetAccumulatorLastValue;
            global::HAL.HALDigital.SpiGetAccumulatorCount = spiGetAccumulatorCount;
            global::HAL.HALDigital.SpiGetAccumulatorValue = spiGetAccumulatorValue;
            global::HAL.HALDigital.SpiGetAccumulatorAverage = spiGetAccumulatorAverage;
            global::HAL.HALDigital.SpiGetAccumulatorOutput = spiGetAccumulatorOutput;
            global::HAL.HALDigital.I2CInitialize = i2CInitialize;
            global::HAL.HALDigital.I2CTransaction = i2CTransaction;
            global::HAL.HALDigital.I2CWrite = i2CWrite;
            global::HAL.HALDigital.I2CRead = i2CRead;
            global::HAL.HALDigital.I2CClose = i2CClose;
        }

        [CalledSimFunction]
        public static IntPtr initializeDigitalPort(IntPtr port_pointer, ref int status)
        {
            DigitalPort p = new DigitalPort { port = (Port)Marshal.PtrToStructure(port_pointer, typeof(Port)) };
            status = 0;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        [CalledSimFunction]
        public static void freeDigitalPort(IntPtr digital_port_pointer)
        {
            if (digital_port_pointer == IntPtr.Zero) return;
            Marshal.FreeHGlobal(digital_port_pointer);
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
        private static void setFilterPeriod(int filterIndex, uint value, ref int status)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        private static uint getFilterPeriod(int filterIndex, ref int status)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        private static void setFilterSelect(IntPtr digitalPortPointer, int filterIndex, ref int status)
        {
            throw new NotImplementedException();
        }

        [CalledSimFunction]
        private static int getFilterSelect(IntPtr digitalPortPointer, ref int status)
        {
            throw new NotImplementedException();
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
            counter.UpSourceTrigger = analogTrigger;

            if (!analogTrigger)
            {
                counter.UpSourceChannel = pin;
            }
            else
            {
                pin = pin - 1;
                uint trigIndex = (pin >> 2);
                counter.UpSourceChannel = (uint)SimData.AnalogTrigger[(int)trigIndex].AnalogPin;
            }

            if (counter.Mode == Mode.ExternalDirection)
            {
                setCounterUpSourceEdge(counter_pointer, true, false, ref status);
            }
            else if (counter.Mode == Mode.TwoPulse)
            {
                if (!analogTrigger)
                {
                    SetCounterUpAsTwoPulseDigital(counter, (int)pin);
                }
                else
                {
                    uint trigIndex = (pin >> 2);
                    SetCounterUpAsTwoPulseAnalog(counter, (int)trigIndex);
                }
                setCounterUpSourceEdge(counter_pointer, true, false, ref status);
            }
        }

        private static void SetCounterUpAsTwoPulseAnalog(CounterData counter, int trigIndex)
        {
            if (!counter.UpSourceTrigger)
            {
                throw new InvalidOperationException("Analog should only be called for IsAnalog triggers");
            }
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

            counter.UpCallback = upCallback;
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


            Counter[idx].DownSourceTrigger = analogTrigger;

            var counter = Counter[idx];

            if (!analogTrigger)
            {
                counter.DownSourceChannel = pin;
            }
            else
            {
                pin = pin - 1;
                uint trigIndex = (pin >> 2);
                counter.DownSourceChannel = (uint)SimData.AnalogTrigger[(int)trigIndex].AnalogPin;
            }

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
                    uint trigIndex = (pin >> 2);
                    SetCounterDownAsTwoPulseAnalog(counter, (int)trigIndex);
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

        private static void SetCounterDownAsTwoPulseAnalog(CounterData counter, int trigIndex)
        {
            if (!counter.DownSourceTrigger)
            {
                throw new InvalidOperationException("Analog should only be called for Analog triggers");
            }
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
            status = 0;
            SimData.SPIAccelerometer[port].Active = true;
        }

        [CalledSimFunction]
        public static int spiTransaction(byte port, byte[] dataToSend, byte[] dataReceived, byte size)
        {
            double GsPerLBS = 0.00390625;
            //We are either an ADXL345 requesting individual axis, or ADXL362 initializing
            if (size == 3)
            {
                if (dataToSend[0] == 0x0B)
                {
                    //We are an ADXL 362 initializing
                    dataReceived[2] = 0xF2;
                }
                else
                {
                    //We are an ADXL345 requesting axis
                    int axis = dataToSend[0] - (0x80 | 0x40 | 0x32);
                    byte[] b;
                    switch (axis)
                    {
                        case 1:
                            b = BitConverter.GetBytes((short)(SimData.SPIAccelerometer[port].X / GsPerLBS));
                            dataReceived[1] = b[0];
                            dataReceived[2] = b[1];
                            break;
                        case 1 << 1:
                            b = BitConverter.GetBytes((short)(SimData.SPIAccelerometer[port].Y / GsPerLBS));
                            dataReceived[1] = b[0];
                            dataReceived[2] = b[1];
                            break;
                        case 1 << 2:
                            b = BitConverter.GetBytes((short)(SimData.SPIAccelerometer[port].Z / GsPerLBS));
                            dataReceived[1] = b[0];
                            dataReceived[2] = b[1];
                            break;
                    }
                }
            }
            else if (size == 4)
            {
                //Read range to get msb
                switch (SimData.SPIAccelerometer[port].Range)
                {
                    case 0:
                        GsPerLBS = 0.001;
                        break;
                    case 1:
                        GsPerLBS = 0.002;
                        break;
                    case 2:
                    case 3:
                        GsPerLBS = 0.004;
                        break;
                }
                byte[] b;
                //We are an ADXL362 requesting an axis
                switch (dataToSend[1] - 0x0e)
                {
                    case 0:
                        b = BitConverter.GetBytes((short)(SimData.SPIAccelerometer[port].X / GsPerLBS));
                        dataReceived[2] = b[0];
                        dataReceived[3] = b[1];
                        break;
                    case 2:
                        b = BitConverter.GetBytes((short)(SimData.SPIAccelerometer[port].Y / GsPerLBS));
                        dataReceived[2] = b[0];
                        dataReceived[3] = b[1];
                        break;
                    case 4:
                        b = BitConverter.GetBytes((short)(SimData.SPIAccelerometer[port].Z / GsPerLBS));
                        dataReceived[2] = b[0];
                        dataReceived[3] = b[1];
                        break;
                }
            }
            else if (size == 7)
            {
                //ADXL345 Get All Axis
                byte[] x = BitConverter.GetBytes((short)(SimData.SPIAccelerometer[port].X / GsPerLBS));
                byte[] y = BitConverter.GetBytes((short)(SimData.SPIAccelerometer[port].Y / GsPerLBS));
                byte[] z = BitConverter.GetBytes((short)(SimData.SPIAccelerometer[port].Z / GsPerLBS));
                dataReceived[1] = x[0];
                dataReceived[2] = x[1];
                dataReceived[3] = y[0];
                dataReceived[4] = y[1];
                dataReceived[5] = z[0];
                dataReceived[6] = z[1];
            }
            else if (size == 8)
            {
                //Read range to get msb
                switch (SimData.SPIAccelerometer[port].Range)
                {
                    case 0:
                        GsPerLBS = 0.001;
                        break;
                    case 1:
                        GsPerLBS = 0.002;
                        break;
                    case 2:
                    case 3:
                        GsPerLBS = 0.004;
                        break;
                }
                //ADXL362 Get All Axis
                byte[] x = BitConverter.GetBytes((short)(SimData.SPIAccelerometer[port].X / GsPerLBS));
                byte[] y = BitConverter.GetBytes((short)(SimData.SPIAccelerometer[port].Y / GsPerLBS));
                byte[] z = BitConverter.GetBytes((short)(SimData.SPIAccelerometer[port].Z / GsPerLBS));
                dataReceived[2] = x[0];
                dataReceived[3] = x[1];
                dataReceived[4] = y[0];
                dataReceived[5] = y[1];
                dataReceived[6] = z[0];
                dataReceived[7] = z[1];
            }
            return 0;
        }

        [CalledSimFunction]
        public static int spiWrite(byte port, byte[] dataToSend, byte sendSize)
        {
            //We are An ADXL362 
            if (sendSize == 3)
            {
                if (dataToSend[1] == 0x2D)
                {
                    //Setting power control register, ignore
                    return 0;
                }
                else if (dataToSend[1] == 0x2C)
                {
                    //We are setting range
                    switch (dataToSend[2])
                    {
                        case ((byte)(0x03 | (((int)0 & 0x03) << 6))):
                            SimData.SPIAccelerometer[port].Range = 0;
                            break;
                        case ((byte)(0x03 | (((int)1 & 0x03) << 6))):
                            SimData.SPIAccelerometer[port].Range = 1;
                            break;
                        case ((byte)(0x03 | (((int)2 & 0x03) << 6))):
                            SimData.SPIAccelerometer[port].Range = 2;
                            break;
                        case ((byte)(0x03 | (((int)3 & 0x03) << 6))):
                            SimData.SPIAccelerometer[port].Range = 3;
                            break;
                    }
                }
            }
            //We are an ADXL345
            else if (sendSize == 2)
            {
                if (dataToSend[0] == 0x31)
                {
                    //We are writing range
                    switch (dataToSend[1])
                    {
                        case (0x08 | 0):
                            SimData.SPIAccelerometer[port].Range = 0;
                            break;
                        case (0x08 | 1):
                            SimData.SPIAccelerometer[port].Range = 1;
                            break;
                        case (0x08 | 2):
                            SimData.SPIAccelerometer[port].Range = 2;
                            break;
                        case (0x08 | 3):
                            SimData.SPIAccelerometer[port].Range = 3;
                            break;
                    }
                }
            }
            return 0;
        }


        [CalledSimFunction]
        public static int spiRead(byte port, byte[] buffer, byte count)
        {
            //Returning for now since nothing uses it
            return 0;
        }

        [CalledSimFunction]
        public static void spiClose(byte port)
        {
            SimData.SPIAccelerometer[port].Active = false;
        }

        [CalledSimFunction]
        public static void spiSetSpeed(byte port, uint speed)
        {
            //We don't care
            return;
        }

        [CalledSimFunction]
        public static void spiSetOpts(byte port, int msb_first, int sample_on_trailing, int clk_idle_high)
        {
            //We don't care
            return;
        }


        [CalledSimFunction]
        public static void spiSetChipSelectActiveHigh(byte port, ref int status)
        {
            //We don't care
            return;
        }


        [CalledSimFunction]
        public static void spiSetChipSelectActiveLow(byte port, ref int status)
        {
            //We don't care
            return;
        }

        [CalledSimFunction]
        public static int spiGetHandle(byte port)
        {
            //We don't care
            return 0;
        }

        [CalledSimFunction]
        public static void spiSetHandle(byte port, int handle)
        {
            //We don't care
            return;
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

        public static void spiInitAccumulator(byte port, uint period, uint cmd, byte xferSize,
            uint validMask, uint validValue, byte dataShift, byte dataSize, bool isSigned,
            bool bigEndian, ref int status)
        {
            status = 0;
            SimData.SPIAccumulator[port].Initialized = true;
        }

        public static void spiFreeAccumulator(byte port, ref int status)
        {
            status = 0;
            SimData.SPIAccumulator[port].Initialized = false;
        }

        public static void spiResetAccumulator(byte port, ref int status)
        {
            status = 0;
            SimData.SPIAccumulator[port].AccumulatorCount = 0;
            SimData.SPIAccumulator[port].AccumulatorValue = 0;
        }

        public static void spiSetAccumulatorCenter(byte port, int center, ref int status)
        {
            status = 0;
            //Ignore for now
        }

        public static void spiSetAccumulatorDeadband(byte port, int deadband, ref int status)
        {
            status = 0;
            //Ignore for now
        }

        public static int spiGetAccumulatorLastValue(byte port, ref int status)
        {
            status = 0;
            //Ignore for now
            return 0;
        }

        public static long spiGetAccumulatorValue(byte port, ref int status)
        {
            status = 0;
            return SimData.SPIAccumulator[port].AccumulatorValue;
        }

        public static uint spiGetAccumulatorCount(byte port, ref int status)
        {
            status = 0;
            return SimData.SPIAccumulator[port].AccumulatorCount;
        }

        public static double spiGetAccumulatorAverage(byte port, ref int status)
        {
            status = 0;
            return 0;
        }

        public static void spiGetAccumulatorOutput(byte port, ref long value, ref uint count, ref int status)
        {
            status = 0;
            value = SimData.SPIAccumulator[port].AccumulatorValue;
            count = SimData.SPIAccumulator[port].AccumulatorCount;
        }
    }
}
