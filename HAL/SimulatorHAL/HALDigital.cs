using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.Simulator;
using HAL.Simulator.Data;
using static HAL.Simulator.SimData;
// ReSharper disable CompareOfFloatsByEqualityOperator

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
#pragma warning disable 1591

namespace HAL.SimulatorHAL
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
            Base.HALDigital.InitializeDigitalPort = initializeDigitalPort;
            Base.HALDigital.FreeDigitalPort = freeDigitalPort;
            Base.HALDigital.CheckPWMChannel = checkPWMChannel;
            Base.HALDigital.CheckRelayChannel = checkRelayChannel;
            Base.HALDigital.SetPWM = setPWM;
            Base.HALDigital.AllocatePWMChannel = allocatePWMChannel;
            Base.HALDigital.FreePWMChannel = freePWMChannel;
            Base.HALDigital.GetPWM = getPWM;
            Base.HALDigital.LatchPWMZero = latchPWMZero;
            Base.HALDigital.SetPWMPeriodScale = setPWMPeriodScale;
            Base.HALDigital.AllocatePWM = allocatePWM;
            Base.HALDigital.FreePWM = freePWM;
            Base.HALDigital.SetPWMRate = setPWMRate;
            Base.HALDigital.SetPWMDutyCycle = setPWMDutyCycle;
            Base.HALDigital.SetPWMOutputChannel = setPWMOutputChannel;
            Base.HALDigital.SetRelayForward = setRelayForward;
            Base.HALDigital.SetRelayReverse = setRelayReverse;
            Base.HALDigital.GetRelayForward = getRelayForward;
            Base.HALDigital.GetRelayReverse = getRelayReverse;
            Base.HALDigital.AllocateDIO = allocateDIO;
            Base.HALDigital.FreeDIO = freeDIO;
            Base.HALDigital.SetDIO = setDIO;
            Base.HALDigital.GetDIO = getDIO;
            Base.HALDigital.GetDIODirection = getDIODirection;
            Base.HALDigital.Pulse = pulse;
            Base.HALDigital.IsPulsing = isPulsing;
            Base.HALDigital.IsAnyPulsing = isAnyPulsing;
            Base.HALDigital.SetFilterPeriod = setFilterPeriod;
            Base.HALDigital.GetFilterPeriod = getFilterPeriod;
            Base.HALDigital.SetFilterSelect = setFilterSelect;
            Base.HALDigital.GetFilterSelect = getFilterSelect;
            Base.HALDigital.InitializeCounter = initializeCounter;
            Base.HALDigital.FreeCounter = freeCounter;
            Base.HALDigital.SetCounterAverageSize = setCounterAverageSize;
            Base.HALDigital.SetCounterUpSource = setCounterUpSource;
            Base.HALDigital.SetCounterUpSourceEdge = setCounterUpSourceEdge;
            Base.HALDigital.ClearCounterUpSource = clearCounterUpSource;
            Base.HALDigital.SetCounterDownSource = setCounterDownSource;
            Base.HALDigital.SetCounterDownSourceEdge = setCounterDownSourceEdge;
            Base.HALDigital.ClearCounterDownSource = clearCounterDownSource;
            Base.HALDigital.SetCounterUpDownMode = setCounterUpDownMode;
            Base.HALDigital.SetCounterExternalDirectionMode = setCounterExternalDirectionMode;
            Base.HALDigital.SetCounterSemiPeriodMode = setCounterSemiPeriodMode;
            Base.HALDigital.SetCounterPulseLengthMode = setCounterPulseLengthMode;
            Base.HALDigital.GetCounterSamplesToAverage = getCounterSamplesToAverage;
            Base.HALDigital.SetCounterSamplesToAverage = setCounterSamplesToAverage;
            Base.HALDigital.ResetCounter = resetCounter;
            Base.HALDigital.GetCounter = getCounter;
            Base.HALDigital.GetCounterPeriod = getCounterPeriod;
            Base.HALDigital.SetCounterMaxPeriod = setCounterMaxPeriod;
            Base.HALDigital.SetCounterUpdateWhenEmpty = setCounterUpdateWhenEmpty;
            Base.HALDigital.GetCounterStopped = getCounterStopped;
            Base.HALDigital.GetCounterDirection = getCounterDirection;
            Base.HALDigital.SetCounterReverseDirection = setCounterReverseDirection;
            Base.HALDigital.InitializeEncoder = initializeEncoder;
            Base.HALDigital.ResetEncoder = resetEncoder;
            Base.HALDigital.FreeEncoder = freeEncoder;
            Base.HALDigital.GetEncoder = getEncoder;
            Base.HALDigital.GetEncoderPeriod = getEncoderPeriod;
            Base.HALDigital.SetEncoderMaxPeriod = setEncoderMaxPeriod;
            Base.HALDigital.GetEncoderStopped = getEncoderStopped;
            Base.HALDigital.GetEncoderDirection = getEncoderDirection;
            Base.HALDigital.SetEncoderReverseDirection = setEncoderReverseDirection;
            Base.HALDigital.SetEncoderSamplesToAverage = setEncoderSamplesToAverage;
            Base.HALDigital.GetEncoderSamplesToAverage = getEncoderSamplesToAverage;
            Base.HALDigital.SetEncoderIndexSource = setEncoderIndexSource;
            Base.HALDigital.GetLoopTiming = getLoopTiming;
            Base.HALDigital.SpiInitialize = spiInitialize;
            Base.HALDigital.SpiTransaction = spiTransaction;
            Base.HALDigital.SpiWrite = spiWrite;
            Base.HALDigital.SpiRead = spiRead;
            Base.HALDigital.SpiClose = spiClose;
            Base.HALDigital.SpiSetSpeed = spiSetSpeed;
            Base.HALDigital.SpiSetOpts = spiSetOpts;
            Base.HALDigital.SpiSetChipSelectActiveHigh = spiSetChipSelectActiveHigh;
            Base.HALDigital.SpiSetChipSelectActiveLow = spiSetChipSelectActiveLow;
            Base.HALDigital.SpiGetHandle = spiGetHandle;
            Base.HALDigital.SpiSetHandle = spiSetHandle;
            Base.HALDigital.SpiInitAccumulator = spiInitAccumulator;
            Base.HALDigital.SpiFreeAccumulator = spiFreeAccumulator;
            Base.HALDigital.SpiResetAccumulator = spiResetAccumulator;
            Base.HALDigital.SpiSetAccumulatorCenter = spiSetAccumulatorCenter;
            Base.HALDigital.SpiSetAccumulatorDeadband = spiSetAccumulatorDeadband;
            Base.HALDigital.SpiGetAccumulatorLastValue = spiGetAccumulatorLastValue;
            Base.HALDigital.SpiGetAccumulatorCount = spiGetAccumulatorCount;
            Base.HALDigital.SpiGetAccumulatorValue = spiGetAccumulatorValue;
            Base.HALDigital.SpiGetAccumulatorAverage = spiGetAccumulatorAverage;
            Base.HALDigital.SpiGetAccumulatorOutput = spiGetAccumulatorOutput;
            Base.HALDigital.I2CInitialize = i2CInitialize;
            Base.HALDigital.I2CTransaction = i2CTransaction;
            Base.HALDigital.I2CWrite = i2CWrite;
            Base.HALDigital.I2CRead = i2CRead;
            Base.HALDigital.I2CClose = i2CClose;
        }

        [CalledSimFunction]
        public static DigitalPortSafeHandle initializeDigitalPort(HALPortSafeHandle port_pointer, ref int status)
        {
            DigitalPort p = new DigitalPort
            {
                port = PortConverters.GetHalPort(port_pointer)
            };
            status = 0;
            return new DigitalPortSafeHandle(p);
        }

        [CalledSimFunction]
        public static void freeDigitalPort(DigitalPortSafeHandle digital_port_pointer)
        {
            //Do Nothing
            return;
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
        public static bool checkPWMChannel(DigitalPortSafeHandle digital_port_pointer)
        {
            var dPort = PortConverters.GetDigitalPort(digital_port_pointer);
            return dPort.port.pin < PwmPins;
        }


        [CalledSimFunction]
        public static bool checkRelayChannel(DigitalPortSafeHandle digital_port_pointer)
        {
            var dPort = PortConverters.GetDigitalPort(digital_port_pointer);
            return dPort.port.pin < RelayPins;
        }


        [CalledSimFunction]
        public static void setPWM(DigitalPortSafeHandle digital_port_pointer, ushort value, ref int status)
        {
            status = 0;
            var pwm = PWM[PortConverters.GetDigitalPort(digital_port_pointer).port.pin];
            pwm.RawValue = value;
            pwm.Value = PWMHelpers.MotorRawToValue(pwm);
        }

        [CalledSimFunction]
        public static bool allocatePWMChannel(DigitalPortSafeHandle digital_port_pointer, ref int status)
        {
            status = 0;
            var pin = PortConverters.GetDigitalPort(digital_port_pointer).port.pin;
            var mxp_port = RemapMXPPWMChannel(pin);
            if (pin >= NumHeaders)
            {
                if (MXP[mxp_port].Initialized)
                {
                    status = HALErrorConstants.RESOURCE_IS_ALLOCATED;
                    return false;
                }
            }
            if (PWM[pin].Initialized)
            {
                status = HALErrorConstants.RESOURCE_IS_ALLOCATED;
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
        public static void freePWMChannel(DigitalPortSafeHandle digital_port_pointer, ref int status)
        {
            status = 0;
            var pin = PortConverters.GetDigitalPort(digital_port_pointer).port.pin;
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
        public static ushort getPWM(DigitalPortSafeHandle digital_port_pointer, ref int status)
        {
            status = 0;
            return (ushort)PWM[PortConverters.GetDigitalPort(digital_port_pointer).port.pin].RawValue;
        }

        [CalledSimFunction]
        public static void latchPWMZero(DigitalPortSafeHandle digital_port_pointer, ref int status)
        {
            status = 0;
            PWM[PortConverters.GetDigitalPort(digital_port_pointer).port.pin].ZeroLatch = true;
        }

        [CalledSimFunction]
        public static void setPWMPeriodScale(DigitalPortSafeHandle digital_port_pointer, uint squelchMask, ref int status)
        {
            status = 0;
            PWM[PortConverters.GetDigitalPort(digital_port_pointer).port.pin].PeriodScale = squelchMask;
        }

        [CalledSimFunction]
        public static IntPtr allocatePWM(ref int status)
        {
            status = 0;
            int i = 0;
            for (i = 0; i < DigitalPWM.Count; i++)
            {
                var cnt = DigitalPWM[i];
                if (!cnt.Initialized)
                {
                    cnt.Initialized = true;
                    cnt.DutyCycle = 0;

                    return (IntPtr) i;
                }
            }

            return (IntPtr) ~0;
        }

        [CalledSimFunction]
        public static void freePWM(IntPtr pwmGenerator, ref int status)
        {
            status = 0;
            int id = pwmGenerator.ToInt32();
            if (id == ~0) return;
            DigitalPWM[id].Initialized = false;
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
            int id = pwmGenerator.ToInt32();
            if (id == ~0) return;
            DigitalPWM[id].DutyCycle = dutyCycle;
        }

        [CalledSimFunction]
        public static void setPWMOutputChannel(IntPtr pwmGenerator, uint pin, ref int status)
        {
            status = 0;
            int id = pwmGenerator.ToInt32();
            if (id == ~0) return;
            DigitalPWM[id].Pin = pin;
        }


        [CalledSimFunction]
        public static void setRelayForward(DigitalPortSafeHandle digital_port_pointer, bool on, ref int status)
        {
            status = 0;
            var dPort = PortConverters.GetDigitalPort(digital_port_pointer);
            var relay = Relay[dPort.port.pin];
            relay.Initialized = true;
            relay.Forward = on;
        }

        [CalledSimFunction]
        public static void setRelayReverse(DigitalPortSafeHandle digital_port_pointer, bool on, ref int status)
        {
            status = 0;
            var dPort = PortConverters.GetDigitalPort(digital_port_pointer);
            var relay = Relay[dPort.port.pin];
            relay.Initialized = true;
            relay.Reverse = on;
        }

        [CalledSimFunction]
        public static bool getRelayForward(DigitalPortSafeHandle digital_port_pointer, ref int status)
        {
            status = 0;
            return Relay[PortConverters.GetDigitalPort(digital_port_pointer).port.pin].Forward;
        }

        [CalledSimFunction]
        public static bool getRelayReverse(DigitalPortSafeHandle digital_port_pointer, ref int status)
        {
            status = 0;
            return Relay[PortConverters.GetDigitalPort(digital_port_pointer).port.pin].Reverse;
        }

        [CalledSimFunction]
        public static bool allocateDIO(DigitalPortSafeHandle digital_port_pointer, bool input,
            ref int status)
        {
            status = 0;
            var pin = PortConverters.GetDigitalPort(digital_port_pointer).port.pin;
            var mxpPort = RemapMXPChannel(pin);
            if (pin >= NumHeaders)
            {
                if (MXP[mxpPort].Initialized)
                {
                    status = HALErrorConstants.RESOURCE_IS_ALLOCATED;
                    return false;
                }
            }
            var dio = DIO[pin];
            if (dio.Initialized)
            {
                status = HALErrorConstants.RESOURCE_IS_ALLOCATED;
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
        public static void freeDIO(DigitalPortSafeHandle digital_port_pointer, ref int status)
        {
            status = 0;
            var pin = PortConverters.GetDigitalPort(digital_port_pointer).port.pin;
            DIO[pin].Initialized = false;
            if (pin >= NumHeaders)
            {
                MXP[RemapMXPChannel(pin)].Initialized = false;
            }

        }

        [CalledSimFunction]
        public static void setDIO(DigitalPortSafeHandle digital_port_pointer, short value, ref int status)
        {
            status = 0;
            DIO[PortConverters.GetDigitalPort(digital_port_pointer).port.pin].Value = value != 0;
        }

        [CalledSimFunction]
        public static bool getDIO(DigitalPortSafeHandle digital_port_pointer, ref int status)
        {
            status = 0;
            return DIO[PortConverters.GetDigitalPort(digital_port_pointer).port.pin].Value;
        }

        [CalledSimFunction]
        public static bool getDIODirection(DigitalPortSafeHandle digital_port_pointer, ref int status)
        {
            status = 0;
            return DIO[PortConverters.GetDigitalPort(digital_port_pointer).port.pin].IsInput;
        }

        [CalledSimFunction]
        public static void pulse(DigitalPortSafeHandle digital_port_pointer, double pulseLength, ref int status)
        {
            status = 0;
            DIO[PortConverters.GetDigitalPort(digital_port_pointer).port.pin].PulseLength = pulseLength;

        }

        [CalledSimFunction]
        public static bool isPulsing(DigitalPortSafeHandle digital_port_pointer, ref int status)
        {
            status = 0;
            return DIO[PortConverters.GetDigitalPort(digital_port_pointer).port.pin].PulseLength != 0;
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
            if (filterIndex < 0 || filterIndex > 2)
            {
                status = HALErrorConstants.PARAMETER_OUT_OF_RANGE;
                return;
            }

            status = 0;
            DigitalGlitchFilter[filterIndex].Period = value;
        }

        [CalledSimFunction]
        private static uint getFilterPeriod(int filterIndex, ref int status)
        {
            if (filterIndex < 0 || filterIndex > 2)
            {
                status = HALErrorConstants.PARAMETER_OUT_OF_RANGE;
                return 0;
            }

            status = 0;
            return DigitalGlitchFilter[filterIndex].Period;
        }

        [CalledSimFunction]
        private static void setFilterSelect(DigitalPortSafeHandle digitalPortPointer, int filterIndex, ref int status)
        {
            DigitalPort port = PortConverters.GetDigitalPort(digitalPortPointer);

            if (filterIndex < 0 || filterIndex > 3)
            {
                status = HALErrorConstants.PARAMETER_OUT_OF_RANGE;
                return;
            }

            if (filterIndex == 0)
            {
                filterIndex = SimData.DIO[port.port.pin].FilterIndex;
                SimData.DIO[port.port.pin].FilterIndex = -1;
                SimData.DigitalGlitchFilter[filterIndex].Enabled = false;
            }
            else
            {
                filterIndex = filterIndex - 1;
                DigitalGlitchFilter[filterIndex].Enabled = true;
                DIO[port.port.pin].FilterIndex = filterIndex;
            }
            status = 0;
        }

        [CalledSimFunction]
        private static int getFilterSelect(DigitalPortSafeHandle digitalPortPointer, ref int status)
        {
            status = 0;
            int filterIdx = DIO[PortConverters.GetDigitalPort(digitalPortPointer).port.pin].FilterIndex;
            if (filterIdx == -1) return 0;
            return filterIdx + 1; 
        }

        [CalledSimFunction]
        public static CounterSafeHandle initializeCounter(Mode mode, ref uint index, ref int status)
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
                    index = (uint)i;

                    return new CounterSafeHandle(c);

                }
            }

            status = HALErrorConstants.NO_AVAILABLE_RESOURCES;
            return null;
        }

        [CalledSimFunction]
        public static void freeCounter(CounterSafeHandle counter_pointer, ref int status)
        {
            status = 0;
            clearCounterUpSource(counter_pointer, ref status);
            clearCounterDownSource(counter_pointer, ref status);
            Counter[PortConverters.GetCounter(counter_pointer).idx].Initialized = false;

            //Marshal.FreeHGlobal(counter_pointer);
        }

        [CalledSimFunction]
        public static void setCounterAverageSize(CounterSafeHandle counter_pointer, int size, ref int status)
        {
            status = 0;
            Counter[PortConverters.GetCounter(counter_pointer).idx].AverageSize = size;
        }

        [CalledSimFunction]
        public static void setCounterUpSource(CounterSafeHandle counter_pointer, uint pin, bool analogTrigger, ref int status)
        {
            var idx = PortConverters.GetCounter(counter_pointer).idx;
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
                uint trigIndex = pin >> 2;
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
                    uint trigIndex = pin >> 2;
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
                HALAnalog.getAnalogTriggerTriggerState(SimData.AnalogTrigger[trigIndex].TriggerPointer,
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
                            SimData.AnalogTrigger[trigIndex].TriggerPointer, ref status);

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
        public static void setCounterUpSourceEdge(CounterSafeHandle counter_pointer, bool risingEdge, bool fallingEdge,
            ref int status)
        {
            status = 0;
            var idx = PortConverters.GetCounter(counter_pointer).idx;
            Counter[idx].UpRisingEdge = risingEdge;
            Counter[idx].UpFallingEdge = fallingEdge;
        }

        [CalledSimFunction]
        public static void clearCounterUpSource(CounterSafeHandle counter_pointer, ref int status)
        {
            status = 0;
            var counter = Counter[PortConverters.GetCounter(counter_pointer).idx];

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
        public static void setCounterDownSource(CounterSafeHandle counter_pointer, uint pin, bool analogTrigger, ref int status)
        {
            var idx = PortConverters.GetCounter(counter_pointer).idx;
            status = 0;

            if (Counter[idx].Mode != Mode.ExternalDirection &&
                Counter[idx].Mode != Mode.TwoPulse)
            {
                status = HALErrorConstants.PARAMETER_OUT_OF_RANGE;
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
                uint trigIndex = pin >> 2;
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
                    uint trigIndex = pin >> 2;
                    SetCounterDownAsTwoPulseAnalog(counter, (int)trigIndex);
                }
            }
        }

        private static void SetCounterDownAsTwoPulseDigital(CounterData counter, int pin)
        {
            bool prevValue = DIO[pin].Value;

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

            DIO[pin].Register("Value", downCallback);
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
                HALAnalog.getAnalogTriggerTriggerState(SimData.AnalogTrigger[trigIndex].TriggerPointer,
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
                            SimData.AnalogTrigger[trigIndex].TriggerPointer, ref status);

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
        public static void setCounterDownSourceEdge(CounterSafeHandle counter_pointer, bool risingEdge, bool fallingEdge,
            ref int status)
        {
            status = 0;
            var idx = PortConverters.GetCounter(counter_pointer).idx;
            Counter[idx].DownRisingEdge = risingEdge;
            Counter[idx].DownFallingEdge = fallingEdge;
        }

        [CalledSimFunction]
        public static void clearCounterDownSource(CounterSafeHandle counter_pointer, ref int status)
        {
            status = 0;
            var counter = Counter[PortConverters.GetCounter(counter_pointer).idx];

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
        public static void setCounterUpDownMode(CounterSafeHandle counter_pointer, ref int status)
        {
            status = 0;
            Counter[PortConverters.GetCounter(counter_pointer).idx].Mode = (int)Mode.TwoPulse;
        }

        [CalledSimFunction]
        public static void setCounterExternalDirectionMode(CounterSafeHandle counter_pointer, ref int status)
        {
            status = 0;
            Counter[PortConverters.GetCounter(counter_pointer).idx].Mode = Mode.ExternalDirection;
        }

        [CalledSimFunction]
        public static void setCounterSemiPeriodMode(CounterSafeHandle counter_pointer, bool highSemiPeriod, ref int status)
        {
            status = 0;
            var counter = Counter[PortConverters.GetCounter(counter_pointer).idx];
            counter.Mode = Mode.Semiperiod;
            counter.UpRisingEdge = highSemiPeriod;
            counter.UpdateWhenEmpty = false;
        }

        [CalledSimFunction]
        public static void setCounterPulseLengthMode(CounterSafeHandle counter_pointer, double threshold, ref int status)
        {
            status = 0;
            var counter = Counter[PortConverters.GetCounter(counter_pointer).idx];
            counter.Mode = Mode.PulseLength;
            counter.PulseLengthThreshold = threshold;
        }

        [CalledSimFunction]
        public static int getCounterSamplesToAverage(CounterSafeHandle counter_pointer, ref int status)
        {
            status = 0;
            return (int)Counter[PortConverters.GetCounter(counter_pointer).idx].SamplesToAverage;
        }

        [CalledSimFunction]
        public static void setCounterSamplesToAverage(CounterSafeHandle counter_pointer, int samplesToAverage, ref int status)
        {
            status = 0;
            Counter[PortConverters.GetCounter(counter_pointer).idx].SamplesToAverage = (uint)samplesToAverage;
        }

        [CalledSimFunction]
        public static void resetCounter(CounterSafeHandle counter_pointer, ref int status)
        {
            status = 0;
            Counter[PortConverters.GetCounter(counter_pointer).idx].Count = 0;
            Counter[PortConverters.GetCounter(counter_pointer).idx].Period = double.MaxValue;
            Counter[PortConverters.GetCounter(counter_pointer).idx].Reset = true;
        }


        [CalledSimFunction]
        public static int getCounter(CounterSafeHandle counter_pointer, ref int status)
        {
            status = 0;
            return Counter[PortConverters.GetCounter(counter_pointer).idx].Count;
        }

        [CalledSimFunction]
        public static double getCounterPeriod(CounterSafeHandle counter_pointer, ref int status)
        {
            status = 0;
            return Counter[PortConverters.GetCounter(counter_pointer).idx].Period;
        }



        [CalledSimFunction]
        public static void setCounterMaxPeriod(CounterSafeHandle counter_pointer, double maxPeriod, ref int status)
        {
            status = 0;
            Counter[PortConverters.GetCounter(counter_pointer).idx].MaxPeriod = maxPeriod;
        }

        [CalledSimFunction]
        public static void setCounterUpdateWhenEmpty(CounterSafeHandle counter_pointer, bool enabled, ref int status)
        {
            status = 0;
            Counter[PortConverters.GetCounter(counter_pointer).idx].UpdateWhenEmpty = enabled;
        }

        [CalledSimFunction]
        public static bool getCounterStopped(CounterSafeHandle counter_pointer, ref int status)
        {
            status = 0;
            var cnt = Counter[PortConverters.GetCounter(counter_pointer).idx];
            return cnt.Period > cnt.MaxPeriod;
        }

        [CalledSimFunction]
        public static bool getCounterDirection(CounterSafeHandle counter_pointer, ref int status)
        {
            status = 0;
            return Counter[PortConverters.GetCounter(counter_pointer).idx].Direction;
        }

        [CalledSimFunction]
        public static void setCounterReverseDirection(CounterSafeHandle counter_pointer, bool reverseDirection, ref int status)
        {
            status = 0;
            Counter[PortConverters.GetCounter(counter_pointer).idx].ReverseDirection = reverseDirection;
        }


        [CalledSimFunction]
        public static EncoderSafeHandle initializeEncoder(byte port_a_module, uint port_a_pin, bool port_a_analog_trigger,
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

                    return new EncoderSafeHandle(e);

                }
            }

            status = HALErrorConstants.NO_AVAILABLE_RESOURCES;
            return null;
        }

        [CalledSimFunction]
        public static void freeEncoder(EncoderSafeHandle encoder_pointer, ref int status)
        {
            status = 0;
            Encoder[PortConverters.GetEncoder(encoder_pointer).idx].Initialized = false;

            //Marshal.FreeHGlobal(encoder_pointer);
        }

        [CalledSimFunction]
        public static void resetEncoder(EncoderSafeHandle encoder_pointer, ref int status)
        {
            status = 0;
            Encoder[PortConverters.GetEncoder(encoder_pointer).idx].Count = 0;
            Encoder[PortConverters.GetEncoder(encoder_pointer).idx].Period = double.MaxValue;
            Encoder[PortConverters.GetEncoder(encoder_pointer).idx].Reset = true;
        }

        [CalledSimFunction]
        public static int getEncoder(EncoderSafeHandle encoder_pointer, ref int status)
        {
            status = 0;
            return Encoder[PortConverters.GetEncoder(encoder_pointer).idx].Count;
        }

        [CalledSimFunction]
        public static double getEncoderPeriod(EncoderSafeHandle encoder_pointer, ref int status)
        {
            status = 0;
            return Encoder[PortConverters.GetEncoder(encoder_pointer).idx].Period;
        }


        [CalledSimFunction]
        public static void setEncoderMaxPeriod(EncoderSafeHandle encoder_pointer, double maxPeriod, ref int status)
        {
            status = 0;
            Encoder[PortConverters.GetEncoder(encoder_pointer).idx].MaxPeriod = maxPeriod;
        }

        [CalledSimFunction]
        public static bool getEncoderStopped(EncoderSafeHandle encoder_pointer, ref int status)
        {
            status = 0;
            var enc = Encoder[PortConverters.GetEncoder(encoder_pointer).idx];
            return enc.Period > enc.MaxPeriod;
        }

        [CalledSimFunction]
        public static bool getEncoderDirection(EncoderSafeHandle encoder_pointer, ref int status)
        {
            status = 0;
            return Encoder[PortConverters.GetEncoder(encoder_pointer).idx].Direction;
        }

        [CalledSimFunction]
        public static void setEncoderReverseDirection(EncoderSafeHandle encoder_pointer, bool reverseDirection, ref int status)
        {
            status = 0;
            Encoder[PortConverters.GetEncoder(encoder_pointer).idx].ReverseDirection = reverseDirection;
        }

        [CalledSimFunction]
        public static void setEncoderSamplesToAverage(EncoderSafeHandle encoder_pointer, uint samplesToAverage, ref int status)
        {
            status = 0;
            Encoder[PortConverters.GetEncoder(encoder_pointer).idx].SamplesToAverage = samplesToAverage;
        }

        [CalledSimFunction]
        public static uint getEncoderSamplesToAverage(EncoderSafeHandle encoder_pointer, ref int status)
        {
            status = 0;
            return Encoder[PortConverters.GetEncoder(encoder_pointer).idx].SamplesToAverage;
        }


        [CalledSimFunction]
        public static void setEncoderIndexSource(EncoderSafeHandle encoder_pointer, uint pin, bool analogTrigger,
            bool activeHigh, bool edgeSensitive, ref int status)
        {
            status = 0;
            var enc = Encoder[PortConverters.GetEncoder(encoder_pointer).idx].Config;
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
            SPIAccelerometer[port].Active = true;
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
                            b = BitConverter.GetBytes((short)(SPIAccelerometer[port].X / GsPerLBS));
                            dataReceived[1] = b[0];
                            dataReceived[2] = b[1];
                            break;
                        case 1 << 1:
                            b = BitConverter.GetBytes((short)(SPIAccelerometer[port].Y / GsPerLBS));
                            dataReceived[1] = b[0];
                            dataReceived[2] = b[1];
                            break;
                        case 1 << 2:
                            b = BitConverter.GetBytes((short)(SPIAccelerometer[port].Z / GsPerLBS));
                            dataReceived[1] = b[0];
                            dataReceived[2] = b[1];
                            break;
                    }
                }
            }
            else if (size == 4)
            {
                //Read range to get msb
                switch (SPIAccelerometer[port].Range)
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
                        b = BitConverter.GetBytes((short)(SPIAccelerometer[port].X / GsPerLBS));
                        dataReceived[2] = b[0];
                        dataReceived[3] = b[1];
                        break;
                    case 2:
                        b = BitConverter.GetBytes((short)(SPIAccelerometer[port].Y / GsPerLBS));
                        dataReceived[2] = b[0];
                        dataReceived[3] = b[1];
                        break;
                    case 4:
                        b = BitConverter.GetBytes((short)(SPIAccelerometer[port].Z / GsPerLBS));
                        dataReceived[2] = b[0];
                        dataReceived[3] = b[1];
                        break;
                }
            }
            else if (size == 7)
            {
                //ADXL345 Get All Axis
                byte[] x = BitConverter.GetBytes((short)(SPIAccelerometer[port].X / GsPerLBS));
                byte[] y = BitConverter.GetBytes((short)(SPIAccelerometer[port].Y / GsPerLBS));
                byte[] z = BitConverter.GetBytes((short)(SPIAccelerometer[port].Z / GsPerLBS));
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
                switch (SPIAccelerometer[port].Range)
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
                byte[] x = BitConverter.GetBytes((short)(SPIAccelerometer[port].X / GsPerLBS));
                byte[] y = BitConverter.GetBytes((short)(SPIAccelerometer[port].Y / GsPerLBS));
                byte[] z = BitConverter.GetBytes((short)(SPIAccelerometer[port].Z / GsPerLBS));
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
                        case 0x03 | ((0 & 0x03) << 6):
                            SPIAccelerometer[port].Range = 0;
                            break;
                        case 0x03 | ((1 & 0x03) << 6):
                            SPIAccelerometer[port].Range = 1;
                            break;
                        case 0x03 | ((2 & 0x03) << 6):
                            SPIAccelerometer[port].Range = 2;
                            break;
                        case 0x03 | ((3 & 0x03) << 6):
                            SPIAccelerometer[port].Range = 3;
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
                        case 0x08 | 0:
                            SPIAccelerometer[port].Range = 0;
                            break;
                        case 0x08 | 1:
                            SPIAccelerometer[port].Range = 1;
                            break;
                        case 0x08 | 2:
                            SPIAccelerometer[port].Range = 2;
                            break;
                        case 0x08 | 3:
                            SPIAccelerometer[port].Range = 3;
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
            SPIAccelerometer[port].Active = false;
        }

        [CalledSimFunction]
        public static void spiSetSpeed(byte port, uint speed)
        {
            //We don't care
        }

        [CalledSimFunction]
        public static void spiSetOpts(byte port, int msb_first, int sample_on_trailing, int clk_idle_high)
        {
            //We don't care
        }


        [CalledSimFunction]
        public static void spiSetChipSelectActiveHigh(byte port, ref int status)
        {
            //We don't care
        }


        [CalledSimFunction]
        public static void spiSetChipSelectActiveLow(byte port, ref int status)
        {
            //We don't care
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
            SPIAccumulator[port].Initialized = true;
        }

        public static void spiFreeAccumulator(byte port, ref int status)
        {
            status = 0;
            SPIAccumulator[port].Initialized = false;
        }

        public static void spiResetAccumulator(byte port, ref int status)
        {
            status = 0;
            SPIAccumulator[port].AccumulatorCount = 0;
            SPIAccumulator[port].AccumulatorValue = 0;
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
            return SPIAccumulator[port].AccumulatorValue;
        }

        public static uint spiGetAccumulatorCount(byte port, ref int status)
        {
            status = 0;
            return SPIAccumulator[port].AccumulatorCount;
        }

        public static double spiGetAccumulatorAverage(byte port, ref int status)
        {
            status = 0;
            return 0;
        }

        public static void spiGetAccumulatorOutput(byte port, ref long value, ref uint count, ref int status)
        {
            status = 0;
            value = SPIAccumulator[port].AccumulatorValue;
            count = SPIAccumulator[port].AccumulatorCount;
        }
    }
}
