using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalAnalogTrigger
{
    public static void CleanAnalogTrigger(HalAnalogTriggerHandle analogTriggerHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        CleanAnalogTriggerRefShim(analogTriggerHandle, ref status);
    }
    public static int GetAnalogTriggerFPGAIndex(HalAnalogTriggerHandle analogTriggerHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogTriggerFPGAIndexRefShim(analogTriggerHandle, ref status);
    }
    public static int GetAnalogTriggerInWindow(HalAnalogTriggerHandle analogTriggerHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogTriggerInWindowRefShim(analogTriggerHandle, ref status);
    }
    public static int GetAnalogTriggerOutput(HalAnalogTriggerHandle analogTriggerHandle, AnalogTriggerType type, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogTriggerOutputRefShim(analogTriggerHandle, type, ref status);
    }
    public static int GetAnalogTriggerTriggerState(HalAnalogTriggerHandle analogTriggerHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogTriggerTriggerStateRefShim(analogTriggerHandle, ref status);
    }
    public static HalAnalogTriggerHandle InitializeAnalogTrigger(HalAnalogInputHandle portHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeAnalogTriggerRefShim(portHandle, ref status);
    }
    public static HalAnalogTriggerHandle InitializeAnalogTriggerDutyCycle(HalDutyCycleHandle dutyCycleHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeAnalogTriggerDutyCycleRefShim(dutyCycleHandle, ref status);
    }
    public static void SetAnalogTriggerAveraged(HalAnalogTriggerHandle analogTriggerHandle, int useAveragedValue, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAnalogTriggerAveragedRefShim(analogTriggerHandle, useAveragedValue, ref status);
    }
    public static void SetAnalogTriggerFiltered(HalAnalogTriggerHandle analogTriggerHandle, int useFilteredValue, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAnalogTriggerFilteredRefShim(analogTriggerHandle, useFilteredValue, ref status);
    }
    public static void SetAnalogTriggerLimitsVoltage(HalAnalogTriggerHandle analogTriggerHandle, double lower, double upper, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAnalogTriggerLimitsVoltageRefShim(analogTriggerHandle, lower, upper, ref status);
    }
    public static void SetAnalogTriggerLimitsRaw(HalAnalogTriggerHandle analogTriggerHandle, int lower, int upper, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAnalogTriggerLimitsRawRefShim(analogTriggerHandle, lower, upper, ref status);
    }
    public static void SetAnalogTriggerLimitsDutyCycle(HalAnalogTriggerHandle analogTriggerHandle, double lower, double upper, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAnalogTriggerLimitsDutyCycleRefShim(analogTriggerHandle, lower, upper, ref status);
    }
}
