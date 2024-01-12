using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalDIO
{
    public static HalDigitalPWMHandle AllocateDigitalPWM(out HalStatus status)
    {
        status = HalStatus.Ok;
        return AllocateDigitalPWMRefShim(ref status);
    }
    public static void FreeDigitalPWM(HalDigitalPWMHandle pwmGenerator, out HalStatus status)
    {
        status = HalStatus.Ok;
        FreeDigitalPWMRefShim(pwmGenerator, ref status);
    }
    public static int GetDIO(HalDigitalHandle dioPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetDIORefShim(dioPortHandle, ref status);
    }
    public static int GetDIODirection(HalDigitalHandle dioPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetDIODirectionRefShim(dioPortHandle, ref status);
    }
    public static long GetFilterPeriod(int filterIndex, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetFilterPeriodRefShim(filterIndex, ref status);
    }
    public static int GetFilterSelect(HalDigitalHandle dioPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetFilterSelectRefShim(dioPortHandle, ref status);
    }
    public static HalDigitalHandle InitializeDIOPort(HalPortHandle portHandle, int input, string allocationLocation, out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeDIOPortRefShim(portHandle, input, allocationLocation, ref status);
    }
    public static int IsAnyPulsing(out HalStatus status)
    {
        status = HalStatus.Ok;
        return IsAnyPulsingRefShim(ref status);
    }
    public static int IsPulsing(HalDigitalHandle dioPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return IsPulsingRefShim(dioPortHandle, ref status);
    }
    public static void Pulse(HalDigitalHandle dioPortHandle, double pulseLengthSeconds, out HalStatus status)
    {
        status = HalStatus.Ok;
        PulseRefShim(dioPortHandle, pulseLengthSeconds, ref status);
    }
    public static void SetDIODirection(HalDigitalHandle dioPortHandle, int input, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetDIODirectionRefShim(dioPortHandle, input, ref status);
    }
    public static void SetDigitalPWMDutyCycle(HalDigitalPWMHandle pwmGenerator, double dutyCycle, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetDigitalPWMDutyCycleRefShim(pwmGenerator, dutyCycle, ref status);
    }
    public static void SetDigitalPWMOutputChannel(HalDigitalPWMHandle pwmGenerator, int channel, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetDigitalPWMOutputChannelRefShim(pwmGenerator, channel, ref status);
    }
    public static void SetDigitalPWMRate(double rate, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetDigitalPWMRateRefShim(rate, ref status);
    }
    public static void SetFilterPeriod(int filterIndex, long value, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetFilterPeriodRefShim(filterIndex, value, ref status);
    }
    public static void SetFilterSelect(HalDigitalHandle dioPortHandle, int filterIndex, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetFilterSelectRefShim(dioPortHandle, filterIndex, ref status);
    }
}
