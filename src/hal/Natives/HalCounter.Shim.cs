using WPIHal.Handles;

namespace WPIHal.Natives;

public static unsafe partial class HalCounter
{
    public static void ClearCounterDownSource(HalCounterHandle counterHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        ClearCounterDownSourceRefShim(counterHandle, ref status);
    }
    public static void ClearCounterUpSource(HalCounterHandle counterHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        ClearCounterUpSourceRefShim(counterHandle, ref status);
    }
    public static void FreeCounter(HalCounterHandle counterHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        FreeCounterRefShim(counterHandle, ref status);
    }
    public static int GetCounterSamplesToAverage(HalCounterHandle counterHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetCounterSamplesToAverageRefShim(counterHandle, ref status);
    }
    public static int GetCounterDirection(HalCounterHandle counterHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetCounterDirectionRefShim(counterHandle, ref status);
    }
    public static double GetCounterPeriod(HalCounterHandle counterHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetCounterPeriodRefShim(counterHandle, ref status);
    }
    public static int GetCounterStopped(HalCounterHandle counterHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        return GetCounterStoppedRefShim(counterHandle, ref status);
    }
    public static HalCounterHandle InitializeCounter(CounterMode mode, out int index, out HalStatus status)
    {
        status = HalStatus.Ok;
        return InitializeCounterRefShim(mode, out index, ref status);
    }
    public static void ResetCounter(HalCounterHandle counterHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        ResetCounterRefShim(counterHandle, ref status);
    }
    public static void SetCounterAverageSize(HalCounterHandle counterHandle, int size, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetCounterAverageSizeRefShim(counterHandle, size, ref status);
    }
    internal static void SetCounterDownSource(HalCounterHandle counterHandle, HalDigitalHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetCounterDownSourceRefShim(counterHandle, digitalSourceHandle, analogTriggerType, ref status);
    }
    public static void SetCounterDownSource(HalCounterHandle counterHandle, HalAnalogTriggerHandle analogTriggerSource, AnalogTriggerType analogTriggerType, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetCounterDownSourceRefShim(counterHandle, analogTriggerSource, analogTriggerType, ref status);
    }
    public static void SetCounterDownSourceEdge(HalCounterHandle counterHandle, int risingEdge, int fallingEdge, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetCounterDownSourceEdgeRefShim(counterHandle, risingEdge, fallingEdge, ref status);
    }
    public static void SetCounterExternalDirectionMode(HalCounterHandle counterHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetCounterExternalDirectionModeRefShim(counterHandle, ref status);
    }
    public static void SetCounterMaxPeriod(HalCounterHandle counterHandle, double maxPeriod, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetCounterMaxPeriodRefShim(counterHandle, maxPeriod, ref status);
    }
    public static void SetCounterPulseLengthMode(HalCounterHandle counterHandle, double threshold, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetCounterPulseLengthModeRefShim(counterHandle, threshold, ref status);
    }
    public static void SetCounterReverseDirection(HalCounterHandle counterHandle, int reverseDirection, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetCounterReverseDirectionRefShim(counterHandle, reverseDirection, ref status);
    }
    public static void SetCounterSamplesToAverage(HalCounterHandle counterHandle, int samplesToAverage, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetCounterSamplesToAverageRefShim(counterHandle, samplesToAverage, ref status);
    }
    public static void SetCounterSemiPeriodMode(HalCounterHandle counterHandle, int highSemiPeriod, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetCounterSemiPeriodModeRefShim(counterHandle, highSemiPeriod, ref status);
    }
    public static void SetCounterUpDownMode(HalCounterHandle counterHandle, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetCounterUpDownModeRefShim(counterHandle, ref status);
    }
    internal static void SetCounterUpSource(HalCounterHandle counterHandle, HalDigitalHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetCounterUpSourceRefShim(counterHandle, digitalSourceHandle, analogTriggerType, ref status);
    }
    public static void SetCounterUpSource(HalCounterHandle counterHandle, HalAnalogTriggerHandle analogTriggerSource, AnalogTriggerType analogTriggerType, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetCounterUpSourceRefShim(counterHandle, analogTriggerSource, analogTriggerType, ref status);
    }
    public static void SetCounterUpSourceEdge(HalCounterHandle counterHandle, int risingEdge, int fallingEdge, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetCounterUpSourceEdgeRefShim(counterHandle, risingEdge, fallingEdge, ref status);
    }
    public static void SetCounterUpdateWhenEmpty(HalCounterHandle counterHandle, int enabled, out HalStatus status)
    {
        status = HalStatus.Ok;
        SetCounterUpdateWhenEmptyRefShim(counterHandle, enabled, ref status);
    }
}
