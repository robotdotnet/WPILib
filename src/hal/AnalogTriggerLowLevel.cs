
using Hal.Natives;
using WPIUtil.ILGeneration;

namespace Hal
{

    public static unsafe class AnalogTriggerLowLevel
    {
        internal static AnalogTriggerLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

        public static void Clean(int analogTriggerHandle)
        {
            lowLevel.HAL_CleanAnalogTrigger(analogTriggerHandle);
        }

        public static int GetFPGAIndex(int analogTriggerHandle)
        {
            return lowLevel.HAL_GetAnalogTriggerFPGAIndex(analogTriggerHandle);
        }

        public static bool GetInWindow(int analogTriggerHandle)
        {
            return lowLevel.HAL_GetAnalogTriggerInWindow(analogTriggerHandle) != 0;
        }

        public static bool GetOutput(int analogTriggerHandle, AnalogTriggerType type)
        {
            return lowLevel.HAL_GetAnalogTriggerOutput(analogTriggerHandle, type) != 0;
        }

        public static bool GetTriggerState(int analogTriggerHandle)
        {
            return lowLevel.HAL_GetAnalogTriggerTriggerState(analogTriggerHandle) != 0;
        }

        public static int Initialize(int portHandle)
        {
            return lowLevel.HAL_InitializeAnalogTrigger(portHandle);
        }

        public static int InitializeDutyCycle(int dutyCycleHandle)
        {
            return lowLevel.HAL_InitializeAnalogTriggerDutyCycle(dutyCycleHandle);
        }

        public static void SetAveraged(int analogTriggerHandle, bool useAveragedValue)
        {
            lowLevel.HAL_SetAnalogTriggerAveraged(analogTriggerHandle, useAveragedValue ? 1 : 0);
        }

        public static void SetFiltered(int analogTriggerHandle, bool useFilteredValue)
        {
            lowLevel.HAL_SetAnalogTriggerFiltered(analogTriggerHandle, useFilteredValue ? 1 : 0);
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
