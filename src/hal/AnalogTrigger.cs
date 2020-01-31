
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IAnalogTrigger))]
    public unsafe static class AnalogTrigger
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IAnalogTrigger lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public static void Clean(int analogTriggerHandle)
        {
            lowLevel.HAL_CleanAnalogTrigger(analogTriggerHandle);
        }

        public static int GetFPGAIndex(int analogTriggerHandle)
        {
            return lowLevel.HAL_GetAnalogTriggerFPGAIndex(analogTriggerHandle);
        }

        public static int GetInWindow(int analogTriggerHandle)
        {
            return lowLevel.HAL_GetAnalogTriggerInWindow(analogTriggerHandle);
        }

        public static int GetOutput(int analogTriggerHandle, AnalogTriggerType type)
        {
            return lowLevel.HAL_GetAnalogTriggerOutput(analogTriggerHandle, type);
        }

        public static int GetTriggerState(int analogTriggerHandle)
        {
            return lowLevel.HAL_GetAnalogTriggerTriggerState(analogTriggerHandle);
        }

        public static int Initialize(int portHandle)
        {
            return lowLevel.HAL_InitializeAnalogTrigger(portHandle);
        }

        public static int InitializeDutyCycle(int dutyCycleHandle)
        {
            return lowLevel.HAL_InitializeAnalogTriggerDutyCycle(dutyCycleHandle);
        }

        public static void SetAveraged(int analogTriggerHandle, int useAveragedValue)
        {
            lowLevel.HAL_SetAnalogTriggerAveraged(analogTriggerHandle, useAveragedValue);
        }

        public static void SetFiltered(int analogTriggerHandle, int useFilteredValue)
        {
            lowLevel.HAL_SetAnalogTriggerFiltered(analogTriggerHandle, useFilteredValue);
        }

        public static void SetLimitsRaw(int analogTriggerHandle, int lower, int upper)
        {
            lowLevel.HAL_SetAnalogTriggerLimitsRaw(analogTriggerHandle, lower, upper);
        }

        public static void SetLimitsVoltage(int analogTriggerHandle, double lower, double upper)
        {
            lowLevel.HAL_SetAnalogTriggerLimitsVoltage(analogTriggerHandle, lower, upper);
        }

        public static void SetLimitsDutyCycle(int analogTriggerHandle, double lower, double upper)
        {
            lowLevel.HAL_SetAnalogTriggerLimitsDutyCycle(analogTriggerHandle, lower, upper);
        }

    }
}
