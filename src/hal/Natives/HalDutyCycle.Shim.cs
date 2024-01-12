using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalDutyCycle
{
    public static int GetDutyCycleFPGAIndex(HalDutyCycleHandle dutyCycleHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetDutyCycleFPGAIndexRefShim(dutyCycleHandle, ref status);
    }
    public static int GetDutyCycleFrequency(HalDutyCycleHandle dutyCycleHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetDutyCycleFrequencyRefShim(dutyCycleHandle, ref status);
    }
    internal static HalDutyCycleHandle InitializeDutyCycle(HalDigitalHandle digitalSourceHandle, AnalogTriggerType triggerType, out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeDutyCycleRefShim(digitalSourceHandle, triggerType, ref status);
    }
    internal static HalDutyCycleHandle InitializeDutyCycle(HalAnalogTriggerHandle analogTriggerHandle, AnalogTriggerType triggerType, out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeDutyCycleRefShim(analogTriggerHandle, triggerType, ref status);
    }
}
