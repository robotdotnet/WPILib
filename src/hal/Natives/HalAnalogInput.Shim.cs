using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalAnalogInput
{
    public static int GetAnalogAverageBits(HalAnalogInputHandle analogPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogAverageBitsRefShim(analogPortHandle, ref status);
    }
    public static int GetAnalogAverageValue(HalAnalogInputHandle analogPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogAverageValueRefShim(analogPortHandle, ref status);
    }
    public static double GetAnalogAverageVoltage(HalAnalogInputHandle analogPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogAverageVoltageRefShim(analogPortHandle, ref status);
    }
    public static int GetAnalogLSBWeight(HalAnalogInputHandle analogPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogLSBWeightRefShim(analogPortHandle, ref status);
    }
    public static int GetAnalogOffset(HalAnalogInputHandle analogPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogOffsetRefShim(analogPortHandle, ref status);
    }
    public static int GetAnalogOversampleBits(HalAnalogInputHandle analogPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogOversampleBitsRefShim(analogPortHandle, ref status);
    }
    public static double GetAnalogSampleRate(out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogSampleRateRefShim(ref status);
    }
    public static int GetAnalogValue(HalAnalogInputHandle analogPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogValueRefShim(analogPortHandle, ref status);
    }
    public static double GetAnalogValueToVolts(HalAnalogInputHandle analogPortHandle, int rawValue, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogValueToVoltsRefShim(analogPortHandle, rawValue, ref status);
    }
    public static double GetAnalogVoltage(HalAnalogInputHandle analogPortHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogVoltageRefShim(analogPortHandle, ref status);
    }
    public static int GetAnalogVoltsToValue(HalAnalogInputHandle analogPortHandle, double voltage, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetAnalogVoltsToValueRefShim(analogPortHandle, voltage, ref status);
    }
    public static HalAnalogInputHandle InitializeAnalogInputPort(HalPortHandle portHandle, string allocationLocation, out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeAnalogInputPortRefShim(portHandle, allocationLocation, ref status);
    }
    public static void SetAnalogAverageBits(HalAnalogInputHandle analogPortHandle, int bits, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAnalogAverageBitsRefShim(analogPortHandle, bits, ref status);
    }
    public static void SetAnalogOversampleBits(HalAnalogInputHandle analogPortHandle, int bits, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAnalogOversampleBitsRefShim(analogPortHandle, bits, ref status);
    }
    public static void SetAnalogSampleRate(double samplesPerSecond, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetAnalogSampleRateRefShim(samplesPerSecond, ref status);
    }
}
