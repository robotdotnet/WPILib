using System;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;
using HAL.Simulator;
using HAL.Simulator.Data;
using HAL.SimulatorHAL.Handles;
using static HAL.Base.HALErrors;
using static HAL.Base.HAL;
using static HAL.SimulatorHAL.HALPorts;
using static HAL.SimulatorHAL.Handles.Handle;
using static HAL.Simulator.SimData;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALAnalogTrigger
    {
        class AnalogTrigger
        {
            public byte AnalogPin { get; set; }
            public byte Index { get; set; }
        }

        private static LimitedHandleResource<AnalogTrigger> analogTriggerHandles = new LimitedHandleResource<AnalogTrigger>(kNumAnalogTriggers, HALHandleEnum.AnalogTrigger);


        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALAnalogTrigger.HAL_InitializeAnalogTrigger = HAL_InitializeAnalogTrigger;
            Base.HALAnalogTrigger.HAL_CleanAnalogTrigger = HAL_CleanAnalogTrigger;
            Base.HALAnalogTrigger.HAL_SetAnalogTriggerLimitsRaw = HAL_SetAnalogTriggerLimitsRaw;
            Base.HALAnalogTrigger.HAL_SetAnalogTriggerLimitsVoltage = HAL_SetAnalogTriggerLimitsVoltage;
            Base.HALAnalogTrigger.HAL_SetAnalogTriggerAveraged = HAL_SetAnalogTriggerAveraged;
            Base.HALAnalogTrigger.HAL_SetAnalogTriggerFiltered = HAL_SetAnalogTriggerFiltered;
            Base.HALAnalogTrigger.HAL_GetAnalogTriggerInWindow = HAL_GetAnalogTriggerInWindow;
            Base.HALAnalogTrigger.HAL_GetAnalogTriggerTriggerState = HAL_GetAnalogTriggerTriggerState;
            Base.HALAnalogTrigger.HAL_GetAnalogTriggerOutput = HAL_GetAnalogTriggerOutput;
        }

        public static int HAL_InitializeAnalogTrigger(int port_handle, ref int index, ref int status)
        {
            if (GetHandleType(port_handle) != HALHandleEnum.AnalogInput)
            {
                status = PARAMETER_OUT_OF_RANGE;
                return HALInvalidHandle;
            }

            var handle = analogTriggerHandles.Allocate();
            if (handle == HALInvalidHandle)
            {
                status = NO_AVAILABLE_RESOURCES;
                return HALInvalidHandle;
            }

            var trigger = analogTriggerHandles.Get(handle);
            if (trigger == null)
            {
                status = HAL_HANDLE_ERROR;
                return HALInvalidHandle;
            }
            trigger.AnalogPin = (byte) GetHandleIndex(port_handle);
            trigger.Index = (byte) GetHandleIndex(handle);

            var analogPort = HALAnalogInput.analogInputHandles.Get(port_handle);
            if (analogPort == null)
            {
                status = HAL_HANDLE_ERROR;
                return HALInvalidHandle;
            }

            return handle;
        }

        public static void HAL_CleanAnalogTrigger(int analog_trigger_handle, ref int status)
        {
            analogTriggerHandles.Free(analog_trigger_handle);
        }
        public static void HAL_SetAnalogTriggerLimitsRaw(int analog_trigger_handle, int lower, int upper, ref int status)
        {
            throw new NotImplementedException("Raw values are not supported for analog triggers");
        }

        public static void HAL_SetAnalogTriggerLimitsVoltage(int analog_trigger_handle, double lower, double upper, ref int status)
        {
            status = 0;
            var trigger = analogTriggerHandles.Get(analog_trigger_handle);
            if (trigger == null)
            {
                status = HAL_HANDLE_ERROR;
                return;
            }

            if (lower > upper)
            {
                status = ANALOG_TRIGGER_LIMIT_ORDER_ERROR;
            }

            SimData.AnalogTrigger[trigger.Index].TrigLower = lower;
            SimData.AnalogTrigger[trigger.Index].TrigUpper = upper;
        }

        public static void HAL_SetAnalogTriggerAveraged(int analog_trigger_handle, bool useAveragedValue, ref int status)
        {
            var trigger = analogTriggerHandles.Get(analog_trigger_handle);
            if (trigger == null)
            {
                status = HAL_HANDLE_ERROR;
                return;
            }

            if (SimData.AnalogTrigger[trigger.Index].TrigType == TrigerType.Filtered)
            {
                status = INCOMPATIBLE_STATE;
            }
            else
            {
                TrigerType val = useAveragedValue ? TrigerType.Averaged : TrigerType.Unassigned;
                SimData.AnalogTrigger[trigger.Index].TrigType = val;
            }
        }

        public static void HAL_SetAnalogTriggerFiltered(int analog_trigger_handle, bool useFilteredValue, ref int status)
        {
            var trigger = analogTriggerHandles.Get(analog_trigger_handle);
            if (trigger == null)
            {
                status = HAL_HANDLE_ERROR;
                return;
            }

            if (SimData.AnalogTrigger[trigger.Index].TrigType == TrigerType.Averaged)
            {
                status = INCOMPATIBLE_STATE;
            }
            else
            {
                TrigerType val = useFilteredValue ? TrigerType.Filtered : TrigerType.Unassigned;
                SimData.AnalogTrigger[trigger.Index].TrigType = val;
            }
        }

        private static double GetTriggerValue(AnalogTrigger trigger)
        {
            var ain = AnalogIn[trigger.AnalogPin];
            var atr = SimData.AnalogTrigger[trigger.Index];
            var trigType = atr.TrigType;
            switch (trigType)
            {
                case TrigerType.Filtered:
                case TrigerType.Averaged:
                case TrigerType.Unassigned:
                    return ain.Voltage;
                default:
                    throw new ArgumentOutOfRangeException(nameof(trigger), "Analog Trigger must be either filtered, averaged or null.");
            }
        }

        public static bool HAL_GetAnalogTriggerInWindow(int analog_trigger_handle, ref int status)
        {
            var trigger = analogTriggerHandles.Get(analog_trigger_handle);
            if (trigger == null)
            {
                status = HAL_HANDLE_ERROR;
                return false;
            }

            var val = GetTriggerValue(trigger);
            var atr = SimData.AnalogTrigger[trigger.Index];

            return val >= atr.TrigLower && val <= atr.TrigUpper;
        }

        public static bool HAL_GetAnalogTriggerTriggerState(int analog_trigger_handle, ref int status)
        {
            var trigger = analogTriggerHandles.Get(analog_trigger_handle);
            if (trigger == null)
            {
                status = HAL_HANDLE_ERROR;
                return false;
            }

            var val = GetTriggerValue(trigger);
            var atr = SimData.AnalogTrigger[trigger.Index];

            if (val < atr.TrigLower)
            {
                atr.TrigState = false;
                return false;
            }
            if (val > atr.TrigUpper)
            {
                atr.TrigState = true;
                return true;
            }
            return atr.TrigState;
        }

        public static bool HAL_GetAnalogTriggerOutput(int analog_trigger_handle, HALAnalogTriggerType type, ref int status)
        {
            var trigger = analogTriggerHandles.Get(analog_trigger_handle);
            if (trigger == null)
            {
                status = HAL_HANDLE_ERROR;
                return false;
            }

            switch (type)
            {
                case HALAnalogTriggerType.InWindow:
                    return HAL_GetAnalogTriggerInWindow(analog_trigger_handle, ref status);
                case HALAnalogTriggerType.State:
                    return HAL_GetAnalogTriggerTriggerState(analog_trigger_handle, ref status);
                default:
                    status = ANALOG_TRIGGER_PULSE_OUTPUT_ERROR;
                    return false;
            }
        }
    }
}

